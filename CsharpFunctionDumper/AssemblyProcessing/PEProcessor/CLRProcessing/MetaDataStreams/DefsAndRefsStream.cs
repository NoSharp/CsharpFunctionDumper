using System;
using System.Collections.Generic;
using CsharpFunctionDumper.AssemblyProcessing.PEProcessor.CLRProcessing.MetaDataStreams.TableRows;
using CsharpFunctionDumper.AssemblyProcessing.PEProcessor.MetaDataStreams;

namespace CsharpFunctionDumper.AssemblyProcessing.PEProcessor.CLRProcessing.MetaDataStreams
{
    /// <summary>
    /// Handles the #~ Stream, otherwise known in the application as the DefsAndRefs Stream.
    /// Sources:
    ///     https://www.red-gate.com/simple-talk/blogs/anatomy-of-a-net-assembly-clr-metadata-3/
    ///     https://www.ecma-international.org/wp-content/uploads/ECMA-335_6th_edition_june_2012.pdf
    /// </summary>
    public class DefsAndRefsStream : StreamHeader
    {

        private MetaDataHeader _metaDataHeader;
        
        public byte HeapOffsetSizes { get; private set; }
        
        public ulong TablesPresent { get; private set; } // AKA "Valid"
        
        public ulong TablesSorted { get; private set; }

        public uint[] TableLengths { get; private set; }

        public Dictionary<MetaDataTableType, List<TableRow>> TableRows { get; private set; }

        public DefsAndRefsStream(AssemblyBuffer buffer, CLRHeader clrHeader, MetaDataHeader metaDataHeader) : base(buffer,clrHeader)
        {
            this._metaDataHeader = metaDataHeader;
            this.TableLengths = new uint[64];
            this.TableRows = new Dictionary<MetaDataTableType, List<TableRow>>();

        }


        public override void ProcessTables(AssemblyBuffer buffer)
        {
            buffer.SetIndexPointer(this.AbsoluteAddress);
            uint val = buffer.ReadDWord(); // Reserved.
            byte major = buffer.ReadByte(); // Major and minor version
            byte minor = buffer.ReadByte(); // Major and minor version
            this.HeapOffsetSizes = buffer.ReadByte(); // Bit flags for the heap index width. Ref: https://www.codeproject.com/Articles/12585/The-NET-File-Format
            
            buffer.ReadByte(); // Padding byte.
            
            this.TablesPresent = buffer.ReadQWord(); // We need to read all of the tables present in the assembly
            this.TablesSorted = buffer.ReadQWord(); // What tables are sorted

            
            for( int i = 0; i < 64; i++)
            {
               
                if (!Enum.IsDefined(typeof(MetaDataTableType), i))
                {
                    continue;
                }

                MetaDataTableType tableType = (MetaDataTableType) i;
                
                ulong bitmask = (ulong)1 << (int)tableType;
                
                if ((this.TablesPresent & bitmask) != 0 || bitmask == 0)
                {
                    this.TableLengths[(ulong) tableType] = buffer.ReadDWord();
                }
            }
            
            this.TableRows[MetaDataTableType.Module] = new List<TableRow>();
            TableRow row = new ModuleTableRow(buffer);
            row.Read(buffer);
            this.TableRows[MetaDataTableType.Module].Add(row);
            /*
            foreach (var tableLength in this.TableLengths)
            {
                //Console.WriteLine();
                MetaDataTableType tableType = (MetaDataTableType) tableLength;
                //uint tableSize = this.TableLengths[tableLength];
                
                Console.WriteLine(row.Display());
                this.TableRows[tableType].Add(row);
            }*/
        }

        public string GetStringFromStringTable(uint idx)
        {
            return "";
        }
    }
}