using System;

namespace CsharpFunctionDumper.CLRProcessing.MetaDataStreams
{
    [Flags]
    public enum ParamAttribute
    {
        In = 0x0001,
        Out = 0x0002,
        Optional = 0x0010,
        HasDefault = 0x1000,
        HasFieldMarshal = 0x2000,
        Unused = 0xcfe0
    }
}