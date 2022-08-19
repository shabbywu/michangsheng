using System;
using Fungus;
using UnityEngine;

// Token: 0x020002D3 RID: 723
[CommandInfo("剑灵", "解锁线索", "解锁线索", 0)]
[AddComponentMenu("")]
public class CmdJianLingUnlockXianSuo : Command
{
	// Token: 0x06001938 RID: 6456 RVA: 0x000B4F29 File Offset: 0x000B3129
	public override void OnEnter()
	{
		PlayerEx.Player.jianLingManager.UnlockXianSuo(this.ID);
		this.Continue();
	}

	// Token: 0x04001472 RID: 5234
	[SerializeField]
	protected string ID;
}
