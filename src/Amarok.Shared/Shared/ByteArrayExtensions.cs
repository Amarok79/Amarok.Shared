﻿/* Copyright(c) 2019, Olaf Kober
 * Licensed under GNU Lesser General Public License v3.0 (LPGL-3.0).
 * https://github.com/Amarok79/Amarok.Shared
 */

#pragma warning disable S4144 // Methods should not have identical implementations

using System;
using Amarok.Contracts;


namespace Amarok.Shared
{
	/// <summary>
	/// This type provides extension methods for <see cref="Byte"/> arrays.
	/// </summary>
	public static class ByteArrayExtensions
	{
		// constants
		private const String __defaultDelimiter = "-";


		/// <summary>
		/// Returns the hexadecimal representation (upper-case) of the given byte array.
		/// </summary>
		/// 
		/// <param name="buffer">
		/// The byte array to format.</param>
		/// 
		/// <returns>
		/// A string representation of the given byte array, for example, "11-FF-B7".</returns>
		/// 
		/// <exception cref="ArgumentNullException">
		/// Null values are invalid.</exception>
		public static String ToHex(this Byte[] buffer)
		{
			Verify.NotNull(buffer, nameof(buffer));
			return HexFormatter.ToUpper(buffer, 0, buffer.Length, __defaultDelimiter);
		}

		/// <summary>
		/// Returns the lower-case hexadecimal representation of the given byte array.
		/// </summary>
		/// 
		/// <param name="buffer">
		/// The byte array to format.</param>
		/// 
		/// <returns>
		/// A string representation of the given byte array, for example, "11-ff-b7".</returns>
		/// 
		/// <exception cref="ArgumentNullException">
		/// Null values are invalid.</exception>
		public static String ToLowerHex(this Byte[] buffer)
		{
			Verify.NotNull(buffer, nameof(buffer));
			return HexFormatter.ToLower(buffer, 0, buffer.Length, __defaultDelimiter);
		}

		/// <summary>
		/// Returns the upper-case hexadecimal representation of the given byte array.
		/// </summary>
		/// 
		/// <param name="buffer">
		/// The byte array to format.</param>
		/// 
		/// <returns>
		/// A string representation of the given byte array, for example, "11-FF-B7".</returns>
		/// 
		/// <exception cref="ArgumentNullException">
		/// Null values are invalid.</exception>
		public static String ToUpperHex(this Byte[] buffer)
		{
			Verify.NotNull(buffer, nameof(buffer));
			return HexFormatter.ToUpper(buffer, 0, buffer.Length, __defaultDelimiter);
		}


		/// <summary>
		/// Returns the hexadecimal representation (upper-case) of the given byte array.
		/// </summary>
		/// 
		/// <param name="buffer">
		/// The byte array to format.</param>
		/// <param name="delimiter">
		/// The delimiter to be used.</param>
		/// 
		/// <returns>
		/// A string representation of the given byte array.</returns>
		/// 
		/// <exception cref="ArgumentNullException">
		/// Null values are invalid.</exception>
		public static String ToHex(this Byte[] buffer, String delimiter)
		{
			Verify.NotNull(buffer, nameof(buffer));
			Verify.NotNull(delimiter, nameof(delimiter));
			return HexFormatter.ToUpper(buffer, 0, buffer.Length, delimiter);
		}

		/// <summary>
		/// Returns the lower-case hexadecimal representation of the given byte array.
		/// </summary>
		/// 
		/// <param name="buffer">
		/// The byte array to format.</param>
		/// <param name="delimiter">
		/// The delimiter to be used.</param>
		/// 
		/// <returns>
		/// A string representation of the given byte array..</returns>
		/// 
		/// <exception cref="ArgumentNullException">
		/// Null values are invalid.</exception>
		public static String ToLowerHex(this Byte[] buffer, String delimiter)
		{
			Verify.NotNull(buffer, nameof(buffer));
			Verify.NotNull(delimiter, nameof(delimiter));
			return HexFormatter.ToLower(buffer, 0, buffer.Length, delimiter);
		}

		/// <summary>
		/// Returns the upper-case hexadecimal representation of the given byte array.
		/// </summary>
		/// 
		/// <param name="buffer">
		/// The byte array to format.</param>
		/// <param name="delimiter">
		/// The delimiter to be used.</param>
		/// 
		/// <returns>
		/// A string representation of the given byte array.</returns>
		/// 
		/// <exception cref="ArgumentNullException">
		/// Null values are invalid.</exception>
		public static String ToUpperHex(this Byte[] buffer, String delimiter)
		{
			Verify.NotNull(buffer, nameof(buffer));
			Verify.NotNull(delimiter, nameof(delimiter));
			return HexFormatter.ToUpper(buffer, 0, buffer.Length, delimiter);
		}


		/// <summary>
		/// Returns the hexadecimal representation (upper-case) of the given byte array.
		/// </summary>
		/// 
		/// <param name="buffer">
		/// The byte array to format.</param>
		/// <param name="offset">
		/// The byte offset in buffer where the data begin.</param>
		/// <param name="count">
		/// The number of bytes.</param>
		/// 
		/// <returns>
		/// A string representation of the given byte array, for example, "11-22-33".</returns>
		/// 
		/// <exception cref="ArgumentNullException">
		/// Null values are invalid.</exception>
		/// <exception cref="ArgumentOutOfRangeException">
		/// Negative values are invalid.</exception>
		/// <exception cref="ArgumentExceedsUpperLimitException">
		/// Values exceeding the inclusive upper limit are invalid.</exception>
		public static String ToHex(this Byte[] buffer, Int32 offset, Int32 count)
		{
			Verify.ArraySegment(buffer, offset, count);
			return HexFormatter.ToUpper(buffer, offset, count, __defaultDelimiter);
		}

		/// <summary>
		/// Returns the lower-case hexadecimal representation of the given byte array.
		/// </summary>
		/// 
		/// <param name="buffer">
		/// The byte array to format.</param>
		/// <param name="offset">
		/// The byte offset in buffer where the data begin.</param>
		/// <param name="count">
		/// The number of bytes.</param>
		/// 
		/// <returns>
		/// A string representation of the given byte array, for example, "11-22-33".</returns>
		/// 
		/// <exception cref="ArgumentNullException">
		/// Null values are invalid.</exception>
		/// <exception cref="ArgumentOutOfRangeException">
		/// Negative values are invalid.</exception>
		/// <exception cref="ArgumentExceedsUpperLimitException">
		/// Values exceeding the inclusive upper limit are invalid.</exception>
		public static String ToLowerHex(this Byte[] buffer, Int32 offset, Int32 count)
		{
			Verify.ArraySegment(buffer, offset, count);
			return HexFormatter.ToLower(buffer, offset, count, __defaultDelimiter);
		}

		/// <summary>
		/// Returns the upper-case hexadecimal representation of the given byte array.
		/// </summary>
		/// 
		/// <param name="buffer">
		/// The byte array to format.</param>
		/// <param name="offset">
		/// The byte offset in buffer where the data begin.</param>
		/// <param name="count">
		/// The number of bytes.</param>
		/// 
		/// <returns>
		/// A string representation of the given byte array, for example, "11-22-33".</returns>
		/// 
		/// <exception cref="ArgumentNullException">
		/// Null values are invalid.</exception>
		/// <exception cref="ArgumentOutOfRangeException">
		/// Negative values are invalid.</exception>
		/// <exception cref="ArgumentExceedsUpperLimitException">
		/// Values exceeding the inclusive upper limit are invalid.</exception>
		public static String ToUpperHex(this Byte[] buffer, Int32 offset, Int32 count)
		{
			Verify.ArraySegment(buffer, offset, count);
			return HexFormatter.ToUpper(buffer, offset, count, __defaultDelimiter);
		}


		/// <summary>
		/// Returns the hexadecimal representation (upper-case) of the given byte array.
		/// </summary>
		/// 
		/// <param name="buffer">
		/// The byte array to format.</param>
		/// <param name="offset">
		/// The byte offset in buffer where the data begin.</param>
		/// <param name="count">
		/// The number of bytes.</param>
		/// <param name="delimiter">
		/// The delimiter to be used.</param>
		/// 
		/// <returns>
		/// A string representation of the given byte array.</returns>
		/// 
		/// <exception cref="ArgumentNullException">
		/// Null values are invalid.</exception>
		/// <exception cref="ArgumentOutOfRangeException">
		/// Negative values are invalid.</exception>
		/// <exception cref="ArgumentExceedsUpperLimitException">
		/// Values exceeding the inclusive upper limit are invalid.</exception>
		public static String ToHex(this Byte[] buffer, Int32 offset, Int32 count, String delimiter)
		{
			Verify.ArraySegment(buffer, offset, count);
			return HexFormatter.ToUpper(buffer, offset, count, delimiter);
		}

		/// <summary>
		/// Returns the lower-case hexadecimal representation of the given byte array.
		/// </summary>
		/// 
		/// <param name="buffer">
		/// The byte array to format.</param>
		/// <param name="offset">
		/// The byte offset in buffer where the data begin.</param>
		/// <param name="count">
		/// The number of bytes.</param>
		/// <param name="delimiter">
		/// The delimiter to be used.</param>
		/// 
		/// <returns>
		/// A string representation of the given byte array.</returns>
		/// 
		/// <exception cref="ArgumentNullException">
		/// Null values are invalid.</exception>
		/// <exception cref="ArgumentOutOfRangeException">
		/// Negative values are invalid.</exception>
		/// <exception cref="ArgumentExceedsUpperLimitException">
		/// Values exceeding the inclusive upper limit are invalid.</exception>
		public static String ToLowerHex(this Byte[] buffer, Int32 offset, Int32 count, String delimiter)
		{
			Verify.ArraySegment(buffer, offset, count);
			return HexFormatter.ToLower(buffer, offset, count, delimiter);
		}

		/// <summary>
		/// Returns the upper-case hexadecimal representation of the given byte array.
		/// </summary>
		/// 
		/// <param name="buffer">
		/// The byte array to format.</param>
		/// <param name="offset">
		/// The byte offset in buffer where the data begin.</param>
		/// <param name="count">
		/// The number of bytes.</param>
		/// <param name="delimiter">
		/// The delimiter to be used.</param>
		/// 
		/// <returns>
		/// A string representation of the given byte array.</returns>
		/// 
		/// <exception cref="ArgumentNullException">
		/// Null values are invalid.</exception>
		/// <exception cref="ArgumentOutOfRangeException">
		/// Negative values are invalid.</exception>
		/// <exception cref="ArgumentExceedsUpperLimitException">
		/// Values exceeding the inclusive upper limit are invalid.</exception>
		public static String ToUpperHex(this Byte[] buffer, Int32 offset, Int32 count, String delimiter)
		{
			Verify.ArraySegment(buffer, offset, count);
			return HexFormatter.ToUpper(buffer, offset, count, delimiter);
		}
	}
}
