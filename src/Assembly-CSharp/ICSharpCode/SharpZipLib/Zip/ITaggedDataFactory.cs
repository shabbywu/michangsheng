using System;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000537 RID: 1335
	internal interface ITaggedDataFactory
	{
		// Token: 0x06002AE0 RID: 10976
		ITaggedData Create(short tag, byte[] data, int offset, int count);
	}
}
