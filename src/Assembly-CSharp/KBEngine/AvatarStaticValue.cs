using System;

namespace KBEngine
{
	// Token: 0x02001009 RID: 4105
	public class AvatarStaticValue
	{
		// Token: 0x06006226 RID: 25126 RVA: 0x0027322C File Offset: 0x0027142C
		public AvatarStaticValue()
		{
			for (int i = 0; i < 2500; i++)
			{
				this.Value[i] = 0;
			}
			this.talk[0] = 501;
			this.talk[1] = 1;
		}

		// Token: 0x04005CAD RID: 23725
		public int[] Value = new int[2500];

		// Token: 0x04005CAE RID: 23726
		public int[] talk = new int[2];

		// Token: 0x0200100A RID: 4106
		public enum StaticValue
		{
			// Token: 0x04005CB0 RID: 23728
			MaxNum = 2500
		}
	}
}
