using System;
using Fungus;
using UnityEngine;

namespace YSGame.TianJiDaBi
{
	// Token: 0x02000A8D RID: 2701
	[CommandInfo("天机大比", "记录玩家对战结果", "记录玩家对战结果（必须先计算玩家的，再模拟NPC的）", 0)]
	[AddComponentMenu("")]
	public class CmdTianJiDaBiRecordPlayer : Command
	{
		// Token: 0x06004BB4 RID: 19380 RVA: 0x0020395D File Offset: 0x00201B5D
		public override void OnEnter()
		{
			CmdTianJiDaBiRecordPlayer.Do(this.PlayerWin);
			this.Continue();
		}

		// Token: 0x06004BB5 RID: 19381 RVA: 0x00203970 File Offset: 0x00201B70
		public static void Do(bool playerWin)
		{
			Match nowMatch = TianJiDaBiManager.GetNowMatch();
			DaBiPlayer daBiPlayer = null;
			DaBiPlayer daBiPlayer2 = null;
			for (int i = 0; i < nowMatch.PlayerCount; i += 2)
			{
				if (nowMatch.PlayerList[i].IsWanJia)
				{
					daBiPlayer = nowMatch.PlayerList[i];
					daBiPlayer2 = nowMatch.PlayerList[i + 1];
					break;
				}
				if (nowMatch.PlayerList[i + 1].IsWanJia)
				{
					daBiPlayer = nowMatch.PlayerList[i + 1];
					daBiPlayer2 = nowMatch.PlayerList[i];
					break;
				}
			}
			DaBiPlayer win;
			DaBiPlayer fail;
			if (playerWin)
			{
				win = daBiPlayer;
				fail = daBiPlayer2;
			}
			else
			{
				win = daBiPlayer2;
				fail = daBiPlayer;
			}
			nowMatch.RecordFight(nowMatch.RoundIndex, win, fail);
		}

		// Token: 0x04004ABE RID: 19134
		[Tooltip("玩家是否获胜")]
		[SerializeField]
		protected bool PlayerWin;
	}
}
