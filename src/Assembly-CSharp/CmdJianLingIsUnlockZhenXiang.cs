using System;
using Fungus;
using UnityEngine;

// Token: 0x020002D1 RID: 721
[CommandInfo("剑灵", "检查是否解锁真相", "检查是否解锁了真相(赋值到TmpBool)", 0)]
[AddComponentMenu("")]
public class CmdJianLingIsUnlockZhenXiang : Command
{
	// Token: 0x06001934 RID: 6452 RVA: 0x000B4ED0 File Offset: 0x000B30D0
	public override void OnEnter()
	{
		bool value = PlayerEx.Player.jianLingManager.IsZhenXiangUnlocked(this.ID);
		this.GetFlowchart().SetBooleanVariable("TmpBool", value);
		this.Continue();
	}

	// Token: 0x04001471 RID: 5233
	[SerializeField]
	protected string ID;
}
