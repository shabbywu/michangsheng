using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001360 RID: 4960
	[Serializable]
	public class BoolVar
	{
		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x06007862 RID: 30818 RVA: 0x00051C87 File Offset: 0x0004FE87
		// (set) Token: 0x06007863 RID: 30819 RVA: 0x00051C8F File Offset: 0x0004FE8F
		public string Key
		{
			get
			{
				return this.key;
			}
			set
			{
				this.key = value;
			}
		}

		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x06007864 RID: 30820 RVA: 0x00051C98 File Offset: 0x0004FE98
		// (set) Token: 0x06007865 RID: 30821 RVA: 0x00051CA0 File Offset: 0x0004FEA0
		public bool Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}

		// Token: 0x04006868 RID: 26728
		[SerializeField]
		protected string key;

		// Token: 0x04006869 RID: 26729
		[SerializeField]
		protected bool value;
	}
}
