using System;

namespace KBEngine
{
	// Token: 0x02000C73 RID: 3187
	public class card
	{
		// Token: 0x060057F8 RID: 22520 RVA: 0x000027FC File Offset: 0x000009FC
		public card()
		{
		}

		// Token: 0x060057F9 RID: 22521 RVA: 0x0024891A File Offset: 0x00246B1A
		public card(int type)
		{
			this.uuid = Tools.getUUID();
			this.cardType = type;
		}

		// Token: 0x040051F6 RID: 20982
		public string uuid;

		// Token: 0x040051F7 RID: 20983
		public int cardType;
	}
}
