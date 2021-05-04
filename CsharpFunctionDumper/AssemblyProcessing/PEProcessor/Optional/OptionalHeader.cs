
namespace CsharpFunctionDumper.AssemblyProcessing.PEProcessor.Optional
{
    /// <summary>
    /// For more information check:
    ///     https://docs.microsoft.com/en-us/windows/win32/api/winnt/ns-winnt-image_optional_header32
    ///     https://www.red-gate.com/simple-talk/blogs/anatomy-of-a-net-assembly-pe-headers/
    /// </summary>
    public class OptionalHeader
    {

        //@TODO: Add some comments and explain what each thing is.
        
        public ushort Magic { get; private set; }

        public byte MajorLinkerVersion { get; private set; }

        public byte MinorLinkerVersion { get; private set; }

        public uint SizeOfCode { get; private set; }
        
        public uint SizeOfInitializedData { get; private set; }

        public uint SizeOfUninitializedData { get; private set; }
        
        public uint AddressOfEntryPoint { get; private set; }
        
        public uint BaseOfCode { get; private set; }
        
        public uint BaseOfData { get; private set; }
        
        public uint ImageBase { get; private set; }
        
        public uint SectionAlignment { get; private set; }
                
        public uint FileAlignment { get; private set; }
        
        public ushort MajorOperatingSystemVersion { get; private set; }
        
        public ushort MinorOperatingSystemVersion { get; private set; }
        
        public ushort MajorImageVersion { get; private set; }
        
        public ushort MinorImageVersion { get; private set; }
        
        public ushort MajorSubsystemVersion { get; private set; }
        
        public ushort MinorSubsystemVersion { get; private set; }
        
        public uint Win32VersionValue { get; private set; }
        
        public uint SizeOfImage { get; private set; }
        
        public uint SizeOfHeaders { get; private set; }

        public uint CheckSum { get; private set; }
        
        public ushort Subsystem { get; private set; }
        
        public ushort DllCharacteristics { get; private set; }
        
        public uint SizeOfStackReserve { get; private set; }
        
        public uint SizeOfStackCommit { get; private set; }
        
        public uint SizeOfHeapReserve { get; private set; }
        
        public uint SizeOfHeapCommit { get; private set; }
        
        public uint LoaderFlags { get; private set; }
        
        public uint NumberOfRvaAndSizes { get; private set; }

        public const uint ImageNumberOfDirectoryEntries = 16;
        
        public DataDirectory[] DataDirectories;
        
        
        
        public OptionalHeader(AssemblyBuffer buffer)
        {
            this.Magic = buffer.ReadWord();
            this.MajorLinkerVersion = buffer.ReadByte();
            this.MinorLinkerVersion = buffer.ReadByte();
            
            this.SizeOfCode = buffer.ReadDWord();
            this.SizeOfInitializedData = buffer.ReadDWord();
            this.SizeOfUninitializedData = buffer.ReadDWord();
            this.AddressOfEntryPoint = buffer.ReadDWord();
            this.BaseOfCode = buffer.ReadDWord();
            this.BaseOfData = buffer.ReadDWord();
            this.ImageBase = buffer.ReadDWord();
            this.SectionAlignment = buffer.ReadDWord();
            this.FileAlignment = buffer.ReadDWord();
            
            this.MajorOperatingSystemVersion = buffer.ReadWord();
            this.MinorOperatingSystemVersion = buffer.ReadWord();

            this.MajorImageVersion = buffer.ReadWord();
            this.MinorImageVersion = buffer.ReadWord();

            this.MajorSubsystemVersion = buffer.ReadWord();
            this.MinorSubsystemVersion = buffer.ReadWord();

            this.Win32VersionValue = buffer.ReadDWord();
            
            this.SizeOfImage = buffer.ReadDWord();
            this.SizeOfHeaders = buffer.ReadDWord();

            this.CheckSum = buffer.ReadDWord();
            this.Subsystem = buffer.ReadWord();

            this.DllCharacteristics = buffer.ReadWord();
            
            this.SizeOfStackReserve = buffer.ReadDWord();
            this.SizeOfStackCommit = buffer.ReadDWord();
            this.SizeOfHeapReserve = buffer.ReadDWord();
            this.SizeOfHeapCommit = buffer.ReadDWord();
            
            this.LoaderFlags = buffer.ReadDWord();
            this.NumberOfRvaAndSizes = buffer.ReadDWord();

            this.DataDirectories = new DataDirectory[ImageNumberOfDirectoryEntries];

            for (int i = 0; i < ImageNumberOfDirectoryEntries; i++)
            {
                this.DataDirectories[i] = new DataDirectory(buffer);
            }

        }
    }
}