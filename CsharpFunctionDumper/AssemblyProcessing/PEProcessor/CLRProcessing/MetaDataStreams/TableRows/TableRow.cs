namespace CsharpFunctionDumper.AssemblyProcessing.PEProcessor.CLRProcessing.MetaDataStreams.TableRows
{
    public abstract class TableRow
    {
        public static MetaDataTableType OwnerTable;
        public TableRow(AssemblyBuffer buffer)
        {
        }

        public string ReadStringAtOffset(uint offset)
        {
            return StringStream.INSTANCE.ReadUntilNull(offset);
        }

        public abstract void Read(AssemblyBuffer buffer);

        public abstract string Display();

    }
}