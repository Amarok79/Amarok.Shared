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
using Amarok.Contracts;


namespace Amarok.Shared
{
    /// <summary>
    ///     This type provides extension methods for <see cref="Byte"/> arrays.
    /// </summary>
    public static class ByteArrayExtensions
    {
        // constants
        private const String DefaultDelimiter = "-";


        /// <summary>
        ///     Returns the hexadecimal representation (upper-case) of the given byte array.
        /// </summary>
        /// 
        /// <param name="buffer">
        ///     The byte array to format.
        /// </param>
        /// 
        /// <returns>
        ///     A string representation of the given byte array, for example, "11-FF-B7".
        /// </returns>
        /// 
        /// <exception cref="ArgumentNullException">
        ///     Null values are invalid.
        /// </exception>
        public static String ToHex(this Byte[] buffer)
        {
            Verify.NotNull(buffer, nameof(buffer));

            return HexFormatter.ToUpper(buffer, 0, buffer.Length, DefaultDelimiter);
        }

        /// <summary>
        ///     Returns the hexadecimal representation (upper-case) of the given byte array.
        /// </summary>
        /// 
        /// <param name="buffer">
        ///     The byte array to format.
        /// </param>
        /// <param name="delimiter">
        ///     The delimiter to be used.
        /// </param>
        /// 
        /// <returns>
        ///     A string representation of the given byte array.
        /// </returns>
        /// 
        /// <exception cref="ArgumentNullException">
        ///     Null values are invalid.
        /// </exception>
        public static String ToHex(this Byte[] buffer, String delimiter)
        {
            Verify.NotNull(buffer, nameof(buffer));
            Verify.NotNull(delimiter, nameof(delimiter));

            return HexFormatter.ToUpper(buffer, 0, buffer.Length, delimiter);
        }

        /// <summary>
        ///     Returns the hexadecimal representation (upper-case) of the given byte array.
        /// </summary>
        /// 
        /// <param name="buffer">
        ///     The byte array to format.
        /// </param>
        /// <param name="offset">
        ///     The byte offset in buffer where the data begin.
        /// </param>
        /// <param name="count">
        ///     The number of bytes.
        /// </param>
        /// 
        /// <returns>
        ///     A string representation of the given byte array, for example, "11-22-33".
        /// </returns>
        /// 
        /// <exception cref="ArgumentNullException">
        ///     Null values are invalid.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Negative values are invalid.
        /// </exception>
        /// <exception cref="ArgumentExceedsUpperLimitException">
        ///     Values exceeding the inclusive upper limit are invalid.
        /// </exception>
        public static String ToHex(this Byte[] buffer, Int32 offset, Int32 count)
        {
            Verify.ArraySegment(buffer, offset, count);

            return HexFormatter.ToUpper(buffer, offset, count, DefaultDelimiter);
        }

        /// <summary>
        ///     Returns the hexadecimal representation (upper-case) of the given byte array.
        /// </summary>
        /// 
        /// <param name="buffer">
        ///     The byte array to format.
        /// </param>
        /// <param name="offset">
        ///     The byte offset in buffer where the data begin.
        /// </param>
        /// <param name="count">
        ///     The number of bytes.
        /// </param>
        /// <param name="delimiter">
        ///     The delimiter to be used.
        /// </param>
        /// 
        /// <returns>
        ///     A string representation of the given byte array.
        /// </returns>
        /// 
        /// <exception cref="ArgumentNullException">
        ///     Null values are invalid.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Negative values are invalid.
        /// </exception>
        /// <exception cref="ArgumentExceedsUpperLimitException">
        ///     Values exceeding the inclusive upper limit are invalid.
        /// </exception>
        public static String ToHex(this Byte[] buffer, Int32 offset, Int32 count, String delimiter)
        {
            Verify.ArraySegment(buffer, offset, count);

            return HexFormatter.ToUpper(buffer, offset, count, delimiter);
        }


        /// <summary>
        ///     Returns the lower-case hexadecimal representation of the given byte array.
        /// </summary>
        /// 
        /// <param name="buffer">
        ///     The byte array to format.
        /// </param>
        /// 
        /// <returns>
        ///     A string representation of the given byte array, for example, "11-ff-b7".
        /// </returns>
        /// 
        /// <exception cref="ArgumentNullException">
        ///     Null values are invalid.
        /// </exception>
        public static String ToLowerHex(this Byte[] buffer)
        {
            Verify.NotNull(buffer, nameof(buffer));

            return HexFormatter.ToLower(buffer, 0, buffer.Length, DefaultDelimiter);
        }

        /// <summary>
        ///     Returns the lower-case hexadecimal representation of the given byte array.
        /// </summary>
        /// 
        /// <param name="buffer">
        ///     The byte array to format.
        /// </param>
        /// <param name="delimiter">
        ///     The delimiter to be used.
        /// </param>
        /// 
        /// <returns>
        ///     A string representation of the given byte array..
        /// </returns>
        /// 
        /// <exception cref="ArgumentNullException">
        ///     Null values are invalid.
        /// </exception>
        public static String ToLowerHex(this Byte[] buffer, String delimiter)
        {
            Verify.NotNull(buffer, nameof(buffer));
            Verify.NotNull(delimiter, nameof(delimiter));

            return HexFormatter.ToLower(buffer, 0, buffer.Length, delimiter);
        }

        /// <summary>
        ///     Returns the lower-case hexadecimal representation of the given byte array.
        /// </summary>
        /// 
        /// <param name="buffer">
        ///     The byte array to format.
        /// </param>
        /// <param name="offset">
        ///     The byte offset in buffer where the data begin.
        /// </param>
        /// <param name="count">
        ///     The number of bytes.
        /// </param>
        /// 
        /// <returns>
        ///     A string representation of the given byte array, for example, "11-22-33".
        /// </returns>
        /// 
        /// <exception cref="ArgumentNullException">
        ///     Null values are invalid.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Negative values are invalid.
        /// </exception>
        /// <exception cref="ArgumentExceedsUpperLimitException">
        ///     Values exceeding the inclusive upper limit are invalid.
        /// </exception>
        public static String ToLowerHex(this Byte[] buffer, Int32 offset, Int32 count)
        {
            Verify.ArraySegment(buffer, offset, count);

            return HexFormatter.ToLower(buffer, offset, count, DefaultDelimiter);
        }

        /// <summary>
        ///     Returns the lower-case hexadecimal representation of the given byte array.
        /// </summary>
        /// 
        /// <param name="buffer">
        ///     The byte array to format.
        /// </param>
        /// <param name="offset">
        ///     The byte offset in buffer where the data begin.
        /// </param>
        /// <param name="count">
        ///     The number of bytes.
        /// </param>
        /// <param name="delimiter">
        ///     The delimiter to be used.
        /// </param>
        /// 
        /// <returns>
        ///     A string representation of the given byte array.
        /// </returns>
        /// 
        /// <exception cref="ArgumentNullException">
        ///     Null values are invalid.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Negative values are invalid.
        /// </exception>
        /// <exception cref="ArgumentExceedsUpperLimitException">
        ///     Values exceeding the inclusive upper limit are invalid.
        /// </exception>
        public static String ToLowerHex(this Byte[] buffer, Int32 offset, Int32 count, String delimiter)
        {
            Verify.ArraySegment(buffer, offset, count);

            return HexFormatter.ToLower(buffer, offset, count, delimiter);
        }


        /// <summary>
        ///     Returns the upper-case hexadecimal representation of the given byte array.
        /// </summary>
        /// 
        /// <param name="buffer">
        ///     The byte array to format.
        /// </param>
        /// 
        /// <returns>
        ///     A string representation of the given byte array, for example, "11-FF-B7".
        /// </returns>
        /// 
        /// <exception cref="ArgumentNullException">
        ///     Null values are invalid.
        /// </exception>
        public static String ToUpperHex(this Byte[] buffer)
        {
            Verify.NotNull(buffer, nameof(buffer));

            return HexFormatter.ToUpper(buffer, 0, buffer.Length, DefaultDelimiter);
        }

        /// <summary>
        ///     Returns the upper-case hexadecimal representation of the given byte array.
        /// </summary>
        /// 
        /// <param name="buffer">
        ///     The byte array to format.
        /// </param>
        /// <param name="delimiter">
        ///     The delimiter to be used.
        /// </param>
        /// 
        /// <returns>
        ///     A string representation of the given byte array.
        /// </returns>
        /// 
        /// <exception cref="ArgumentNullException">
        ///     Null values are invalid.
        /// </exception>
        public static String ToUpperHex(this Byte[] buffer, String delimiter)
        {
            Verify.NotNull(buffer, nameof(buffer));
            Verify.NotNull(delimiter, nameof(delimiter));

            return HexFormatter.ToUpper(buffer, 0, buffer.Length, delimiter);
        }

        /// <summary>
        ///     Returns the upper-case hexadecimal representation of the given byte array.
        /// </summary>
        /// 
        /// <param name="buffer">
        ///     The byte array to format.
        /// </param>
        /// <param name="offset">
        ///     The byte offset in buffer where the data begin.
        /// </param>
        /// <param name="count">
        ///     The number of bytes.
        /// </param>
        /// 
        /// <returns>
        ///     A string representation of the given byte array, for example, "11-22-33".
        /// </returns>
        /// 
        /// <exception cref="ArgumentNullException">
        ///     Null values are invalid.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Negative values are invalid.
        /// </exception>
        /// <exception cref="ArgumentExceedsUpperLimitException">
        ///     Values exceeding the inclusive upper limit are invalid.
        /// </exception>
        public static String ToUpperHex(this Byte[] buffer, Int32 offset, Int32 count)
        {
            Verify.ArraySegment(buffer, offset, count);

            return HexFormatter.ToUpper(buffer, offset, count, DefaultDelimiter);
        }

        /// <summary>
        ///     Returns the upper-case hexadecimal representation of the given byte array.
        /// </summary>
        /// 
        /// <param name="buffer">
        ///     The byte array to format.
        /// </param>
        /// <param name="offset">
        ///     The byte offset in buffer where the data begin.
        /// </param>
        /// <param name="count">
        ///     The number of bytes.
        /// </param>
        /// <param name="delimiter">
        ///     The delimiter to be used.
        /// </param>
        /// 
        /// <returns>
        ///     A string representation of the given byte array.
        /// </returns>
        /// 
        /// <exception cref="ArgumentNullException">
        ///     Null values are invalid.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Negative values are invalid.
        /// </exception>
        /// <exception cref="ArgumentExceedsUpperLimitException">
        ///     Values exceeding the inclusive upper limit are invalid.
        /// </exception>
        public static String ToUpperHex(this Byte[] buffer, Int32 offset, Int32 count, String delimiter)
        {
            Verify.ArraySegment(buffer, offset, count);

            return HexFormatter.ToUpper(buffer, offset, count, delimiter);
        }
    }
}
