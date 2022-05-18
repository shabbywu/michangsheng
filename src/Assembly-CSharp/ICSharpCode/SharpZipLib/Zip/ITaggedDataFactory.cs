using System;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x020007D0 RID: 2000
	internal interface ITaggedDataFactory
	{
		// Token: 0x060032F7 RID: 13047
		ITaggedData Create(short tag, byte[] data, int offset, int count);
	}
}
