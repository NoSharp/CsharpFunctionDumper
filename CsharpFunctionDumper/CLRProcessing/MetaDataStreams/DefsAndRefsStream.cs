using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CsharpFunctionDumper.AssemblyProcessing;
using CsharpFunctionDumper.CLRProcessing.MetaDataStreams.TableRows;

namespace CsharpFunctionDumper.CLRProcessing.MetaDataStreams
{
    /// <summary>
    /// Handles the #~ Stream, otherwise known in the application as the DefsAndRefs Stream.
    /// Sources:
    ///     https://www.red-gate.com/simple-talk/blogs/anatomy-of-a-net-assembly-clr-metadata-3/
    ///     https://www.ecma-international.org/wp-content/uploads/ECMA-335_6th_edition_june_2012.pdf
    ///     https://www.codeproject.com/Articles/42649/NET-File-Format-Signatures-Under-the-Hood-Part-1-o#MethodDefSig4.3
    /// </summary>
    public class DefsAndRefsStream : StreamHeader
    {
        private static DefsAndRefsStream Instance;

        private MetaDataHeader _metaDataHeader;

        public byte HeapOffsetSizes { get; private set; }

        public ulong TablesPresent { get; private set; } // AKA "Valid"

        public ulong TablesSorted { get; private set; }

        public uint[] TableLengths { get; private set; }

        public Dictionary<MetaDataTableType, Type> TableRowTypes { get; private set; }

        public Dictionary<MetaDataTableType, List<TableRow>> TableRows { get; private set; }

        public DefsAndRefsStream(AssemblyBuffer buffer, CLRHeader clrHeader, MetaDataHeader metaDataHeader) : base(
            buffer, clrHeader)
        {
            this._metaDataHeader = metaDataHeader;
            this.TableLengths = new uint[64];
            this.TableRows = new Dictionary<MetaDataTableType, List<TableRow>>();

            Instance = this;
        }

        public static DefsAndRefsStream GetInstance()
        {
            return Instance;
        }

        public override void ProcessTables(AssemblyBuffer buffer)
        {
            buffer.SetIndexPointer(this.AbsoluteAddress);
            uint val = buffer.ReadDWord(); // Reserved.
            byte major = buffer.ReadByte(); // Major and minor version
            byte minor = buffer.ReadByte(); // Major and minor version
            this.HeapOffsetSizes =
                buffer.ReadByte(); // Bit flags for the heap index width. Ref: https://www.codeproject.com/Articles/12585/The-NET-File-Format

            buffer.ReadByte(); // Padding byte.

            this.TablesPresent = buffer.ReadQWord(); // We need to read all of the tables present in the assembly
            this.TablesSorted = buffer.ReadQWord(); // What tables are sorted

            this.TableRowTypes = this.GetPopulatedTableRowTypes();

            // Check which types apply to the current stream.
            for (int i = 0; i < 64; i++)
            {
                if (!Enum.IsDefined(typeof(MetaDataTableType), i))
                    continue;


                var tableType = (MetaDataTableType) i;
                var bitmask = (ulong) 1 << (int) tableType;

                if ((this.TablesPresent & bitmask) != 0 || bitmask == 0)
                    this.TableLengths[(ulong) tableType] = buffer.ReadDWord();
            }

            this.PopulateTableRows(buffer);
        }

        private MetaDataTableType GetMetaTableTypeFromType(Type type)
        {
            return (MetaDataTableType) type.GetField("OwnerTable", BindingFlags.Static | BindingFlags.Public)
                .GetValue(null);
        }

        private List<Type> GetTableRows()
        {
            return (from t in Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .Where(x => x.Namespace ==
                                "CsharpFunctionDumper.CLRProcessing.MetaDataStreams.TableRows")
                where !t.IsAbstract
                select t).ToList();
        }

        private Dictionary<MetaDataTableType, Type> GetPopulatedTableRowTypes()
        {
            Dictionary<MetaDataTableType, Type> tableTypes = new Dictionary<MetaDataTableType, Type>();
            List<Type> rows = this.GetTableRows();

            foreach (var type in rows)
                tableTypes.Add(this.GetMetaTableTypeFromType(type), type);

            return tableTypes;
        }

        private void PopulateTableRows(AssemblyBuffer buffer)
        {
            for (int idx = 0; idx < this.TableLengths.Length; idx++)
            {
                MetaDataTableType tableType = (MetaDataTableType) idx;
                // These weren't valid in the present DWORD.
                if (this.TableLengths[idx] == 0) continue;
                if (!this.TableRowTypes.ContainsKey(tableType))
                {
                    Console.WriteLine($"WARNING: NO TYPE HANDLER FOR: {tableType.ToString()}");
                    continue;
                }

                Type rowType = this.TableRowTypes[tableType];
                List<TableRow> tableRows = new List<TableRow>();
                for (int tableIdx = 0; tableIdx < this.TableLengths[idx]; tableIdx++)
                {
                    TableRow row = (TableRow) Activator.CreateInstance(rowType, buffer);
                    if (row == null)
                    {
                        Console.WriteLine($"WARNING: No Row type for :{tableType.ToString()}");
                        continue;
                    }

                    row.SetStreamHeaderState(this._metaDataHeader.Streams);
                    row.Read(buffer);

                    tableRows.Add(row);
                }

                this.TableRows.Add(tableType, tableRows);
            }
        }

        public List<MethodTableRow> GetMethodTableRowsFromOffset(int offset)
        {
            List<MethodTableRow> methodTableRows = new List<MethodTableRow>();
            List<TableRow> tableRows = this.TableRows[MetaDataTableType.MethodDef];
            int currentOffset = offset;
            methodTableRows.Add((MethodTableRow) tableRows[currentOffset]);
            currentOffset++;

            while (true)
            {
                if (currentOffset >= tableRows.Count) break;
                MethodTableRow methodTableRow = (MethodTableRow) tableRows[currentOffset];
                if (methodTableRow.Name == ".ctor") break;
                methodTableRows.Add(methodTableRow);
                currentOffset++;
            }

            return methodTableRows;
        }

        public List<ParamTableRow> GetParameterTableRowsFromOffset(int offset)
        {
            List<ParamTableRow> methodTableRows = new List<ParamTableRow>();
            List<TableRow> tableRows = this.TableRows[MetaDataTableType.Param];
            int currentOffset = offset;
            if (currentOffset >= tableRows.Count)
            {
                return methodTableRows;
            }

            methodTableRows.Add((ParamTableRow) tableRows[currentOffset]);
            currentOffset++;

            while (true)
            {
                if (currentOffset >= tableRows.Count) break;
                ParamTableRow methodTableRow = (ParamTableRow) tableRows[currentOffset];
                if (methodTableRow.Sequence == 1) break;
                methodTableRows.Add(methodTableRow);
                currentOffset++;
            }

            return methodTableRows;
        }
    }
}