using System;
using CsharpFunctionDumper.AssemblyProcessing;

namespace CsharpFunctionDumper.COFF
{
    /// <summary>
    /// Represents the COFF header in the PE file.
    /// Sources:
    ///     https://docs.microsoft.com/en-us/windows/win32/api/winnt/ns-winnt-image_file_header
    /// </summary>
    public class COFFHeader
    {

        /// <summary>
        /// The Signature of the Assembly.
        /// </summary>
        public uint Signature { get; private set; }
        
        /// <summary>
        /// The target machine to run the assembly on.
        /// </summary>
        public ushort Machine { get; private set; }
        
        /// <summary>
        /// The amount of sections in the assembly. 
        /// </summary>
        public ushort SectionCount { get; private set; }
        
        /// <summary>
        /// The time stamp of the assembly being created
        /// </summary>
        public uint TimeDataStamp { get; private set; }

        /// <summary>
        /// A pointer to the Symbol table.
        /// </summary>
        [Obsolete]
        public uint PointerToSymbolTable { get; private set; }

        /// <summary>
        /// The number of symbols in the symbol table
        /// </summary>
        [Obsolete]
        public uint NumberOfSymbols { get; private set; }

        /// <summary>
        /// The Size of the optional header in bytes.
        /// </summary>
        public ushort SizeOfOptionalHeader { get; private set; }
        
        /// <summary>
        /// The characteristics of the Header.
        /// </summary>
        public ushort Characteristics { get; private set; }

        public COFFHeader(AssemblyBuffer buffer, DOSHeader currentHeader)
        {
            buffer.SetIndexPointer(currentHeader.PEHeaderPointer);
            this.Signature = buffer.ReadDWord();
            this.Machine = buffer.ReadWord();
            this.SectionCount = buffer.ReadWord();
            this.TimeDataStamp = buffer.ReadDWord();
            this.PointerToSymbolTable = buffer.ReadDWord();
            this.NumberOfSymbols = buffer.ReadDWord();
            this.SizeOfOptionalHeader = buffer.ReadWord();
            this.Characteristics = buffer.ReadWord();
            
        }

        /// <summary>
        /// Checks if the assembly has a certain characteristic
        /// based on the COFF Header's characteristics.
        /// </summary>
        /// <param name="characteristic"> The Characteristic to check against</param>
        /// <returns> If the characteristics contains that specific characteristic.</returns>
        public bool DoesHaveCharacteristic(COFFHeaderCharacteristic characteristic)
        {
            return (this.Characteristics & (uint)characteristic) != 1;
        }

    }
}