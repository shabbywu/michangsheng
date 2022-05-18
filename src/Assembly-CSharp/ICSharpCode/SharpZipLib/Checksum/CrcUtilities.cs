using System;
using System.Runtime.CompilerServices;

namespace ICSharpCode.SharpZipLib.Checksum
{
	// Token: 0x02000837 RID: 2103
	internal static class CrcUtilities
	{
		// Token: 0x06003715 RID: 14101 RVA: 0x0019CDE8 File Offset: 0x0019AFE8
		internal static uint[] GenerateSlicingLookupTable(uint polynomial, bool isReversed)
		{
			uint[] array = new uint[4096];
			uint num = isReversed ? 1U : 2147483648U;
			for (int i = 0; i < 256; i++)
			{
				uint num2 = (uint)(isReversed ? i : ((uint)i << 24));
				for (int j = 0; j < 16; j++)
				{
					for (int k = 0; k < 8; k++)
					{
						if (isReversed)
						{
							num2 = (((num2 & num) == 1U) ? (polynomial ^ num2 >> 1) : (num2 >> 1));
						}
						else
						{
							num2 = (((num2 & num) != 0U) ? (polynomial ^ num2 << 1) : (num2 << 1));
						}
					}
					array[256 * j + i] = num2;
				}
			}
			return array;
		}

		// Token: 0x06003716 RID: 14102 RVA: 0x0019CE7C File Offset: 0x0019B07C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static uint UpdateDataForNormalPoly(byte[] input, int offset, uint[] crcTable, uint checkValue)
		{
			byte x = (byte)(checkValue >> 24) ^ input[offset];
			byte x2 = (byte)(checkValue >> 16) ^ input[offset + 1];
			byte x3 = (byte)(checkValue >> 8) ^ input[offset + 2];
			byte x4 = (byte)checkValue ^ input[offset + 3];
			return CrcUtilities.UpdateDataCommon(input, offset, crcTable, x, x2, x3, x4);
		}

		// Token: 0x06003717 RID: 14103 RVA: 0x0019CEC4 File Offset: 0x0019B0C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static uint UpdateDataForReversedPoly(byte[] input, int offset, uint[] crcTable, uint checkValue)
		{
			byte x = (byte)checkValue ^ input[offset];
			byte x2 = (byte)(checkValue >>= 8) ^ input[offset + 1];
			byte x3 = (byte)(checkValue >>= 8) ^ input[offset + 2];
			byte x4 = (byte)(checkValue >>= 8) ^ input[offset + 3];
			return CrcUtilities.UpdateDataCommon(input, offset, crcTable, x, x2, x3, x4);
		}

		// Token: 0x06003718 RID: 14104 RVA: 0x0019CF14 File Offset: 0x0019B114
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static uint UpdateDataCommon(byte[] input, int offset, uint[] crcTable, byte x1, byte x2, byte x3, byte x4)
		{
			uint num = crcTable[(int)x1 + 3840] ^ crcTable[(int)x2 + 3584];
			uint num2 = crcTable[(int)x3 + 3328] ^ crcTable[(int)x4 + 3072];
			uint num3 = crcTable[(int)input[offset + 4] + 2816] ^ crcTable[(int)input[offset + 5] + 2560];
			num ^= crcTable[(int)input[offset + 9] + 1536];
			uint num4 = num3 ^ crcTable[(int)input[offset + 6] + 2304] ^ crcTable[(int)input[offset + 7] + 2048] ^ crcTable[(int)input[offset + 8] + 1792];
			num2 ^= crcTable[(int)input[offset + 13] + 512];
			return num4 ^ crcTable[(int)input[offset + 10] + 1280] ^ crcTable[(int)input[offset + 11] + 1024] ^ crcTable[(int)input[offset + 12] + 768] ^ num ^ crcTable[(int)input[offset + 14] + 256] ^ crcTable[(int)input[offset + 15]] ^ num2;
		}

		// Token: 0x04003132 RID: 12594
		internal const int SlicingDegree = 16;
	}
}
