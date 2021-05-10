using CsharpFunctionDumper.AssemblyProcessing;

namespace CsharpFunctionDumper.CLRProcessing.MetaDataStreams.Signatures
{
    public interface IAssemblyReader
    {
        public void PopulateFields(AssemblyBuffer buffer);

    }
}