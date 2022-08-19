using System;
using Fungus;
using UnityEngine;

// Token: 0x020002D0 RID: 720
[CommandInfo("剑灵", "检查是否解锁线索", "检查是否解锁了线索(赋值到TmpBool)", 0)]
[AddComponentMenu("")]
public class CmdJianLingIsUnlockXianSuo : Command
{
	// Token: 0x06001932 RID: 6450 RVA: 0x000B4E94 File Offset: 0x000B3094
	public override void OnEnter()
	{
		bool value = PlayerEx.Player.jianLingManager.IsXianSuoUnlocked(this.ID);
		this.GetFlowchart().SetBooleanVariable("TmpBool", value);
		this.Continue();
	}

	// Token: 0x04001470 RID: 5232
	[SerializeField]
	protected string ID;
}
