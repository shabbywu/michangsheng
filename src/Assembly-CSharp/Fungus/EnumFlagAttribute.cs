using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200130D RID: 4877
	public class EnumFlagAttribute : PropertyAttribute
	{
		// Token: 0x06007700 RID: 30464 RVA: 0x000247E0 File Offset: 0x000229E0
		public EnumFlagAttribute()
		{
		}

		// Token: 0x06007701 RID: 30465 RVA: 0x00050FB1 File Offset: 0x0004F1B1
		public EnumFlagAttribute(string name)
		{
			this.enumName = name;
		}

		// Token: 0x040067CF RID: 26575
		public string enumName;
	}
}
