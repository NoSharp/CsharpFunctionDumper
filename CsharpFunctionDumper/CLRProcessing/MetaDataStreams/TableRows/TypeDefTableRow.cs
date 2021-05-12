using System.Collections.Generic;
using System.Reflection;
using System.Text;
using CsharpFunctionDumper.AssemblyProcessing;

namespace CsharpFunctionDumper.CLRProcessing.MetaDataStreams.TableRows
{
    public class TypeDefTableRow : TableRow
    {
        public static MetaDataTableType OwnerTable = MetaDataTableType.TypeDef;

        public uint Flags { get; private set; }
        
        public uint NameAddress { get; private set; }
        public uint NamespaceAddress { get; private set; }
        public ushort Extends { get; private set; }
        public ushort FieldList { get; private set; }
        public ushort MethodList { get; private set; }

        public string Namespace { get; private set; }

        public string Name { get; private set; }
        
        
        public Dictionary<FieldAttributes, string> FieldPrefixes = new Dictionary<FieldAttributes, string>()
        {
            {FieldAttributes.Private, "private"},
            {FieldAttributes.Public, "public"},
            {FieldAttributes.Assembly, "internal"},
            {FieldAttributes.Family, "protected"},
            {FieldAttributes.FamORAssem, "protected internal"},
            {FieldAttributes.FamANDAssem, "private protected"},
            {FieldAttributes.InitOnly, "readonly"},
            {FieldAttributes.Literal, "const"},
        };
        
        public TypeDefTableRow(AssemblyBuffer buffer) : base(buffer)
        {
        }

        public override void Read(AssemblyBuffer buffer)
        {
            this.Flags = buffer.ReadDWord();
            this.NameAddress = this.ReadStringTableOffset(buffer);
            this.NamespaceAddress = this.ReadStringTableOffset(buffer);
            this.Extends = buffer.ReadWord();
            this.FieldList = buffer.ReadWord();
            this.MethodList = buffer.ReadWord();
            
            this.Name = this.ReadStringAtOffset(this.NameAddress);
            this.Namespace = this.ReadStringAtOffset(this.NamespaceAddress);

        }

        public string FormFieldPrefix()
        {
            StringBuilder prefix = new StringBuilder();

            foreach (var fieldPrefixesKey in this.FieldPrefixes.Keys)
            {
                if(this.DoesFlagContainBitMask(this.Flags, (uint)fieldPrefixesKey))
                    prefix.Append($"{this.FieldPrefixes[fieldPrefixesKey]} ");
            }

            return prefix.ToString();
        }

        public override string Display()
        {
            DefsAndRefsStream defsAndRefsStream = DefsAndRefsStream.GetInstance();
            StringBuilder classFormat = new StringBuilder();
            classFormat.Append($"{this.Namespace}.{this.Name}\n");
            foreach (var methodTableRow in defsAndRefsStream.GetMethodTableRowsFromTypeDef(this))
            {
                classFormat.Append($"\t {methodTableRow.Display()}\n");
            }
            return classFormat.ToString();
        }
    }
}