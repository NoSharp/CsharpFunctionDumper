﻿namespace CsharpFunctionDumper.AssemblyProcessing.PEProcessor.OptionalHeader
{
    public class DataDirectory
    {

        public uint RVA { get; private set; }

        public uint Size { get; private set; }

        public DataDirectory(AssemblyBuffer buffer)
        {
            this.RVA = buffer.ReadDWord();
            this.Size = buffer.ReadDWord();
        }

    }
}