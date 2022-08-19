using System;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x0200052D RID: 1325
	public static class ZipConstants
	{
		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06002A5D RID: 10845 RVA: 0x00141263 File Offset: 0x0013F463
		// (set) Token: 0x06002A5E RID: 10846 RVA: 0x0014126A File Offset: 0x0013F46A
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

		// Token: 0x06002A5F RID: 10847 RVA: 0x00141272 File Offset: 0x0013F472
		[Obsolete("Use ZipStrings.ConvertToString instead")]
		public static string ConvertToString(byte[] data, int count)
		{
			return ZipStrings.ConvertToString(data, count);
		}

		// Token: 0x06002A60 RID: 10848 RVA: 0x0014127B File Offset: 0x0013F47B
		[Obsolete("Use ZipStrings.ConvertToString instead")]
		public static string ConvertToString(byte[] data)
		{
			return ZipStrings.ConvertToString(data);
		}

		// Token: 0x06002A61 RID: 10849 RVA: 0x00141283 File Offset: 0x0013F483
		[Obsolete("Use ZipStrings.ConvertToStringExt instead")]
		public static string ConvertToStringExt(int flags, byte[] data, int count)
		{
			return ZipStrings.ConvertToStringExt(flags, data, count);
		}

		// Token: 0x06002A62 RID: 10850 RVA: 0x0014128D File Offset: 0x0013F48D
		[Obsolete("Use ZipStrings.ConvertToStringExt instead")]
		public static string ConvertToStringExt(int flags, byte[] data)
		{
			return ZipStrings.ConvertToStringExt(flags, data);
		}

		// Token: 0x06002A63 RID: 10851 RVA: 0x00141296 File Offset: 0x0013F496
		[Obsolete("Use ZipStrings.ConvertToArray instead")]
		public static byte[] ConvertToArray(string str)
		{
			return ZipStrings.ConvertToArray(str);
		}

		// Token: 0x06002A64 RID: 10852 RVA: 0x0014129E File Offset: 0x0013F49E
		[Obsolete("Use ZipStrings.ConvertToArray instead")]
		public static byte[] ConvertToArray(int flags, string str)
		{
			return ZipStrings.ConvertToArray(flags, str);
		}

		// Token: 0x04002697 RID: 9879
		public const int VersionMadeBy = 51;

		// Token: 0x04002698 RID: 9880
		[Obsolete("Use VersionMadeBy instead")]
		public const int VERSION_MADE_BY = 51;

		// Token: 0x04002699 RID: 9881
		public const int VersionStrongEncryption = 50;

		// Token: 0x0400269A RID: 9882
		[Obsolete("Use VersionStrongEncryption instead")]
		public const int VERSION_STRONG_ENCRYPTION = 50;

		// Token: 0x0400269B RID: 9883
		public const int VERSION_AES = 51;

		// Token: 0x0400269C RID: 9884
		public const int VersionZip64 = 45;

		// Token: 0x0400269D RID: 9885
		public const int VersionBZip2 = 46;

		// Token: 0x0400269E RID: 9886
		public const int LocalHeaderBaseSize = 30;

		// Token: 0x0400269F RID: 9887
		[Obsolete("Use LocalHeaderBaseSize instead")]
		public const int LOCHDR = 30;

		// Token: 0x040026A0 RID: 9888
		public const int Zip64DataDescriptorSize = 24;

		// Token: 0x040026A1 RID: 9889
		public const int DataDescriptorSize = 16;

		// Token: 0x040026A2 RID: 9890
		[Obsolete("Use DataDescriptorSize instead")]
		public const int EXTHDR = 16;

		// Token: 0x040026A3 RID: 9891
		public const int CentralHeaderBaseSize = 46;

		// Token: 0x040026A4 RID: 9892
		[Obsolete("Use CentralHeaderBaseSize instead")]
		public const int CENHDR = 46;

		// Token: 0x040026A5 RID: 9893
		public const int EndOfCentralRecordBaseSize = 22;

		// Token: 0x040026A6 RID: 9894
		[Obsolete("Use EndOfCentralRecordBaseSize instead")]
		public const int ENDHDR = 22;

		// Token: 0x040026A7 RID: 9895
		public const int CryptoHeaderSize = 12;

		// Token: 0x040026A8 RID: 9896
		[Obsolete("Use CryptoHeaderSize instead")]
		public const int CRYPTO_HEADER_SIZE = 12;

		// Token: 0x040026A9 RID: 9897
		public const int Zip64EndOfCentralDirectoryLocatorSize = 20;

		// Token: 0x040026AA RID: 9898
		public const int LocalHeaderSignature = 67324752;

		// Token: 0x040026AB RID: 9899
		[Obsolete("Use LocalHeaderSignature instead")]
		public const int LOCSIG = 67324752;

		// Token: 0x040026AC RID: 9900
		public const int SpanningSignature = 134695760;

		// Token: 0x040026AD RID: 9901
		[Obsolete("Use SpanningSignature instead")]
		public const int SPANNINGSIG = 134695760;

		// Token: 0x040026AE RID: 9902
		public const int SpanningTempSignature = 808471376;

		// Token: 0x040026AF RID: 9903
		[Obsolete("Use SpanningTempSignature instead")]
		public const int SPANTEMPSIG = 808471376;

		// Token: 0x040026B0 RID: 9904
		public const int DataDescriptorSignature = 134695760;

		// Token: 0x040026B1 RID: 9905
		[Obsolete("Use DataDescriptorSignature instead")]
		public const int EXTSIG = 134695760;

		// Token: 0x040026B2 RID: 9906
		[Obsolete("Use CentralHeaderSignature instead")]
		public const int CENSIG = 33639248;

		// Token: 0x040026B3 RID: 9907
		public const int CentralHeaderSignature = 33639248;

		// Token: 0x040026B4 RID: 9908
		public const int Zip64CentralFileHeaderSignature = 101075792;

		// Token: 0x040026B5 RID: 9909
		[Obsolete("Use Zip64CentralFileHeaderSignature instead")]
		public const int CENSIG64 = 101075792;

		// Token: 0x040026B6 RID: 9910
		public const int Zip64CentralDirLocatorSignature = 117853008;

		// Token: 0x040026B7 RID: 9911
		public const int ArchiveExtraDataSignature = 117853008;

		// Token: 0x040026B8 RID: 9912
		public const int CentralHeaderDigitalSignature = 84233040;

		// Token: 0x040026B9 RID: 9913
		[Obsolete("Use CentralHeaderDigitalSignaure instead")]
		public const int CENDIGITALSIG = 84233040;

		// Token: 0x040026BA RID: 9914
		public const int EndOfCentralDirectorySignature = 101010256;

		// Token: 0x040026BB RID: 9915
		[Obsolete("Use EndOfCentralDirectorySignature instead")]
		public const int ENDSIG = 101010256;
	}
}
