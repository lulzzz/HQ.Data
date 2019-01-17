using System;
using System.Text;

namespace HQ.Evolve.Fields
{
    public readonly ref struct BooleanField
    {
        public bool? Value => !_encoding.TryParse(_buffer, out bool value) ? default(bool?) : value;
        public string RawValue => _encoding.GetString(_buffer);

        private readonly Encoding _encoding;
        private readonly ReadOnlySpan<byte> _buffer;

        public BooleanField(ReadOnlySpan<byte> buffer, Encoding encoding)
        {
            _buffer = buffer;
            _encoding = encoding;
        }

        public unsafe BooleanField(byte* start, int length, Encoding encoding)
        {
            try
            {
                _buffer = new ReadOnlySpan<byte>(start, length);
                _encoding = encoding;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
