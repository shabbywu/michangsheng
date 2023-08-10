using System;
using System.Runtime.CompilerServices;

namespace ICSharpCode.SharpZipLib.Checksum;

public sealed class Crc32 : IChecksum
{
	private static readonly uint crcInit = uint.MaxValue;

	private static readonly uint crcXor = uint.MaxValue;

	private static readonly uint[] crcTable = CrcUtilities.GenerateSlicingLookupTable(3988292384u, isReversed: true);

	private uint checkValue;

	public long Value => checkValue ^ crcXor;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal static uint ComputeCrc32(uint oldCrc, byte bval)
	{
		return crcTable[(oldCrc ^ bval) & 0xFF] ^ (oldCrc >> 8);
	}

	public Crc32()
	{
		Reset();
	}

	public void Reset()
	{
		checkValue = crcInit;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Update(int bval)
	{
		checkValue = crcTable[(checkValue ^ bval) & 0xFF] ^ (checkValue >> 8);
	}

	public void Update(byte[] buffer)
	{
		if (buffer == null)
		{
			throw new ArgumentNullException("buffer");
		}
		Update(buffer, 0, buffer.Length);
	}

	public void Update(ArraySegment<byte> segment)
	{
		Update(segment.Array, segment.Offset, segment.Count);
	}

	private void Update(byte[] data, int offset, int count)
	{
		int num = count % 16;
		int num2 = offset + count - num;
		while (offset != num2)
		{
			checkValue = CrcUtilities.UpdateDataForReversedPoly(data, offset, crcTable, checkValue);
			offset += 16;
		}
		if (num != 0)
		{
			SlowUpdateLoop(data, offset, num2 + num);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	private void SlowUpdateLoop(byte[] data, int offset, int end)
	{
		while (offset != end)
		{
			Update(data[offset++]);
		}
	}
}
