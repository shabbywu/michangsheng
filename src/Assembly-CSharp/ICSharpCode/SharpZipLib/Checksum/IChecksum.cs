using System;

namespace ICSharpCode.SharpZipLib.Checksum
{
	// Token: 0x02000838 RID: 2104
	public interface IChecksum
	{
		// Token: 0x06003719 RID: 14105
		void Reset();

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x0600371A RID: 14106
		long Value { get; }

		// Token: 0x0600371B RID: 14107
		void Update(int bval);

		// Token: 0x0600371C RID: 14108
		void Update(byte[] buffer);

		// Token: 0x0600371D RID: 14109
		void Update(ArraySegment<byte> segment);
	}
}
