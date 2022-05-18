using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200135D RID: 4957
	[Serializable]
	public class StringVar
	{
		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x06007853 RID: 30803 RVA: 0x00051C21 File Offset: 0x0004FE21
		// (set) Token: 0x06007854 RID: 30804 RVA: 0x00051C29 File Offset: 0x0004FE29
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

		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x06007855 RID: 30805 RVA: 0x00051C32 File Offset: 0x0004FE32
		// (set) Token: 0x06007856 RID: 30806 RVA: 0x00051C3A File Offset: 0x0004FE3A
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

		// Token: 0x04006862 RID: 26722
		[SerializeField]
		protected string key;

		// Token: 0x04006863 RID: 26723
		[SerializeField]
		protected string value;
	}
}
