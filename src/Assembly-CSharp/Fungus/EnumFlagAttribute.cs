using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E94 RID: 3732
	public class EnumFlagAttribute : PropertyAttribute
	{
		// Token: 0x060069D5 RID: 27093 RVA: 0x0013FF05 File Offset: 0x0013E105
		public EnumFlagAttribute()
		{
		}

		// Token: 0x060069D6 RID: 27094 RVA: 0x00292190 File Offset: 0x00290390
		public EnumFlagAttribute(string name)
		{
			this.enumName = name;
		}

		// Token: 0x040059C0 RID: 22976
		public string enumName;
	}
}
