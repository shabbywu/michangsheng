using System;
using System.Collections.Generic;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace ICSharpCode.SharpZipLib.Zip.Compression
{
	// Token: 0x020007FE RID: 2046
	public class InflaterHuffmanTree
	{
		// Token: 0x060034CE RID: 13518 RVA: 0x00197340 File Offset: 0x00195540
		static InflaterHuffmanTree()
		{
			try
			{
				byte[] array = new byte[288];
				int i = 0;
				while (i < 144)
				{
					array[i++] = 8;
				}
				while (i < 256)
				{
					array[i++] = 9;
				}
				while (i < 280)
				{
					array[i++] = 7;
				}
				while (i < 288)
				{
					array[i++] = 8;
				}
				InflaterHuffmanTree.defLitLenTree = new InflaterHuffmanTree(array);
				array = new byte[32];
				i = 0;
				while (i < 32)
				{
					array[i++] = 5;
				}
				InflaterHuffmanTree.defDistTree = new InflaterHuffmanTree(array);
			}
			catch (Exception)
			{
				throw new SharpZipBaseException("InflaterHuffmanTree: static tree length illegal");
			}
		}

		// Token: 0x060034CF RID: 13519 RVA: 0x00026940 File Offset: 0x00024B40
		public InflaterHuffmanTree(IList<byte> codeLengths)
		{
			this.BuildTree(codeLengths);
		}

		// Token: 0x060034D0 RID: 13520 RVA: 0x001973F0 File Offset: 0x001955F0
		private void BuildTree(IList<byte> codeLengths)
		{
			int[] array = new int[16];
			int[] array2 = new int[16];
			for (int i = 0; i < codeLengths.Count; i++)
			{
				int num = (int)codeLengths[i];
				if (num > 0)
				{
					array[num]++;
				}
			}
			int num2 = 0;
			int num3 = 512;
			for (int j = 1; j <= 15; j++)
			{
				array2[j] = num2;
				num2 += array[j] << 16 - j;
				if (j >= 10)
				{
					int num4 = array2[j] & 130944;
					int num5 = num2 & 130944;
					num3 += num5 - num4 >> 16 - j;
				}
			}
			this.tree = new short[num3];
			int num6 = 512;
			for (int k = 15; k >= 10; k--)
			{
				int num7 = num2 & 130944;
				num2 -= array[k] << 16 - k;
				for (int l = num2 & 130944; l < num7; l += 128)
				{
					this.tree[(int)DeflaterHuffman.BitReverse(l)] = (short)(-num6 << 4 | k);
					num6 += 1 << k - 9;
				}
			}
			for (int m = 0; m < codeLengths.Count; m++)
			{
				int num8 = (int)codeLengths[m];
				if (num8 != 0)
				{
					num2 = array2[num8];
					int num9 = (int)DeflaterHuffman.BitReverse(num2);
					if (num8 <= 9)
					{
						do
						{
							this.tree[num9] = (short)(m << 4 | num8);
							num9 += 1 << num8;
						}
						while (num9 < 512);
					}
					else
					{
						int num10 = (int)this.tree[num9 & 511];
						int num11 = 1 << (num10 & 15);
						num10 = -(num10 >> 4);
						do
						{
							this.tree[num10 | num9 >> 9] = (short)(m << 4 | num8);
							num9 += 1 << num8;
						}
						while (num9 < num11);
					}
					array2[num8] = num2 + (1 << 16 - num8);
				}
			}
		}

		// Token: 0x060034D1 RID: 13521 RVA: 0x001975DC File Offset: 0x001957DC
		public int GetSymbol(StreamManipulator input)
		{
			int num;
			if ((num = input.PeekBits(9)) >= 0)
			{
				int num2 = (int)this.tree[num];
				int num3 = num2 & 15;
				if (num2 >= 0)
				{
					if (num3 == 0)
					{
						throw new SharpZipBaseException("Encountered invalid codelength 0");
					}
					input.DropBits(num3);
					return num2 >> 4;
				}
				else
				{
					int num4 = -(num2 >> 4);
					if ((num = input.PeekBits(num3)) >= 0)
					{
						num2 = (int)this.tree[num4 | num >> 9];
						input.DropBits(num2 & 15);
						return num2 >> 4;
					}
					int availableBits = input.AvailableBits;
					num = input.PeekBits(availableBits);
					num2 = (int)this.tree[num4 | num >> 9];
					if ((num2 & 15) <= availableBits)
					{
						input.DropBits(num2 & 15);
						return num2 >> 4;
					}
					return -1;
				}
			}
			else
			{
				int availableBits2 = input.AvailableBits;
				num = input.PeekBits(availableBits2);
				int num2 = (int)this.tree[num];
				if (num2 >= 0 && (num2 & 15) <= availableBits2)
				{
					input.DropBits(num2 & 15);
					return num2 >> 4;
				}
				return -1;
			}
		}

		// Token: 0x0400301E RID: 12318
		private const int MAX_BITLEN = 15;

		// Token: 0x0400301F RID: 12319
		private short[] tree;

		// Token: 0x04003020 RID: 12320
		public static InflaterHuffmanTree defLitLenTree;

		// Token: 0x04003021 RID: 12321
		public static InflaterHuffmanTree defDistTree;
	}
}
