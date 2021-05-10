namespace CsharpFunctionDumper.CLRProcessing.MetaDataStreams
{
    public enum CallingConvention
    {
        HasThis = 0x20,
        Generic = 0x10,
        HasThisAndGeneric = 0x30,
        Default = 0x00,
        Vararg = 0x05,
    }
}