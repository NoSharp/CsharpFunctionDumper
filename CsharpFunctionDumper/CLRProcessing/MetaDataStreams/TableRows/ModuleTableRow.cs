using CsharpFunctionDumper.AssemblyProcessing;

namespace CsharpFunctionDumper.CLRProcessing.MetaDataStreams.TableRows
{
    public class ModuleTableRow : TableRow
    {
        public ushort Generation { get; private set; }
        public ushort NameAddress { get; private set; }
        public ushort MvId { get; private set; }
        public ushort EncId { get; private set; }
        public ushort EncBaseId { get; private set; }

        public string Name { get; private set; }

        public static MetaDataTableType OwnerTable = MetaDataTableType.Module;
        
        public ModuleTableRow(AssemblyBuffer buffer) : base(buffer)
        {
        }

        public override void Read(AssemblyBuffer buffer)
        {
            this.Generation = buffer.ReadWord();
            this.NameAddress = buffer.ReadWord();
            this.MvId = buffer.ReadWord();
            this.EncId = buffer.ReadWord();
            this.EncBaseId = buffer.ReadWord();
            this.Name = this.ReadStringAtOffset(this.NameAddress);
        }

        public override string Display()
        {
            return $"Generation: {this.Generation} Name: {this.Name} MvId: {this.MvId} EncId: {this.EncId}";
        }
    }
}