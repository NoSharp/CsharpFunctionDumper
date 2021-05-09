namespace CsharpFunctionDumper.COFF
{
    public enum COFFHeaderMachineTypes
    {
        /// <summary>
        /// x86 Assemblies
        /// </summary>
        I386 = 0x014C,
        
        /// <summary>
        /// Intel Itanium  
        /// </summary>
        IA64 = 0x0200,
        
        /// <summary>
        /// 64Bit Assemblies
        /// </summary>
        AMD64 = 0x8664
    }
}