using System;
using System.Security.Cryptography;
using ICSharpCode.SharpZipLib.Checksum;

namespace ICSharpCode.SharpZipLib.Encryption
{
	// Token: 0x0200081A RID: 2074
	public abstract class PkzipClassic : SymmetricAlgorithm
	{
		// Token: 0x06003668 RID: 13928 RVA: 0x0019B974 File Offset: 0x00199B74
		public static byte[] GenerateKeys(byte[] seed)
		{
			if (seed == null)
			{
				throw new ArgumentNullException("seed");
			}
			if (seed.Length == 0)
			{
				throw new ArgumentException("Length is zero", "seed");
			}
			uint[] array = new uint[]
			{
				305419896U,
				591751049U,
				878082192U
			};
			for (int i = 0; i < seed.Length; i++)
			{
				array[0] = Crc32.ComputeCrc32(array[0], seed[i]);
				array[1] = array[1] + (uint)((byte)array[0]);
				array[1] = array[1] * 134775813U + 1U;
				array[2] = Crc32.ComputeCrc32(array[2], (byte)(array[1] >> 24));
			}
			return new byte[]
			{
				(byte)(array[0] & 255U),
				(byte)(array[0] >> 8 & 255U),
				(byte)(array[0] >> 16 & 255U),
				(byte)(array[0] >> 24 & 255U),
				(byte)(array[1] & 255U),
				(byte)(array[1] >> 8 & 255U),
				(byte)(array[1] >> 16 & 255U),
				(byte)(array[1] >> 24 & 255U),
				(byte)(array[2] & 255U),
				(byte)(array[2] >> 8 & 255U),
				(byte)(array[2] >> 16 & 255U),
				(byte)(array[2] >> 24 & 255U)
			};
		}
	}
}
