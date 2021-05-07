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

        protected AssemblyBuffer _cachedAssemblyBuffer { get; private set; }


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
            this._cachedBuffer = new byte[this.Size];
            this._cachedBuffer = buffer.ReadBytes(this.Size);
            this._cachedAssemblyBuffer = new AssemblyBuffer("", this._cachedBuffer);
        }
        
        
    }
}