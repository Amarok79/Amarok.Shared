﻿/* Copyright(c) 2019, Olaf Kober
 * Licensed under GNU Lesser General Public License v3.0 (LPGL-3.0).
 * https://github.com/Amarok79/Amarok.Shared
 */

using System;
using System.Text;


namespace Amarok.Shared
{
	/// <summary>
	/// This static type provides a pool of <see cref="StringBuilder"/> instances.
	/// </summary>
	public static class StringBuilderPool
	{
		// constants
		internal const Int32 __initialCapacity = 512;
		internal const Int32 __maxCapacity = 40 * 1024;
		internal const Int32 __maxNumberOfItems = 64;

		// static data
		private static readonly ObjectPool<StringBuilder> sPool = new ObjectPool<StringBuilder>(
			() => new StringBuilder(__initialCapacity),
			__maxNumberOfItems
		);


		/// <summary>
		/// Returns a <see cref="StringBuilder"/> either from the pool or a newly created instance.
		/// </summary>
		public static StringBuilder Allocate()
		{
			return sPool.Allocate();
		}

		/// <summary>
		/// Returns the specified <see cref="StringBuilder"/> to the pool.
		/// 
		/// The <see cref="StringBuilder"/> instance is cleared as part of this.
		/// </summary>
		/// 
		/// <param name="builder">
		/// The instance to return. Null values are accepted and ignored.</param>
		public static void Free(StringBuilder builder)
		{
			if (builder == null)
				return;

			builder.Clear();

			if (builder.Capacity > __maxCapacity)
				builder.Capacity = __maxCapacity;

			sPool.Free(builder);
		}
	}
}