using System;
using Fungus;
using UnityEngine;

// Token: 0x0200052E RID: 1326
[CommandInfo("YSTool", "打开修船界面", "打开修船界面", 0)]
[AddComponentMenu("")]
public class CmdOpenXiuChuan : Command
{
	// Token: 0x060021E9 RID: 8681 RVA: 0x0001BD61 File Offset: 0x00019F61
	public override void OnEnter()
	{
		UIXiuChuanPanel.OpenDefaultXiuChuan();
		this.Continue();
	}
}
