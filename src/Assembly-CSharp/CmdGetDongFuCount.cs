using System;
using Fungus;
using UnityEngine;

// Token: 0x020001FD RID: 509
[CommandInfo("YSDongFu", "获取玩家洞府数量", "获取玩家洞府数量，并赋值到TmpValue", 0)]
[AddComponentMenu("")]
public class CmdGetDongFuCount : Command
{
	// Token: 0x060014AD RID: 5293 RVA: 0x00084C14 File Offset: 0x00082E14
	public override void OnEnter()
	{
		this.GetFlowchart().SetIntegerVariable("TmpValue", DongFuManager.GetPlayerDongFuCount());
		this.Continue();
	}
}
