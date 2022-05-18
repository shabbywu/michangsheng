using System;
using Fungus;
using UnityEngine;

// Token: 0x0200041D RID: 1053
[CommandInfo("剑灵", "检查是否解锁真相", "检查是否解锁了真相(赋值到TmpBool)", 0)]
[AddComponentMenu("")]
public class CmdJianLingIsUnlockZhenXiang : Command
{
	// Token: 0x06001C3C RID: 7228 RVA: 0x000FAE9C File Offset: 0x000F909C
	public override void OnEnter()
	{
		bool value = PlayerEx.Player.jianLingManager.IsZhenXiangUnlocked(this.ID);
		this.GetFlowchart().SetBooleanVariable("TmpBool", value);
		this.Continue();
	}

	// Token: 0x0400183F RID: 6207
	[SerializeField]
	protected string ID;
}
