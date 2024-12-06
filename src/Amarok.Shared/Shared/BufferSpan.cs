// Copyright (c) 2024, Olaf Kober <olaf.kober@outlook.com>

using System;
using Amarok.Contracts;


namespace Amarok.Shared;


/// <summary>
///     This type represents a span of bytes, specified by offset and count, in an underlying byte array.
/// </summary>
public readonly struct BufferSpan
{
    // data
    private readonly Byte[] mBuffer;
    private readonly Int32 mOffset;
    private readonly Int32 mCount;


    /// <summary>
    ///     Returns an empty buffer span.
    /// </summary>
    public static BufferSpan Empty { get; } = new([ ], 0, 0);


    /// <summary>
    ///     Returns a new span for the given byte array.
    /// </summary>
    public static BufferSpan From(params Byte[] buffer)
    {
        Verify.NotNull(buffer, nameof(buffer));

        return new BufferSpan(buffer, 0, buffer.Length);
    }

    /// <summary>
    ///     Returns a new span for the given byte array segment.
    /// </summary>
    public static BufferSpan From(Byte[] buffer, Int32 count)
    {
        Verify.ArraySegment(buffer, count);

        return new BufferSpan(buffer, 0, count);
    }

    /// <summary>
    ///     Returns a new span for the given byte array segment.
    /// </summary>
    public static BufferSpan From(Byte[] buffer, Int32 offset, Int32 count)
    {
        Verify.ArraySegment(buffer, offset, count);

        return new BufferSpan(buffer, offset, count);
    }


    /// <summary>
    ///     Gets the underlying byte array containing data.
    /// </summary>
    public Byte[] Buffer => mBuffer ?? [ ];

    /// <summary>
    ///     Gets the length of the underlying byte array.
    /// </summary>
    internal Int32 BufferLength => mBuffer?.Length ?? 0;

    /// <summary>
    ///     Gets the number of free bytes at the end of the underlying byte array.
    /// </summary>
    internal Int32 FreeBytes => BufferLength - mCount - mOffset;

    /// <summary>
    ///     Gets the offset in the underlying buffer where data starts.
    /// </summary>
    public Int32 Offset => mOffset;

    /// <summary>
    ///     Gets the length of data in number of bytes.
    /// </summary>
    public Int32 Count => mCount;

    /// <summary>
    ///     Gets a boolean value indicating whether the buffer span is empty.
    /// </summary>
    public Boolean IsEmpty => Buffer == null || Count == 0;


    /// <summary>
    ///     Gets the data byte at the specified index.
    /// </summary>
    public Byte this[Int32 index]
    {
        get
        {
            _VerifyIndex(index);

            return mBuffer[mOffset + index];
        }
    }


    private BufferSpan(Byte[] buffer, Int32 offset, Int32 count)
    {
        mBuffer = buffer;
        mOffset = offset;
        mCount  = count;
    }


    /// <summary>
    ///     Returns an empty span that re-uses the underlying byte array.
    /// </summary>
    public BufferSpan Clear()
    {
        return new BufferSpan(mBuffer, 0, 0);
    }

    /// <summary>
    ///     Returns a new span with the given data appended to existing data.
    /// </summary>
    public BufferSpan Append(in BufferSpan data)
    {
        if (data.IsEmpty)
            return this;

        var freeBytes = FreeBytes - data.Count;

        if (freeBytes >= 0)
            return _AppendToExistingBuffer(data);

        if (-freeBytes <= Offset)
            return _AppendToExistingBufferConsolidating(data);

        return _AppendIntoNewBuffer(data);
    }

    private BufferSpan _AppendToExistingBuffer(in BufferSpan data)
    {
        System.Buffer.BlockCopy(data.Buffer, data.Offset, mBuffer, mOffset + mCount, data.Count);

        return new BufferSpan(mBuffer, mOffset, mCount + data.Count);
    }

    private BufferSpan _AppendToExistingBufferConsolidating(in BufferSpan data)
    {
        System.Buffer.BlockCopy(mBuffer, mOffset, mBuffer, 0, mCount);

        System.Buffer.BlockCopy(data.Buffer, data.Offset, mBuffer, mCount, data.Count);

        return new BufferSpan(mBuffer, 0, mCount + data.Count);
    }

    private BufferSpan _AppendIntoNewBuffer(in BufferSpan data)
    {
        var buffer = new Byte[Count + data.Count];

        System.Buffer.BlockCopy(mBuffer, mOffset, buffer, 0, mCount);

        System.Buffer.BlockCopy(data.Buffer, data.Offset, buffer, mCount, data.Count);

        return new BufferSpan(buffer, 0, mCount + data.Count);
    }

    /// <summary>
    ///     Returns a new span with the given number of bytes discarded from the beginning of existing data.
    /// </summary>
    public BufferSpan Discard(Int32 bytes)
    {
        _VerifyCount(bytes);

        if (bytes == 0)
            return this;

        if (bytes == mCount)
            return new BufferSpan(mBuffer, 0, 0);

        return new BufferSpan(mBuffer, mOffset + bytes, mCount - bytes);
    }

    /// <summary>
    ///     Returns a new span with the given slice of existing data.
    /// </summary>
    public BufferSpan Slice(Int32 index, Int32 count)
    {
        _VerifyIndexCount(index, count);

        if (count == mCount)
            return this;

        return new BufferSpan(mBuffer, mOffset + index, count);
    }

    /// <summary>
    ///     Returns a new span with a newly allocated buffer that clones the current span of data.
    /// </summary>
    public BufferSpan Clone()
    {
        if (IsEmpty)
            return Empty;

        var buffer = new Byte[Count];

        System.Buffer.BlockCopy(Buffer, Offset, buffer, 0, Count);

        return From(buffer, 0, buffer.Length);
    }

    /// <summary>
    ///     Copies the data into a new byte array.
    /// </summary>
    public Byte[] ToArray()
    {
        if (IsEmpty)
            return [ ];

        var buffer = new Byte[Count];

        System.Buffer.BlockCopy(Buffer, Offset, buffer, 0, Count);

        return buffer;
    }

    /// <summary>
    ///     Returns a string that represents the current instance.
    /// </summary>
    public override String ToString()
    {
        if (IsEmpty)
            return "<empty>";

        return HexFormatter.ToUpper(mBuffer, mOffset, mCount, "-");
    }

    /// <summary>
    ///     Returns a string that represents the current instance.
    /// </summary>
    public String ToString(String delimiter)
    {
        if (IsEmpty)
            return "<empty>";

        return HexFormatter.ToUpper(mBuffer, mOffset, mCount, delimiter);
    }


    private void _VerifyIndex(Int32 index)
    {
        if (index < 0)
            throw new ArgumentOutOfRangeException(nameof(index), index, "Outside of bounds.");

        if (index >= mCount)
            throw new ArgumentOutOfRangeException(nameof(index), index, "Outside of bounds.");
    }

    private void _VerifyCount(Int32 count)
    {
        if (count < 0)
            throw new ArgumentOutOfRangeException(nameof(count), count, "Outside of bounds.");

        if (count > mCount)
            throw new ArgumentOutOfRangeException(nameof(count), count, "Outside of bounds.");
    }

    private void _VerifyIndexCount(Int32 index, Int32 count)
    {
        if (index < 0 || index > mCount)
            throw new ArgumentOutOfRangeException(nameof(index), index, "Outside of bounds.");

        if (count < 0 || count > mCount)
            throw new ArgumentOutOfRangeException(nameof(count), count, "Outside of bounds.");

        if (index + count > mCount)
        {
            throw new ArgumentOutOfRangeException(
                nameof(index) + "+" + nameof(count),
                index + count,
                "Outside of bounds."
            );
        }
    }
}
