using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000EC0 RID: 3776
	[Serializable]
	public class IntVar
	{
		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x06006ABD RID: 27325 RVA: 0x00293F3F File Offset: 0x0029213F
		// (set) Token: 0x06006ABE RID: 27326 RVA: 0x00293F47 File Offset: 0x00292147
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

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x06006ABF RID: 27327 RVA: 0x00293F50 File Offset: 0x00292150
		// (set) Token: 0x06006AC0 RID: 27328 RVA: 0x00293F58 File Offset: 0x00292158
		public int Value
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

		// Token: 0x04005A05 RID: 23045
		[SerializeField]
		protected string key;

		// Token: 0x04005A06 RID: 23046
		[SerializeField]
		protected int value;
	}
}
