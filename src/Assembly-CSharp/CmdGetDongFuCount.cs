using System;
using Fungus;
using UnityEngine;

// Token: 0x02000312 RID: 786
[CommandInfo("YSDongFu", "获取玩家洞府数量", "获取玩家洞府数量，并赋值到TmpValue", 0)]
[AddComponentMenu("")]
public class CmdGetDongFuCount : Command
{
	// Token: 0x06001757 RID: 5975 RVA: 0x000149C6 File Offset: 0x00012BC6
	public override void OnEnter()
	{
		this.GetFlowchart().SetIntegerVariable("TmpValue", DongFuManager.GetPlayerDongFuCount());
		this.Continue();
	}
}
