using System;
using Fungus;
using UnityEngine;

// Token: 0x020001DD RID: 477
[CommandInfo("YSPlayer", "离开随机副本", "离开随机副本，必须在随机副本调用", 0)]
[AddComponentMenu("")]
public class CmdOutRandomFuben : Command
{
	// Token: 0x0600142D RID: 5165 RVA: 0x00082826 File Offset: 0x00080A26
	public override void OnEnter()
	{
		MapRandomCompent.ShowOutRandomFubenTalk();
		this.Continue();
	}
}
