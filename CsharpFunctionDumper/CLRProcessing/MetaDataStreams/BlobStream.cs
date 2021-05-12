using System;
using System.Text;
using CsharpFunctionDumper.AssemblyProcessing;
using CsharpFunctionDumper.CLRProcessing.MetaDataStreams.Signatures;

namespace CsharpFunctionDumper.CLRProcessing.MetaDataStreams
{
    /// <summary>
    ///     Handles the Blob stream.
    ///     Sources:
    ///     https://www.codeproject.com/Articles/42649/NET-File-Format-Signatures-Under-the-Hood-Part-1-o#MethodDefSig4.3
    ///     https://www.ecma-international.org/wp-content/uploads/ECMA-335_6th_edition_june_2012.pdf
    /// </summary>
    public class BlobStream : StreamHeader
    {
        public BlobStream(AssemblyBuffer buffer, CLRHeader clrHeader) : 
            base(buffer, clrHeader)
        {
        }

        public uint GetSignatureAtOffset(uint offset)
        {
            return CachedBuffer[offset];
        }

        public MethodDefSignature GetMethodDefValue(uint signatureOffset)
        {
            CachedAssemblyBuffer.SetIndexPointer(signatureOffset);
            
            MethodDefSignature signature = new MethodDefSignature();
            signature.PopulateFields(CachedAssemblyBuffer);
            
            return signature;
        }

        public override void ProcessTables(AssemblyBuffer buffer)
        {
        }
    }
}