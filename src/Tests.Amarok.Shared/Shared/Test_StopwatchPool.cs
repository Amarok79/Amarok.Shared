/* Copyright(c) 2019, Olaf Kober
 * Licensed under GNU Lesser General Public License v3.0 (LPGL-3.0).
 * https://github.com/Amarok79/Amarok.Shared
 */

using NCrunch.Framework;
using NFluent;
using NUnit.Framework;


namespace Amarok.Shared
{
	[TestFixture, Serial]
	public class Test_StopwatchPool
	{
		[Test]
		public void Allocate_Free_SingleItem()
		{
			var sw = StopwatchPool.Allocate();

			Check.That(sw)
				.IsNotNull();
			Check.That(sw.IsRunning)
				.IsFalse();

			sw.Start();

			Check.That(sw.IsRunning)
				.IsTrue();

			StopwatchPool.Free(sw);

			Check.That(sw.IsRunning)
				.IsFalse();
		}

		[Test]
		public void Allocate_Free_SingleItem_MultipleTimes()
		{
			var sw1 = StopwatchPool.Allocate();

			Check.That(sw1.IsRunning)
				.IsFalse();

			sw1.Start();

			Check.That(sw1.IsRunning)
				.IsTrue();

			StopwatchPool.Free(sw1);

			Check.That(sw1.IsRunning)
				.IsFalse();

			var sw2 = StopwatchPool.Allocate();

			Check.That(sw2.IsRunning)
				.IsFalse();

			Check.That(sw2)
				.IsSameReferenceAs(sw1);
		}

		[Test]
		public void Free_Ignores_Null()
		{
			Check.ThatCode(() => StopwatchPool.Free(null))
				.DoesNotThrow();
		}

		[Test]
		public void Free_Resets_Stopwatch()
		{
			var sw = StopwatchPool.Allocate();
			sw.Start();

			Check.That(sw.IsRunning)
				.IsTrue();

			StopwatchPool.Free(sw);

			Check.That(sw.IsRunning)
				.IsFalse();
		}
	}
}
