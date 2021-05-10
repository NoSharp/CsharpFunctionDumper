namespace CsharpFunctionDumper.CLRProcessing.MetaDataStreams.Signatures
{
    public static class CallingConventionHelpers
    {
        public static bool IsGeneric(this CallingConvention callingConvention)
        {
            return callingConvention == CallingConvention.Generic ||
                   callingConvention == CallingConvention.HasThisAndGeneric;
        }
    }
}