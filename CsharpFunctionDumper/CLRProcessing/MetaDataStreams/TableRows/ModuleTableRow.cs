using CsharpFunctionDumper.AssemblyProcessing;

namespace CsharpFunctionDumper.CLRProcessing.MetaDataStreams.TableRows
{
    public class ModuleTableRow : TableRow
    {
        public ushort Generation { get; private set; }
        public uint NameAddress { get; private set; }
        public uint MvId { get; private set; }
        public uint EncId { get; private set; }
        public uint EncBaseId { get; private set; }

        public string Name { get; private set; }

        public static MetaDataTableType OwnerTable = MetaDataTableType.Module;
        
        public ModuleTableRow(AssemblyBuffer buffer) : base(buffer)
        {
        }

        public override void Read(AssemblyBuffer buffer)
        {
            this.Generation = buffer.ReadWord();
            this.NameAddress = this.ReadStringTableOffset(buffer);
            this.MvId = this.ReadGuidOffset(buffer);
            this.EncId = this.ReadGuidOffset(buffer);
            this.EncBaseId = this.ReadGuidOffset(buffer);
            this.Name = this.ReadStringAtOffset(this.NameAddress);
        }

        public override string Display()
        {
            return $"Generation: {this.Generation} Name: {this.Name} MvId: {this.MvId} EncId: {this.EncId}";
        }
    }
}