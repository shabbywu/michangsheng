using System.Collections.Generic;
using Fungus;
using UnityEngine;

namespace YSGame.TianJiDaBi;

[CommandInfo("天机大比", "开始天机大比", "开始天机大比", 0)]
[AddComponentMenu("")]
public class CmdTianJiDaBiStart : Command
{
	[Tooltip("玩家是否参加")]
	[SerializeField]
	protected bool PlayerJoin;

	[Tooltip("加塞的NPC")]
	[SerializeField]
	protected List<int> JiaSaiNPCList;

	public override void OnEnter()
	{
		TianJiDaBiManager.CmdTianJiDaBiStart(PlayerJoin, JiaSaiNPCList);
		Continue();
	}
}
