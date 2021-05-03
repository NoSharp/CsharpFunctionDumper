using System;

namespace CsharpFunctionDumper.AssemblyProcessing.PEProcessor.CLRProcessing.MetaDataStreams
{
    public abstract class StreamHeader
    {
        public uint Offset { get; private set; }

        public uint Size { get; private set; }

        public string Name { get; private set; }

        public uint AbsoluteAddress { get; private set; }

        protected byte[] _cachedBuffer { get; private set; }

        private CLRHeader header { get; }

        private AssemblyBuffer buffer { get; }


        public StreamHeader(AssemblyBuffer buffer, CLRHeader clrHeader)
        {
            this.Offset = buffer.ReadDWord();
            this.Size = buffer.ReadDWord();
            this.Name = buffer.ReadString();
            buffer.IncrementIndexPointer(1);
            this.AbsoluteAddress = (clrHeader.MetaData.RVA - 0x1E00) + this.Offset;
            this.header = clrHeader;
            this.buffer = buffer;
        }

        public abstract void ProcessTables(AssemblyBuffer buffer);

        public void CacheBuffer(AssemblyBuffer buffer)
        {
           buffer.SetIndexPointer(this.AbsoluteAddress);
            this._cachedBuffer = new byte[this.Size];
            this._cachedBuffer = buffer.ReadBytes(this.Size);
        }

    }
}