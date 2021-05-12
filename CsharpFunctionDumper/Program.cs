using System;
using System.IO;
using CsharpFunctionDumper.CLRProcessing.MetaDataStreams.Signatures;

namespace CsharpFunctionDumper
{
    class Program
    {
        public static void Main(string [] args)
        {

            string inPath = "";
            string outPath = "";
            
            for (int i = 0; i < args.Length; i+=0)
            {
                string argument = args[i];
                switch (argument)
                {
                    case "--in":
                    {
                        inPath = args[i + 1];
                        
                        i += 2;
                        continue;
                    }
                    
                    case "--out":
                    {
                        outPath = args[i + 1];
                        i += 2;
                        continue;
                    }
                }
                i += 1;
            }
            
            ProcessedPEFile funcData = ProcessedPEFile.ProcessFile(inPath);
            File.WriteAllText(outPath, funcData.GetOutput());
        }
    }
}