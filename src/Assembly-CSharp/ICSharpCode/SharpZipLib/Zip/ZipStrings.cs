using System;
using System.Text;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x0200054F RID: 1359
	public static class ZipStrings
	{
		// Token: 0x06002BFF RID: 11263 RVA: 0x001487A4 File Offset: 0x001469A4
		static ZipStrings()
		{
			try
			{
				int num = Encoding.GetEncoding(0).CodePage;
				ZipStrings.SystemDefaultCodePage = ((num == 1 || num == 2 || num == 3 || num == 42) ? 437 : num);
			}
			catch
			{
				ZipStrings.SystemDefaultCodePage = 437;
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06002C00 RID: 11264 RVA: 0x00148800 File Offset: 0x00146A00
		// (set) Token: 0x06002C01 RID: 11265 RVA: 0x0014881A File Offset: 0x00146A1A
		public static int CodePage
		{
			get
			{
				if (ZipStrings.codePage != -1)
				{
					return ZipStrings.codePage;
				}
				return Encoding.UTF8.CodePage;
			}
			set
			{
				if (value < 0 || value > 65535 || value == 1 || value == 2 || value == 3 || value == 42)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				ZipStrings.codePage = value;
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06002C02 RID: 11266 RVA: 0x0014884A File Offset: 0x00146A4A
		public static int SystemDefaultCodePage { get; }

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06002C03 RID: 11267 RVA: 0x00148851 File Offset: 0x00146A51
		// (set) Token: 0x06002C04 RID: 11268 RVA: 0x00148864 File Offset: 0x00146A64
		public static bool UseUnicode
		{
			get
			{
				return ZipStrings.codePage == Encoding.UTF8.CodePage;
			}
			set
			{
				if (value)
				{
					ZipStrings.codePage = Encoding.UTF8.CodePage;
					return;
				}
				ZipStrings.codePage = ZipStrings.SystemDefaultCodePage;
			}
		}

		// Token: 0x06002C05 RID: 11269 RVA: 0x00148883 File Offset: 0x00146A83
		public static string ConvertToString(byte[] data, int count)
		{
			if (data != null)
			{
				return Encoding.GetEncoding(ZipStrings.CodePage).GetString(data, 0, count);
			}
			return string.Empty;
		}

		// Token: 0x06002C06 RID: 11270 RVA: 0x001488A0 File Offset: 0x00146AA0
		public static string ConvertToString(byte[] data)
		{
			return ZipStrings.ConvertToString(data, data.Length);
		}

		// Token: 0x06002C07 RID: 11271 RVA: 0x001488AB File Offset: 0x00146AAB
		private static Encoding EncodingFromFlag(int flags)
		{
			if ((flags & 2048) == 0)
			{
				return Encoding.GetEncoding((ZipStrings.codePage == -1) ? ZipStrings.SystemDefaultCodePage : ZipStrings.codePage);
			}
			return Encoding.UTF8;
		}

		// Token: 0x06002C08 RID: 11272 RVA: 0x001488D5 File Offset: 0x00146AD5
		public static string ConvertToStringExt(int flags, byte[] data, int count)
		{
			if (data != null)
			{
				return ZipStrings.EncodingFromFlag(flags).GetString(data, 0, count);
			}
			return string.Empty;
		}

		// Token: 0x06002C09 RID: 11273 RVA: 0x001488EE File Offset: 0x00146AEE
		public static string ConvertToStringExt(int flags, byte[] data)
		{
			return ZipStrings.ConvertToStringExt(flags, data, data.Length);
		}

		// Token: 0x06002C0A RID: 11274 RVA: 0x001488FA File Offset: 0x00146AFA
		public static byte[] ConvertToArray(string str)
		{
			if (str != null)
			{
				return Encoding.GetEncoding(ZipStrings.CodePage).GetBytes(str);
			}
			return new byte[0];
		}

		// Token: 0x06002C0B RID: 11275 RVA: 0x00148916 File Offset: 0x00146B16
		public static byte[] ConvertToArray(int flags, string str)
		{
			if (!string.IsNullOrEmpty(str))
			{
				return ZipStrings.EncodingFromFlag(flags).GetBytes(str);
			}
			return new byte[0];
		}

		// Token: 0x04002750 RID: 10064
		private static int codePage = -1;

		// Token: 0x04002751 RID: 10065
		private const int AutomaticCodePage = -1;

		// Token: 0x04002752 RID: 10066
		private const int FallbackCodePage = 437;
	}
}
