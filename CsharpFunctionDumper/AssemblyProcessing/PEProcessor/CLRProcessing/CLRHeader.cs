using CsharpFunctionDumper.AssemblyProcessing.PEProcessor.OptionalHeader;

namespace CsharpFunctionDumper.AssemblyProcessing.PEProcessor
{
    /// <summary>
    /// The header of all .net related stuff within the Assembly.
    /// Useful for getting function names, namespaces, types etc.
    /// This is a COR20 Header.
    /// Sources:
    ///     https://www.ntcore.com/files/dotnetformat.htm & https://www.codeproject.com/Articles/12585/The-NET-File-Format
    ///         (pretty sure they're written by the same person)
    ///     http://secana.github.io/PeNet/api/PeNet.Structures.IMAGE_COR20_HEADER.html
    /// </summary>
    public class CLRHeader
    {
        /// <summary>
        /// The size of the header
        /// </summary>
        public uint cb { get; private set; }

        public ushort MajorRuntimeVersion { get; private set; }
        public ushort MinorRuntimeVersion { get; private set; }

        public DataDirectory MetaData { get; private set; }

        public uint Flags { get; private set; }

        /// <summary>
        /// This is considered a Union in C++.
        /// Of either EntryPointToken OR EntryPointRVA.
        /// </summary>
        public uint EntryPointTokenOrEntryPointRVA { get; private set; }

        public DataDirectory Resources { get; private set; }
        public DataDirectory StrongNameSignature { get; private set; }
        public DataDirectory CodeManagerTable { get; private set; }
        public DataDirectory VTableFixups { get; private set; }
        public DataDirectory ExportAddressTableJumps { get; private set; }
        public DataDirectory ManagedNativeHeader { get; private set; }


        public CLRHeader(AssemblyBuffer buffer, OptionalHeader.OptionalHeader header, SectionsHeaders sectionsHeaders)
        {
            this.cb = buffer.ReadDWord();
            this.MajorRuntimeVersion = buffer.ReadWord();
            this.MinorRuntimeVersion = buffer.ReadWord();
            this.MetaData = new DataDirectory(buffer);
            this.Flags = buffer.ReadDWord();
            
            this.EntryPointTokenOrEntryPointRVA = buffer.ReadDWord();

            this.Resources = new DataDirectory(buffer);
            this.StrongNameSignature = new DataDirectory(buffer);
            this.CodeManagerTable = new DataDirectory(buffer);
            this.VTableFixups = new DataDirectory(buffer);
            this.ExportAddressTableJumps = new DataDirectory(buffer);
            this.ManagedNativeHeader = new DataDirectory(buffer);

        }

    }
}