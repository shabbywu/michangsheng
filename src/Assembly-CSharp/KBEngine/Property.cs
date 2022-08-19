using System;

namespace KBEngine
{
	// Token: 0x02000C5E RID: 3166
	public class Property
	{
		// Token: 0x0600561B RID: 22043 RVA: 0x0023C0EA File Offset: 0x0023A2EA
		public bool isBase()
		{
			return this.properFlags == 32U || this.properFlags == 64U;
		}

		// Token: 0x0600561C RID: 22044 RVA: 0x0023C102 File Offset: 0x0023A302
		public bool isOwnerOnly()
		{
			return this.properFlags == 8U || this.properFlags == 16U;
		}

		// Token: 0x0600561D RID: 22045 RVA: 0x0023C119 File Offset: 0x0023A319
		public bool isOtherOnly()
		{
			return this.properFlags == 128U || this.properFlags == 128U;
		}

		// Token: 0x040050F3 RID: 20723
		public string name = "";

		// Token: 0x040050F4 RID: 20724
		public ushort properUtype;

		// Token: 0x040050F5 RID: 20725
		public uint properFlags;

		// Token: 0x040050F6 RID: 20726
		public short aliasID = -1;

		// Token: 0x040050F7 RID: 20727
		public object defaultVal;

		// Token: 0x02001600 RID: 5632
		public enum EntityDataFlags
		{
			// Token: 0x04007108 RID: 28936
			ED_FLAG_UNKOWN,
			// Token: 0x04007109 RID: 28937
			ED_FLAG_CELL_PUBLIC,
			// Token: 0x0400710A RID: 28938
			ED_FLAG_CELL_PRIVATE,
			// Token: 0x0400710B RID: 28939
			ED_FLAG_ALL_CLIENTS = 4,
			// Token: 0x0400710C RID: 28940
			ED_FLAG_CELL_PUBLIC_AND_OWN = 8,
			// Token: 0x0400710D RID: 28941
			ED_FLAG_OWN_CLIENT = 16,
			// Token: 0x0400710E RID: 28942
			ED_FLAG_BASE_AND_CLIENT = 32,
			// Token: 0x0400710F RID: 28943
			ED_FLAG_BASE = 64,
			// Token: 0x04007110 RID: 28944
			ED_FLAG_OTHER_CLIENTS = 128
		}
	}
}
