/* Copyright(c) 2019, Olaf Kober
 * Licensed under GNU Lesser General Public License v3.0 (LPGL-3.0).
 * https://github.com/Amarok79/Amarok.Shared
 */

using System;
using System.Globalization;
using System.Text;


namespace Amarok.Shared
{
	internal static class HexFormatter
	{
		private static readonly String[] sLower = new String[Byte.MaxValue + 1];
		private static readonly String[] sUpper = new String[Byte.MaxValue + 1];


		static HexFormatter()
		{
			for (Int32 i = 0; i <= Byte.MaxValue; i++)
			{
				sLower[i] = i.ToString("x2", CultureInfo.InvariantCulture);
				sUpper[i] = i.ToString("X2", CultureInfo.InvariantCulture);
			}
		}


		public static String ToLower(Byte value)
		{
			return sLower[value];
		}

		public static String ToUpper(Byte value)
		{
			return sUpper[value];
		}


		public static String ToLower(Byte[] buffer, Int32 offset, Int32 count, String delimiter)
		{
			return _Format(sLower, buffer, offset, count, delimiter);
		}

		public static String ToUpper(Byte[] buffer, Int32 offset, Int32 count, String delimiter)
		{
			return _Format(sUpper, buffer, offset, count, delimiter);
		}


		private static String _Format(String[] strings, Byte[] buffer, Int32 offset, Int32 count, String delimiter)
		{
			StringBuilder sb = null;
			try
			{
				sb = StringBuilderPool.Allocate();

				for (Int32 i = offset; i < offset + count; i++)
				{
					if (sb.Length > 0)
						sb.Append(delimiter);
					if (i > offset && (i - offset) % 8 == 0)
						sb.Append(delimiter);

					sb.Append(strings[buffer[i]]);
				}

				return sb.ToString();
			}
			finally
			{
				StringBuilderPool.Free(sb);
			}
		}
	}
}
