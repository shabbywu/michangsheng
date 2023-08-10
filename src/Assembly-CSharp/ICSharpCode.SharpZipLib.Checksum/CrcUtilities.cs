using System.Runtime.CompilerServices;

namespace ICSharpCode.SharpZipLib.Checksum;

internal static class CrcUtilities
{
	internal const int SlicingDegree = 16;

	internal static uint[] GenerateSlicingLookupTable(uint polynomial, bool isReversed)
	{
		uint[] array = new uint[4096];
		uint num = (isReversed ? 1u : 2147483648u);
		for (int i = 0; i < 256; i++)
		{
			uint num2 = (uint)(isReversed ? i : (i << 24));
			for (int j = 0; j < 16; j++)
			{
				for (int k = 0; k < 8; k++)
				{
					num2 = ((!isReversed) ? (((num2 & num) != 0) ? (polynomial ^ (num2 << 1)) : (num2 << 1)) : (((num2 & num) == 1) ? (polynomial ^ (num2 >> 1)) : (num2 >> 1)));
				}
				array[256 * j + i] = num2;
			}
		}
		return array;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal static uint UpdateDataForNormalPoly(byte[] input, int offset, uint[] crcTable, uint checkValue)
	{
		byte x = (byte)((byte)(checkValue >> 24) ^ input[offset]);
		byte x2 = (byte)((byte)(checkValue >> 16) ^ input[offset + 1]);
		byte x3 = (byte)((byte)(checkValue >> 8) ^ input[offset + 2]);
		byte x4 = (byte)((byte)checkValue ^ input[offset + 3]);
		return UpdateDataCommon(input, offset, crcTable, x, x2, x3, x4);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal static uint UpdateDataForReversedPoly(byte[] input, int offset, uint[] crcTable, uint checkValue)
	{
		byte x = (byte)((byte)checkValue ^ input[offset]);
		byte x2 = (byte)((byte)(checkValue >>= 8) ^ input[offset + 1]);
		byte x3 = (byte)((byte)(checkValue >>= 8) ^ input[offset + 2]);
		byte x4 = (byte)((byte)(checkValue >>= 8) ^ input[offset + 3]);
		return UpdateDataCommon(input, offset, crcTable, x, x2, x3, x4);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static uint UpdateDataCommon(byte[] input, int offset, uint[] crcTable, byte x1, byte x2, byte x3, byte x4)
	{
		uint num = crcTable[x1 + 3840] ^ crcTable[x2 + 3584];
		uint num2 = crcTable[x3 + 3328] ^ crcTable[x4 + 3072];
		uint num3 = crcTable[input[offset + 4] + 2816] ^ crcTable[input[offset + 5] + 2560];
		num ^= crcTable[input[offset + 9] + 1536];
		uint num4 = num3 ^ crcTable[input[offset + 6] + 2304] ^ crcTable[input[offset + 7] + 2048] ^ crcTable[input[offset + 8] + 1792];
		num2 ^= crcTable[input[offset + 13] + 512];
		return num4 ^ crcTable[input[offset + 10] + 1280] ^ crcTable[input[offset + 11] + 1024] ^ crcTable[input[offset + 12] + 768] ^ num ^ crcTable[input[offset + 14] + 256] ^ crcTable[input[offset + 15]] ^ num2;
	}
}
