using System;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000F2F RID: 3887
	public struct VECTOR2
	{
		// Token: 0x06005DA8 RID: 23976 RVA: 0x00041D1C File Offset: 0x0003FF1C
		private VECTOR2(Vector2 value)
		{
			this.value = value;
		}

		// Token: 0x06005DA9 RID: 23977 RVA: 0x00041D25 File Offset: 0x0003FF25
		public static implicit operator Vector2(VECTOR2 value)
		{
			return value.value;
		}

		// Token: 0x06005DAA RID: 23978 RVA: 0x00041D2D File Offset: 0x0003FF2D
		public static implicit operator VECTOR2(Vector2 value)
		{
			return new VECTOR2(value);
		}

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x06005DAB RID: 23979 RVA: 0x00041D35 File Offset: 0x0003FF35
		// (set) Token: 0x06005DAC RID: 23980 RVA: 0x00041D42 File Offset: 0x0003FF42
		public float x
		{
			get
			{
				return this.value.x;
			}
			set
			{
				this.value.x = value;
			}
		}

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x06005DAD RID: 23981 RVA: 0x00041D50 File Offset: 0x0003FF50
		// (set) Token: 0x06005DAE RID: 23982 RVA: 0x00041D5D File Offset: 0x0003FF5D
		public float y
		{
			get
			{
				return this.value.y;
			}
			set
			{
				this.value.y = value;
			}
		}

		// Token: 0x04005AD9 RID: 23257
		private Vector2 value;
	}
}
