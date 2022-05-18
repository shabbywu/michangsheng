using System;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000F31 RID: 3889
	public struct VECTOR4
	{
		// Token: 0x06005DB8 RID: 23992 RVA: 0x00041DD5 File Offset: 0x0003FFD5
		private VECTOR4(Vector4 value)
		{
			this.value = value;
		}

		// Token: 0x06005DB9 RID: 23993 RVA: 0x00041DDE File Offset: 0x0003FFDE
		public static implicit operator Vector4(VECTOR4 value)
		{
			return value.value;
		}

		// Token: 0x06005DBA RID: 23994 RVA: 0x00041DE6 File Offset: 0x0003FFE6
		public static implicit operator VECTOR4(Vector4 value)
		{
			return new VECTOR4(value);
		}

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x06005DBB RID: 23995 RVA: 0x00041DEE File Offset: 0x0003FFEE
		// (set) Token: 0x06005DBC RID: 23996 RVA: 0x00041DFB File Offset: 0x0003FFFB
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

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x06005DBD RID: 23997 RVA: 0x00041E09 File Offset: 0x00040009
		// (set) Token: 0x06005DBE RID: 23998 RVA: 0x00041E16 File Offset: 0x00040016
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

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x06005DBF RID: 23999 RVA: 0x00041E24 File Offset: 0x00040024
		// (set) Token: 0x06005DC0 RID: 24000 RVA: 0x00041E31 File Offset: 0x00040031
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

		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x06005DC1 RID: 24001 RVA: 0x00041E3F File Offset: 0x0004003F
		// (set) Token: 0x06005DC2 RID: 24002 RVA: 0x00041E4C File Offset: 0x0004004C
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

		// Token: 0x04005ADB RID: 23259
		private Vector4 value;
	}
}
