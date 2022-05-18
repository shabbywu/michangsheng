using System;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000F40 RID: 3904
	public struct POSITION3D
	{
		// Token: 0x06005E09 RID: 24073 RVA: 0x00041FCD File Offset: 0x000401CD
		private POSITION3D(Vector3 value)
		{
			this.value = value;
		}

		// Token: 0x06005E0A RID: 24074 RVA: 0x00041FD6 File Offset: 0x000401D6
		public static implicit operator Vector3(POSITION3D value)
		{
			return value.value;
		}

		// Token: 0x06005E0B RID: 24075 RVA: 0x00041FDE File Offset: 0x000401DE
		public static implicit operator POSITION3D(Vector3 value)
		{
			return new POSITION3D(value);
		}

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x06005E0C RID: 24076 RVA: 0x00041FE6 File Offset: 0x000401E6
		// (set) Token: 0x06005E0D RID: 24077 RVA: 0x00041FF3 File Offset: 0x000401F3
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

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x06005E0E RID: 24078 RVA: 0x00042001 File Offset: 0x00040201
		// (set) Token: 0x06005E0F RID: 24079 RVA: 0x0004200E File Offset: 0x0004020E
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

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x06005E10 RID: 24080 RVA: 0x0004201C File Offset: 0x0004021C
		// (set) Token: 0x06005E11 RID: 24081 RVA: 0x00042029 File Offset: 0x00040229
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

		// Token: 0x04005AEA RID: 23274
		private Vector3 value;
	}
}
