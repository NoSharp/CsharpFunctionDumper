namespace CsharpFunctionDumper.AssemblyProcessing.PEProcessor.CLRProcessing.MetaDataStreams.TableRows
{
    public class ParamTableRow : TableRow
    {
        
        public static MetaDataTableType OwnerTable = MetaDataTableType.Param;
        
        public ushort Flags { get; private set; }
        public ushort Sequence { get; private set; }
        public ushort NameAddresss { get; private set; }
        
        public string Name { get; private set; }

        public ParamTableRow(AssemblyBuffer buffer) : base(buffer)
        {
        }

        public override void Read(AssemblyBuffer buffer)
        {
            this.Flags = buffer.ReadWord();
            this.Sequence = buffer.ReadWord();
            this.NameAddresss = buffer.ReadWord();
            this.Name = this.ReadStringAtOffset(this.NameAddresss);
        }

        public override string Display()
        {
            return $"Flags: {this.Flags:x8} Sequence: {this.Sequence} Name: {this.Name}";
        }
    }
}