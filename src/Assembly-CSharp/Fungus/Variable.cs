using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E8D RID: 3725
	[RequireComponent(typeof(Flowchart))]
	public abstract class Variable : MonoBehaviour
	{
		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x06006989 RID: 27017 RVA: 0x002912E7 File Offset: 0x0028F4E7
		// (set) Token: 0x0600698A RID: 27018 RVA: 0x002912EF File Offset: 0x0028F4EF
		public virtual VariableScope Scope
		{
			get
			{
				return this.scope;
			}
			set
			{
				this.scope = value;
			}
		}

		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x0600698B RID: 27019 RVA: 0x002912F8 File Offset: 0x0028F4F8
		// (set) Token: 0x0600698C RID: 27020 RVA: 0x00291300 File Offset: 0x0028F500
		public virtual string Key
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

		// Token: 0x0600698D RID: 27021
		public abstract void OnReset();

		// Token: 0x04005983 RID: 22915
		[SerializeField]
		protected VariableScope scope;

		// Token: 0x04005984 RID: 22916
		[SerializeField]
		protected string key = "";
	}
}
