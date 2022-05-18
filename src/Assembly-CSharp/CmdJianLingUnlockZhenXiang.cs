using System;
using Fungus;
using UnityEngine;

// Token: 0x02000420 RID: 1056
[CommandInfo("剑灵", "解锁真相", "解锁真相", 0)]
[AddComponentMenu("")]
public class CmdJianLingUnlockZhenXiang : Command
{
	// Token: 0x06001C42 RID: 7234 RVA: 0x000179EB File Offset: 0x00015BEB
	public override void OnEnter()
	{
		PlayerEx.Player.jianLingManager.UnlockZhenXiang(this.ID);
		this.Continue();
	}

	// Token: 0x04001841 RID: 6209
	[SerializeField]
	protected string ID;
}
