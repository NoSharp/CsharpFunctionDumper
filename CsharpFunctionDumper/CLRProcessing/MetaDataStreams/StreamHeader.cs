using CsharpFunctionDumper.AssemblyProcessing;

namespace CsharpFunctionDumper.CLRProcessing.MetaDataStreams
{
    public abstract class StreamHeader
    {
        public uint Offset { get; private set; }

        public uint Size { get; private set; }

        public string Name { get; private set; }

        public uint AbsoluteAddress { get; private set; }

        protected byte[] CachedBuffer { get; private set; }

        protected AssemblyBuffer CachedAssemblyBuffer { get; private set; }
        

        public StreamHeader(AssemblyBuffer buffer, CLRHeader clrHeader)
        {
            this.Offset = buffer.ReadDWord();
            this.Size = buffer.ReadDWord();
            this.Name = buffer.ReadDwordAlignedString();
            this.AbsoluteAddress = (clrHeader.MetaData.RVA - 0x1E00) + this.Offset;
        }

        public abstract void ProcessTables(AssemblyBuffer buffer);

        public void CacheBuffer(AssemblyBuffer buffer)
        {
           buffer.SetIndexPointer(this.AbsoluteAddress);
            this.CachedBuffer = new byte[this.Size];
            this.CachedBuffer = buffer.ReadBytes(this.Size);
            this.CachedAssemblyBuffer = new AssemblyBuffer("", this.CachedBuffer);
        }
        
        
    }
}