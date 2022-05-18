using System;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000F41 RID: 3905
	public struct DIRECTION3D
	{
		// Token: 0x06005E12 RID: 24082 RVA: 0x00042037 File Offset: 0x00040237
		private DIRECTION3D(Vector3 value)
		{
			this.value = value;
		}

		// Token: 0x06005E13 RID: 24083 RVA: 0x00042040 File Offset: 0x00040240
		public static implicit operator Vector3(DIRECTION3D value)
		{
			return value.value;
		}

		// Token: 0x06005E14 RID: 24084 RVA: 0x00042048 File Offset: 0x00040248
		public static implicit operator DIRECTION3D(Vector3 value)
		{
			return new DIRECTION3D(value);
		}

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x06005E15 RID: 24085 RVA: 0x00042050 File Offset: 0x00040250
		// (set) Token: 0x06005E16 RID: 24086 RVA: 0x0004205D File Offset: 0x0004025D
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

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x06005E17 RID: 24087 RVA: 0x0004206B File Offset: 0x0004026B
		// (set) Token: 0x06005E18 RID: 24088 RVA: 0x00042078 File Offset: 0x00040278
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

		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x06005E19 RID: 24089 RVA: 0x00042086 File Offset: 0x00040286
		// (set) Token: 0x06005E1A RID: 24090 RVA: 0x00042093 File Offset: 0x00040293
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

		// Token: 0x04005AEB RID: 23275
		private Vector3 value;
	}
}
