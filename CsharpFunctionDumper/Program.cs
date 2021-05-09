using System;

namespace CsharpFunctionDumper
{
    class Program
    {
        public static void Main(string [] args){

            ProcessedPEFile.ProcessFile($"{Environment.CurrentDirectory}/TestFolder/Test.dll");
        }
    }
}