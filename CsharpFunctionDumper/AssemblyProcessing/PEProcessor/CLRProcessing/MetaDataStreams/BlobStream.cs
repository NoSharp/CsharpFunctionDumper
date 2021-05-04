namespace CsharpFunctionDumper.AssemblyProcessing.PEProcessor.CLRProcessing.MetaDataStreams
{
    public class BlobStream : StreamHeader
    {
        public BlobStream(AssemblyBuffer buffer, CLRHeader clrHeader) : base(buffer, clrHeader)
        {
        }

        public override void ProcessTables(AssemblyBuffer buffer)
        {
            
        }
    }
}