namespace CsharpFunctionDumper.AssemblyProcessing.PEProcessor.CLRProcessing.MetaDataStreams.TableRows
{
    public class MethodTableRow : TableRow
    {
        public static MetaDataTableType OwnerTable = MetaDataTableType.MethodDef;

        public uint RVA { get; private set; }

        public ushort ImplementationFlags { get; private set; }
        
        public ushort DefinitionFlags { get; private set; }

        public ushort NameAddress { get; private set; }
        public string Name { get; private set; }

        public ushort Signature { get; private set; }

        public ushort ParamsListIndex { get; private set; }


        public MethodTableRow(AssemblyBuffer buffer) : base(buffer)
        {
        }

        public override void Read(AssemblyBuffer buffer)
        {
            this.RVA = buffer.ReadDWord();
            this.ImplementationFlags = buffer.ReadWord();
            this.DefinitionFlags = buffer.ReadWord();
            this.NameAddress = buffer.ReadWord();
            this.Signature = buffer.ReadWord();
            this.ParamsListIndex = buffer.ReadWord();

            this.Name = this.ReadStringAtOffset(this.NameAddress);
        }

        public override string Display()
        {
            return $"Name: {this.Name} Implementation Flags: {this.ImplementationFlags:x8} Definition Flags: {this.DefinitionFlags:x8} Parameters List Idx: {this.ParamsListIndex} ";
        }
        
    }
}