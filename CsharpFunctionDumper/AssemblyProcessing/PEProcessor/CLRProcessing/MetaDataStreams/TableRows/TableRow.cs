namespace CsharpFunctionDumper.AssemblyProcessing.PEProcessor.CLRProcessing.MetaDataStreams.TableRows
{
    public abstract class TableRow
    {
        public MetaDataTableType OwnerTable { get; private set; }
        public TableRow(MetaDataTableType table,AssemblyBuffer buffer)
        {
            this.OwnerTable = table;
        }

        public abstract void Read(AssemblyBuffer buffer);

        public abstract string Display();

    }
}