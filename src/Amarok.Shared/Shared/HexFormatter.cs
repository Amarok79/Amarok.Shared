/* MIT License
 * 
 * Copyright (c) 2020, Olaf Kober
 * https://github.com/Amarok79/Amarok.Shared
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using System;
using System.Globalization;
using System.Text;


namespace Amarok.Shared
{
    internal static class HexFormatter
    {
        // static data
        private static readonly String[] sLower = new String[Byte.MaxValue + 1];
        private static readonly String[] sUpper = new String[Byte.MaxValue + 1];


        static HexFormatter()
        {
            for (var i = 0; i <= Byte.MaxValue; i++)
            {
                sLower[i] = i.ToString("x2", CultureInfo.InvariantCulture);
                sUpper[i] = i.ToString("X2", CultureInfo.InvariantCulture);
            }
        }


        internal static String ToLower(Byte value)
        {
            return sLower[value];
        }

        internal static String ToLower(Byte[] buffer, Int32 offset, Int32 count, String delimiter)
        {
            return _Format(
                sLower,
                buffer,
                offset,
                count,
                delimiter
            );
        }


        internal static String ToUpper(Byte value)
        {
            return sUpper[value];
        }

        internal static String ToUpper(Byte[] buffer, Int32 offset, Int32 count, String delimiter)
        {
            return _Format(
                sUpper,
                buffer,
                offset,
                count,
                delimiter
            );
        }


        private static String _Format(
            String[] strings,
            Byte[] buffer,
            Int32 offset,
            Int32 count,
            String delimiter
        )
        {
            StringBuilder? sb = null;

            try
            {
                sb = StringBuilderPool.Rent();

                for (var i = offset; i < offset + count; i++)
                {
                    if (sb.Length > 0)
                        sb.Append(delimiter);

                    if (i > offset && ( i - offset ) % 8 == 0)
                        sb.Append(delimiter);

                    sb.Append(strings[buffer[i]]);
                }

                return sb.ToString();
            }
            finally
            {
                StringBuilderPool.Free(sb);
            }
        }
    }
}
