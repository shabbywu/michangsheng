using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020012A1 RID: 4769
	[CommandInfo("Flow", "Stop Flowchart", "Stops execution of all Blocks in a Flowchart", 0)]
	[AddComponentMenu("")]
	public class StopFlowchart : Command
	{
		// Token: 0x0600738E RID: 29582 RVA: 0x002AB4C0 File Offset: 0x002A96C0
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

		// Token: 0x0600738F RID: 29583 RVA: 0x0004ED69 File Offset: 0x0004CF69
		public override bool IsReorderableArray(string propertyName)
		{
			return propertyName == "targetFlowcharts";
		}

		// Token: 0x06007390 RID: 29584 RVA: 0x0004C5E0 File Offset: 0x0004A7E0
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x04006569 RID: 25961
		[Tooltip("Stop all executing Blocks in the Flowchart that contains this command")]
		[SerializeField]
		protected bool stopParentFlowchart;

		// Token: 0x0400656A RID: 25962
		[Tooltip("Stop all executing Blocks in a list of target Flowcharts")]
		[SerializeField]
		protected List<Flowchart> targetFlowcharts = new List<Flowchart>();
	}
}
