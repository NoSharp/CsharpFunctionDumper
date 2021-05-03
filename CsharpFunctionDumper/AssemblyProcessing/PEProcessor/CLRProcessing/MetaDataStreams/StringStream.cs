using System;

namespace CsharpFunctionDumper.AssemblyProcessing.PEProcessor.CLRProcessing.MetaDataStreams
{
    public class StringStream : StreamHeader
    {
        private static StringStream Instance;
        public StringStream(AssemblyBuffer buffer, CLRHeader clrHeader) : base(buffer,clrHeader)
        {
            Instance = this;
            
        }

        public static StringStream GetInstance()
        {
            return Instance;
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