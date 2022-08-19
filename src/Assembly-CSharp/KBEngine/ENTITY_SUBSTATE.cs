using System;

namespace KBEngine
{
	// Token: 0x02000BC6 RID: 3014
	public struct ENTITY_SUBSTATE
	{
		// Token: 0x06005400 RID: 21504 RVA: 0x00233FEF File Offset: 0x002321EF
		private ENTITY_SUBSTATE(byte value)
		{
			this.value = value;
		}

		// Token: 0x06005401 RID: 21505 RVA: 0x00233FF8 File Offset: 0x002321F8
		public static implicit operator byte(ENTITY_SUBSTATE value)
		{
			return value.value;
		}

		// Token: 0x06005402 RID: 21506 RVA: 0x00234000 File Offset: 0x00232200
		public static implicit operator ENTITY_SUBSTATE(byte value)
		{
			return new ENTITY_SUBSTATE(value);
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06005403 RID: 21507 RVA: 0x0023391B File Offset: 0x00231B1B
		public static byte MaxValue
		{
			get
			{
				return byte.MaxValue;
			}
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06005404 RID: 21508 RVA: 0x0000280F File Offset: 0x00000A0F
		public static byte MinValue
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x04005052 RID: 20562
		private byte value;
	}
}
