using System;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000533 RID: 1331
	public interface ITaggedData
	{
		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06002ABE RID: 10942
		short TagID { get; }

		// Token: 0x06002ABF RID: 10943
		void SetData(byte[] data, int offset, int count);

		// Token: 0x06002AC0 RID: 10944
		byte[] GetData();
	}
}
