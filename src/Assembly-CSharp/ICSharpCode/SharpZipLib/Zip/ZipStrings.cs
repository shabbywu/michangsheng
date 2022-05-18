using System;
using System.Text;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x020007F2 RID: 2034
	public static class ZipStrings
	{
		// Token: 0x0600345A RID: 13402 RVA: 0x001944C4 File Offset: 0x001926C4
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

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x0600345B RID: 13403 RVA: 0x00026311 File Offset: 0x00024511
		// (set) Token: 0x0600345C RID: 13404 RVA: 0x0002632B File Offset: 0x0002452B
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

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x0600345D RID: 13405 RVA: 0x0002635B File Offset: 0x0002455B
		public static int SystemDefaultCodePage { get; }

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x0600345E RID: 13406 RVA: 0x00026362 File Offset: 0x00024562
		// (set) Token: 0x0600345F RID: 13407 RVA: 0x00026375 File Offset: 0x00024575
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

		// Token: 0x06003460 RID: 13408 RVA: 0x00026394 File Offset: 0x00024594
		public static string ConvertToString(byte[] data, int count)
		{
			if (data != null)
			{
				return Encoding.GetEncoding(ZipStrings.CodePage).GetString(data, 0, count);
			}
			return string.Empty;
		}

		// Token: 0x06003461 RID: 13409 RVA: 0x000263B1 File Offset: 0x000245B1
		public static string ConvertToString(byte[] data)
		{
			return ZipStrings.ConvertToString(data, data.Length);
		}

		// Token: 0x06003462 RID: 13410 RVA: 0x000263BC File Offset: 0x000245BC
		private static Encoding EncodingFromFlag(int flags)
		{
			if ((flags & 2048) == 0)
			{
				return Encoding.GetEncoding((ZipStrings.codePage == -1) ? ZipStrings.SystemDefaultCodePage : ZipStrings.codePage);
			}
			return Encoding.UTF8;
		}

		// Token: 0x06003463 RID: 13411 RVA: 0x000263E6 File Offset: 0x000245E6
		public static string ConvertToStringExt(int flags, byte[] data, int count)
		{
			if (data != null)
			{
				return ZipStrings.EncodingFromFlag(flags).GetString(data, 0, count);
			}
			return string.Empty;
		}

		// Token: 0x06003464 RID: 13412 RVA: 0x000263FF File Offset: 0x000245FF
		public static string ConvertToStringExt(int flags, byte[] data)
		{
			return ZipStrings.ConvertToStringExt(flags, data, data.Length);
		}

		// Token: 0x06003465 RID: 13413 RVA: 0x0002640B File Offset: 0x0002460B
		public static byte[] ConvertToArray(string str)
		{
			if (str != null)
			{
				return Encoding.GetEncoding(ZipStrings.CodePage).GetBytes(str);
			}
			return new byte[0];
		}

		// Token: 0x06003466 RID: 13414 RVA: 0x00026427 File Offset: 0x00024627
		public static byte[] ConvertToArray(int flags, string str)
		{
			if (!string.IsNullOrEmpty(str))
			{
				return ZipStrings.EncodingFromFlag(flags).GetBytes(str);
			}
			return new byte[0];
		}

		// Token: 0x04002F72 RID: 12146
		private static int codePage = -1;

		// Token: 0x04002F73 RID: 12147
		private const int AutomaticCodePage = -1;

		// Token: 0x04002F74 RID: 12148
		private const int FallbackCodePage = 437;
	}
}
