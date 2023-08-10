using Fungus;
using UnityEngine;

[CommandInfo("YSNPCJiaoHu", "跳转到NPC交互Talk", "跳转到NPC交互Talk", 0)]
[AddComponentMenu("")]
public class CmdWarpToNPCJiaoHuTalk : Command
{
	public override void OnEnter()
	{
		UINPCJiaoHu.Inst.IsNeedWarpToNPCTalk = true;
		Continue();
	}
}
