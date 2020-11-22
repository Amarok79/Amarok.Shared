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
    public class Test_ByteExtensions
    {
        [Test]
        public void Test_ToHex()
        {
            Check.That(( (Byte) 0x00 ).ToHex()).IsEqualTo("00");
            Check.That(( (Byte) 0x0F ).ToHex()).IsEqualTo("0F");
            Check.That(( (Byte) 0xF0 ).ToHex()).IsEqualTo("F0");
            Check.That(( (Byte) 0xFF ).ToHex()).IsEqualTo("FF");
        }

        [Test]
        public void Test_ToLowerHex()
        {
            Check.That(( (Byte) 0x00 ).ToLowerHex()).IsEqualTo("00");
            Check.That(( (Byte) 0x0F ).ToLowerHex()).IsEqualTo("0f");
            Check.That(( (Byte) 0xF0 ).ToLowerHex()).IsEqualTo("f0");
            Check.That(( (Byte) 0xFF ).ToLowerHex()).IsEqualTo("ff");
        }

        [Test]
        public void Test_ToUpperHex()
        {
            Check.That(( (Byte) 0x00 ).ToUpperHex()).IsEqualTo("00");
            Check.That(( (Byte) 0x0F ).ToUpperHex()).IsEqualTo("0F");
            Check.That(( (Byte) 0xF0 ).ToUpperHex()).IsEqualTo("F0");
            Check.That(( (Byte) 0xFF ).ToUpperHex()).IsEqualTo("FF");
        }
    }
}
