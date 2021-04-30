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
        private byte[] ReadBytes(int amount)
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