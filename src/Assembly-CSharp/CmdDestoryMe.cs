using System;
using Fungus;
using UnityEngine;

// Token: 0x020005F9 RID: 1529
[CommandInfo("YSTool", "销毁自身", "销毁自身", 0)]
[AddComponentMenu("")]
public class CmdDestoryMe : Command
{
	// Token: 0x06002657 RID: 9815 RVA: 0x0012E498 File Offset: 0x0012C698
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
