using System;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000F30 RID: 3888
	public struct VECTOR3
	{
		// Token: 0x06005DAF RID: 23983 RVA: 0x00041D6B File Offset: 0x0003FF6B
		private VECTOR3(Vector3 value)
		{
			this.value = value;
		}

		// Token: 0x06005DB0 RID: 23984 RVA: 0x00041D74 File Offset: 0x0003FF74
		public static implicit operator Vector3(VECTOR3 value)
		{
			return value.value;
		}

		// Token: 0x06005DB1 RID: 23985 RVA: 0x00041D7C File Offset: 0x0003FF7C
		public static implicit operator VECTOR3(Vector3 value)
		{
			return new VECTOR3(value);
		}

		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x06005DB2 RID: 23986 RVA: 0x00041D84 File Offset: 0x0003FF84
		// (set) Token: 0x06005DB3 RID: 23987 RVA: 0x00041D91 File Offset: 0x0003FF91
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

		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x06005DB4 RID: 23988 RVA: 0x00041D9F File Offset: 0x0003FF9F
		// (set) Token: 0x06005DB5 RID: 23989 RVA: 0x00041DAC File Offset: 0x0003FFAC
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

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x06005DB6 RID: 23990 RVA: 0x00041DBA File Offset: 0x0003FFBA
		// (set) Token: 0x06005DB7 RID: 23991 RVA: 0x00041DC7 File Offset: 0x0003FFC7
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

		// Token: 0x04005ADA RID: 23258
		private Vector3 value;
	}
}
