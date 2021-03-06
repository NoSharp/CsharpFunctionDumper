using CsharpFunctionDumper.AssemblyProcessing;

namespace CsharpFunctionDumper.CLRProcessing.MetaDataStreams.TableRows
{
    public class FieldTableRow : TableRow
    {
        public static MetaDataTableType OwnerTable = MetaDataTableType.Field;

        public uint NameAddress { get; private set; }
        
        public string Name { get; private set; }

        public ushort Flags { get; private set; }

        public uint Signature { get; private set; }
        

        public FieldTableRow(AssemblyBuffer buffer) : base(buffer)
        {
        }

        public override void Read(AssemblyBuffer buffer)
        {
            this.Flags = buffer.ReadWord();
            this.NameAddress = this.ReadStringTableOffset(buffer);
            this.Signature = this.ReadBlobTableOffset(buffer);

            this.Name = this.ReadStringAtOffset(this.NameAddress);
        }

        public override string Display()
        {
            return $"Name: {this.Name} Flags: {this.Flags:x8} Signature: {this.Signature}";
        }
    }
}