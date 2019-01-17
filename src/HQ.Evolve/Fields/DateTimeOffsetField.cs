using System;
using System.Text;

namespace HQ.Evolve.Fields
{
    public readonly ref struct DateTimeOffsetField
    {
        public DateTimeOffset? Value => !_encoding.TryParse(_buffer, out DateTimeOffset value) ? default(DateTimeOffset?) : value;
        public string RawValue => _encoding.GetString(_buffer);

        private readonly Encoding _encoding;
        private readonly ReadOnlySpan<byte> _buffer;

        public DateTimeOffsetField(ReadOnlySpan<byte> buffer, Encoding encoding)
        {
            _buffer = buffer;
            _encoding = encoding;
        }

        public unsafe DateTimeOffsetField(byte* start, int length, Encoding encoding)
        {
            _buffer = new ReadOnlySpan<byte>(start, length);
            _encoding = encoding;
        }
    }
}
