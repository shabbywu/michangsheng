using System;
using Fungus;
using UnityEngine;

// Token: 0x0200021E RID: 542
[CommandInfo("YSDongFu", "获取表白题库", "获取表白题库，赋值到TiWen，optionDesc1，optionDesc2，optionDesc3", 0)]
[AddComponentMenu("")]
public class CmdGetBiaoBaiTiKu : Command
{
	// Token: 0x060015AD RID: 5549 RVA: 0x00091344 File Offset: 0x0008F544
	public override void OnEnter()
	{
		TiKuData tiKuData;
		BiaoBaiManager.GetRandomTiKu(this.type, out tiKuData);
		Flowchart flowchart = this.GetFlowchart();
		flowchart.SetStringVariable("TiWen", tiKuData.TiWen);
		flowchart.SetStringVariable("optionDesc1", tiKuData.optionDesc[0]);
		flowchart.SetStringVariable("optionDesc2", tiKuData.optionDesc[1]);
		flowchart.SetStringVariable("optionDesc3", tiKuData.optionDesc[2]);
		this.Continue();
	}

	// Token: 0x04001038 RID: 4152
	[Tooltip("题干类型 1正邪2性格3标签")]
	[SerializeField]
	protected int type;
}
