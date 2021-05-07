using System;

namespace CsharpFunctionDumper.AssemblyProcessing.PEProcessor.CLRProcessing.MetaDataStreams
{
    public class StringStream : StreamHeader
    {
   
        public StringStream(AssemblyBuffer buffer, CLRHeader clrHeader) : base(buffer,clrHeader)
        {
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