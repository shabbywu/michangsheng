using System;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000BBD RID: 3005
	public struct POSITION3D
	{
		// Token: 0x060053CB RID: 21451 RVA: 0x00233E6C File Offset: 0x0023206C
		private POSITION3D(Vector3 value)
		{
			this.value = value;
		}

		// Token: 0x060053CC RID: 21452 RVA: 0x00233E75 File Offset: 0x00232075
		public static implicit operator Vector3(POSITION3D value)
		{
			return value.value;
		}

		// Token: 0x060053CD RID: 21453 RVA: 0x00233E7D File Offset: 0x0023207D
		public static implicit operator POSITION3D(Vector3 value)
		{
			return new POSITION3D(value);
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x060053CE RID: 21454 RVA: 0x00233E85 File Offset: 0x00232085
		// (set) Token: 0x060053CF RID: 21455 RVA: 0x00233E92 File Offset: 0x00232092
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

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x060053D0 RID: 21456 RVA: 0x00233EA0 File Offset: 0x002320A0
		// (set) Token: 0x060053D1 RID: 21457 RVA: 0x00233EAD File Offset: 0x002320AD
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

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x060053D2 RID: 21458 RVA: 0x00233EBB File Offset: 0x002320BB
		// (set) Token: 0x060053D3 RID: 21459 RVA: 0x00233EC8 File Offset: 0x002320C8
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

		// Token: 0x04005049 RID: 20553
		private Vector3 value;
	}
}
