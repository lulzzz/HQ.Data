#region LICENSE

// Unless explicitly acquired and licensed from Licensor under another
// license, the contents of this file are subject to the Reciprocal Public
// License ("RPL") Version 1.5, or subsequent versions as allowed by the RPL,
// and You may not copy or use this file in either source code or executable
// form, except in compliance with the terms and conditions of the RPL.
// 
// All software distributed under the RPL is provided strictly on an "AS
// IS" basis, WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESS OR IMPLIED, AND
// LICENSOR HEREBY DISCLAIMS ALL SUCH WARRANTIES, INCLUDING WITHOUT
// LIMITATION, ANY WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
// PURPOSE, QUIET ENJOYMENT, OR NON-INFRINGEMENT. See the RPL for specific
// language governing rights and limitations under the RPL.

#endregion

using System;
using System.Text;
using HQ.Evolve.Internal;

namespace HQ.Evolve
{
    public static class LineValuesReader
    {
        public static void ReadValues(ulong lineNumber, ReadOnlySpan<byte> line, Encoding encoding, string separator, NewValue newValue)
        {
            ReadValues(lineNumber, line, encoding, encoding.GetSeparatorBuffer(separator), newValue);
        }

        public static unsafe void ReadValues(ulong lineNumber, byte* start, int length, Encoding encoding, string separator,
            NewValue newValue)
        {
            ReadValues(lineNumber, start, length, encoding, encoding.GetSeparatorBuffer(separator), newValue);
        }

        private static unsafe void ReadValues(ulong lineNumber, byte* start, int length, Encoding encoding, byte[] separator, NewValue newValue)
        {
            ReadValues(lineNumber, new ReadOnlySpan<byte>(start, length), encoding, separator, newValue);
        }

        private static void ReadValues(ulong lineNumber, ReadOnlySpan<byte> line, Encoding encoding, byte[] separator, NewValue newValue)
        {
            var position = 0;
            while (true)
            {
                var next = line.IndexOf(separator);
                if (next == -1)
                {
                    newValue?.Invoke(lineNumber, position, line, encoding);
                    break;
                }
                newValue?.Invoke(lineNumber, position, line.Slice(0, next), encoding);
                line = line.Slice(next + separator.Length);
                position += next + separator.Length;
            }
        }
    }
}
