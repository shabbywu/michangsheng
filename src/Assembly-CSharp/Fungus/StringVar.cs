using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000EBF RID: 3775
	[Serializable]
	public class StringVar
	{
		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x06006AB8 RID: 27320 RVA: 0x00293F1D File Offset: 0x0029211D
		// (set) Token: 0x06006AB9 RID: 27321 RVA: 0x00293F25 File Offset: 0x00292125
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

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x06006ABA RID: 27322 RVA: 0x00293F2E File Offset: 0x0029212E
		// (set) Token: 0x06006ABB RID: 27323 RVA: 0x00293F36 File Offset: 0x00292136
		public string Value
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

		// Token: 0x04005A03 RID: 23043
		[SerializeField]
		protected string key;

		// Token: 0x04005A04 RID: 23044
		[SerializeField]
		protected string value;
	}
}
