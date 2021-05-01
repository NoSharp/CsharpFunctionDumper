using System;

namespace CsharpFunctionDumper.AssemblyProcessing
{
    public class Section
    {

        public string Name { get; private set; }

        public uint VirtualSize { get; private set; }

        public uint VirtualAddress { get; private set; }

        public uint SizeOfRawData { get; private set; }
        
        public uint PointerToRawData { get; private set; }

        public uint RelocationAddress { get; private set; }

        public uint LineNumbersAddress { get; private set; }

        public ushort NumberOfRelocations { get; private set; }

        public ushort NumberOfLineNumbers { get; private set; }

        public uint Characteristics { get; private set; }


        public Section(AssemblyBuffer buffer)
        {
            this.Name = buffer.ReadStringOfLength(8);
            this.VirtualSize = buffer.ReadDWord();
            this.VirtualAddress = buffer.ReadDWord();
            this.SizeOfRawData = buffer.ReadDWord();
            this.PointerToRawData = buffer.ReadDWord();
            this.RelocationAddress = buffer.ReadDWord();
            this.LineNumbersAddress = buffer.ReadDWord();
            this.NumberOfRelocations = buffer.ReadWord();
            this.NumberOfLineNumbers = buffer.ReadWord();
            this.Characteristics = buffer.ReadDWord();
        }

    }
}