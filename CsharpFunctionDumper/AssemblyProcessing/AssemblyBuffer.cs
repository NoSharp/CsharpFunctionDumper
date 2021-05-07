using System;

namespace CsharpFunctionDumper.AssemblyProcessing
{
    /// <summary>
    /// Used to handle the reading of an assembly.
    /// </summary>
    public class AssemblyBuffer
    {
        private byte[] _buffer;
        private string _assemblyName;
        private uint _currentIndex;
        
        /// <summary>
        /// Creates an Assembly buffer
        /// </summary>
        /// <param name="assemblyName"> The name of the assembly </param>
        /// <param name="buffer"> The buffer in a byte array. </param>
        public AssemblyBuffer(string assemblyName, byte[] buffer)
        {
            _assemblyName = assemblyName;
            _buffer = buffer;
            _currentIndex = 0;
        }

        /// <summary>
        /// Reads x amount of bytes from the buffer and increases the current index pointer.
        /// </summary>
        /// <param name="amount"> the amount of bytes to read. </param>
        /// <returns> The bytes read from the buffer.</returns>
        public byte[] ReadBytes(uint amount)
        {
            byte[] data = new byte[amount];
            
            for (var i = 0; i < amount; i++)
            {
                data[i] = _buffer[this._currentIndex];
                _currentIndex++;
            }

            return data;
        }

        /// <summary>
        /// Reads a string up to the next 4 byte boundary.
        /// </summary>
        /// <returns>The string read, minus the null terminators.</returns>
        public string ReadDwordAlignedString()
        {
            string converted = "";
            byte val;
            while(true)
            {
                val = ReadByte();
                
                if (val == 0x0)
                {
                    // We're reading a Null terminated string so break out of the loop.
                    break;
                }

                converted += (char) val;
            }

            this._currentIndex += 4 - (this._currentIndex % 4);
                
            return converted;
        }

        /// <summary>
        /// Gets the position within the buffer.
        /// </summary>
        /// <returns>The Buffer position</returns>
        public uint GetBufferPosition()
        {
            return this._currentIndex;
        }

        /// <summary>
        /// Reads 2 bytes from the buffer and converts it into a ushort.
        /// </summary>
        /// <returns>The data read from the buffer.</returns>
        public ushort ReadWord()
        {
            return BitConverter.ToUInt16(ReadBytes(2));
        }

        /// <summary>
        /// Reads 4 Bytes from the buffer and converts it to a uint. (It's presumed it's unsigned.)
        /// </summary>
        /// <returns></returns>
        public uint ReadDWord()
        {
            return BitConverter.ToUInt32(ReadBytes(4));
        }

        
        /// <summary>
        /// Reads a singular byte from the current buffer.
        /// </summary>
        /// <returns> The next byte in the buffer </returns>
        public byte ReadByte()
        {
            return this.ReadBytes(1)[0];
        }
        
        /// <summary>
        /// Reads a QWord from the buffer.
        /// </summary>
        /// <returns> The QWord from the Buffer</returns>
        public ulong ReadQWord()
        {
            return BitConverter.ToUInt64(this.ReadBytes(8));
        }

        /// <summary>
        /// Reads the next "length" of bytes and converts them into characters
        /// which are appended together to make a string.
        /// </summary>
        /// <param name="length"> The amount of bytes to read</param>
        /// <returns>A string made from those bytes.</returns>
        public string ReadStringOfLength(uint length)
        {
            byte[] readBytes = ReadBytes(length);
            string converted = "";
            
            for (var i = 0; i < readBytes.Length; i++)
            {
                converted += (char) readBytes[i];
            }

            return converted;
        }

        /// <summary>
        /// Reads a null terminated string from the buffer.
        /// </summary>
        /// <returns> The string converted from bytes .</returns>
        public string ReadString()
        {
            string converted = "";
            byte val;
            while(true)
            {
                val = ReadByte();
                
                if (val == 0x0)
                {
                    // We're reading a Null terminated string so break out of the loop.
                    break;
                }

                converted += (char) val;
            }

            return converted;

        }

        /// <summary>
        /// Increments the current position within the buffer.
        /// </summary>
        /// <param name="amount"> The amount to increment the pointer by.</param>
        public void IncrementIndexPointer(uint amount)
        {
            this._currentIndex += amount;
        }

        /// <summary>
        /// Used to set the index pointer to an absolute value.
        /// </summary>
        /// <param name="newIdx"> The new position within the buffer for the index to point to. </param>
        public void SetIndexPointer(uint newIdx)
        {
            this._currentIndex = newIdx;
        }

    }
}