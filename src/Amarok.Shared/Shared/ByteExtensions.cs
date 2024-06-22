// Copyright (c) 2024, Olaf Kober <olaf.kober@outlook.com>

using System;


namespace Amarok.Shared;


/// <summary>
///     This type provides extension methods for <see cref="byte"/>.
/// </summary>
public static class ByteExtensions
{
    /// <summary>
    ///     Returns the hexadecimal representation (upper-case) of the given value.
    /// </summary>
    /// 
    /// <param name="value">
    ///     The byte value to format.
    /// </param>
    /// 
    /// <returns>
    ///     A string representation of the given value, for example, "A7" or "F0".
    /// </returns>
    public static String ToHex(this Byte value)
    {
        return HexFormatter.ToUpper(value);
    }


    /// <summary>
    ///     Returns the lower-case hexadecimal representation of the given value.
    /// </summary>
    /// 
    /// <param name="value">
    ///     The byte value to format.
    /// </param>
    /// 
    /// <returns>
    ///     A string representation of the given value, for example, "a7" or "f0".
    /// </returns>
    public static String ToLowerHex(this Byte value)
    {
        return HexFormatter.ToLower(value);
    }

    /// <summary>
    ///     Returns the upper-case hexadecimal representation of the given value.
    /// </summary>
    /// 
    /// <param name="value">
    ///     The byte value to format.
    /// </param>
    /// 
    /// <returns>
    ///     A string representation of the given value, for example, "A7" or "F0".
    /// </returns>
    public static String ToUpperHex(this Byte value)
    {
        return HexFormatter.ToUpper(value);
    }
}
