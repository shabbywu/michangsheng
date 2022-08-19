using System;
using Fungus;
using UnityEngine;

// Token: 0x020001FF RID: 511
[CommandInfo("YSDongFu", "玩家是否拥有某洞府", "玩家是否拥有某洞府，赋值到TmpValue，0没有，1有，赋值洞府名字到TmpString", 0)]
[AddComponentMenu("")]
public class CmdPlayerHasDongFu : Command
{
	// Token: 0x060014B1 RID: 5297 RVA: 0x00084C44 File Offset: 0x00082E44
	public override void OnEnter()
	{
		int value = 0;
		if (DongFuManager.PlayerHasDongFu(this.dongFuID))
		{
			value = 1;
		}
		Flowchart flowchart = this.GetFlowchart();
		flowchart.SetIntegerVariable("TmpValue", value);
		flowchart.SetStringVariable("TmpString", DongFuManager.GetDongFuName(this.dongFuID));
		this.Continue();
	}

	// Token: 0x04000F72 RID: 3954
	[Tooltip("洞府ID")]
	[SerializeField]
	protected int dongFuID;
}
