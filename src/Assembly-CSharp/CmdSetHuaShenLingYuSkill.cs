using System;
using Fungus;
using JSONClass;
using KBEngine;
using UnityEngine;

// Token: 0x020002F4 RID: 756
[CommandInfo("YSPlayer", "设置玩家化神领域", "设置玩家化神领域", 0)]
[AddComponentMenu("")]
public class CmdSetHuaShenLingYuSkill : Command
{
	// Token: 0x060016D9 RID: 5849 RVA: 0x000CBAE0 File Offset: 0x000C9CE0
	public override void OnEnter()
	{
		HuaShenData huaShenData = HuaShenData.DataDict[(int)this.WuDao];
		Avatar player = PlayerEx.Player;
		player.HuaShenLingYuSkill = new JSONObject(huaShenData.Skill);
		player.HuaShenWuDao = new JSONObject((int)this.WuDao);
		this.Continue();
	}

	// Token: 0x0400123C RID: 4668
	[SerializeField]
	protected WuDaoType WuDao;
}
