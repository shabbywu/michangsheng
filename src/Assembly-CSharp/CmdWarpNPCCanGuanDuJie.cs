using System;
using Fungus;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020002AB RID: 683
[CommandInfo("渡劫", "传送受邀参观渡劫的NPC到指定场景", "传送受邀参观渡劫的NPC到指定场景(三级场景)，拉来的人的数量赋值到TmpValue", 0)]
[AddComponentMenu("")]
public class CmdWarpNPCCanGuanDuJie : Command
{
	// Token: 0x0600182B RID: 6187 RVA: 0x000A8B6C File Offset: 0x000A6D6C
	public override void OnEnter()
	{
		NPCMap npcMap = NpcJieSuanManager.inst.npcMap;
		string name = SceneManager.GetActiveScene().name;
		int num = 0;
		foreach (int num2 in PlayerEx.Player.TianJieCanGuanNPCs.ToList())
		{
			if (!NPCEx.IsDeath(num2))
			{
				num++;
				npcMap.RemoveNpcByList(num2);
				npcMap.AddNpcToThreeScene(num2, name);
				NPCEx.SetNPCAction(num2, 117);
			}
		}
		this.GetFlowchart().SetIntegerVariable("TmpValue", num);
		NpcJieSuanManager.inst.isUpDateNpcList = true;
		this.Continue();
	}

	// Token: 0x0400133B RID: 4923
	[Tooltip("场景名")]
	[SerializeField]
	protected string SceneName;
}
