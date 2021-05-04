using System.IO;
using CsharpFunctionDumper.AssemblyProcessing.PEProcessor.CLRProcessing;
using CsharpFunctionDumper.AssemblyProcessing.PEProcessor.Optional;

namespace CsharpFunctionDumper.AssemblyProcessing.PEProcessor
{
    /// <summary>
    /// A state for the current PE file being processed.
    /// </summary>
    public class ProcessedPEFile
    {

        public DOSHeader DosHeader { get; private set; }
        public COFFHeader CoffHeader { get; private set; }
        public OptionalHeader OptionalHeader { get; private set;  }
        public SectionsHeaders SectionsHeaders { get; private set;  }
        public CLRHeader ClrHeader { get; private set; }
        public MetaDataHeader MetaDataHeader { get; private set; }


        public ProcessedPEFile(AssemblyBuffer assemblyBuffer)
        {
            this.DosHeader = new DOSHeader(assemblyBuffer);
            this.CoffHeader = new COFFHeader(assemblyBuffer, this.DosHeader);
            this.OptionalHeader = new OptionalHeader(assemblyBuffer);
            this.SectionsHeaders = new SectionsHeaders(assemblyBuffer);
            
            this.ClrHeader = new CLRHeader(assemblyBuffer, this.OptionalHeader, this.SectionsHeaders);
            this.MetaDataHeader = new MetaDataHeader(assemblyBuffer, this.ClrHeader, this.SectionsHeaders);
        }

        /// <summary>
        /// Processes a PE file at a specific directory.
        /// </summary>
        /// <param name="filePath"> an ABSOLUTE path to the target file. </param>
        /// <returns> The processedPEFile from it .</returns>
        public static ProcessedPEFile ProcessFile(string filePath)
        {
            
            byte[] buffer = File.ReadAllBytes(filePath);
            AssemblyBuffer assemblyBuffer = new AssemblyBuffer("", buffer);

            return new ProcessedPEFile(assemblyBuffer);
        }

    }
}