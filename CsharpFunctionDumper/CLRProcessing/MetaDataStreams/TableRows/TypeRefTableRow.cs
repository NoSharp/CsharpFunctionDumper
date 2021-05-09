using CsharpFunctionDumper.AssemblyProcessing;

namespace CsharpFunctionDumper.CLRProcessing.MetaDataStreams.TableRows
{
    public class TypeRefTableRow: TableRow
    {
        public ushort ResolutionScope { get; private set; }
        public ushort NameAddress { get; private set; }
        public ushort NamespaceAddress { get; private set; }

        public string Namespace { get; private set; }
        public string Name { get; private set; }

        public static MetaDataTableType OwnerTable = MetaDataTableType.TypeRef;
        
        public TypeRefTableRow(AssemblyBuffer buffer) : base(buffer)
        {
        }

        public override void Read(AssemblyBuffer buffer)
        {
            this.ResolutionScope = buffer.ReadWord();
            this.NameAddress = buffer.ReadWord();
            this.NamespaceAddress = buffer.ReadWord();
            this.Name = this.ReadStringAtOffset(this.NameAddress);
            this.Namespace = this.ReadStringAtOffset(this.NamespaceAddress);

        }

        public override string Display()
        {
            return $"Resolution Scope: {this.ResolutionScope} Type Name: {this.Name} Namespace: {this.Namespace}";
        }
    }
}