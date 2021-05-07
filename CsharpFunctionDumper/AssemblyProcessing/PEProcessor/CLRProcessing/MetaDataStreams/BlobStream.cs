using System;
using System.Collections.Generic;

namespace CsharpFunctionDumper.AssemblyProcessing.PEProcessor.CLRProcessing.MetaDataStreams
{
    public class BlobStream : StreamHeader
    {
        public BlobStream(AssemblyBuffer buffer, CLRHeader clrHeader) : base(buffer, clrHeader)
        {
        }

        public uint GetSignatureAtOffset(uint offset)
        {
            return this._cachedBuffer[offset];
        }

        public byte[] GetValueOfSignature(uint signatureOffset)
        {
            this._cachedAssemblyBuffer.SetIndexPointer(signatureOffset);
            bool hasThis = false;
            bool explicitThis = false;
            
            
            if (this._cachedAssemblyBuffer.ReadByte() == (uint) CallingConvention.HasThis)
            {
                hasThis = true;
                
                if (this._cachedAssemblyBuffer.ReadByte() == (uint) CallingConvention.ExplictThis)
                {
                    explicitThis = true;
                }
            }

            CallingConvention methodType;
            byte rawMethodType = this._cachedAssemblyBuffer.ReadByte();
            uint genericParameters = 0;
            if (rawMethodType == (uint) CallingConvention.Default)
            {
                Console.WriteLine($"{rawMethodType.ToString()}");
                methodType = CallingConvention.Default;
            }else if(rawMethodType == (uint) CallingConvention.VarArg)
            {
                Console.WriteLine($"{rawMethodType.ToString()}");
                methodType = CallingConvention.VarArg;
            }
            else
            {
                Console.WriteLine($"{rawMethodType.ToString()}");
                methodType = CallingConvention.Generic;
                genericParameters = _cachedAssemblyBuffer.ReadDWord();
            }

            uint paramCount = this._cachedAssemblyBuffer.ReadDWord();
            uint returnType = this._cachedAssemblyBuffer.ReadDWord();


            return new byte[2];
        }

        public override void ProcessTables(AssemblyBuffer buffer)
        {
            
        }
    }
}