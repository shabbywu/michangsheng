using System;

namespace ICSharpCode.SharpZipLib.Checksum
{
	// Token: 0x0200058F RID: 1423
	public interface IChecksum
	{
		// Token: 0x06002EA3 RID: 11939
		void Reset();

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06002EA4 RID: 11940
		long Value { get; }

		// Token: 0x06002EA5 RID: 11941
		void Update(int bval);

		// Token: 0x06002EA6 RID: 11942
		void Update(byte[] buffer);

		// Token: 0x06002EA7 RID: 11943
		void Update(ArraySegment<byte> segment);
	}
}
