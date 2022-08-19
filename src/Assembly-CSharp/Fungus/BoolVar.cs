using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000EC2 RID: 3778
	[Serializable]
	public class BoolVar
	{
		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x06006AC7 RID: 27335 RVA: 0x00293F83 File Offset: 0x00292183
		// (set) Token: 0x06006AC8 RID: 27336 RVA: 0x00293F8B File Offset: 0x0029218B
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

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x06006AC9 RID: 27337 RVA: 0x00293F94 File Offset: 0x00292194
		// (set) Token: 0x06006ACA RID: 27338 RVA: 0x00293F9C File Offset: 0x0029219C
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

		// Token: 0x04005A09 RID: 23049
		[SerializeField]
		protected string key;

		// Token: 0x04005A0A RID: 23050
		[SerializeField]
		protected bool value;
	}
}
