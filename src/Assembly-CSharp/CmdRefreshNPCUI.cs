using System;
using Fungus;
using UnityEngine;

// Token: 0x02000248 RID: 584
[CommandInfo("YSNPCJiaoHu", "刷新交互UI", "刷新交互UI", 0)]
[AddComponentMenu("")]
public class CmdRefreshNPCUI : Command
{
	// Token: 0x0600163F RID: 5695 RVA: 0x0009699B File Offset: 0x00094B9B
	public override void OnEnter()
	{
		NpcJieSuanManager.inst.isUpDateNpcList = true;
		this.Continue();
	}
}
