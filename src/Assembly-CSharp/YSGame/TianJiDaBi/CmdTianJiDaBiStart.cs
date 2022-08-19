using System;
using System.Collections.Generic;
using Fungus;
using UnityEngine;

namespace YSGame.TianJiDaBi
{
	// Token: 0x02000A8E RID: 2702
	[CommandInfo("天机大比", "开始天机大比", "开始天机大比", 0)]
	[AddComponentMenu("")]
	public class CmdTianJiDaBiStart : Command
	{
		// Token: 0x06004BB7 RID: 19383 RVA: 0x00203A24 File Offset: 0x00201C24
		public override void OnEnter()
		{
			TianJiDaBiManager.CmdTianJiDaBiStart(this.PlayerJoin, this.JiaSaiNPCList);
			this.Continue();
		}

		// Token: 0x04004ABF RID: 19135
		[Tooltip("玩家是否参加")]
		[SerializeField]
		protected bool PlayerJoin;

		// Token: 0x04004AC0 RID: 19136
		[Tooltip("加塞的NPC")]
		[SerializeField]
		protected List<int> JiaSaiNPCList;
	}
}
