namespace CsharpFunctionDumper.CLRProcessing.MetaDataStreams
{
    public enum HeapOffsetSize
    {
        StringTableIndexOversize = 0x01,
        GuidTableIndexOversize = 0x02,
        BlobTableIndexOversize = 0x04
    }
}