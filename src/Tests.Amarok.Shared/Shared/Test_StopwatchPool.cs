// Copyright (c) 2024, Olaf Kober <olaf.kober@outlook.com>

using System;
using System.Diagnostics;
using System.Threading.Tasks;
using NFluent;
using NUnit.Framework;


namespace Amarok.Shared;


[TestFixture]
public class Test_StopwatchPool
{
    [Test]
    public void Rent_Free_SingleItem()
    {
        var sw = StopwatchPool.Rent();

        Check.That(sw).IsNotNull();

        Check.That(sw.IsRunning).IsFalse();

        sw.Start();

        Check.That(sw.IsRunning).IsTrue();

        StopwatchPool.Free(sw);

        Check.That(sw.IsRunning).IsFalse();
    }

    [Test]
    public void Rent_Free_SingleItem_MultipleTimes()
    {
        var sw1 = StopwatchPool.Rent();

        Check.That(sw1.IsRunning).IsFalse();

        sw1.Start();

        Check.That(sw1.IsRunning).IsTrue();

        StopwatchPool.Free(sw1);

        Check.That(sw1.IsRunning).IsFalse();

        var sw2 = StopwatchPool.Rent();

        Check.That(sw2.IsRunning).IsFalse();

        Check.That(sw2).IsSameReferenceAs(sw1);
    }

    [Test]
    public void Free_Ignores_Null()
    {
        Check.ThatCode(() => StopwatchPool.Free(null)).DoesNotThrow();
    }

    [Test]
    public void Free_Resets_Stopwatch()
    {
        var sw = StopwatchPool.Rent();
        sw.Start();

        Check.That(sw.IsRunning).IsTrue();

        StopwatchPool.Free(sw);

        Check.That(sw.IsRunning).IsFalse();
    }

    [Test]
    public void Stress()
    {
        const Int32 count = StopwatchPool.MaxNumberOfItems;

        var objs = new Stopwatch[count * 2];

        for (var i = 0; i < 100; i++)
        {
            Parallel.For(0, count, x => objs[x] = StopwatchPool.Rent());
            Parallel.For(0, count, x => StopwatchPool.Free(objs[x]));
        }
    }
}
