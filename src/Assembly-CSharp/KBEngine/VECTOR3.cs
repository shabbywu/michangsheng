using System;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000BAD RID: 2989
	public struct VECTOR3
	{
		// Token: 0x06005371 RID: 21361 RVA: 0x00233C0A File Offset: 0x00231E0A
		private VECTOR3(Vector3 value)
		{
			this.value = value;
		}

		// Token: 0x06005372 RID: 21362 RVA: 0x00233C13 File Offset: 0x00231E13
		public static implicit operator Vector3(VECTOR3 value)
		{
			return value.value;
		}

		// Token: 0x06005373 RID: 21363 RVA: 0x00233C1B File Offset: 0x00231E1B
		public static implicit operator VECTOR3(Vector3 value)
		{
			return new VECTOR3(value);
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x06005374 RID: 21364 RVA: 0x00233C23 File Offset: 0x00231E23
		// (set) Token: 0x06005375 RID: 21365 RVA: 0x00233C30 File Offset: 0x00231E30
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

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06005376 RID: 21366 RVA: 0x00233C3E File Offset: 0x00231E3E
		// (set) Token: 0x06005377 RID: 21367 RVA: 0x00233C4B File Offset: 0x00231E4B
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

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06005378 RID: 21368 RVA: 0x00233C59 File Offset: 0x00231E59
		// (set) Token: 0x06005379 RID: 21369 RVA: 0x00233C66 File Offset: 0x00231E66
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

		// Token: 0x04005039 RID: 20537
		private Vector3 value;
	}
}
