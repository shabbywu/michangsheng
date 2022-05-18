using System;

namespace KBEngine
{
	// Token: 0x02000FE8 RID: 4072
	public class Property
	{
		// Token: 0x0600606A RID: 24682 RVA: 0x00042F47 File Offset: 0x00041147
		public bool isBase()
		{
			return this.properFlags == 32U || this.properFlags == 64U;
		}

		// Token: 0x0600606B RID: 24683 RVA: 0x00042F5F File Offset: 0x0004115F
		public bool isOwnerOnly()
		{
			return this.properFlags == 8U || this.properFlags == 16U;
		}

		// Token: 0x0600606C RID: 24684 RVA: 0x00042F76 File Offset: 0x00041176
		public bool isOtherOnly()
		{
			return this.properFlags == 128U || this.properFlags == 128U;
		}

		// Token: 0x04005BA3 RID: 23459
		public string name = "";

		// Token: 0x04005BA4 RID: 23460
		public ushort properUtype;

		// Token: 0x04005BA5 RID: 23461
		public uint properFlags;

		// Token: 0x04005BA6 RID: 23462
		public short aliasID = -1;

		// Token: 0x04005BA7 RID: 23463
		public object defaultVal;

		// Token: 0x02000FE9 RID: 4073
		public enum EntityDataFlags
		{
			// Token: 0x04005BA9 RID: 23465
			ED_FLAG_UNKOWN,
			// Token: 0x04005BAA RID: 23466
			ED_FLAG_CELL_PUBLIC,
			// Token: 0x04005BAB RID: 23467
			ED_FLAG_CELL_PRIVATE,
			// Token: 0x04005BAC RID: 23468
			ED_FLAG_ALL_CLIENTS = 4,
			// Token: 0x04005BAD RID: 23469
			ED_FLAG_CELL_PUBLIC_AND_OWN = 8,
			// Token: 0x04005BAE RID: 23470
			ED_FLAG_OWN_CLIENT = 16,
			// Token: 0x04005BAF RID: 23471
			ED_FLAG_BASE_AND_CLIENT = 32,
			// Token: 0x04005BB0 RID: 23472
			ED_FLAG_BASE = 64,
			// Token: 0x04005BB1 RID: 23473
			ED_FLAG_OTHER_CLIENTS = 128
		}
	}
}
