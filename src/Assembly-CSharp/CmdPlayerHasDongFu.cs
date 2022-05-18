using System;
using Fungus;
using UnityEngine;

// Token: 0x02000314 RID: 788
[CommandInfo("YSDongFu", "玩家是否拥有某洞府", "玩家是否拥有某洞府，赋值到TmpValue，0没有，1有，赋值洞府名字到TmpString", 0)]
[AddComponentMenu("")]
public class CmdPlayerHasDongFu : Command
{
	// Token: 0x0600175B RID: 5979 RVA: 0x000CD73C File Offset: 0x000CB93C
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

	// Token: 0x040012B8 RID: 4792
	[Tooltip("洞府ID")]
	[SerializeField]
	protected int dongFuID;
}
