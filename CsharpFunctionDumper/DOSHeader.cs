using CsharpFunctionDumper.AssemblyProcessing;

namespace CsharpFunctionDumper
{
     /// <summary>
     /// Handles the PE DOS Header.
     /// Sources:
     /// http://stixproject.github.io/data-model/1.2/WinExecutableFileObj/DOSHeaderType/
     /// https://www.nirsoft.net/kernel_struct/vista/IMAGE_DOS_HEADER.html
     /// </summary>
    public class DOSHeader
    {
        /// <summary>
        /// This is the Magic number used in the DOS Header.
        /// This should be "MZ" or 0x4D 0x5A
        /// </summary>
        public ushort MagicNumber { get; private set; }

        public uint PEHeaderPointer { get; private set; }

        public DOSHeader(AssemblyBuffer currentBuffer)
        {
            this.MagicNumber = currentBuffer.ReadWord();
            currentBuffer.SetIndexPointer(0x3C); // We don't need the rest of the struct so skip to the end to get the PE header pointer. 
            this.PEHeaderPointer = currentBuffer.ReadDWord();
            
        }

    }
}