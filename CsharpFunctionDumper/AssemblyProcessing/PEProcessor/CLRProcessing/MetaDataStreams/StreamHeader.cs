namespace CsharpFunctionDumper.AssemblyProcessing.PEProcessor.MetaDataStreams
{
    public class StreamHeader
    {
        public uint Offset { get; private set; }

        public uint Size { get; private set; }

        public StreamHeader(AssemblyBuffer buffer)
        {
            this.Offset = buffer.ReadDWord();
            this.Size = buffer.ReadDWord();
        }
    }
}