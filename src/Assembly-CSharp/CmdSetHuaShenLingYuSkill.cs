using System;
using Fungus;
using JSONClass;
using KBEngine;
using UnityEngine;

// Token: 0x020001E1 RID: 481
[CommandInfo("YSPlayer", "设置玩家化神领域", "设置玩家化神领域", 0)]
[AddComponentMenu("")]
public class CmdSetHuaShenLingYuSkill : Command
{
	// Token: 0x06001434 RID: 5172 RVA: 0x00082975 File Offset: 0x00080B75
	public override void OnEnter()
	{
		CmdSetHuaShenLingYuSkill.Do(this.WuDao);
		this.Continue();
	}

	// Token: 0x06001435 RID: 5173 RVA: 0x00082988 File Offset: 0x00080B88
	public static void Do(WuDaoType wuDao)
	{
		HuaShenData huaShenData = HuaShenData.DataDict[(int)wuDao];
		Avatar player = PlayerEx.Player;
		player.HuaShenLingYuSkill = new JSONObject(huaShenData.Skill);
		player.HuaShenWuDao = new JSONObject((int)wuDao);
	}

	// Token: 0x04000EFE RID: 3838
	[SerializeField]
	protected WuDaoType WuDao;
}
