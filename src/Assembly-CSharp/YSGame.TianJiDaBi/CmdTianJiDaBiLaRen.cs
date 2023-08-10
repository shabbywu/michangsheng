using Fungus;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace YSGame.TianJiDaBi;

[CommandInfo("天机大比", "把参赛选手都拉到当前场景", "把参赛选手都拉到当前场景并设置状态为正常", 0)]
[AddComponentMenu("")]
public class CmdTianJiDaBiLaRen : Command
{
	public override void OnEnter()
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		Match nowMatch = TianJiDaBiManager.GetNowMatch();
		NPCMap npcMap = NpcJieSuanManager.inst.npcMap;
		Scene activeScene = SceneManager.GetActiveScene();
		string name = ((Scene)(ref activeScene)).name;
		foreach (DaBiPlayer player in nowMatch.PlayerList)
		{
			if (!player.IsWanJia)
			{
				npcMap.RemoveNpcByList(player.ID);
				npcMap.AddNpcToThreeScene(player.ID, name);
				NpcJieSuanManager.inst.npcStatus.SetNpcStatus(player.ID, 1);
				NPCEx.SetNPCAction(player.ID, 46);
			}
		}
		NpcJieSuanManager.inst.isUpDateNpcList = true;
		Continue();
	}
}
