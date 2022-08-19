using System;

namespace KBEngine
{
	// Token: 0x02000BC7 RID: 3015
	public struct ENTITY_FORBIDS
	{
		// Token: 0x06005405 RID: 21509 RVA: 0x00234008 File Offset: 0x00232208
		private ENTITY_FORBIDS(int value)
		{
			this.value = value;
		}

		// Token: 0x06005406 RID: 21510 RVA: 0x00234011 File Offset: 0x00232211
		public static implicit operator int(ENTITY_FORBIDS value)
		{
			return value.value;
		}

		// Token: 0x06005407 RID: 21511 RVA: 0x00234019 File Offset: 0x00232219
		public static implicit operator ENTITY_FORBIDS(int value)
		{
			return new ENTITY_FORBIDS(value);
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06005408 RID: 21512 RVA: 0x002339E0 File Offset: 0x00231BE0
		public static int MaxValue
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06005409 RID: 21513 RVA: 0x002339E7 File Offset: 0x00231BE7
		public static int MinValue
		{
			get
			{
				return int.MinValue;
			}
		}

		// Token: 0x04005053 RID: 20563
		private int value;
	}
}
