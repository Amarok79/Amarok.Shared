// Copyright (c) 2024, Olaf Kober <olaf.kober@outlook.com>

#pragma warning disable CA1825  // Avoid zero-length array allocations
#pragma warning disable IDE0230 // Use UTF-8 string literal

using System;
using NFluent;
using NUnit.Framework;


namespace Amarok.Shared;


public class Test_BufferSpan
{
    [TestFixture]
    public class Default
    {
        [Test]
        public void Construction()
        {
            var span = new BufferSpan();

            Check.That(span.Buffer).IsSameReferenceAs(Array.Empty<Byte>());

            Check.That(span.Buffer.Length).IsEqualTo(0);

            Check.That(span.BufferLength).IsEqualTo(0);

            Check.That(span.FreeBytes).IsEqualTo(0);

            Check.That(span.Offset).IsEqualTo(0);

            Check.That(span.Count).IsEqualTo(0);

            Check.That(span.IsEmpty).IsTrue();

            Check.That(span.ToArray()).IsSameReferenceAs(Array.Empty<Byte>());

            Check.That(span.ToString()).IsEqualTo("<empty>");
        }
    }

    [TestFixture]
    public class Empty
    {
        [Test]
        public void Construction()
        {
            var span = BufferSpan.Empty;

            Check.That(span.Buffer).IsSameReferenceAs(Array.Empty<Byte>());

            Check.That(span.Buffer.Length).IsEqualTo(0);

            Check.That(span.BufferLength).IsEqualTo(0);

            Check.That(span.FreeBytes).IsEqualTo(0);

            Check.That(span.Offset).IsEqualTo(0);

            Check.That(span.Count).IsEqualTo(0);

            Check.That(span.IsEmpty).IsTrue();

            Check.That(span.ToArray()).IsSameReferenceAs(Array.Empty<Byte>());

            Check.That(span.ToString()).IsEqualTo("<empty>");
        }
    }

    [TestFixture]
    public class From_ByteArray
    {
        [Test]
        public void Construction()
        {
            var span = BufferSpan.From(0x11, 0x22, 0x33);

            Check.That(span.Buffer).ContainsExactly(0x11, 0x22, 0x33);

            Check.That(span.BufferLength).IsEqualTo(3);

            Check.That(span.FreeBytes).IsEqualTo(0);

            Check.That(span.Offset).IsEqualTo(0);

            Check.That(span.Count).IsEqualTo(3);

            Check.That(span.IsEmpty).IsFalse();

            Check.That(span.ToArray()).ContainsExactly(0x11, 0x22, 0x33).And.Not.IsSameReferenceAs(span.Buffer);

            Check.That(span.ToString()).IsEqualTo("11-22-33");
        }

        [Test]
        public void Construction_With_EmptyArray()
        {
            var span = BufferSpan.From();

            Check.That(span.Buffer).IsEmpty();

            Check.That(span.BufferLength).IsEqualTo(0);

            Check.That(span.FreeBytes).IsEqualTo(0);

            Check.That(span.Offset).IsEqualTo(0);

            Check.That(span.Count).IsEqualTo(0);

            Check.That(span.IsEmpty).IsTrue();

            Check.That(span.ToArray()).IsEmpty();

            Check.That(span.ToString()).IsEqualTo("<empty>");
        }

        [Test]
        public void Exception_When_BufferIsNull()
        {
            Check.ThatCode(() => BufferSpan.From(null))
                .Throws<ArgumentNullException>()
                .WithProperty(x => x.ParamName, "buffer");
        }
    }

    [TestFixture]
    public class From_ByteArray_Count
    {
        [Test]
        public void Construction_With_LargerArray()
        {
            var buffer = new Byte[] { 0x11, 0x22, 0x33, 0x44, 0x55 };
            var span   = BufferSpan.From(buffer, 3);

            Check.That(span.Buffer).IsSameReferenceAs(buffer);

            Check.That(span.BufferLength).IsEqualTo(5);

            Check.That(span.FreeBytes).IsEqualTo(2);

            Check.That(span.Offset).IsEqualTo(0);

            Check.That(span.Count).IsEqualTo(3);

            Check.That(span.IsEmpty).IsFalse();

            Check.That(span.ToArray()).ContainsExactly(0x11, 0x22, 0x33).And.Not.IsSameReferenceAs(span.Buffer);

            Check.That(span.ToString()).IsEqualTo("11-22-33");
        }

        [Test]
        public void Construction_With_SameSizeArray()
        {
            var buffer = new Byte[] { 0x11, 0x22, 0x33 };
            var span   = BufferSpan.From(buffer, 3);

            Check.That(span.Buffer).IsSameReferenceAs(buffer);

            Check.That(span.BufferLength).IsEqualTo(3);

            Check.That(span.FreeBytes).IsEqualTo(0);

            Check.That(span.Offset).IsEqualTo(0);

            Check.That(span.Count).IsEqualTo(3);

            Check.That(span.IsEmpty).IsFalse();

            Check.That(span.ToArray()).ContainsExactly(0x11, 0x22, 0x33).And.Not.IsSameReferenceAs(span.Buffer);

            Check.That(span.ToString()).IsEqualTo("11-22-33");
        }

        [Test]
        public void Construction_With_EmptyArray()
        {
            var buffer = new Byte[] { };
            var span   = BufferSpan.From(buffer, 0);

            Check.That(span.Buffer).IsSameReferenceAs(buffer);

            Check.That(span.BufferLength).IsEqualTo(0);

            Check.That(span.FreeBytes).IsEqualTo(0);

            Check.That(span.Offset).IsEqualTo(0);

            Check.That(span.Count).IsEqualTo(0);

            Check.That(span.IsEmpty).IsTrue();

            Check.That(span.ToArray()).IsEmpty().And.Not.IsSameReferenceAs(span.Buffer);

            Check.That(span.ToString()).IsEqualTo("<empty>");
        }

        [Test]
        public void Exception_When_BufferIsNull()
        {
            Check.ThatCode(() => BufferSpan.From(null, 3))
                .Throws<ArgumentNullException>()
                .WithProperty(x => x.ParamName, "array");
        }

        [Test]
        public void Exception_When_CountIsNegative()
        {
            var buffer = new Byte[] { 0x11, 0x22, 0x33, 0x44, 0x55 };

            Check.ThatCode(() => BufferSpan.From(buffer, -1))
                .Throws<ArgumentOutOfRangeException>()
                .WithProperty(x => x.ParamName, "count");
        }

        [Test]
        public void Exception_When_CountGreaterThanArrayLength()
        {
            var buffer = new Byte[] { 0x11, 0x22, 0x33 };

            Check.ThatCode(() => BufferSpan.From(buffer, 4))
                .Throws<ArgumentOutOfRangeException>()
                .WithProperty(x => x.ParamName, "count");
        }
    }

    [TestFixture]
    public class From_ByteArray_Offset_Count
    {
        [Test]
        public void Construction_With_LargerArray()
        {
            var buffer = new Byte[] { 0x00, 0x11, 0x22, 0x33, 0x44, 0x55 };
            var span   = BufferSpan.From(buffer, 1, 3);

            Check.That(span.Buffer).IsSameReferenceAs(buffer);

            Check.That(span.BufferLength).IsEqualTo(6);

            Check.That(span.FreeBytes).IsEqualTo(2);

            Check.That(span.Offset).IsEqualTo(1);

            Check.That(span.Count).IsEqualTo(3);

            Check.That(span.IsEmpty).IsFalse();

            Check.That(span.ToArray()).ContainsExactly(0x11, 0x22, 0x33).And.Not.IsSameReferenceAs(span.Buffer);

            Check.That(span.ToString()).IsEqualTo("11-22-33");
        }

        [Test]
        public void Construction_With_SameSizeArray()
        {
            var buffer = new Byte[] { 0x11, 0x22, 0x33 };
            var span   = BufferSpan.From(buffer, 0, 3);

            Check.That(span.Buffer).IsSameReferenceAs(buffer);

            Check.That(span.BufferLength).IsEqualTo(3);

            Check.That(span.FreeBytes).IsEqualTo(0);

            Check.That(span.Offset).IsEqualTo(0);

            Check.That(span.Count).IsEqualTo(3);

            Check.That(span.IsEmpty).IsFalse();

            Check.That(span.ToArray()).ContainsExactly(0x11, 0x22, 0x33).And.Not.IsSameReferenceAs(span.Buffer);

            Check.That(span.ToString()).IsEqualTo("11-22-33");
        }

        [Test]
        public void Construction_With_EmptyArray()
        {
            var buffer = new Byte[] { };
            var span   = BufferSpan.From(buffer, 0, 0);

            Check.That(span.Buffer).IsSameReferenceAs(buffer);

            Check.That(span.BufferLength).IsEqualTo(0);

            Check.That(span.FreeBytes).IsEqualTo(0);

            Check.That(span.Offset).IsEqualTo(0);

            Check.That(span.Count).IsEqualTo(0);

            Check.That(span.IsEmpty).IsTrue();

            Check.That(span.ToArray()).IsEmpty().And.Not.IsSameReferenceAs(span.Buffer);

            Check.That(span.ToString()).IsEqualTo("<empty>");
        }

        [Test]
        public void Exception_When_BufferIsNull()
        {
            Check.ThatCode(() => BufferSpan.From(null, 1, 3))
                .Throws<ArgumentNullException>()
                .WithProperty(x => x.ParamName, "array");
        }

        [Test]
        public void Exception_When_OffsetIsNegative()
        {
            var buffer = new Byte[] { 0x11, 0x22, 0x33, 0x44, 0x55 };

            Check.ThatCode(() => BufferSpan.From(buffer, -1, 3))
                .Throws<ArgumentOutOfRangeException>()
                .WithProperty(x => x.ParamName, "offset");
        }

        [Test]
        public void Exception_When_CountIsNegative()
        {
            var buffer = new Byte[] { 0x11, 0x22, 0x33, 0x44, 0x55 };

            Check.ThatCode(() => BufferSpan.From(buffer, 0, -1))
                .Throws<ArgumentOutOfRangeException>()
                .WithProperty(x => x.ParamName, "count");
        }

        [Test]
        public void Exception_When_OffsetGreaterThanArrayLength()
        {
            var buffer = new Byte[] { 0x11, 0x22, 0x33 };

            Check.ThatCode(() => BufferSpan.From(buffer, 3, 0))
                .Throws<ArgumentOutOfRangeException>()
                .WithProperty(x => x.ParamName, "offset");
        }

        [Test]
        public void Exception_When_CountGreatherThanArrayLength()
        {
            var buffer = new Byte[] { 0x11, 0x22, 0x33 };

            Check.ThatCode(() => BufferSpan.From(buffer, 0, 4))
                .Throws<ArgumentOutOfRangeException>()
                .WithProperty(x => x.ParamName, "count");
        }

        [Test]
        public void Exception_When_OffsetAndCountGreatherThanArrayLength()
        {
            var buffer = new Byte[] { 0x11, 0x22, 0x33 };

            Check.ThatCode(() => BufferSpan.From(buffer, 1, 3))
                .Throws<ArgumentOutOfRangeException>()
                .WithProperty(x => x.ParamName, "count");
        }
    }

    [TestFixture]
    public class Indexer
    {
        [Test]
        public void Usage()
        {
            var buffer = new Byte[] { 0x00, 0x11, 0x22, 0x33, 0x44, 0x55 };
            var span   = BufferSpan.From(buffer, 1, 3);

            Check.ThatCode(() => span[-1]).Throws<ArgumentOutOfRangeException>();

            Check.That(span[0]).IsEqualTo(0x11);

            Check.That(span[1]).IsEqualTo(0x22);

            Check.That(span[2]).IsEqualTo(0x33);

            Check.ThatCode(() => span[3]).Throws<ArgumentOutOfRangeException>();
        }

        [Test]
        public void Usage_EmptyArray()
        {
            var span = BufferSpan.Empty;

            Check.ThatCode(() => span[-1]).Throws<ArgumentOutOfRangeException>();

            Check.ThatCode(() => span[0]).Throws<ArgumentOutOfRangeException>();
        }
    }

    [TestFixture]
    public class Clear
    {
        [Test]
        public void Usage()
        {
            var buffer = new Byte[] { 0x00, 0x11, 0x22, 0x33, 0x44, 0x55 };
            var span   = BufferSpan.From(buffer, 1, 3);

            var span2 = span.Clear();

            Check.That(span2.Buffer).IsSameReferenceAs(buffer);

            Check.That(span2.BufferLength).IsEqualTo(6);

            Check.That(span2.Offset).IsEqualTo(0);

            Check.That(span2.Count).IsEqualTo(0);

            Check.That(span2.IsEmpty).IsTrue();

            Check.That(span2.ToArray()).IsEmpty().And.Not.IsSameReferenceAs(span.Buffer);

            Check.That(span2.ToString()).IsEqualTo("<empty>");
        }

        [Test]
        public void Usage_With_EmptySpan()
        {
            var span  = BufferSpan.Empty;
            var span2 = span.Clear();

            Check.That(span2.Buffer).IsSameReferenceAs(span.Buffer);

            Check.That(span2.BufferLength).IsEqualTo(0);

            Check.That(span2.Offset).IsEqualTo(0);

            Check.That(span2.Count).IsEqualTo(0);

            Check.That(span2.IsEmpty).IsTrue();

            Check.That(span2.ToArray()).IsEmpty();

            Check.That(span2.ToString()).IsEqualTo("<empty>");
        }
    }

    [TestFixture]
    public class Append
    {
        [Test]
        public void Append_EmptySpan()
        {
            var buffer = new Byte[] {
                0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77,
                0x88, 0x99,
            };

            var span   = BufferSpan.From(buffer, 1, 3);
            var data   = BufferSpan.Empty;
            var result = span.Append(data);

            Check.That(result.Buffer).IsSameReferenceAs(buffer);

            Check.That(result.BufferLength).IsEqualTo(10);

            Check.That(result.FreeBytes).IsEqualTo(6);

            Check.That(result.Offset).IsEqualTo(1);

            Check.That(result.Count).IsEqualTo(3);

            Check.That(result.IsEmpty).IsFalse();

            Check.That(result.ToArray()).ContainsExactly(0x11, 0x22, 0x33).And.Not.IsSameReferenceAs(span.Buffer);

            Check.That(result.ToString()).IsEqualTo("11-22-33");
        }

        [Test]
        public void Append_ToEndOfExistingBuffer()
        {
            var buffer = new Byte[] {
                0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77,
                0x88, 0x99,
            };

            var span   = BufferSpan.From(buffer, 1, 3);
            var data   = BufferSpan.From(0xDD, 0xEE);
            var result = span.Append(data);

            Check.That(result.Buffer).IsSameReferenceAs(buffer);

            Check.That(result.BufferLength).IsEqualTo(10);

            Check.That(result.FreeBytes).IsEqualTo(4);

            Check.That(result.Offset).IsEqualTo(1);

            Check.That(result.Count).IsEqualTo(5);

            Check.That(result.IsEmpty).IsFalse();

            Check.That(result.ToArray())
                .ContainsExactly(0x11, 0x22, 0x33, 0xDD, 0xEE)
                .And.Not.IsSameReferenceAs(span.Buffer);

            Check.That(result.ToString()).IsEqualTo("11-22-33-DD-EE");
        }

        [Test]
        public void Append_ToEndOfExistingBuffer_DataWithOffset()
        {
            var buffer = new Byte[] {
                0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77,
                0x88, 0x99,
            };

            var span   = BufferSpan.From(buffer, 1, 3);
            var data   = BufferSpan.From(new Byte[] { 0xAA, 0xDD, 0xEE, 0xBB, 0xCC }, 1, 2);
            var result = span.Append(data);

            Check.That(result.Buffer).IsSameReferenceAs(buffer);

            Check.That(result.BufferLength).IsEqualTo(10);

            Check.That(result.FreeBytes).IsEqualTo(4);

            Check.That(result.Offset).IsEqualTo(1);

            Check.That(result.Count).IsEqualTo(5);

            Check.That(result.IsEmpty).IsFalse();

            Check.That(result.ToArray())
                .ContainsExactly(0x11, 0x22, 0x33, 0xDD, 0xEE)
                .And.Not.IsSameReferenceAs(span.Buffer);

            Check.That(result.ToString()).IsEqualTo("11-22-33-DD-EE");
        }

        [Test]
        public void Append_ToEndOfExistingBuffer_ExactByteArrayLength()
        {
            var buffer = new Byte[] { 0x00, 0x11, 0x22, 0x33, 0x44, 0x55 };
            var span   = BufferSpan.From(buffer, 1, 3);
            var data   = BufferSpan.From(0xDD, 0xEE);
            var result = span.Append(data);

            Check.That(result.Buffer).IsSameReferenceAs(buffer);

            Check.That(result.BufferLength).IsEqualTo(6);

            Check.That(result.FreeBytes).IsEqualTo(0);

            Check.That(result.Offset).IsEqualTo(1);

            Check.That(result.Count).IsEqualTo(5);

            Check.That(result.IsEmpty).IsFalse();

            Check.That(result.ToArray())
                .ContainsExactly(0x11, 0x22, 0x33, 0xDD, 0xEE)
                .And.Not.IsSameReferenceAs(span.Buffer);

            Check.That(result.ToString()).IsEqualTo("11-22-33-DD-EE");
        }

        [Test]
        public void Append_ToEndOfExistingBuffer_ExactByteArrayLength_DataWithOffset()
        {
            var buffer = new Byte[] { 0x00, 0x11, 0x22, 0x33, 0x44, 0x55 };
            var span   = BufferSpan.From(buffer, 1, 3);
            var data   = BufferSpan.From(new Byte[] { 0xAA, 0xDD, 0xEE, 0xBB, 0xCC }, 1, 2);
            var result = span.Append(data);

            Check.That(result.Buffer).IsSameReferenceAs(buffer);

            Check.That(result.BufferLength).IsEqualTo(6);

            Check.That(result.FreeBytes).IsEqualTo(0);

            Check.That(result.Offset).IsEqualTo(1);

            Check.That(result.Count).IsEqualTo(5);

            Check.That(result.IsEmpty).IsFalse();

            Check.That(result.ToArray())
                .ContainsExactly(0x11, 0x22, 0x33, 0xDD, 0xEE)
                .And.Not.IsSameReferenceAs(span.Buffer);

            Check.That(result.ToString()).IsEqualTo("11-22-33-DD-EE");
        }

        [Test]
        public void Append_ToEndOfExistingBufferConsolidating()
        {
            var buffer = new Byte[] {
                0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77,
                0x88, 0x99,
            };

            var span   = BufferSpan.From(buffer, 6, 3);
            var data   = BufferSpan.From(0xDD, 0xEE);
            var result = span.Append(data);

            Check.That(result.Buffer).IsSameReferenceAs(buffer);

            Check.That(result.BufferLength).IsEqualTo(10);

            Check.That(result.FreeBytes).IsEqualTo(5);

            Check.That(result.Offset).IsEqualTo(0);

            Check.That(result.Count).IsEqualTo(5);

            Check.That(result.IsEmpty).IsFalse();

            Check.That(result.ToArray())
                .ContainsExactly(0x66, 0x77, 0x88, 0xDD, 0xEE)
                .And.Not.IsSameReferenceAs(span.Buffer);

            Check.That(result.ToString()).IsEqualTo("66-77-88-DD-EE");
        }

        [Test]
        public void Append_ToEndOfExistingBufferConsolidating_DataWithOffset()
        {
            var buffer = new Byte[] {
                0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77,
                0x88, 0x99,
            };

            var span   = BufferSpan.From(buffer, 6, 3);
            var data   = BufferSpan.From(new Byte[] { 0xAA, 0xDD, 0xEE, 0xBB, 0xCC }, 1, 2);
            var result = span.Append(data);

            Check.That(result.Buffer).IsSameReferenceAs(buffer);

            Check.That(result.BufferLength).IsEqualTo(10);

            Check.That(result.FreeBytes).IsEqualTo(5);

            Check.That(result.Offset).IsEqualTo(0);

            Check.That(result.Count).IsEqualTo(5);

            Check.That(result.IsEmpty).IsFalse();

            Check.That(result.ToArray())
                .ContainsExactly(0x66, 0x77, 0x88, 0xDD, 0xEE)
                .And.Not.IsSameReferenceAs(span.Buffer);

            Check.That(result.ToString()).IsEqualTo("66-77-88-DD-EE");
        }

        [Test]
        public void Append_ToEndOfExistingBufferConsolidating_OverlappingBlockCopy()
        {
            var buffer = new Byte[] { 0x00, 0x11, 0x22, 0x33, 0x44, 0x55 };
            var span   = BufferSpan.From(buffer, 2, 3);
            var data   = BufferSpan.From(0xDD, 0xEE);
            var result = span.Append(data);

            Check.That(result.Buffer).IsSameReferenceAs(buffer);

            Check.That(result.BufferLength).IsEqualTo(6);

            Check.That(result.FreeBytes).IsEqualTo(1);

            Check.That(result.Offset).IsEqualTo(0);

            Check.That(result.Count).IsEqualTo(5);

            Check.That(result.IsEmpty).IsFalse();

            Check.That(result.ToArray())
                .ContainsExactly(0x22, 0x33, 0x44, 0xDD, 0xEE)
                .And.Not.IsSameReferenceAs(span.Buffer);

            Check.That(result.ToString()).IsEqualTo("22-33-44-DD-EE");
        }

        [Test]
        public void Append_ToEndOfExistingBufferConsolidating_ExactByteArrayLength()
        {
            var buffer = new Byte[] { 0x00, 0x11, 0x22, 0x33, 0x44 };
            var span   = BufferSpan.From(buffer, 2, 3);
            var data   = BufferSpan.From(0xDD, 0xEE);
            var result = span.Append(data);

            Check.That(result.Buffer).IsSameReferenceAs(buffer);

            Check.That(result.BufferLength).IsEqualTo(5);

            Check.That(result.FreeBytes).IsEqualTo(0);

            Check.That(result.Offset).IsEqualTo(0);

            Check.That(result.Count).IsEqualTo(5);

            Check.That(result.IsEmpty).IsFalse();

            Check.That(result.ToArray())
                .ContainsExactly(0x22, 0x33, 0x44, 0xDD, 0xEE)
                .And.Not.IsSameReferenceAs(span.Buffer);

            Check.That(result.ToString()).IsEqualTo("22-33-44-DD-EE");
        }

        [Test]
        public void Append_ToEndOfExistingBufferConsolidating_ExactByteArrayLength_DataWithOffset()
        {
            var buffer = new Byte[] { 0x00, 0x11, 0x22, 0x33, 0x44 };
            var span   = BufferSpan.From(buffer, 2, 3);
            var data   = BufferSpan.From(new Byte[] { 0xAA, 0xDD, 0xEE, 0xBB, 0xCC }, 1, 2);
            var result = span.Append(data);

            Check.That(result.Buffer).IsSameReferenceAs(buffer);

            Check.That(result.BufferLength).IsEqualTo(5);

            Check.That(result.FreeBytes).IsEqualTo(0);

            Check.That(result.Offset).IsEqualTo(0);

            Check.That(result.Count).IsEqualTo(5);

            Check.That(result.IsEmpty).IsFalse();

            Check.That(result.ToArray())
                .ContainsExactly(0x22, 0x33, 0x44, 0xDD, 0xEE)
                .And.Not.IsSameReferenceAs(span.Buffer);

            Check.That(result.ToString()).IsEqualTo("22-33-44-DD-EE");
        }

        [Test]
        public void Append_IntoNewBuffer()
        {
            var buffer = new Byte[] { 0x00, 0x11, 0x22, 0x33, 0x44, 0x55 };
            var span   = BufferSpan.From(buffer, 2, 3);

            var data = BufferSpan.From(0xAA, 0xBB, 0xCC, 0xDD, 0xEE);

            var result = span.Append(data);

            Check.That(result.Buffer).Not.IsSameReferenceAs(buffer);

            Check.That(result.BufferLength).IsEqualTo(8);

            Check.That(result.FreeBytes).IsEqualTo(0);

            Check.That(result.Offset).IsEqualTo(0);

            Check.That(result.Count).IsEqualTo(8);

            Check.That(result.IsEmpty).IsFalse();

            Check.That(result.ToArray())
                .ContainsExactly(0x22, 0x33, 0x44, 0xAA, 0xBB, 0xCC, 0xDD, 0xEE)
                .And.Not.IsSameReferenceAs(span.Buffer);

            Check.That(result.ToString()).IsEqualTo("22-33-44-AA-BB-CC-DD-EE");
        }

        [Test]
        public void Append_IntoNewBuffer_DataWithOffset()
        {
            var buffer = new Byte[] { 0x00, 0x11, 0x22, 0x33, 0x44, 0x55 };
            var span   = BufferSpan.From(buffer, 2, 3);

            var data = BufferSpan.From(new Byte[] { 0x12, 0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0x21 }, 1, 5);

            var result = span.Append(data);

            Check.That(result.Buffer).Not.IsSameReferenceAs(buffer);

            Check.That(result.BufferLength).IsEqualTo(8);

            Check.That(result.FreeBytes).IsEqualTo(0);

            Check.That(result.Offset).IsEqualTo(0);

            Check.That(result.Count).IsEqualTo(8);

            Check.That(result.IsEmpty).IsFalse();

            Check.That(result.ToArray())
                .ContainsExactly(0x22, 0x33, 0x44, 0xAA, 0xBB, 0xCC, 0xDD, 0xEE)
                .And.Not.IsSameReferenceAs(span.Buffer);

            Check.That(result.ToString()).IsEqualTo("22-33-44-AA-BB-CC-DD-EE");
        }

        [Test]
        public void Append_ToEmptySpan()
        {
            var span = BufferSpan.Empty;

            var data = BufferSpan.From(0xAA, 0xBB, 0xCC, 0xDD, 0xEE);

            var result = span.Append(data);

            Check.That(result.Buffer).Not.IsSameReferenceAs(span.Buffer);

            Check.That(result.BufferLength).IsEqualTo(5);

            Check.That(result.FreeBytes).IsEqualTo(0);

            Check.That(result.Offset).IsEqualTo(0);

            Check.That(result.Count).IsEqualTo(5);

            Check.That(result.IsEmpty).IsFalse();

            Check.That(result.ToArray()).ContainsExactly(0xAA, 0xBB, 0xCC, 0xDD, 0xEE);

            Check.That(result.ToString()).IsEqualTo("AA-BB-CC-DD-EE");
        }
    }

    [TestFixture]
    public class Discard
    {
        [Test]
        public void Discard_OneByteFromExistingBuffer()
        {
            var buffer = new Byte[] { 0x00, 0x11, 0x22, 0x33, 0x44, 0x55 };
            var span   = BufferSpan.From(buffer, 1, 3);
            var result = span.Discard(1);

            Check.That(result.Buffer).IsSameReferenceAs(buffer);

            Check.That(result.BufferLength).IsEqualTo(6);

            Check.That(result.FreeBytes).IsEqualTo(2);

            Check.That(result.Offset).IsEqualTo(2);

            Check.That(result.Count).IsEqualTo(2);

            Check.That(result.IsEmpty).IsFalse();

            Check.That(result.ToArray()).ContainsExactly(0x22, 0x33).And.Not.IsSameReferenceAs(span.Buffer);

            Check.That(result.ToString()).IsEqualTo("22-33");
        }

        [Test]
        public void Discard_ZeroBytesFromExistingBuffer()
        {
            var buffer = new Byte[] { 0x00, 0x11, 0x22, 0x33, 0x44, 0x55 };
            var span   = BufferSpan.From(buffer, 1, 3);
            var result = span.Discard(0);

            Check.That(result.Buffer).IsSameReferenceAs(buffer);

            Check.That(result.BufferLength).IsEqualTo(6);

            Check.That(result.FreeBytes).IsEqualTo(2);

            Check.That(result.Offset).IsEqualTo(1);

            Check.That(result.Count).IsEqualTo(3);

            Check.That(result.IsEmpty).IsFalse();

            Check.That(result.ToArray()).ContainsExactly(0x11, 0x22, 0x33).And.Not.IsSameReferenceAs(span.Buffer);

            Check.That(result.ToString()).IsEqualTo("11-22-33");
        }

        [Test]
        public void Discard_AllBytesFromExistingBuffer()
        {
            var buffer = new Byte[] { 0x00, 0x11, 0x22, 0x33, 0x44, 0x55 };
            var span   = BufferSpan.From(buffer, 1, 3);
            var result = span.Discard(3);

            Check.That(result.Buffer).IsSameReferenceAs(buffer);

            Check.That(result.BufferLength).IsEqualTo(6);

            Check.That(result.FreeBytes).IsEqualTo(6);

            Check.That(result.Offset).IsEqualTo(0);

            Check.That(result.Count).IsEqualTo(0);

            Check.That(result.IsEmpty).IsTrue();

            Check.That(result.ToArray()).IsEmpty();

            Check.That(result.ToString()).IsEqualTo("<empty>");
        }

        [Test]
        public void Exception_When_BytesIsNegative()
        {
            var buffer = new Byte[] { 0x00, 0x11, 0x22, 0x33, 0x44, 0x55 };
            var span   = BufferSpan.From(buffer, 1, 3);

            Check.ThatCode(() => span.Discard(-1)).Throws<ArgumentOutOfRangeException>();
        }

        [Test]
        public void Exception_When_BytesGreaterThanCount()
        {
            var buffer = new Byte[] { 0x00, 0x11, 0x22, 0x33, 0x44, 0x55 };
            var span   = BufferSpan.From(buffer, 1, 3);

            Check.ThatCode(() => span.Discard(4)).Throws<ArgumentOutOfRangeException>();
        }
    }

    [TestFixture]
    public class Slice
    {
        [Test]
        public void Slice_OneBytesFromExistingData()
        {
            var buffer = new Byte[] { 0x00, 0x11, 0x22, 0x33, 0x44, 0x55 };
            var span   = BufferSpan.From(buffer, 1, 4);
            var result = span.Slice(3, 1);

            Check.That(result.Buffer).IsSameReferenceAs(buffer);

            Check.That(result.BufferLength).IsEqualTo(6);

            Check.That(result.FreeBytes).IsEqualTo(1);

            Check.That(result.Offset).IsEqualTo(4);

            Check.That(result.Count).IsEqualTo(1);

            Check.That(result.IsEmpty).IsFalse();

            Check.That(result.ToArray()).ContainsExactly(0x44).And.Not.IsSameReferenceAs(span.Buffer);

            Check.That(result.ToString()).IsEqualTo("44");
        }

        [Test]
        public void Slice_TwoBytesFromExistingData()
        {
            var buffer = new Byte[] { 0x00, 0x11, 0x22, 0x33, 0x44, 0x55 };
            var span   = BufferSpan.From(buffer, 1, 4);
            var result = span.Slice(1, 2);

            Check.That(result.Buffer).IsSameReferenceAs(buffer);

            Check.That(result.BufferLength).IsEqualTo(6);

            Check.That(result.FreeBytes).IsEqualTo(2);

            Check.That(result.Offset).IsEqualTo(2);

            Check.That(result.Count).IsEqualTo(2);

            Check.That(result.IsEmpty).IsFalse();

            Check.That(result.ToArray()).ContainsExactly(0x22, 0x33).And.Not.IsSameReferenceAs(span.Buffer);

            Check.That(result.ToString()).IsEqualTo("22-33");
        }

        [Test]
        public void Slice_AllBytesFromExistingData()
        {
            var buffer = new Byte[] { 0x00, 0x11, 0x22, 0x33, 0x44, 0x55 };
            var span   = BufferSpan.From(buffer, 1, 4);
            var result = span.Slice(0, 4);

            Check.That(result.Buffer).IsSameReferenceAs(buffer);

            Check.That(result.BufferLength).IsEqualTo(6);

            Check.That(result.FreeBytes).IsEqualTo(1);

            Check.That(result.Offset).IsEqualTo(1);

            Check.That(result.Count).IsEqualTo(4);

            Check.That(result.IsEmpty).IsFalse();

            Check.That(result.ToArray()).ContainsExactly(0x11, 0x22, 0x33, 0x44).And.Not.IsSameReferenceAs(span.Buffer);

            Check.That(result.ToString()).IsEqualTo("11-22-33-44");
        }

        [Test]
        public void Exception_When_IndexIsNegative()
        {
            var buffer = new Byte[] { 0x00, 0x11, 0x22, 0x33, 0x44, 0x55 };
            var span   = BufferSpan.From(buffer, 1, 4);

            Check.ThatCode(() => span.Slice(-1, 2)).Throws<ArgumentOutOfRangeException>();
        }

        [Test]
        public void Exception_When_IndexIsGreaterThanCount()
        {
            var buffer = new Byte[] { 0x00, 0x11, 0x22, 0x33, 0x44, 0x55 };
            var span   = BufferSpan.From(buffer, 1, 4);

            Check.ThatCode(() => span.Slice(5, 0)).Throws<ArgumentOutOfRangeException>();
        }

        [Test]
        public void Exception_When_CountIsNegative()
        {
            var buffer = new Byte[] { 0x00, 0x11, 0x22, 0x33, 0x44, 0x55 };
            var span   = BufferSpan.From(buffer, 1, 4);

            Check.ThatCode(() => span.Slice(0, -1)).Throws<ArgumentOutOfRangeException>();
        }

        [Test]
        public void Exception_When_CountIsGreaterThanCount()
        {
            var buffer = new Byte[] { 0x00, 0x11, 0x22, 0x33, 0x44, 0x55 };
            var span   = BufferSpan.From(buffer, 1, 4);

            Check.ThatCode(() => span.Slice(0, 5)).Throws<ArgumentOutOfRangeException>();
        }

        [Test]
        public void Exception_When_OffsetAndCountIsGreaterThanCount()
        {
            var buffer = new Byte[] { 0x00, 0x11, 0x22, 0x33, 0x44, 0x55 };
            var span   = BufferSpan.From(buffer, 1, 4);

            Check.ThatCode(() => span.Slice(2, 3)).Throws<ArgumentOutOfRangeException>();
        }
    }

    [TestFixture]
    public class Clone
    {
        [Test]
        public void Empty()
        {
            var org = new BufferSpan();

            var span = org.Clone();

            Check.That(span.IsEmpty).IsTrue();
        }

        [Test]
        public void ByteSpan()
        {
            var org = BufferSpan.From(0x11, 0x22, 0x33, 0x44, 0x55);

            org = org.Slice(1, 3);

            var span = org.Clone();

            Check.That(span.Buffer).Not.IsSameReferenceAs(org.Buffer);

            Check.That(span.BufferLength).IsEqualTo(3);

            Check.That(span.Offset).IsEqualTo(0);

            Check.That(span.Count).IsEqualTo(3);

            Check.That(span.Buffer).ContainsExactly(0x22, 0x33, 0x44);
        }
    }

    [TestFixture]
    public class TestToString
    {
        [Test]
        public void Empty()
        {
            var span = BufferSpan.Empty;

            Check.That(span.ToString()).IsEqualTo("<empty>");
        }

        [Test]
        public void SingleByte()
        {
            var span = BufferSpan.From(0x11);

            Check.That(span.ToString()).IsEqualTo("11");
        }

        [Test]
        public void SevenBytes()
        {
            var span = BufferSpan.From(0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77);

            Check.That(span.ToString()).IsEqualTo("11-22-33-44-55-66-77");
        }

        [Test]
        public void EightBytes()
        {
            var span = BufferSpan.From(0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88);

            Check.That(span.ToString()).IsEqualTo("11-22-33-44-55-66-77-88");
        }

        [Test]
        public void NineBytes()
        {
            var span = BufferSpan.From(0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99);

            Check.That(span.ToString()).IsEqualTo("11-22-33-44-55-66-77-88--99");
        }

        [Test]
        public void SixteenBytes()
        {
            var span = BufferSpan.From(
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
                0x11
            );

            Check.That(span.ToString()).IsEqualTo("11-22-33-44-55-66-77-88--99-AA-BB-CC-DD-EE-FF-11");
        }

        [Test]
        public void SeventeenBytes()
        {
            var span = BufferSpan.From(
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
                0x22
            );

            Check.That(span.ToString()).IsEqualTo("11-22-33-44-55-66-77-88--99-AA-BB-CC-DD-EE-FF-11--22");
        }
    }

    [TestFixture]
    public class TestToString_Delimiter
    {
        [Test]
        public void Empty()
        {
            var span = BufferSpan.Empty;

            Check.That(span.ToString(":")).IsEqualTo("<empty>");
        }

        [Test]
        public void SingleByte()
        {
            var span = BufferSpan.From(0x11);

            Check.That(span.ToString(":")).IsEqualTo("11");
        }

        [Test]
        public void SevenBytes()
        {
            var span = BufferSpan.From(0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77);

            Check.That(span.ToString(":")).IsEqualTo("11:22:33:44:55:66:77");
        }

        [Test]
        public void EightBytes()
        {
            var span = BufferSpan.From(0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88);

            Check.That(span.ToString(":")).IsEqualTo("11:22:33:44:55:66:77:88");
        }

        [Test]
        public void NineBytes()
        {
            var span = BufferSpan.From(0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99);

            Check.That(span.ToString(":")).IsEqualTo("11:22:33:44:55:66:77:88::99");
        }

        [Test]
        public void SixteenBytes()
        {
            var span = BufferSpan.From(
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
                0x11
            );

            Check.That(span.ToString(String.Empty)).IsEqualTo("112233445566778899AABBCCDDEEFF11");
        }

        [Test]
        public void SeventeenBytes()
        {
            var span = BufferSpan.From(
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
                0x22
            );

            Check.That(span.ToString(" ")).IsEqualTo("11 22 33 44 55 66 77 88  99 AA BB CC DD EE FF 11  22");
        }
    }
}
