using Fungus;
using UnityEngine;

[CommandInfo("YSNPCJiaoHu", "刷新交互UI", "刷新交互UI", 0)]
[AddComponentMenu("")]
public class CmdRefreshNPCUI : Command
{
	public override void OnEnter()
	{
		NpcJieSuanManager.inst.isUpDateNpcList = true;
		Continue();
	}
}
