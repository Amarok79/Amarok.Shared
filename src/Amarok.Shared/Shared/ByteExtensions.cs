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


namespace Amarok.Shared
{
	/// <summary>
	/// This type provides extension methods for <see cref="Byte"/>.
	/// </summary>
	public static class ByteExtensions
	{
		/// <summary>
		/// Returns the hexadecimal representation (upper-case) of the given value.
		/// </summary>
		/// 
		/// <param name="value">
		/// The byte value to format.</param>
		/// 
		/// <returns>
		/// A string representation of the given value, for example, "A7" or "F0".</returns>
		public static String ToHex(this Byte value)
		{
			return HexFormatter.ToUpper(value);
		}


		/// <summary>
		/// Returns the lower-case hexadecimal representation of the given value.
		/// </summary>
		/// 
		/// <param name="value">
		/// The byte value to format.</param>
		/// 
		/// <returns>
		/// A string representation of the given value, for example, "a7" or "f0".</returns>
		public static String ToLowerHex(this Byte value)
		{
			return HexFormatter.ToLower(value);
		}

		/// <summary>
		/// Returns the upper-case hexadecimal representation of the given value.
		/// </summary>
		/// 
		/// <param name="value">
		/// The byte value to format.</param>
		/// 
		/// <returns>
		/// A string representation of the given value, for example, "A7" or "F0".</returns>
		public static String ToUpperHex(this Byte value)
		{
			return HexFormatter.ToUpper(value);
		}
	}
}
