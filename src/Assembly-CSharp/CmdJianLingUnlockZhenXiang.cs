using System;
using Fungus;
using UnityEngine;

// Token: 0x020002D4 RID: 724
[CommandInfo("剑灵", "解锁真相", "解锁真相", 0)]
[AddComponentMenu("")]
public class CmdJianLingUnlockZhenXiang : Command
{
	// Token: 0x0600193A RID: 6458 RVA: 0x000B4F46 File Offset: 0x000B3146
	public override void OnEnter()
	{
		PlayerEx.Player.jianLingManager.UnlockZhenXiang(this.ID);
		this.Continue();
	}

	// Token: 0x04001473 RID: 5235
	[SerializeField]
	protected string ID;
}
