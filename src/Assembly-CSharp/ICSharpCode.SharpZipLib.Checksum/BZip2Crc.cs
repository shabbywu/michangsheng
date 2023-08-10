using System;
using System.Runtime.CompilerServices;

namespace ICSharpCode.SharpZipLib.Checksum;

public sealed class BZip2Crc : IChecksum
{
	private const uint crcInit = uint.MaxValue;

	private static readonly uint[] crcTable = CrcUtilities.GenerateSlicingLookupTable(79764919u, isReversed: false);

	private uint checkValue;

	public long Value => ~checkValue;

	public BZip2Crc()
	{
		Reset();
	}

	public void Reset()
	{
		checkValue = uint.MaxValue;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Update(int bval)
	{
		checkValue = crcTable[(byte)(((checkValue >> 24) & 0xFF) ^ bval)] ^ (checkValue << 8);
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
			checkValue = CrcUtilities.UpdateDataForNormalPoly(data, offset, crcTable, checkValue);
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
