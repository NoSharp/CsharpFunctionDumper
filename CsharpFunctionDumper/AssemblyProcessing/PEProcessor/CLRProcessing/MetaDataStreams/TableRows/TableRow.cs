using System.Collections.Generic;
using CsharpFunctionDumper.AssemblyProcessing.PEProcessor.MetaDataStreams;
using Microsoft.VisualBasic;

namespace CsharpFunctionDumper.AssemblyProcessing.PEProcessor.CLRProcessing.MetaDataStreams.TableRows
{
    public abstract class TableRow
    {
        public static MetaDataTableType OwnerTable;

        protected StreamHeader[] _streamHeaders;
        
        public TableRow(AssemblyBuffer buffer)
        {
        }

        public string ReadStringAtOffset(uint offset)
        {
            return ((StringStream)this._streamHeaders[(uint)MetaDataStreamType.STRINGS]).ReadUntilNull(offset);
        }
        
        public bool DoesFlagContainBitMask(uint flag, uint mask)
        {
            return (flag & mask) != 0;
        }
        
        public void SetStreamHeaderState(StreamHeader[] streamHeaders)
        {
            this._streamHeaders = streamHeaders;
        }

        /// <summary>
        /// Used to read from the Buffer instance.
        /// </summary>
        /// <param name="buffer"></param>
        public abstract void Read(AssemblyBuffer buffer);

        /// <summary>
        /// Used to format the data within the PE file.
        /// </summary>
        /// <returns>A formatted string of data.</returns>
        public abstract string Display();

    }
}