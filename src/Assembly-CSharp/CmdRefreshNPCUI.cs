using System;
using Fungus;
using UnityEngine;

// Token: 0x02000364 RID: 868
[CommandInfo("YSNPCJiaoHu", "刷新交互UI", "刷新交互UI", 0)]
[AddComponentMenu("")]
public class CmdRefreshNPCUI : Command
{
	// Token: 0x060018F7 RID: 6391 RVA: 0x000156CD File Offset: 0x000138CD
	public override void OnEnter()
	{
		NpcJieSuanManager.inst.isUpDateNpcList = true;
		this.Continue();
	}
}
