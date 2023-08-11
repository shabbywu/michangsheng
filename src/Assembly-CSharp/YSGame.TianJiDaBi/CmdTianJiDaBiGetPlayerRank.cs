using System;
using Fungus;
using UnityEngine;

namespace YSGame.TianJiDaBi;

[CommandInfo("天机大比", "获取玩家名次", "获取玩家名次", 0)]
[AddComponentMenu("")]
public class CmdTianJiDaBiGetPlayerRank : Command
{
	[Tooltip("名次")]
	[SerializeField]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	protected IntegerVariable Rank;

	public override void OnEnter()
	{
		Match nowMatch = TianJiDaBiManager.GetNowMatch();
		for (int i = 0; i < nowMatch.PlayerCount; i++)
		{
			if (nowMatch.PlayerList[i].IsWanJia)
			{
				Rank.Value = i + 1;
				break;
			}
		}
		Continue();
	}
}
