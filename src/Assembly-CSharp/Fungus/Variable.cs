using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020012FE RID: 4862
	[RequireComponent(typeof(Flowchart))]
	public abstract class Variable : MonoBehaviour
	{
		// Token: 0x17000B0E RID: 2830
		// (get) Token: 0x06007688 RID: 30344 RVA: 0x00050AEA File Offset: 0x0004ECEA
		// (set) Token: 0x06007689 RID: 30345 RVA: 0x00050AF2 File Offset: 0x0004ECF2
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

		// Token: 0x17000B0F RID: 2831
		// (get) Token: 0x0600768A RID: 30346 RVA: 0x00050AFB File Offset: 0x0004ECFB
		// (set) Token: 0x0600768B RID: 30347 RVA: 0x00050B03 File Offset: 0x0004ED03
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

		// Token: 0x0600768C RID: 30348
		public abstract void OnReset();

		// Token: 0x04006764 RID: 26468
		[SerializeField]
		protected VariableScope scope;

		// Token: 0x04006765 RID: 26469
		[SerializeField]
		protected string key = "";
	}
}
