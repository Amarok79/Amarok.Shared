/* Copyright(c) 2019, Olaf Kober
 * Licensed under GNU Lesser General Public License v3.0 (LPGL-3.0).
 * https://github.com/Amarok79/Amarok.Shared
 */

using System;
using System.Text;
using System.Threading.Tasks;
using NCrunch.Framework;
using NFluent;
using NUnit.Framework;


namespace Amarok.Shared
{
	[TestFixture, Serial, Isolated]
	public class Test_StringBuilderPool
	{
		[Test]
		public void Allocate_Free_SingleItem()
		{
			var sb = StringBuilderPool.Allocate();

			Check.That(sb)
				.IsNotNull();
			Check.That(sb.Capacity)
				.IsEqualTo(StringBuilderPool.__initialCapacity);
			Check.That(sb.Length)
				.IsEqualTo(0);

			sb.Append("abc");

			Check.That(sb.Capacity)
				.IsEqualTo(StringBuilderPool.__initialCapacity);
			Check.That(sb.Length)
				.IsEqualTo(3);

			StringBuilderPool.Free(sb);

			Check.That(sb.Capacity)
				.IsEqualTo(StringBuilderPool.__initialCapacity);
			Check.That(sb.Length)
				.IsEqualTo(0);
		}

		[Test]
		public void Allocate_Free_SingleItem_MultipleTimes()
		{
			var sb1 = StringBuilderPool.Allocate();

			Check.That(sb1.Capacity)
				.IsEqualTo(StringBuilderPool.__initialCapacity);
			Check.That(sb1.Length)
				.IsEqualTo(0);

			sb1.Append("abc");

			Check.That(sb1.Capacity)
				.IsEqualTo(StringBuilderPool.__initialCapacity);
			Check.That(sb1.Length)
				.IsEqualTo(3);

			StringBuilderPool.Free(sb1);

			Check.That(sb1.Capacity)
				.IsEqualTo(StringBuilderPool.__initialCapacity);
			Check.That(sb1.Length)
				.IsEqualTo(0);

			var sb2 = StringBuilderPool.Allocate();

			Check.That(sb2.Capacity)
				.IsEqualTo(StringBuilderPool.__initialCapacity);
			Check.That(sb2.Length)
				.IsEqualTo(0);

			Check.That(sb2)
				.IsSameReferenceAs(sb1);
		}

		[Test]
		public void Free_Ignores_Null()
		{
			Check.ThatCode(() => StringBuilderPool.Free(null))
				.DoesNotThrow();
		}

		[Test]
		public void Free_Clears_StringBuilder()
		{
			var sb = StringBuilderPool.Allocate();
			sb.Append("abc");

			Check.That(sb.Capacity)
				.IsEqualTo(StringBuilderPool.__initialCapacity);
			Check.That(sb.Length)
				.IsEqualTo(3);

			StringBuilderPool.Free(sb);

			Check.That(sb.Capacity)
				.IsEqualTo(StringBuilderPool.__initialCapacity);
			Check.That(sb.Length)
				.IsEqualTo(0);
		}

		[Test]
		public void Free_Shrinks_StringBuilder()
		{
			var sb = StringBuilderPool.Allocate();
			sb.Append(new String('A', 50 * 1024));

			Check.That(sb.Capacity)
				.IsEqualTo(50 * 1024);
			Check.That(sb.Length)
				.IsEqualTo(50 * 1024);

			StringBuilderPool.Free(sb);

			Check.That(sb.Capacity)
				.IsEqualTo(StringBuilderPool.__maxCapacity);
			Check.That(sb.Length)
				.IsEqualTo(0);
		}

		[Test]
		public void Stress()
		{
			const Int32 __count = StringBuilderPool.__maxNumberOfItems;

			var objs = new StringBuilder[__count * 2];
			for (Int32 i = 0; i < 100; i++)
			{
				Parallel.For(0, __count, x => objs[x] = StringBuilderPool.Allocate());
				Parallel.For(0, __count, x => StringBuilderPool.Free(objs[x]));
			}
		}
	}
}
