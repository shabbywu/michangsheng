using System;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x020007CB RID: 1995
	public interface ITaggedData
	{
		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x060032D5 RID: 13013
		short TagID { get; }

		// Token: 0x060032D6 RID: 13014
		void SetData(byte[] data, int offset, int count);

		// Token: 0x060032D7 RID: 13015
		byte[] GetData();
	}
}
