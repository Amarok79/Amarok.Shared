// Copyright (c) 2024, Olaf Kober <olaf.kober@outlook.com>

using System;
using System.Text;


namespace Amarok.Shared;


/// <summary>
///     This static type provides a pool of <see cref="StringBuilder"/> instances.
/// </summary>
public static class StringBuilderPool
{
    // constants
    internal const Int32 InitialCapacity = 512;
    internal const Int32 MaxCapacity = 40 * 1024;
    internal const Int32 MaxNumberOfItems = 64;

    // static data
    private static readonly ObjectPool<StringBuilder> sPool = new(
        () => new StringBuilder(InitialCapacity),
        MaxNumberOfItems
    );


    /// <summary>
    ///     Returns a <see cref="StringBuilder"/> either from the pool or a newly created instance.
    /// </summary>
    public static StringBuilder Rent()
    {
        return sPool.Allocate();
    }

    /// <summary>
    ///     Returns the specified <see cref="StringBuilder"/> to the pool. The <see cref="StringBuilder"/>
    ///     instance is cleared as part of this.
    /// </summary>
    /// 
    /// <param name="builder">
    ///     The instance to return. Null values are accepted and ignored.
    /// </param>
    public static void Free(StringBuilder? builder)
    {
        if (builder == null)
        {
            return;
        }

        builder.Clear();

        if (builder.Capacity > MaxCapacity)
        {
            builder.Capacity = MaxCapacity;
        }

        sPool.Free(builder);
    }
}
