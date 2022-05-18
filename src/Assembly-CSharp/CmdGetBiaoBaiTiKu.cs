using System;
using Fungus;
using UnityEngine;

// Token: 0x0200033A RID: 826
[CommandInfo("YSDongFu", "获取表白题库", "获取表白题库，赋值到TiWen，optionDesc1，optionDesc2，optionDesc3", 0)]
[AddComponentMenu("")]
public class CmdGetBiaoBaiTiKu : Command
{
	// Token: 0x06001865 RID: 6245 RVA: 0x000D9E68 File Offset: 0x000D8068
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

	// Token: 0x04001390 RID: 5008
	[Tooltip("题干类型 1正邪2性格3标签")]
	[SerializeField]
	protected int type;
}
