using System.Collections.Generic;
using UnityEngine;

namespace Fungus;

[CommandInfo("Flow", "Stop Flowchart", "Stops execution of all Blocks in a Flowchart", 0)]
[AddComponentMenu("")]
public class StopFlowchart : Command
{
	[Tooltip("Stop all executing Blocks in the Flowchart that contains this command")]
	[SerializeField]
	protected bool stopParentFlowchart;

	[Tooltip("Stop all executing Blocks in a list of target Flowcharts")]
	[SerializeField]
	protected List<Flowchart> targetFlowcharts = new List<Flowchart>();

	public override void OnEnter()
	{
		Flowchart flowchart = GetFlowchart();
		for (int i = 0; i < targetFlowcharts.Count; i++)
		{
			targetFlowcharts[i].StopAllBlocks();
		}
		if (stopParentFlowchart)
		{
			flowchart.StopAllBlocks();
		}
		Continue();
	}

	public override bool IsReorderableArray(string propertyName)
	{
		if (propertyName == "targetFlowcharts")
		{
			return true;
		}
		return false;
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)235, (byte)191, (byte)217, byte.MaxValue));
	}
}
