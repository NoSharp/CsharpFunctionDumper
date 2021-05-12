using System.Text;
using CsharpFunctionDumper.AssemblyProcessing;

namespace CsharpFunctionDumper.CLRProcessing.MetaDataStreams.TableRows
{
    public class ParamTableRow : TableRow
    {
        
        public static MetaDataTableType OwnerTable = MetaDataTableType.Param;
        
        public ushort Flags { get; private set; }
        public ushort Sequence { get; private set; }
        public uint NameAddresss { get; private set; }
        public string Name { get; private set; }


        public ParamTableRow(AssemblyBuffer buffer) : base(buffer)
        {
        }

        public override void Read(AssemblyBuffer buffer)
        {
            this.Flags = buffer.ReadWord();
            this.Sequence = buffer.ReadWord();
            this.NameAddresss = this.ReadStringTableOffset(buffer);
            this.Name = this.ReadStringAtOffset(this.NameAddresss);
        }

        private string GetParamPrefix()
        {
            StringBuilder prefix = new StringBuilder();

            if ((this.Flags & (uint) ParamAttribute.In) != 0)
            {
                prefix.Append("[In]");
            }
            
            if ((this.Flags & (uint) ParamAttribute.Out) != 0)
            {
                prefix.Append("[Out]");
            }
            
            if ((this.Flags & (uint) ParamAttribute.Optional) != 0)
            {
                prefix.Append("[Optional]");
            }
            
            if ((this.Flags & (uint) ParamAttribute.HasDefault) != 0)
            {
                prefix.Append("[Default]");
            }
            
            if ((this.Flags & (uint) ParamAttribute.HasFieldMarshal) != 0)
            {
                prefix.Append("[FieldMarsh]");
            }
            
            if ((this.Flags & (uint) ParamAttribute.Unused) != 0)
            {
                prefix.Append("[Unused]");
            }

            
            return prefix.ToString();
        }

        public override string Display()
        {
            return $"{this.GetParamPrefix()} {this.Name}";
        }
    }
}