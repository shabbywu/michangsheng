using System;

namespace script.ItemSource.Interface
{
	// Token: 0x02000A1E RID: 2590
	public abstract class ABItemSourceMag
	{
		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06004784 RID: 18308 RVA: 0x001E3F24 File Offset: 0x001E2124
		public static ABItemSourceMag Inst
		{
			get
			{
				if (ABItemSourceMag._inst == null)
				{
					ABItemSourceMag._inst = new ItemSourceMag();
				}
				return ABItemSourceMag._inst;
			}
		}

		// Token: 0x06004785 RID: 18309 RVA: 0x001E3F3C File Offset: 0x001E213C
		public static void SetNull()
		{
			ABItemSourceMag._inst = null;
		}

		// Token: 0x04004889 RID: 18569
		public ABItemSourceUpdate Update;

		// Token: 0x0400488A RID: 18570
		public ABItemSourceIO IO;

		// Token: 0x0400488B RID: 18571
		private static ABItemSourceMag _inst;
	}
}
