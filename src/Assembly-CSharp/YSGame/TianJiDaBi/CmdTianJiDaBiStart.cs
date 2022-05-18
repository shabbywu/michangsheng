using System;
using System.Collections.Generic;
using Fungus;
using UnityEngine;

namespace YSGame.TianJiDaBi
{
	// Token: 0x02000DC0 RID: 3520
	[CommandInfo("天机大比", "开始天机大比", "开始天机大比", 0)]
	[AddComponentMenu("")]
	public class CmdTianJiDaBiStart : Command
	{
		// Token: 0x060054D3 RID: 21715 RVA: 0x0003CA8E File Offset: 0x0003AC8E
		public override void OnEnter()
		{
			TianJiDaBiManager.CmdTianJiDaBiStart(this.PlayerJoin, this.JiaSaiNPCList);
			this.Continue();
		}

		// Token: 0x0400547D RID: 21629
		[Tooltip("玩家是否参加")]
		[SerializeField]
		protected bool PlayerJoin;

		// Token: 0x0400547E RID: 21630
		[Tooltip("加塞的NPC")]
		[SerializeField]
		protected List<int> JiaSaiNPCList;
	}
}
