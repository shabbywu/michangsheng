using System;
using Fungus;
using UnityEngine;

namespace YSGame.TianJiDaBi
{
	// Token: 0x02000A88 RID: 2696
	[CommandInfo("天机大比", "获取玩家名次", "获取玩家名次", 0)]
	[AddComponentMenu("")]
	public class CmdTianJiDaBiGetPlayerRank : Command
	{
		// Token: 0x06004BAA RID: 19370 RVA: 0x0020382C File Offset: 0x00201A2C
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

		// Token: 0x04004ABD RID: 19133
		[Tooltip("名次")]
		[SerializeField]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		protected IntegerVariable Rank;
	}
}
