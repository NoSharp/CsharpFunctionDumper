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
            return StringStream.GetInstance().ReadUntilNull(offset);
        }

        public string ReadParamsUntilOffset(uint offset)
        {
            string parameterList = "";

            return parameterList;
        }

        public bool DoesFlagContainBitMask(uint flag, uint mask)
        {
            return (flag & mask) != 0;
        }

        public abstract void Read(AssemblyBuffer buffer);

        public abstract string Display();

    }
}