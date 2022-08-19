using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000EC1 RID: 3777
	[Serializable]
	public class FloatVar
	{
		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x06006AC2 RID: 27330 RVA: 0x00293F61 File Offset: 0x00292161
		// (set) Token: 0x06006AC3 RID: 27331 RVA: 0x00293F69 File Offset: 0x00292169
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

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x06006AC4 RID: 27332 RVA: 0x00293F72 File Offset: 0x00292172
		// (set) Token: 0x06006AC5 RID: 27333 RVA: 0x00293F7A File Offset: 0x0029217A
		public float Value
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

		// Token: 0x04005A07 RID: 23047
		[SerializeField]
		protected string key;

		// Token: 0x04005A08 RID: 23048
		[SerializeField]
		protected float value;
	}
}
