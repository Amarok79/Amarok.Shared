// Copyright (c) 2024, Olaf Kober <olaf.kober@outlook.com>

using System;
using NFluent;
using NUnit.Framework;


namespace Amarok.Shared;


[TestFixture]
public class Test_ByteArrayExtensions
{
    [Test]
    public void Test_ToHex_Buffer()
    {
        Check.That(new Byte[] { 0x11, 0xFF, 0xB3 }.ToHex()).IsEqualTo("11-FF-B3");
    }

    [Test]
    public void Test_ToLowerHex_Buffer()
    {
        Check.That(new Byte[] { 0x11, 0xFF, 0xB3 }.ToLowerHex()).IsEqualTo("11-ff-b3");
    }

    [Test]
    public void Test_ToUpperHex_Buffer()
    {
        Check.That(new Byte[] { 0x11, 0xFF, 0xB3 }.ToUpperHex()).IsEqualTo("11-FF-B3");
    }


    [Test]
    public void Test_ToHex_Buffer_Delimiter()
    {
        Check.That(new Byte[] { 0x11, 0xFF, 0xB3 }.ToHex(":")).IsEqualTo("11:FF:B3");
    }

    [Test]
    public void Test_ToLowerHex_Buffer_Delimiter()
    {
        Check.That(new Byte[] { 0x11, 0xFF, 0xB3 }.ToLowerHex(":")).IsEqualTo("11:ff:b3");
    }

    [Test]
    public void Test_ToUpperHex_Buffer_Delimiter()
    {
        Check.That(new Byte[] { 0x11, 0xFF, 0xB3 }.ToUpperHex(":")).IsEqualTo("11:FF:B3");
    }


    [Test]
    public void Test_ToHex_BufferOffsetCount()
    {
        Check.That(new Byte[] { 0x11, 0xFF, 0xB3 }.ToHex(1, 2)).IsEqualTo("FF-B3");
    }

    [Test]
    public void Test_ToLowerHex_BufferOffsetCount()
    {
        Check.That(new Byte[] { 0x11, 0xFF, 0xB3 }.ToLowerHex(1, 2)).IsEqualTo("ff-b3");
    }

    [Test]
    public void Test_ToUpperHex_BufferOffsetCount()
    {
        Check.That(new Byte[] { 0x11, 0xFF, 0xB3 }.ToUpperHex(1, 2)).IsEqualTo("FF-B3");
    }


    [Test]
    public void Test_ToHex_BufferOffsetCount_Delimiter()
    {
        Check.That(new Byte[] { 0x11, 0xFF, 0xB3 }.ToHex(1, 2, ":")).IsEqualTo("FF:B3");
    }

    [Test]
    public void Test_ToLowerHex_BufferOffsetCount_Delimiter()
    {
        Check.That(new Byte[] { 0x11, 0xFF, 0xB3 }.ToLowerHex(1, 2, ":")).IsEqualTo("ff:b3");
    }

    [Test]
    public void Test_ToUpperHex_BufferOffsetCount_Delimiter()
    {
        Check.That(new Byte[] { 0x11, 0xFF, 0xB3 }.ToUpperHex(1, 2, ":")).IsEqualTo("FF:B3");
    }
}
