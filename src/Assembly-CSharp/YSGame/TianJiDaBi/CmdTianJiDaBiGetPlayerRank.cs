using System;
using Fungus;
using UnityEngine;

namespace YSGame.TianJiDaBi
{
	// Token: 0x02000DBA RID: 3514
	[CommandInfo("天机大比", "获取玩家名次", "获取玩家名次", 0)]
	[AddComponentMenu("")]
	public class CmdTianJiDaBiGetPlayerRank : Command
	{
		// Token: 0x060054C6 RID: 21702 RVA: 0x00234D6C File Offset: 0x00232F6C
		public override void OnEnter()
		{
			Match nowMatch = TianJiDaBiManager.GetNowMatch();
			for (int i = 0; i < nowMatch.PlayerCount; i++)
			{
				if (nowMatch.PlayerList[i].IsWanJia)
				{
					this.Rank.Value = i + 1;
					break;
				}
			}
			this.Continue();
		}

		// Token: 0x0400547B RID: 21627
		[Tooltip("名次")]
		[SerializeField]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		protected IntegerVariable Rank;
	}
}
