using System;

namespace KBEngine
{
	// Token: 0x02000F3D RID: 3901
	public struct ENTITY_ID
	{
		// Token: 0x06005DFA RID: 24058 RVA: 0x00041F82 File Offset: 0x00040182
		private ENTITY_ID(int value)
		{
			this.value = value;
		}

		// Token: 0x06005DFB RID: 24059 RVA: 0x00041F8B File Offset: 0x0004018B
		public static implicit operator int(ENTITY_ID value)
		{
			return value.value;
		}

		// Token: 0x06005DFC RID: 24060 RVA: 0x00041F93 File Offset: 0x00040193
		public static implicit operator ENTITY_ID(int value)
		{
			return new ENTITY_ID(value);
		}

		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x06005DFD RID: 24061 RVA: 0x00041B41 File Offset: 0x0003FD41
		public static int MaxValue
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x06005DFE RID: 24062 RVA: 0x00041B48 File Offset: 0x0003FD48
		public static int MinValue
		{
			get
			{
				return int.MinValue;
			}
		}

		// Token: 0x04005AE7 RID: 23271
		private int value;
	}
}
