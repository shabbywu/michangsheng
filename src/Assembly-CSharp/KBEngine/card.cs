using System;

namespace KBEngine
{
	// Token: 0x02001017 RID: 4119
	public class card
	{
		// Token: 0x06006273 RID: 25203 RVA: 0x0000403D File Offset: 0x0000223D
		public card()
		{
		}

		// Token: 0x06006274 RID: 25204 RVA: 0x000442B2 File Offset: 0x000424B2
		public card(int type)
		{
			this.uuid = Tools.getUUID();
			this.cardType = type;
		}

		// Token: 0x04005CE6 RID: 23782
		public string uuid;

		// Token: 0x04005CE7 RID: 23783
		public int cardType;
	}
}
