using System;

namespace KBEngine
{
	// Token: 0x02000F48 RID: 3912
	public struct ENTITY_STATE
	{
		// Token: 0x06005E39 RID: 24121 RVA: 0x00042137 File Offset: 0x00040337
		private ENTITY_STATE(sbyte value)
		{
			this.value = value;
		}

		// Token: 0x06005E3A RID: 24122 RVA: 0x00042140 File Offset: 0x00040340
		public static implicit operator sbyte(ENTITY_STATE value)
		{
			return value.value;
		}

		// Token: 0x06005E3B RID: 24123 RVA: 0x00042148 File Offset: 0x00040348
		public static implicit operator ENTITY_STATE(sbyte value)
		{
			return new ENTITY_STATE(value);
		}

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x06005E3C RID: 24124 RVA: 0x00041AF9 File Offset: 0x0003FCF9
		public static sbyte MaxValue
		{
			get
			{
				return sbyte.MaxValue;
			}
		}

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x06005E3D RID: 24125 RVA: 0x00041AFD File Offset: 0x0003FCFD
		public static sbyte MinValue
		{
			get
			{
				return sbyte.MinValue;
			}
		}

		// Token: 0x04005AF2 RID: 23282
		private sbyte value;
	}
}
