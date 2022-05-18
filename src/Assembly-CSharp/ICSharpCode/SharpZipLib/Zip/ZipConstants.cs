using System;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x020007C3 RID: 1987
	public static class ZipConstants
	{
		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06003274 RID: 12916 RVA: 0x00024BCA File Offset: 0x00022DCA
		// (set) Token: 0x06003275 RID: 12917 RVA: 0x00024BD1 File Offset: 0x00022DD1
		[Obsolete("Use ZipStrings instead")]
		public static int DefaultCodePage
		{
			get
			{
				return ZipStrings.CodePage;
			}
			set
			{
				ZipStrings.CodePage = value;
			}
		}

		// Token: 0x06003276 RID: 12918 RVA: 0x00024BD9 File Offset: 0x00022DD9
		[Obsolete("Use ZipStrings.ConvertToString instead")]
		public static string ConvertToString(byte[] data, int count)
		{
			return ZipStrings.ConvertToString(data, count);
		}

		// Token: 0x06003277 RID: 12919 RVA: 0x00024BE2 File Offset: 0x00022DE2
		[Obsolete("Use ZipStrings.ConvertToString instead")]
		public static string ConvertToString(byte[] data)
		{
			return ZipStrings.ConvertToString(data);
		}

		// Token: 0x06003278 RID: 12920 RVA: 0x00024BEA File Offset: 0x00022DEA
		[Obsolete("Use ZipStrings.ConvertToStringExt instead")]
		public static string ConvertToStringExt(int flags, byte[] data, int count)
		{
			return ZipStrings.ConvertToStringExt(flags, data, count);
		}

		// Token: 0x06003279 RID: 12921 RVA: 0x00024BF4 File Offset: 0x00022DF4
		[Obsolete("Use ZipStrings.ConvertToStringExt instead")]
		public static string ConvertToStringExt(int flags, byte[] data)
		{
			return ZipStrings.ConvertToStringExt(flags, data);
		}

		// Token: 0x0600327A RID: 12922 RVA: 0x00024BFD File Offset: 0x00022DFD
		[Obsolete("Use ZipStrings.ConvertToArray instead")]
		public static byte[] ConvertToArray(string str)
		{
			return ZipStrings.ConvertToArray(str);
		}

		// Token: 0x0600327B RID: 12923 RVA: 0x00024C05 File Offset: 0x00022E05
		[Obsolete("Use ZipStrings.ConvertToArray instead")]
		public static byte[] ConvertToArray(int flags, string str)
		{
			return ZipStrings.ConvertToArray(flags, str);
		}

		// Token: 0x04002E8B RID: 11915
		public const int VersionMadeBy = 51;

		// Token: 0x04002E8C RID: 11916
		[Obsolete("Use VersionMadeBy instead")]
		public const int VERSION_MADE_BY = 51;

		// Token: 0x04002E8D RID: 11917
		public const int VersionStrongEncryption = 50;

		// Token: 0x04002E8E RID: 11918
		[Obsolete("Use VersionStrongEncryption instead")]
		public const int VERSION_STRONG_ENCRYPTION = 50;

		// Token: 0x04002E8F RID: 11919
		public const int VERSION_AES = 51;

		// Token: 0x04002E90 RID: 11920
		public const int VersionZip64 = 45;

		// Token: 0x04002E91 RID: 11921
		public const int VersionBZip2 = 46;

		// Token: 0x04002E92 RID: 11922
		public const int LocalHeaderBaseSize = 30;

		// Token: 0x04002E93 RID: 11923
		[Obsolete("Use LocalHeaderBaseSize instead")]
		public const int LOCHDR = 30;

		// Token: 0x04002E94 RID: 11924
		public const int Zip64DataDescriptorSize = 24;

		// Token: 0x04002E95 RID: 11925
		public const int DataDescriptorSize = 16;

		// Token: 0x04002E96 RID: 11926
		[Obsolete("Use DataDescriptorSize instead")]
		public const int EXTHDR = 16;

		// Token: 0x04002E97 RID: 11927
		public const int CentralHeaderBaseSize = 46;

		// Token: 0x04002E98 RID: 11928
		[Obsolete("Use CentralHeaderBaseSize instead")]
		public const int CENHDR = 46;

		// Token: 0x04002E99 RID: 11929
		public const int EndOfCentralRecordBaseSize = 22;

		// Token: 0x04002E9A RID: 11930
		[Obsolete("Use EndOfCentralRecordBaseSize instead")]
		public const int ENDHDR = 22;

		// Token: 0x04002E9B RID: 11931
		public const int CryptoHeaderSize = 12;

		// Token: 0x04002E9C RID: 11932
		[Obsolete("Use CryptoHeaderSize instead")]
		public const int CRYPTO_HEADER_SIZE = 12;

		// Token: 0x04002E9D RID: 11933
		public const int Zip64EndOfCentralDirectoryLocatorSize = 20;

		// Token: 0x04002E9E RID: 11934
		public const int LocalHeaderSignature = 67324752;

		// Token: 0x04002E9F RID: 11935
		[Obsolete("Use LocalHeaderSignature instead")]
		public const int LOCSIG = 67324752;

		// Token: 0x04002EA0 RID: 11936
		public const int SpanningSignature = 134695760;

		// Token: 0x04002EA1 RID: 11937
		[Obsolete("Use SpanningSignature instead")]
		public const int SPANNINGSIG = 134695760;

		// Token: 0x04002EA2 RID: 11938
		public const int SpanningTempSignature = 808471376;

		// Token: 0x04002EA3 RID: 11939
		[Obsolete("Use SpanningTempSignature instead")]
		public const int SPANTEMPSIG = 808471376;

		// Token: 0x04002EA4 RID: 11940
		public const int DataDescriptorSignature = 134695760;

		// Token: 0x04002EA5 RID: 11941
		[Obsolete("Use DataDescriptorSignature instead")]
		public const int EXTSIG = 134695760;

		// Token: 0x04002EA6 RID: 11942
		[Obsolete("Use CentralHeaderSignature instead")]
		public const int CENSIG = 33639248;

		// Token: 0x04002EA7 RID: 11943
		public const int CentralHeaderSignature = 33639248;

		// Token: 0x04002EA8 RID: 11944
		public const int Zip64CentralFileHeaderSignature = 101075792;

		// Token: 0x04002EA9 RID: 11945
		[Obsolete("Use Zip64CentralFileHeaderSignature instead")]
		public const int CENSIG64 = 101075792;

		// Token: 0x04002EAA RID: 11946
		public const int Zip64CentralDirLocatorSignature = 117853008;

		// Token: 0x04002EAB RID: 11947
		public const int ArchiveExtraDataSignature = 117853008;

		// Token: 0x04002EAC RID: 11948
		public const int CentralHeaderDigitalSignature = 84233040;

		// Token: 0x04002EAD RID: 11949
		[Obsolete("Use CentralHeaderDigitalSignaure instead")]
		public const int CENDIGITALSIG = 84233040;

		// Token: 0x04002EAE RID: 11950
		public const int EndOfCentralDirectorySignature = 101010256;

		// Token: 0x04002EAF RID: 11951
		[Obsolete("Use EndOfCentralDirectorySignature instead")]
		public const int ENDSIG = 101010256;
	}
}
