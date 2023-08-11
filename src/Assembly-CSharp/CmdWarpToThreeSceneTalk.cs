using Fungus;
using UnityEngine;

[CommandInfo("YSNPCJiaoHu", "跳转到三级场景NPCTalk(TODO)", "跳转到三级场景NPCTalk，并将NPCID赋值到全局变量400", 0)]
[AddComponentMenu("")]
public class CmdWarpToThreeSceneTalk : Command
{
	private static bool inited;

	public override void OnEnter()
	{
		UINPCData nowJiaoHuNPC = UINPCJiaoHu.Inst.NowJiaoHuNPC;
		GlobalValue.Set(400, nowJiaoHuNPC.ID, "TODO");
		Continue();
	}

	private static void Init()
	{
		if (inited)
		{
			return;
		}
		foreach (JSONObject item in jsonData.instance.ThreeSenceJsonData.list)
		{
			_ = item;
		}
		inited = true;
	}
}
