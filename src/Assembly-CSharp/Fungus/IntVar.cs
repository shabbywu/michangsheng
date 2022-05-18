using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200135E RID: 4958
	[Serializable]
	public class IntVar
	{
		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x06007858 RID: 30808 RVA: 0x00051C43 File Offset: 0x0004FE43
		// (set) Token: 0x06007859 RID: 30809 RVA: 0x00051C4B File Offset: 0x0004FE4B
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

		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x0600785A RID: 30810 RVA: 0x00051C54 File Offset: 0x0004FE54
		// (set) Token: 0x0600785B RID: 30811 RVA: 0x00051C5C File Offset: 0x0004FE5C
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

		// Token: 0x04006864 RID: 26724
		[SerializeField]
		protected string key;

		// Token: 0x04006865 RID: 26725
		[SerializeField]
		protected int value;
	}
}
