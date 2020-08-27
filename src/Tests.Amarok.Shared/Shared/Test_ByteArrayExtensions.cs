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
using NFluent;
using NUnit.Framework;


namespace Amarok.Shared
{
	[TestFixture]
	public class Test_ByteArrayExtensions
	{
		[Test]
		public void Test_ToHex_Buffer()
		{
			Check.That(new Byte[] { 0x11, 0xFF, 0xB3 }.ToHex())
				.IsEqualTo("11-FF-B3");
		}

		[Test]
		public void Test_ToLowerHex_Buffer()
		{
			Check.That(new Byte[] { 0x11, 0xFF, 0xB3 }.ToLowerHex())
				.IsEqualTo("11-ff-b3");
		}

		[Test]
		public void Test_ToUpperHex_Buffer()
		{
			Check.That(new Byte[] { 0x11, 0xFF, 0xB3 }.ToUpperHex())
				.IsEqualTo("11-FF-B3");
		}


		[Test]
		public void Test_ToHex_Buffer_Delimiter()
		{
			Check.That(new Byte[] { 0x11, 0xFF, 0xB3 }.ToHex(":"))
				.IsEqualTo("11:FF:B3");
		}

		[Test]
		public void Test_ToLowerHex_Buffer_Delimiter()
		{
			Check.That(new Byte[] { 0x11, 0xFF, 0xB3 }.ToLowerHex(":"))
				.IsEqualTo("11:ff:b3");
		}

		[Test]
		public void Test_ToUpperHex_Buffer_Delimiter()
		{
			Check.That(new Byte[] { 0x11, 0xFF, 0xB3 }.ToUpperHex(":"))
				.IsEqualTo("11:FF:B3");
		}


		[Test]
		public void Test_ToHex_BufferOffsetCount()
		{
			Check.That(new Byte[] { 0x11, 0xFF, 0xB3 }.ToHex(1, 2))
				.IsEqualTo("FF-B3");
		}

		[Test]
		public void Test_ToLowerHex_BufferOffsetCount()
		{
			Check.That(new Byte[] { 0x11, 0xFF, 0xB3 }.ToLowerHex(1, 2))
				.IsEqualTo("ff-b3");
		}

		[Test]
		public void Test_ToUpperHex_BufferOffsetCount()
		{
			Check.That(new Byte[] { 0x11, 0xFF, 0xB3 }.ToUpperHex(1, 2))
				.IsEqualTo("FF-B3");
		}


		[Test]
		public void Test_ToHex_BufferOffsetCount_Delimiter()
		{
			Check.That(new Byte[] { 0x11, 0xFF, 0xB3 }.ToHex(1, 2, ":"))
				.IsEqualTo("FF:B3");
		}

		[Test]
		public void Test_ToLowerHex_BufferOffsetCount_Delimiter()
		{
			Check.That(new Byte[] { 0x11, 0xFF, 0xB3 }.ToLowerHex(1, 2, ":"))
				.IsEqualTo("ff:b3");
		}

		[Test]
		public void Test_ToUpperHex_BufferOffsetCount_Delimiter()
		{
			Check.That(new Byte[] { 0x11, 0xFF, 0xB3 }.ToUpperHex(1, 2, ":"))
				.IsEqualTo("FF:B3");
		}
	}
}
