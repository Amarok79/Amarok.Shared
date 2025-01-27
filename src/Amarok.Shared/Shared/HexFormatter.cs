﻿// Copyright (c) 2024, Olaf Kober <olaf.kober@outlook.com>

using System;
using System.Globalization;
using System.Text;


namespace Amarok.Shared;


internal static class HexFormatter
{
    // static data
    private static readonly String[] sLower = new String[Byte.MaxValue + 1];
    private static readonly String[] sUpper = new String[Byte.MaxValue + 1];


    static HexFormatter()
    {
        for (var i = 0; i <= Byte.MaxValue; i++)
        {
            sLower[i] = i.ToString("x2", CultureInfo.InvariantCulture);
            sUpper[i] = i.ToString("X2", CultureInfo.InvariantCulture);
        }
    }


    internal static String ToLower(Byte value)
    {
        return sLower[value];
    }

    internal static String ToLower(Byte[] buffer, Int32 offset, Int32 count, String delimiter)
    {
        return _Format(sLower, buffer, offset, count, delimiter);
    }


    internal static String ToUpper(Byte value)
    {
        return sUpper[value];
    }

    internal static String ToUpper(Byte[] buffer, Int32 offset, Int32 count, String delimiter)
    {
        return _Format(sUpper, buffer, offset, count, delimiter);
    }


    private static String _Format(String[] strings, Byte[] buffer, Int32 offset, Int32 count, String delimiter)
    {
        StringBuilder? sb = null;

        try
        {
            sb = StringBuilderPool.Rent();

            for (var i = offset; i < offset + count; i++)
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
