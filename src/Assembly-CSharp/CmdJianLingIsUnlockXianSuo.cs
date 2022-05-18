using System;
using Fungus;
using UnityEngine;

// Token: 0x0200041C RID: 1052
[CommandInfo("剑灵", "检查是否解锁线索", "检查是否解锁了线索(赋值到TmpBool)", 0)]
[AddComponentMenu("")]
public class CmdJianLingIsUnlockXianSuo : Command
{
	// Token: 0x06001C3A RID: 7226 RVA: 0x000FAE60 File Offset: 0x000F9060
	public override void OnEnter()
	{
		bool value = PlayerEx.Player.jianLingManager.IsXianSuoUnlocked(this.ID);
		this.GetFlowchart().SetBooleanVariable("TmpBool", value);
		this.Continue();
	}

	// Token: 0x0400183E RID: 6206
	[SerializeField]
	protected string ID;
}
