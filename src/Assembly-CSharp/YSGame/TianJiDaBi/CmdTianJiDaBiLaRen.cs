using System;
using Fungus;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace YSGame.TianJiDaBi
{
	// Token: 0x02000DBC RID: 3516
	[CommandInfo("天机大比", "把参赛选手都拉到当前场景", "把参赛选手都拉到当前场景并设置状态为正常", 0)]
	[AddComponentMenu("")]
	public class CmdTianJiDaBiLaRen : Command
	{
		// Token: 0x060054CA RID: 21706 RVA: 0x00234DBC File Offset: 0x00232FBC
		public override void OnEnter()
		{
			Match nowMatch = TianJiDaBiManager.GetNowMatch();
			NPCMap npcMap = NpcJieSuanManager.inst.npcMap;
			string name = SceneManager.GetActiveScene().name;
			foreach (DaBiPlayer daBiPlayer in nowMatch.PlayerList)
			{
				if (!daBiPlayer.IsWanJia)
				{
					npcMap.RemoveNpcByList(daBiPlayer.ID);
					npcMap.AddNpcToThreeScene(daBiPlayer.ID, name);
					NpcJieSuanManager.inst.npcStatus.SetNpcStatus(daBiPlayer.ID, 1);
					NPCEx.SetNPCAction(daBiPlayer.ID, 46);
				}
			}
			NpcJieSuanManager.inst.isUpDateNpcList = true;
			this.Continue();
		}
	}
}
