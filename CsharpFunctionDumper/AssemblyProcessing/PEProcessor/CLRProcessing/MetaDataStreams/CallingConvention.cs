namespace CsharpFunctionDumper.AssemblyProcessing.PEProcessor.CLRProcessing.MetaDataStreams
{
    public enum CallingConvention
    {
        HasThis = 0x20,
        ExplictThis = 0x40,
        Default = 0x0,
        VarArg = 0x5,
        Generic = 0x10,
        C = 0x1,
        StdCall = 0x2,
        ThisCall = 0x3,
        FastCall = 0x4,
        Sentinel = 0x41
    }
}