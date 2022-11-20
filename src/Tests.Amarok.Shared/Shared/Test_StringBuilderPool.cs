// Copyright (c) 2022, Olaf Kober <olaf.kober@outlook.com>

using System;
using System.Text;
using System.Threading.Tasks;
using NFluent;
using NUnit.Framework;


namespace Amarok.Shared;


[TestFixture]
public class Test_StringBuilderPool
{
    [Test]
    public void Rent_Free_SingleItem()
    {
        var sb = StringBuilderPool.Rent();

        Check.That(sb).IsNotNull();

        //Check.That(sb.Capacity)
        //	.IsEqualTo(StringBuilderPool.InitialCapacity);
        Check.That(sb.Length).IsEqualTo(0);

        sb.Append("abc");

        //Check.That(sb.Capacity)
        //	.IsEqualTo(StringBuilderPool.InitialCapacity);
        Check.That(sb.Length).IsEqualTo(3);

        StringBuilderPool.Free(sb);

        //Check.That(sb.Capacity)
        //	.IsEqualTo(StringBuilderPool.InitialCapacity);
        Check.That(sb.Length).IsEqualTo(0);
    }

    [Test]
    public void Rent_Free_SingleItem_MultipleTimes()
    {
        var sb1 = StringBuilderPool.Rent();

        //Check.That(sb1.Capacity)
        //	.IsEqualTo(StringBuilderPool.InitialCapacity);
        Check.That(sb1.Length).IsEqualTo(0);

        sb1.Append("abc");

        //Check.That(sb1.Capacity)
        //	.IsEqualTo(StringBuilderPool.InitialCapacity);
        Check.That(sb1.Length).IsEqualTo(3);

        StringBuilderPool.Free(sb1);

        //Check.That(sb1.Capacity)
        //	.IsEqualTo(StringBuilderPool.InitialCapacity);
        Check.That(sb1.Length).IsEqualTo(0);

        var sb2 = StringBuilderPool.Rent();

        //Check.That(sb2.Capacity)
        //	.IsEqualTo(StringBuilderPool.InitialCapacity);
        Check.That(sb2.Length).IsEqualTo(0);

        Check.That(sb2).IsSameReferenceAs(sb1);
    }

    [Test]
    public void Free_Ignores_Null()
    {
        Check.ThatCode(() => StringBuilderPool.Free(null)).DoesNotThrow();
    }

    [Test]
    public void Free_Clears_StringBuilder()
    {
        var sb = StringBuilderPool.Rent();
        sb.Append("abc");

        Check.That(sb.Capacity).IsEqualTo(StringBuilderPool.InitialCapacity);

        Check.That(sb.Length).IsEqualTo(3);

        StringBuilderPool.Free(sb);

        Check.That(sb.Capacity).IsEqualTo(StringBuilderPool.InitialCapacity);

        Check.That(sb.Length).IsEqualTo(0);
    }

    [Test]
    public void Free_Shrinks_StringBuilder()
    {
        var sb = StringBuilderPool.Rent();
        sb.Append(new String('A', 50 * 1024));

        Check.That(sb.Capacity).IsEqualTo(50 * 1024);

        Check.That(sb.Length).IsEqualTo(50 * 1024);

        StringBuilderPool.Free(sb);

        Check.That(sb.Capacity).IsEqualTo(StringBuilderPool.MaxCapacity);

        Check.That(sb.Length).IsEqualTo(0);
    }

    [Test]
    public void Stress()
    {
        const Int32 count = StringBuilderPool.MaxNumberOfItems;

        var objs = new StringBuilder[count * 2];

        for (var i = 0; i < 100; i++)
        {
            Parallel.For(0, count, x => objs[x] = StringBuilderPool.Rent());
            Parallel.For(0, count, x => StringBuilderPool.Free(objs[x]));
        }
    }
}
