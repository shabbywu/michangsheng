using System;
using Fungus;
using UnityEngine;

// Token: 0x020003A5 RID: 933
[CommandInfo("YSTool", "打开修船界面", "打开修船界面", 0)]
[AddComponentMenu("")]
public class CmdOpenXiuChuan : Command
{
	// Token: 0x06001E68 RID: 7784 RVA: 0x000D60A6 File Offset: 0x000D42A6
	public override void OnEnter()
	{
		UIXiuChuanPanel.OpenDefaultXiuChuan();
		this.Continue();
	}
}
