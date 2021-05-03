namespace CsharpFunctionDumper.AssemblyProcessing.PEProcessor.CLRProcessing.MetaDataStreams.TableRows
{
    public class ModuleTableRow : TableRow
    {
        public ushort Generation { get; private set; }
        public ushort Name { get; private set; }
        public ushort MvId { get; private set; }
        public ushort EncId { get; private set; }
        public ushort EncBaseId { get; private set; }

        public ModuleTableRow(AssemblyBuffer buffer) : base(MetaDataTableType.Assembly, buffer)
        {
        }

        public override void Read(AssemblyBuffer buffer)
        {
            this.Generation = buffer.ReadWord();
            this.Name = buffer.ReadWord();
            this.MvId = buffer.ReadWord();
            this.EncId = buffer.ReadWord();
            this.EncBaseId = buffer.ReadWord();
        }

        public override string Display()
        {
            //@TODO: This is a serious bodge and will be fixed in a following update.
            return $"Generation: {this.Generation} Offset: {this.Name}: {StringStream.INSTANCE.ReadUntilNull(this.Name)} MvId: {this.MvId} EncId: {this.EncId}";
        }
    }
}