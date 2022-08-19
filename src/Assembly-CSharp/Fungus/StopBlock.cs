using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E4F RID: 3663
	[CommandInfo("Flow", "Stop Block", "Stops executing the named Block", 0)]
	public class StopBlock : Command
	{
		// Token: 0x060066FB RID: 26363 RVA: 0x00288660 File Offset: 0x00286860
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

		// Token: 0x060066FC RID: 26364 RVA: 0x002886DE File Offset: 0x002868DE
		public override string GetSummary()
		{
			return this.blockName;
		}

		// Token: 0x060066FD RID: 26365 RVA: 0x0027D1B6 File Offset: 0x0027B3B6
		public override Color GetButtonColor()
		{
			return new Color32(253, 253, 150, byte.MaxValue);
		}

		// Token: 0x060066FE RID: 26366 RVA: 0x002886EB File Offset: 0x002868EB
		public override bool HasReference(Variable variable)
		{
			return this.blockName.stringRef == variable || base.HasReference(variable);
		}

		// Token: 0x04005823 RID: 22563
		[Tooltip("Flowchart containing the Block. If none is specified, the parent Flowchart is used.")]
		[SerializeField]
		protected Flowchart flowchart;

		// Token: 0x04005824 RID: 22564
		[Tooltip("Name of the Block to stop")]
		[SerializeField]
		protected StringData blockName = new StringData("");
	}
}
