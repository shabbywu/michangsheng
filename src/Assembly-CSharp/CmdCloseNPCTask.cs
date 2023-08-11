using Fungus;
using UnityEngine;

[CommandInfo("YSNPCJiaoHu", "关闭NPC任务", "关闭NPC任务", 0)]
[AddComponentMenu("")]
public class CmdCloseNPCTask : Command
{
	[SerializeField]
	[Tooltip("目标NPCID的变量名")]
	protected string TargetNPCIDVar;

	public override void OnEnter()
	{
		int integerVariable = GetFlowchart().GetIntegerVariable(TargetNPCIDVar);
		jsonData.instance.AvatarJsonData[integerVariable.ToString()].SetField("IsNeedHelp", val: false);
		UINPCSVItem.RefreshNPCTaskID = integerVariable;
		Continue();
	}
}
