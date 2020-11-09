/* MIT License
 * 
 * Copyright (c) 2020, Olaf Kober
 * https://github.com/Amarok79/Amarok.Shared
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
*/

using System;
using System.Diagnostics;
using System.Threading.Tasks;
using NCrunch.Framework;
using NFluent;
using NUnit.Framework;


namespace Amarok.Shared
{
    [TestFixture, Serial, Isolated]
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
}
