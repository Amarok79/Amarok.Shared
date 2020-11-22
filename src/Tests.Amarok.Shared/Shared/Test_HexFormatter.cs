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
    public class Test_HexFormatter
    {
        [Test]
        public void Format_Byte_ToLower()
        {
            Check.That(HexFormatter.ToLower(0x00)).IsEqualTo("00");
            Check.That(HexFormatter.ToLower(0x01)).IsEqualTo("01");
            Check.That(HexFormatter.ToLower(0x10)).IsEqualTo("10");
            Check.That(HexFormatter.ToLower(0xAB)).IsEqualTo("ab");
            Check.That(HexFormatter.ToLower(0xFF)).IsEqualTo("ff");
        }

        [Test]
        public void Format_Byte_ToUpper()
        {
            Check.That(HexFormatter.ToUpper(0x00)).IsEqualTo("00");
            Check.That(HexFormatter.ToUpper(0x01)).IsEqualTo("01");
            Check.That(HexFormatter.ToUpper(0x10)).IsEqualTo("10");
            Check.That(HexFormatter.ToUpper(0xAB)).IsEqualTo("AB");
            Check.That(HexFormatter.ToUpper(0xFF)).IsEqualTo("FF");
        }

        [Test]
        public void Format_Array_ToLower()
        {
            Check.That(HexFormatter.ToLower(new Byte[] { }, 0, 0, "-")).IsEqualTo(String.Empty);
            Check.That(HexFormatter.ToLower(new Byte[] { 0x11 }, 0, 0, "-")).IsEqualTo(String.Empty);
            Check.That(HexFormatter.ToLower(new Byte[] { 0x11 }, 0, 1, "-")).IsEqualTo("11");
            Check.That(HexFormatter.ToLower(new Byte[] { 0x11, 0x22, 0x33 }, 0, 1, "-")).IsEqualTo("11");
            Check.That(HexFormatter.ToLower(new Byte[] { 0x11, 0x22, 0x33 }, 1, 1, "-")).IsEqualTo("22");
            Check.That(HexFormatter.ToLower(new Byte[] { 0x11, 0x22, 0x33 }, 1, 2, "-")).IsEqualTo("22-33");

            Check.That(HexFormatter.ToLower(new Byte[] { 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77 }, 0, 7, "-"))
                 .IsEqualTo("11-22-33-44-55-66-77");

            Check.That(HexFormatter.ToLower(new Byte[] { 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88 }, 0, 8, "-"))
                 .IsEqualTo("11-22-33-44-55-66-77-88");

            Check.That(
                      HexFormatter.ToLower(
                          new Byte[] {
                              0x11,
                              0x22,
                              0x33,
                              0x44,
                              0x55,
                              0x66,
                              0x77,
                              0x88,
                              0x99,
                              0xAA,
                              0xBB,
                              0xCC,
                              0xDD,
                              0xEE,
                              0xFF,
                              0x11,
                              0x22,
                              0x33,
                          },
                          0,
                          9,
                          "-"
                      )
                  )
                 .IsEqualTo("11-22-33-44-55-66-77-88--99");

            Check.That(
                      HexFormatter.ToLower(
                          new Byte[] {
                              0x11,
                              0x22,
                              0x33,
                              0x44,
                              0x55,
                              0x66,
                              0x77,
                              0x88,
                              0x99,
                              0xAA,
                              0xBB,
                              0xCC,
                              0xDD,
                              0xEE,
                              0xFF,
                              0x11,
                              0x22,
                              0x33,
                          },
                          0,
                          15,
                          "-"
                      )
                  )
                 .IsEqualTo("11-22-33-44-55-66-77-88--99-aa-bb-cc-dd-ee-ff");

            Check.That(
                      HexFormatter.ToLower(
                          new Byte[] {
                              0x11,
                              0x22,
                              0x33,
                              0x44,
                              0x55,
                              0x66,
                              0x77,
                              0x88,
                              0x99,
                              0xAA,
                              0xBB,
                              0xCC,
                              0xDD,
                              0xEE,
                              0xFF,
                              0x11,
                              0x22,
                              0x33,
                          },
                          0,
                          15,
                          " "
                      )
                  )
                 .IsEqualTo("11 22 33 44 55 66 77 88  99 aa bb cc dd ee ff");

            Check.That(
                      HexFormatter.ToLower(
                          new Byte[] {
                              0x11,
                              0x22,
                              0x33,
                              0x44,
                              0x55,
                              0x66,
                              0x77,
                              0x88,
                              0x99,
                              0xAA,
                              0xBB,
                              0xCC,
                              0xDD,
                              0xEE,
                              0xFF,
                              0x11,
                              0x22,
                              0x33,
                          },
                          0,
                          16,
                          "-"
                      )
                  )
                 .IsEqualTo("11-22-33-44-55-66-77-88--99-aa-bb-cc-dd-ee-ff-11");

            Check.That(
                      HexFormatter.ToLower(
                          new Byte[] {
                              0x11,
                              0x22,
                              0x33,
                              0x44,
                              0x55,
                              0x66,
                              0x77,
                              0x88,
                              0x99,
                              0xAA,
                              0xBB,
                              0xCC,
                              0xDD,
                              0xEE,
                              0xFF,
                              0x11,
                              0x22,
                              0x33,
                          },
                          0,
                          17,
                          "-"
                      )
                  )
                 .IsEqualTo("11-22-33-44-55-66-77-88--99-aa-bb-cc-dd-ee-ff-11--22");

            Check.That(
                      HexFormatter.ToLower(
                          new Byte[] {
                              0x11,
                              0x22,
                              0x33,
                              0x44,
                              0x55,
                              0x66,
                              0x77,
                              0x88,
                              0x99,
                              0xAA,
                              0xBB,
                              0xCC,
                              0xDD,
                              0xEE,
                              0xFF,
                              0x11,
                              0x22,
                              0x33,
                          },
                          0,
                          17,
                          ":"
                      )
                  )
                 .IsEqualTo("11:22:33:44:55:66:77:88::99:aa:bb:cc:dd:ee:ff:11::22");

            Check.That(
                      HexFormatter.ToLower(
                          new Byte[] {
                              0x11,
                              0x22,
                              0x33,
                              0x44,
                              0x55,
                              0x66,
                              0x77,
                              0x88,
                              0x99,
                              0xAA,
                              0xBB,
                              0xCC,
                              0xDD,
                              0xEE,
                              0xFF,
                              0x11,
                              0x22,
                              0x33,
                          },
                          0,
                          17,
                          String.Empty
                      )
                  )
                 .IsEqualTo("112233445566778899aabbccddeeff1122");

            Check.That(
                      HexFormatter.ToLower(
                          new Byte[] {
                              0x11,
                              0x22,
                              0x33,
                              0x44,
                              0x55,
                              0x66,
                              0x77,
                              0x88,
                              0x99,
                              0xAA,
                              0xBB,
                              0xCC,
                              0xDD,
                              0xEE,
                              0xFF,
                              0x11,
                              0x22,
                              0x33,
                          },
                          7,
                          10,
                          "-"
                      )
                  )
                 .IsEqualTo("88-99-aa-bb-cc-dd-ee-ff--11-22");
        }

        [Test]
        public void Format_Array_ToUpper()
        {
            Check.That(HexFormatter.ToUpper(new Byte[] { }, 0, 0, "-")).IsEqualTo(String.Empty);
            Check.That(HexFormatter.ToUpper(new Byte[] { 0x11 }, 0, 0, "-")).IsEqualTo(String.Empty);
            Check.That(HexFormatter.ToUpper(new Byte[] { 0x11 }, 0, 1, "-")).IsEqualTo("11");
            Check.That(HexFormatter.ToUpper(new Byte[] { 0x11, 0x22, 0x33 }, 0, 1, "-")).IsEqualTo("11");
            Check.That(HexFormatter.ToUpper(new Byte[] { 0x11, 0x22, 0x33 }, 1, 1, "-")).IsEqualTo("22");
            Check.That(HexFormatter.ToUpper(new Byte[] { 0x11, 0x22, 0x33 }, 1, 2, "-")).IsEqualTo("22-33");

            Check.That(HexFormatter.ToUpper(new Byte[] { 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77 }, 0, 7, "-"))
                 .IsEqualTo("11-22-33-44-55-66-77");

            Check.That(HexFormatter.ToUpper(new Byte[] { 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88 }, 0, 8, "-"))
                 .IsEqualTo("11-22-33-44-55-66-77-88");

            Check.That(
                      HexFormatter.ToUpper(
                          new Byte[] {
                              0x11,
                              0x22,
                              0x33,
                              0x44,
                              0x55,
                              0x66,
                              0x77,
                              0x88,
                              0x99,
                              0xAA,
                              0xBB,
                              0xCC,
                              0xDD,
                              0xEE,
                              0xFF,
                              0x11,
                              0x22,
                              0x33,
                          },
                          0,
                          9,
                          "-"
                      )
                  )
                 .IsEqualTo("11-22-33-44-55-66-77-88--99");

            Check.That(
                      HexFormatter.ToUpper(
                          new Byte[] {
                              0x11,
                              0x22,
                              0x33,
                              0x44,
                              0x55,
                              0x66,
                              0x77,
                              0x88,
                              0x99,
                              0xAA,
                              0xBB,
                              0xCC,
                              0xDD,
                              0xEE,
                              0xFF,
                              0x11,
                              0x22,
                              0x33,
                          },
                          0,
                          15,
                          "-"
                      )
                  )
                 .IsEqualTo("11-22-33-44-55-66-77-88--99-AA-BB-CC-DD-EE-FF");

            Check.That(
                      HexFormatter.ToUpper(
                          new Byte[] {
                              0x11,
                              0x22,
                              0x33,
                              0x44,
                              0x55,
                              0x66,
                              0x77,
                              0x88,
                              0x99,
                              0xAA,
                              0xBB,
                              0xCC,
                              0xDD,
                              0xEE,
                              0xFF,
                              0x11,
                              0x22,
                              0x33,
                          },
                          0,
                          15,
                          " "
                      )
                  )
                 .IsEqualTo("11 22 33 44 55 66 77 88  99 AA BB CC DD EE FF");

            Check.That(
                      HexFormatter.ToUpper(
                          new Byte[] {
                              0x11,
                              0x22,
                              0x33,
                              0x44,
                              0x55,
                              0x66,
                              0x77,
                              0x88,
                              0x99,
                              0xAA,
                              0xBB,
                              0xCC,
                              0xDD,
                              0xEE,
                              0xFF,
                              0x11,
                              0x22,
                              0x33,
                          },
                          0,
                          16,
                          "-"
                      )
                  )
                 .IsEqualTo("11-22-33-44-55-66-77-88--99-AA-BB-CC-DD-EE-FF-11");

            Check.That(
                      HexFormatter.ToUpper(
                          new Byte[] {
                              0x11,
                              0x22,
                              0x33,
                              0x44,
                              0x55,
                              0x66,
                              0x77,
                              0x88,
                              0x99,
                              0xAA,
                              0xBB,
                              0xCC,
                              0xDD,
                              0xEE,
                              0xFF,
                              0x11,
                              0x22,
                              0x33,
                          },
                          0,
                          17,
                          "-"
                      )
                  )
                 .IsEqualTo("11-22-33-44-55-66-77-88--99-AA-BB-CC-DD-EE-FF-11--22");

            Check.That(
                      HexFormatter.ToUpper(
                          new Byte[] {
                              0x11,
                              0x22,
                              0x33,
                              0x44,
                              0x55,
                              0x66,
                              0x77,
                              0x88,
                              0x99,
                              0xAA,
                              0xBB,
                              0xCC,
                              0xDD,
                              0xEE,
                              0xFF,
                              0x11,
                              0x22,
                              0x33,
                          },
                          0,
                          17,
                          ":"
                      )
                  )
                 .IsEqualTo("11:22:33:44:55:66:77:88::99:AA:BB:CC:DD:EE:FF:11::22");

            Check.That(
                      HexFormatter.ToUpper(
                          new Byte[] {
                              0x11,
                              0x22,
                              0x33,
                              0x44,
                              0x55,
                              0x66,
                              0x77,
                              0x88,
                              0x99,
                              0xAA,
                              0xBB,
                              0xCC,
                              0xDD,
                              0xEE,
                              0xFF,
                              0x11,
                              0x22,
                              0x33,
                          },
                          0,
                          17,
                          String.Empty
                      )
                  )
                 .IsEqualTo("112233445566778899AABBCCDDEEFF1122");

            Check.That(
                      HexFormatter.ToUpper(
                          new Byte[] {
                              0x11,
                              0x22,
                              0x33,
                              0x44,
                              0x55,
                              0x66,
                              0x77,
                              0x88,
                              0x99,
                              0xAA,
                              0xBB,
                              0xCC,
                              0xDD,
                              0xEE,
                              0xFF,
                              0x11,
                              0x22,
                              0x33,
                          },
                          7,
                          10,
                          "-"
                      )
                  )
                 .IsEqualTo("88-99-AA-BB-CC-DD-EE-FF--11-22");
        }
    }
}
