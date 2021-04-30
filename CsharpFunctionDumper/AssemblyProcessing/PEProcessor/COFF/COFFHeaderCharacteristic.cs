using System;

namespace CsharpFunctionDumper.AssemblyProcessing.PEProcessor
{
    [Flags]
    public enum COFFHeaderCharacteristic
    {
        FILE_RELOCS_STRIPPED = 0x0001,
        FILE_EXECUTABLE_IMAGE = 0x0002,
        FILE_LINE_NUMS_STRIPPED = 0x0004,
        FILE_LOCAL_SYMS_STRIPPED = 0x0008,
        FILE_AGRESSIVE_WS_TRIM = 0x0010,
        FILE_LARGE_ADDRESS_AWARE = 0x0020,
        FILE_BYTES_RESERVED_LO = 0x0080,
        FILE_B32BIT_MACHINE = 0x0100,
        FILE_DEBUG_STRIPPED = 0x0200,
        FILE_REMOVEABLE_RUN_FROM_SWAP = 0x0400,
        FILE_NET_RUN_FROM_SWAP = 0x0800,
        FILE_SYSTEM = 0x1000,
        FILE_DLL = 0x2000,
        FILE_UP_SYSTEM_ONLY = 0x4000,
        FILE_BYTES_RESERVED_HI = 0x8000
        
    }
}