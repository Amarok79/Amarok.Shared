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
		private static readonly String[] sStrings = new String[Byte.MaxValue + 1];


		static HexFormatter()
		{
			for (Int32 i = 0; i <= Byte.MaxValue; i++)
				sStrings[i] = i.ToString("X2", CultureInfo.InvariantCulture);
		}


		public static String Format(Byte value)
		{
			return sStrings[value];
		}

		public static String Format(Byte[] buffer, Int32 offset, Int32 count, String delimiter)
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

					sb.Append(sStrings[buffer[i]]);
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
