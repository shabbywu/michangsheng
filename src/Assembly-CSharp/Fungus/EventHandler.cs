using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02000E6F RID: 3695
	[RequireComponent(typeof(Block))]
	[RequireComponent(typeof(Flowchart))]
	[AddComponentMenu("")]
	public class EventHandler : MonoBehaviour
	{
		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x06006816 RID: 26646 RVA: 0x0028BBD8 File Offset: 0x00289DD8
		// (set) Token: 0x06006817 RID: 26647 RVA: 0x0028BBE0 File Offset: 0x00289DE0
		public virtual Block ParentBlock
		{
			get
			{
				return this.parentBlock;
			}
			set
			{
				this.parentBlock = value;
			}
		}

		// Token: 0x06006818 RID: 26648 RVA: 0x0028BBEC File Offset: 0x00289DEC
		public virtual bool ExecuteBlock()
		{
			if (this.ParentBlock == null)
			{
				return false;
			}
			if (this.ParentBlock._EventHandler != this)
			{
				return false;
			}
			Flowchart flowchart = this.ParentBlock.GetFlowchart();
			if (flowchart == null || !flowchart.isActiveAndEnabled)
			{
				return false;
			}
			if (flowchart.SelectedBlock == null)
			{
				flowchart.SelectedBlock = this.ParentBlock;
			}
			return flowchart.ExecuteBlock(this.ParentBlock, 0, null);
		}

		// Token: 0x06006819 RID: 26649 RVA: 0x001D84A0 File Offset: 0x001D66A0
		public virtual string GetSummary()
		{
			return "";
		}

		// Token: 0x040058C4 RID: 22724
		[HideInInspector]
		[FormerlySerializedAs("parentSequence")]
		[SerializeField]
		protected Block parentBlock;
	}
}
