using System;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000BAC RID: 2988
	public struct VECTOR2
	{
		// Token: 0x0600536A RID: 21354 RVA: 0x00233BBB File Offset: 0x00231DBB
		private VECTOR2(Vector2 value)
		{
			this.value = value;
		}

		// Token: 0x0600536B RID: 21355 RVA: 0x00233BC4 File Offset: 0x00231DC4
		public static implicit operator Vector2(VECTOR2 value)
		{
			return value.value;
		}

		// Token: 0x0600536C RID: 21356 RVA: 0x00233BCC File Offset: 0x00231DCC
		public static implicit operator VECTOR2(Vector2 value)
		{
			return new VECTOR2(value);
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x0600536D RID: 21357 RVA: 0x00233BD4 File Offset: 0x00231DD4
		// (set) Token: 0x0600536E RID: 21358 RVA: 0x00233BE1 File Offset: 0x00231DE1
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

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x0600536F RID: 21359 RVA: 0x00233BEF File Offset: 0x00231DEF
		// (set) Token: 0x06005370 RID: 21360 RVA: 0x00233BFC File Offset: 0x00231DFC
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

		// Token: 0x04005038 RID: 20536
		private Vector2 value;
	}
}
