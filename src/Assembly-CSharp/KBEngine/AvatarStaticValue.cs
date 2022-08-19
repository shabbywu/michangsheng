using System;

namespace KBEngine
{
	// Token: 0x02000C6B RID: 3179
	public class AvatarStaticValue
	{
		// Token: 0x060057AE RID: 22446 RVA: 0x00246F24 File Offset: 0x00245124
		public AvatarStaticValue()
		{
			for (int i = 0; i < 2500; i++)
			{
				this.Value[i] = 0;
			}
			this.talk[0] = 501;
			this.talk[1] = 1;
		}

		// Token: 0x040051D1 RID: 20945
		public int[] Value = new int[2500];

		// Token: 0x040051D2 RID: 20946
		public int[] talk = new int[2];

		// Token: 0x02001614 RID: 5652
		public enum StaticValue
		{
			// Token: 0x04007139 RID: 28985
			MaxNum = 2500
		}
	}
}
