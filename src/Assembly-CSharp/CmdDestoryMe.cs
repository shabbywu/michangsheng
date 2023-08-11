using Fungus;
using UnityEngine;

[CommandInfo("YSTool", "销毁自身", "销毁自身", 0)]
[AddComponentMenu("")]
public class CmdDestoryMe : Command
{
	public override void OnEnter()
	{
		Flowchart flowchart = GetFlowchart();
		GameObject obj = ((!((Object)(object)((Component)flowchart).transform.parent != (Object)null)) ? ((Component)((Component)flowchart).transform).gameObject : ((Component)((Component)flowchart).transform.parent).gameObject);
		clientApp.Inst.DestoryTalk(obj);
		Continue();
	}
}
