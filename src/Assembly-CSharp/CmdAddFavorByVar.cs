using Fungus;
using UnityEngine;

[CommandInfo("YSNPCJiaoHu", "根据变量名增加好感度", "根据变量增加好感度", 0)]
[AddComponentMenu("")]
public class CmdAddFavorByVar : Command
{
	[SerializeField]
	[Tooltip("目标NPCID的变量名")]
	protected string addTargetNPCIDVar;

	[SerializeField]
	[Tooltip("好感度增加量的变量名")]
	protected string addCountVar;

	public override void OnEnter()
	{
		Flowchart flowchart = GetFlowchart();
		int integerVariable = flowchart.GetIntegerVariable(addTargetNPCIDVar);
		int integerVariable2 = flowchart.GetIntegerVariable(addCountVar);
		UINPCSVItem.RefreshNPCFavorID = integerVariable;
		NPCEx.AddFavor(integerVariable, integerVariable2);
		Continue();
	}
}
