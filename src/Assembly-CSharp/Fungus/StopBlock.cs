using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020012A0 RID: 4768
	[CommandInfo("Flow", "Stop Block", "Stops executing the named Block", 0)]
	public class StopBlock : Command
	{
		// Token: 0x06007389 RID: 29577 RVA: 0x002AB440 File Offset: 0x002A9640
		public override void OnEnter()
		{
			if (this.blockName.Value == "")
			{
				this.Continue();
			}
			if (this.flowchart == null)
			{
				this.flowchart = this.GetFlowchart();
			}
			Block block = this.flowchart.FindBlock(this.blockName.Value);
			if (block == null || !block.IsExecuting())
			{
				this.Continue();
			}
			block.Stop();
			this.Continue();
		}

		// Token: 0x0600738A RID: 29578 RVA: 0x0004ED26 File Offset: 0x0004CF26
		public override string GetSummary()
		{
			return this.blockName;
		}

		// Token: 0x0600738B RID: 29579 RVA: 0x0004C5A3 File Offset: 0x0004A7A3
		public override Color GetButtonColor()
		{
			return new Color32(253, 253, 150, byte.MaxValue);
		}

		// Token: 0x0600738C RID: 29580 RVA: 0x0004ED33 File Offset: 0x0004CF33
		public override bool HasReference(Variable variable)
		{
			return this.blockName.stringRef == variable || base.HasReference(variable);
		}

		// Token: 0x04006567 RID: 25959
		[Tooltip("Flowchart containing the Block. If none is specified, the parent Flowchart is used.")]
		[SerializeField]
		protected Flowchart flowchart;

		// Token: 0x04006568 RID: 25960
		[Tooltip("Name of the Block to stop")]
		[SerializeField]
		protected StringData blockName = new StringData("");
	}
}
