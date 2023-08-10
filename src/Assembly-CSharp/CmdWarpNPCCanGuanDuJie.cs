using Fungus;
using UnityEngine;
using UnityEngine.SceneManagement;

[CommandInfo("渡劫", "传送受邀参观渡劫的NPC到指定场景", "传送受邀参观渡劫的NPC到指定场景(三级场景)，拉来的人的数量赋值到TmpValue", 0)]
[AddComponentMenu("")]
public class CmdWarpNPCCanGuanDuJie : Command
{
	[Tooltip("场景名")]
	[SerializeField]
	protected string SceneName;

	public override void OnEnter()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		NPCMap npcMap = NpcJieSuanManager.inst.npcMap;
		Scene activeScene = SceneManager.GetActiveScene();
		string name = ((Scene)(ref activeScene)).name;
		int num = 0;
		foreach (int item in PlayerEx.Player.TianJieCanGuanNPCs.ToList())
		{
			if (!NPCEx.IsDeath(item))
			{
				num++;
				npcMap.RemoveNpcByList(item);
				npcMap.AddNpcToThreeScene(item, name);
				NPCEx.SetNPCAction(item, 117);
			}
		}
		GetFlowchart().SetIntegerVariable("TmpValue", num);
		NpcJieSuanManager.inst.isUpDateNpcList = true;
		Continue();
	}
}
