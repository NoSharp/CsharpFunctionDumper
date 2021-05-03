using System;

namespace CsharpFunctionDumper.AssemblyProcessing.PEProcessor.CLRProcessing.MetaDataStreams
{
    public class StringStream : StreamHeader
    {
        public static StringStream INSTANCE;
        public StringStream(AssemblyBuffer buffer, CLRHeader clrHeader) : base(buffer,clrHeader)
        {
            INSTANCE = this;
            
        }

        public string ReadUntilNull(uint startOffset)
        {
            
            string converted = "";
            byte val;
            while(true)
            {
                val = this._cachedBuffer[startOffset];
                
                if (val == 0x0)
                {
                    // We're reading a Null terminated string so break out of the loop.
                    break;
                }
                
                converted += (char) val;
                startOffset++;
            }

            return converted;
            
        }

        public string GetStringAtOffset(uint offset)
        {
            return this.ReadUntilNull(offset);
        }

        public override void ProcessTables(AssemblyBuffer buffer)
        {
        }
    }
}