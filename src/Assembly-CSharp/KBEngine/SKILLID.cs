using System;

namespace KBEngine
{
	// Token: 0x02000F37 RID: 3895
	public struct SKILLID
	{
		// Token: 0x06005DDC RID: 24028 RVA: 0x00041ED7 File Offset: 0x000400D7
		private SKILLID(int value)
		{
			this.value = value;
		}

		// Token: 0x06005DDD RID: 24029 RVA: 0x00041EE0 File Offset: 0x000400E0
		public static implicit operator int(SKILLID value)
		{
			return value.value;
		}

		// Token: 0x06005DDE RID: 24030 RVA: 0x00041EE8 File Offset: 0x000400E8
		public static implicit operator SKILLID(int value)
		{
			return new SKILLID(value);
		}

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x06005DDF RID: 24031 RVA: 0x00041B41 File Offset: 0x0003FD41
		public static int MaxValue
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x06005DE0 RID: 24032 RVA: 0x00041B48 File Offset: 0x0003FD48
		public static int MinValue
		{
			get
			{
				return int.MinValue;
			}
		}

		// Token: 0x04005AE1 RID: 23265
		private int value;
	}
}
