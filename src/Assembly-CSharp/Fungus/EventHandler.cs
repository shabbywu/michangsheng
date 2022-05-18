using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x020012CC RID: 4812
	[RequireComponent(typeof(Block))]
	[RequireComponent(typeof(Flowchart))]
	[AddComponentMenu("")]
	public class EventHandler : MonoBehaviour
	{
		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x060074C8 RID: 29896 RVA: 0x0004FBE0 File Offset: 0x0004DDE0
		// (set) Token: 0x060074C9 RID: 29897 RVA: 0x0004FBE8 File Offset: 0x0004DDE8
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

		// Token: 0x060074CA RID: 29898 RVA: 0x002AE508 File Offset: 0x002AC708
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

		// Token: 0x060074CB RID: 29899 RVA: 0x00032110 File Offset: 0x00030310
		public virtual string GetSummary()
		{
			return "";
		}

		// Token: 0x0400665C RID: 26204
		[HideInInspector]
		[FormerlySerializedAs("parentSequence")]
		[SerializeField]
		protected Block parentBlock;
	}
}
