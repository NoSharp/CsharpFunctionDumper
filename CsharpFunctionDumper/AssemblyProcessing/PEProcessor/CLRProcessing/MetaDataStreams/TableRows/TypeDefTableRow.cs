namespace CsharpFunctionDumper.AssemblyProcessing.PEProcessor.CLRProcessing.MetaDataStreams.TableRows
{
    public class TypeDefTableRow : TableRow
    {
        public static MetaDataTableType OwnerTable = MetaDataTableType.TypeDef;
        
        public uint Flags { get; private set; }
        
        public ushort NameAddress { get; private set; }
        public ushort NamespaceAddress { get; private set; }
        public ushort Extends { get; private set; }
        public ushort FieldList { get; private set; }
        public ushort MethodList { get; private set; }

        public string Namespace { get; private set; }

        public string Name { get; private set; }
        public TypeDefTableRow(AssemblyBuffer buffer) : base(buffer)
        {
        }

        public override void Read(AssemblyBuffer buffer)
        {
            this.Flags = buffer.ReadDWord();
            this.NameAddress = buffer.ReadWord();
            this.NamespaceAddress = buffer.ReadWord();
            this.Extends = buffer.ReadWord();
            this.FieldList = buffer.ReadWord();
            this.MethodList = buffer.ReadWord();
            
            this.Name = this.ReadStringAtOffset(this.NameAddress);
            this.Namespace = this.ReadStringAtOffset(this.NamespaceAddress);

        }

        public override string Display()
        {
            return $"Flags: {this.Flags:x8} Type Name: {this.Name} Namespace: {this.Namespace}";
        }
    }
}