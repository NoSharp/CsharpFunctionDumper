using CsharpFunctionDumper.AssemblyProcessing;

namespace CsharpFunctionDumper
{
    public class SectionsHeaders
    {

        public Section TextSection { get; private set; }

        public Section RsrcSection { get; private set; }

        public Section RelocSection { get; private set; }

        public SectionsHeaders(AssemblyBuffer buffer)
        {
            this.TextSection = new Section(buffer);
            this.RsrcSection = new Section(buffer);
            this.RelocSection = new Section(buffer);
            buffer.IncrementIndexPointer(16); // The buffer between reloc Section header and the .text section contents
            buffer.IncrementIndexPointer(8); // Some more buffer space.
        }

    }
}