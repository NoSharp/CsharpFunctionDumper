using CsharpFunctionDumper.AssemblyProcessing;

namespace CsharpFunctionDumper.CLRProcessing.MetaDataStreams.Signatures
{
    public class ParameterType : IAssemblyReader
    {
        public TypeConstant Type { get; private set; }

        public void PopulateFields(AssemblyBuffer buffer)
        {
            this.Type = (TypeConstant) buffer.ReadByte();
        }
        
    }
}