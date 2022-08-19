using System;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000BBE RID: 3006
	public struct DIRECTION3D
	{
		// Token: 0x060053D4 RID: 21460 RVA: 0x00233ED6 File Offset: 0x002320D6
		private DIRECTION3D(Vector3 value)
		{
			this.value = value;
		}

		// Token: 0x060053D5 RID: 21461 RVA: 0x00233EDF File Offset: 0x002320DF
		public static implicit operator Vector3(DIRECTION3D value)
		{
			return value.value;
		}

		// Token: 0x060053D6 RID: 21462 RVA: 0x00233EE7 File Offset: 0x002320E7
		public static implicit operator DIRECTION3D(Vector3 value)
		{
			return new DIRECTION3D(value);
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x060053D7 RID: 21463 RVA: 0x00233EEF File Offset: 0x002320EF
		// (set) Token: 0x060053D8 RID: 21464 RVA: 0x00233EFC File Offset: 0x002320FC
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

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x060053D9 RID: 21465 RVA: 0x00233F0A File Offset: 0x0023210A
		// (set) Token: 0x060053DA RID: 21466 RVA: 0x00233F17 File Offset: 0x00232117
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

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x060053DB RID: 21467 RVA: 0x00233F25 File Offset: 0x00232125
		// (set) Token: 0x060053DC RID: 21468 RVA: 0x00233F32 File Offset: 0x00232132
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

		// Token: 0x0400504A RID: 20554
		private Vector3 value;
	}
}
