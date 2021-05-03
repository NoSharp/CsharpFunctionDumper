using System;
using System.Collections.Generic;
using CsharpFunctionDumper.AssemblyProcessing.PEProcessor.CLRProcessing.MetaDataStreams;
using CsharpFunctionDumper.AssemblyProcessing.PEProcessor.CLRProcessing.MetaDataStreams.TableRows;
using CsharpFunctionDumper.AssemblyProcessing.PEProcessor.MetaDataStreams;

namespace CsharpFunctionDumper.AssemblyProcessing.PEProcessor.CLRProcessing
{
    /// <summary>
    /// The header of all .net related stuff within the Assembly.
    /// Useful for getting function names, namespaces, types etc.
    /// This is a COR20 HEADER.
    /// Sources:
    ///     https://www.ntcore.com/files/dotnetformat.htm & https://www.codeproject.com/Articles/12585/The-NET-File-Format
    ///         (pretty sure they're written by the same person)
    ///     http://secana.github.io/PeNet/api/PeNet.Structures.IMAGE_COR20_HEADER.html
    /// </summary>
    public class MetaDataHeader
    {

        public uint Signature { get; private set; }
        
        public ushort MajorVersion { get; private set; }
        public ushort MinorVersion { get; private set; }

        public uint Reserved { get; private set; }
        
        public uint VersionLength { get; private set; }
        public string VersionString { get; private set; }
        
        public ushort Flags { get; private set; }
        public ushort NumberOfStreams { get; private set; }

        public StreamHeader[] Streams { get; private set; }


        public MetaDataHeader(AssemblyBuffer buffer, CLRHeader clrHeader, SectionsHeaders sectionsHeaders)
        {
            buffer.SetIndexPointer(clrHeader.MetaData.RVA - 0x1E00);
            this.Signature = buffer.ReadDWord();
            this.MajorVersion = buffer.ReadWord();
            this.MinorVersion = buffer.ReadWord();
            this.Reserved = buffer.ReadDWord();
            this.VersionLength = buffer.ReadDWord();
            this.VersionString = buffer.ReadStringOfLength(this.VersionLength);
            this.Flags = buffer.ReadWord();
            this.NumberOfStreams = buffer.ReadWord();

            this.Streams = new StreamHeader[this.NumberOfStreams];

            this.Streams[(uint)MetaDataStreamType.DEFS_AND_REFS] = new DefsAndRefsStream(buffer, clrHeader,this);
            this.Streams[(uint)MetaDataStreamType.STRINGS] = new StringStream(buffer, clrHeader);
            
            this.Streams[(uint)MetaDataStreamType.STRINGS].CacheBuffer(buffer);
            this.Streams[(uint)MetaDataStreamType.STRINGS].ProcessTables(buffer);
            
            this.Streams[(uint)MetaDataStreamType.DEFS_AND_REFS].CacheBuffer(buffer);
            this.Streams[(uint)MetaDataStreamType.DEFS_AND_REFS].ProcessTables(buffer);

            DefsAndRefsStream defsAndRefsStream = (DefsAndRefsStream) this.Streams[(uint) MetaDataStreamType.DEFS_AND_REFS];
            foreach (var tableRow in defsAndRefsStream.TableRows[MetaDataTableType.TypeDef])
            {
                Console.WriteLine($"{tableRow.Display()}");
            }
            
        }

    }
}