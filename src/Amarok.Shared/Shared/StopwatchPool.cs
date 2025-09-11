// Copyright (c) 2024, Olaf Kober <olaf.kober@outlook.com>

using System;
using System.Diagnostics;


namespace Amarok.Shared;


/// <summary>
///     This static type provides a pool of <see cref="Stopwatch"/> instances.
/// </summary>
public static class StopwatchPool
{
    // constants
    internal const Int32 MaxNumberOfItems = 64;

    // static data
    private static readonly ObjectPool<Stopwatch> sPool = new(() => new Stopwatch(), MaxNumberOfItems);


    /// <summary>
    ///     Returns a <see cref="Stopwatch"/> either from the pool or a newly created instance. The
    ///     returned instance is not automatically started.
    /// </summary>
    public static Stopwatch Rent()
    {
        return sPool.Allocate();
    }

    /// <summary>
    ///     Returns the specified <see cref="Stopwatch"/> to the pool. The <see cref="Stopwatch"/> instance
    ///     is reset as part of this.
    /// </summary>
    /// 
    /// <param name="watch">
    ///     The instance to return. Null values are accepted and ignored.
    /// </param>
    public static void Free(Stopwatch? watch)
    {
        if (watch == null)
        {
            return;
        }

        watch.Reset();

        sPool.Free(watch);
    }
}
