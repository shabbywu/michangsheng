using System;

namespace script.ItemSource.Interface
{
	// Token: 0x02000A1C RID: 2588
	[Serializable]
	public abstract class ABItemSourceData
	{
		// Token: 0x0600477F RID: 18303 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void Init()
		{
		}

		// Token: 0x04004885 RID: 18565
		public int Id;

		// Token: 0x04004886 RID: 18566
		public int Count;

		// Token: 0x04004887 RID: 18567
		public int UpdateTime;

		// Token: 0x04004888 RID: 18568
		public int HasCostTime;
	}
}
