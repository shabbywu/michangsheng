using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E72 RID: 3698
	[AddComponentMenu("")]
	public class FungusState : MonoBehaviour
	{
		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x06006889 RID: 26761 RVA: 0x0028D33E File Offset: 0x0028B53E
		// (set) Token: 0x0600688A RID: 26762 RVA: 0x0028D346 File Offset: 0x0028B546
		public virtual Flowchart SelectedFlowchart
		{
			get
			{
				return this.selectedFlowchart;
			}
			set
			{
				this.selectedFlowchart = value;
			}
		}

		// Token: 0x040058E8 RID: 22760
		[SerializeField]
		protected Flowchart selectedFlowchart;
	}
}
