using System;
using Fungus;
using UnityEngine;

// Token: 0x020002F0 RID: 752
[CommandInfo("YSPlayer", "离开随机副本", "离开随机副本，必须在随机副本调用", 0)]
[AddComponentMenu("")]
public class CmdOutRandomFuben : Command
{
	// Token: 0x060016D2 RID: 5842 RVA: 0x00014390 File Offset: 0x00012590
	public override void OnEnter()
	{
		MapRandomCompent.ShowOutRandomFubenTalk();
		this.Continue();
	}
}
