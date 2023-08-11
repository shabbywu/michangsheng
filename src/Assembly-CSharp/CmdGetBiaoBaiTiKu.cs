using Fungus;
using UnityEngine;

[CommandInfo("YSDongFu", "获取表白题库", "获取表白题库，赋值到TiWen，optionDesc1，optionDesc2，optionDesc3", 0)]
[AddComponentMenu("")]
public class CmdGetBiaoBaiTiKu : Command
{
	[Tooltip("题干类型 1正邪2性格3标签")]
	[SerializeField]
	protected int type;

	public override void OnEnter()
	{
		BiaoBaiManager.GetRandomTiKu(type, out var ti);
		Flowchart flowchart = GetFlowchart();
		flowchart.SetStringVariable("TiWen", ti.TiWen);
		flowchart.SetStringVariable("optionDesc1", ti.optionDesc[0]);
		flowchart.SetStringVariable("optionDesc2", ti.optionDesc[1]);
		flowchart.SetStringVariable("optionDesc3", ti.optionDesc[2]);
		Continue();
	}
}
