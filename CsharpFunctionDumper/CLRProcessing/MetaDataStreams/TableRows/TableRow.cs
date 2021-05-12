using CsharpFunctionDumper.AssemblyProcessing;

namespace CsharpFunctionDumper.CLRProcessing.MetaDataStreams.TableRows
{
    public abstract class TableRow
    {
        public static MetaDataTableType OwnerTable;

        protected StreamHeader[] _streamHeaders;
        
        public TableRow(AssemblyBuffer buffer)
        {
        }

        protected DefsAndRefsStream GetDefsAndRefsStream()
        {
            return (DefsAndRefsStream)this._streamHeaders[(uint) MetaDataStreamType.DEFS_AND_REFS];
        }

        protected uint ReadStringTableOffset(AssemblyBuffer buffer)
        {
            return this.GetDefsAndRefsStream().ReadStringOffset(buffer);
        }

        protected uint ReadBlobTableOffset(AssemblyBuffer buffer)
        {
            return this.GetDefsAndRefsStream().ReadBlobOffset(buffer);
        }

        protected uint ReadGuidOffset(AssemblyBuffer buffer)
        {
            return this.GetDefsAndRefsStream().ReadGuidOffset(buffer);
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

        protected BlobStream GetBlobStream()
        {
            return ((BlobStream) this._streamHeaders[(uint)MetaDataStreamType.BLOB]);
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