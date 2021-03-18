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


namespace Amarok.Shared
{
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
        ///     Returns a <see cref="Stopwatch"/> either from the pool or a newly created instance. The returned instance is not
        ///     automatically started.
        /// </summary>
        public static Stopwatch Rent()
        {
            return sPool.Allocate();
        }

        /// <summary>
        ///     Returns the specified <see cref="Stopwatch"/> to the pool. The <see cref="Stopwatch"/> instance is reset as part of
        ///     this.
        /// </summary>
        /// 
        /// <param name="watch">
        ///     The instance to return. Null values are accepted and ignored.
        /// </param>
        public static void Free(Stopwatch? watch)
        {
            if (watch == null)
                return;

            watch.Reset();

            sPool.Free(watch);
        }
    }
}
