using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200135F RID: 4959
	[Serializable]
	public class FloatVar
	{
		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x0600785D RID: 30813 RVA: 0x00051C65 File Offset: 0x0004FE65
		// (set) Token: 0x0600785E RID: 30814 RVA: 0x00051C6D File Offset: 0x0004FE6D
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

		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x0600785F RID: 30815 RVA: 0x00051C76 File Offset: 0x0004FE76
		// (set) Token: 0x06007860 RID: 30816 RVA: 0x00051C7E File Offset: 0x0004FE7E
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

		// Token: 0x04006866 RID: 26726
		[SerializeField]
		protected string key;

		// Token: 0x04006867 RID: 26727
		[SerializeField]
		protected float value;
	}
}
