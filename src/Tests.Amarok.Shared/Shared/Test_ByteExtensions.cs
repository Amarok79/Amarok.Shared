/* Copyright(c) 2019, Olaf Kober
 * Licensed under GNU Lesser General Public License v3.0 (LPGL-3.0).
 * https://github.com/Amarok79/Amarok.Shared
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
			Check.That(((Byte)0x00).ToHex())
				.IsEqualTo("00");
			Check.That(((Byte)0x0F).ToHex())
				.IsEqualTo("0F");
			Check.That(((Byte)0xF0).ToHex())
				.IsEqualTo("F0");
			Check.That(((Byte)0xFF).ToHex())
				.IsEqualTo("FF");
		}

		[Test]
		public void Test_ToLowerHex()
		{
			Check.That(((Byte)0x00).ToLowerHex())
				.IsEqualTo("00");
			Check.That(((Byte)0x0F).ToLowerHex())
				.IsEqualTo("0f");
			Check.That(((Byte)0xF0).ToLowerHex())
				.IsEqualTo("f0");
			Check.That(((Byte)0xFF).ToLowerHex())
				.IsEqualTo("ff");
		}

		[Test]
		public void Test_ToUpperHex()
		{
			Check.That(((Byte)0x00).ToUpperHex())
				.IsEqualTo("00");
			Check.That(((Byte)0x0F).ToUpperHex())
				.IsEqualTo("0F");
			Check.That(((Byte)0xF0).ToUpperHex())
				.IsEqualTo("F0");
			Check.That(((Byte)0xFF).ToUpperHex())
				.IsEqualTo("FF");
		}
	}
}
