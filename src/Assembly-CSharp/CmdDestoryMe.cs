using System;
using Fungus;
using UnityEngine;

// Token: 0x02000442 RID: 1090
[CommandInfo("YSTool", "销毁自身", "销毁自身", 0)]
[AddComponentMenu("")]
public class CmdDestoryMe : Command
{
	// Token: 0x06002298 RID: 8856 RVA: 0x000ED658 File Offset: 0x000EB858
	public override void OnEnter()
	{
		Flowchart flowchart = this.GetFlowchart();
		GameObject gameObject;
		if (flowchart.transform.parent != null)
		{
			gameObject = flowchart.transform.parent.gameObject;
		}
		else
		{
			gameObject = flowchart.transform.gameObject;
		}
		clientApp.Inst.DestoryTalk(gameObject);
		this.Continue();
	}
}
