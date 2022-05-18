using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020012D0 RID: 4816
	[AddComponentMenu("")]
	public class FungusState : MonoBehaviour
	{
		// Token: 0x17000AD9 RID: 2777
		// (get) Token: 0x06007542 RID: 30018 RVA: 0x0004FF88 File Offset: 0x0004E188
		// (set) Token: 0x06007543 RID: 30019 RVA: 0x0004FF90 File Offset: 0x0004E190
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

		// Token: 0x04006686 RID: 26246
		[SerializeField]
		protected Flowchart selectedFlowchart;
	}
}
