using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E50 RID: 3664
	[CommandInfo("Flow", "Stop Flowchart", "Stops execution of all Blocks in a Flowchart", 0)]
	[AddComponentMenu("")]
	public class StopFlowchart : Command
	{
		// Token: 0x06006700 RID: 26368 RVA: 0x00288724 File Offset: 0x00286924
		public override void OnEnter()
		{
			Flowchart flowchart = this.GetFlowchart();
			for (int i = 0; i < this.targetFlowcharts.Count; i++)
			{
				this.targetFlowcharts[i].StopAllBlocks();
			}
			if (this.stopParentFlowchart)
			{
				flowchart.StopAllBlocks();
			}
			this.Continue();
		}

		// Token: 0x06006701 RID: 26369 RVA: 0x00288773 File Offset: 0x00286973
		public override bool IsReorderableArray(string propertyName)
		{
			return propertyName == "targetFlowcharts";
		}

		// Token: 0x06006702 RID: 26370 RVA: 0x0027D3DB File Offset: 0x0027B5DB
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x04005825 RID: 22565
		[Tooltip("Stop all executing Blocks in the Flowchart that contains this command")]
		[SerializeField]
		protected bool stopParentFlowchart;

		// Token: 0x04005826 RID: 22566
		[Tooltip("Stop all executing Blocks in a list of target Flowcharts")]
		[SerializeField]
		protected List<Flowchart> targetFlowcharts = new List<Flowchart>();
	}
}
