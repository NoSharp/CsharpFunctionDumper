using System.Collections.Generic;
using System.Text;
using CsharpFunctionDumper.AssemblyProcessing;
using CsharpFunctionDumper.CLRProcessing.MetaDataStreams.Signatures;

namespace CsharpFunctionDumper.CLRProcessing.MetaDataStreams.TableRows
{
    public class MethodTableRow : TableRow
    {
        public static MetaDataTableType OwnerTable = MetaDataTableType.MethodDef;

        public uint RVA { get; private set; }
        public ushort ImplementationFlags { get; private set; }
        public ushort DefinitionFlags { get; private set; }
        public ushort NameAddress { get; private set; }
        public ushort Signature { get; private set; }
        public ushort ParamsListIndex { get; private set; }
        public string Name { get; private set; }

        public List<byte> MethodBody { get; private set; }

        public MethodTableRow(AssemblyBuffer buffer) : base(buffer)
        {
        }

        public override void Read(AssemblyBuffer buffer)
        {
            this.RVA = buffer.ReadDWord();
            this.ImplementationFlags = buffer.ReadWord();
            this.DefinitionFlags = buffer.ReadWord();
            this.NameAddress = buffer.ReadWord();
            this.Signature = buffer.ReadWord();
            this.ParamsListIndex = buffer.ReadWord();

            this.Name = this.ReadStringAtOffset(this.NameAddress);

            uint oldBufferPos = buffer.GetBufferPosition();
            
            buffer.SetIndexPointer(this.RVA);
            
            // Do the reading of the function
            this.ReadFunctionBody(buffer);
            
            buffer.SetIndexPointer(oldBufferPos);
        }

        private void ReadFunctionBody(AssemblyBuffer buffer)
        {
            List<byte> methodBody = new List<byte>();
            while (true)
            {
                byte opCode = buffer.ReadByte();
                methodBody.Add(opCode);
                if (opCode == 0x2A) break; // When we reach "return" we know this is the end of the function.
            }

            this.MethodBody = methodBody;
        }

        public override string Display()
        {
            StringBuilder funcDef = new StringBuilder();
            funcDef.Append($"func {this.Name}(");
            DefsAndRefsStream defsAndRefsStream = DefsAndRefsStream.GetInstance();
            List<ParamTableRow> paramTableRows = defsAndRefsStream.GetParameterTableRowsFromOffset(this.ParamsListIndex);
            MethodDefSignature method = this.GetBlobStream().GetMethodDefValue(this.Signature);
            for (var i = 0; i < paramTableRows.Count; i++)
            {
                ParamTableRow paramTableRow = paramTableRows[i];

                
                if (i < method.ParameterTypes.Count)
                {
                    ParameterType parameterType = method.ParameterTypes[i];
                    funcDef.Append($"{(i == 0 ? "" : ",")} {parameterType.Type.ToString()} {paramTableRow.Display()}");
                }
                else
                {
                    funcDef.Append($"{(i == 0 ? "" : ",")}{paramTableRow.Display()}");
                }

            }

            funcDef.Append($") -> {method.ReturnType.Type.ToString()}");
            return funcDef.ToString();
        }
        
    }
}