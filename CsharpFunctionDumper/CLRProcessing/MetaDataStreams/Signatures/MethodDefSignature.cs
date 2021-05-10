using System;
using System.Collections.Generic;
using System.Text;
using CsharpFunctionDumper.AssemblyProcessing;

namespace CsharpFunctionDumper.CLRProcessing.MetaDataStreams.Signatures
{
    public class MethodDefSignature : IAssemblyReader, ICLRSignature
    {
        public CallingConvention CallingType {get; private set; }
        
        public byte SignatureLength { get; private set; }
        
        public ReturnType ReturnType { get; private set; }
        
        public List<ParameterType> ParameterTypes { get; private set; }

        public MethodDefSignature()
        {
            ReturnType = new ReturnType();
        }

        private void PopulateParameters(AssemblyBuffer buffer, byte paramCount)
        {
            ParameterTypes = new List<ParameterType>();
            
            for (int i = 0; i < paramCount; i++)
            {
                ParameterType parameterType = new ParameterType();
                parameterType.PopulateFields(buffer);
                ParameterTypes.Add(parameterType);
                
            }
        }

        public void PopulateFields(AssemblyBuffer buffer)
        {
            SignatureLength = buffer.ReadByte();
            CallingType = (CallingConvention) buffer.ReadByte();
            if (CallingType.IsGeneric())
            {
                
                //TODO: Handle generic values.
                buffer.ReadByte();
            }

            byte paramCount = buffer.ReadByte();
            
            ReturnType.PopulateFields(buffer);
            PopulateParameters(buffer,paramCount);
        }

        public string DisplaySignature()
        {
            StringBuilder builder = new StringBuilder();
            
                
            return builder.ToString();
        }
    }
}