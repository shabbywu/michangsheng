using System;
using Fungus;
using UnityEngine;

// Token: 0x0200041F RID: 1055
[CommandInfo("剑灵", "解锁线索", "解锁线索", 0)]
[AddComponentMenu("")]
public class CmdJianLingUnlockXianSuo : Command
{
	// Token: 0x06001C40 RID: 7232 RVA: 0x000179CE File Offset: 0x00015BCE
	public override void OnEnter()
	{
		PlayerEx.Player.jianLingManager.UnlockXianSuo(this.ID);
		this.Continue();
	}

	// Token: 0x04001840 RID: 6208
	[SerializeField]
	protected string ID;
}
