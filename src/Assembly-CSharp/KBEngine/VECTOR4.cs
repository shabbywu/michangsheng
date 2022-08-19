using System;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000BAE RID: 2990
	public struct VECTOR4
	{
		// Token: 0x0600537A RID: 21370 RVA: 0x00233C74 File Offset: 0x00231E74
		private VECTOR4(Vector4 value)
		{
			this.value = value;
		}

		// Token: 0x0600537B RID: 21371 RVA: 0x00233C7D File Offset: 0x00231E7D
		public static implicit operator Vector4(VECTOR4 value)
		{
			return value.value;
		}

		// Token: 0x0600537C RID: 21372 RVA: 0x00233C85 File Offset: 0x00231E85
		public static implicit operator VECTOR4(Vector4 value)
		{
			return new VECTOR4(value);
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x0600537D RID: 21373 RVA: 0x00233C8D File Offset: 0x00231E8D
		// (set) Token: 0x0600537E RID: 21374 RVA: 0x00233C9A File Offset: 0x00231E9A
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

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x0600537F RID: 21375 RVA: 0x00233CA8 File Offset: 0x00231EA8
		// (set) Token: 0x06005380 RID: 21376 RVA: 0x00233CB5 File Offset: 0x00231EB5
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

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06005381 RID: 21377 RVA: 0x00233CC3 File Offset: 0x00231EC3
		// (set) Token: 0x06005382 RID: 21378 RVA: 0x00233CD0 File Offset: 0x00231ED0
		public float z
		{
			get
			{
				return this.value.z;
			}
			set
			{
				this.value.z = value;
			}
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06005383 RID: 21379 RVA: 0x00233CDE File Offset: 0x00231EDE
		// (set) Token: 0x06005384 RID: 21380 RVA: 0x00233CEB File Offset: 0x00231EEB
		public float w
		{
			get
			{
				return this.value.w;
			}
			set
			{
				this.value.w = value;
			}
		}

		// Token: 0x0400503A RID: 20538
		private Vector4 value;
	}
}
