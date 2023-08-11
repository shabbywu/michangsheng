using System;
using Fungus;
using UnityEngine;

[CommandInfo("YSPlayer", "改变NPC行为", "改变NPC行为", 0)]
[AddComponentMenu("")]
public class CmdChangeNPCAction : Command
{
	[SerializeField]
	[Tooltip("NPCID(老ID或者新ID皆可)")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	protected IntegerVariable NPCID;

	[SerializeField]
	[Tooltip("ActionID")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	protected IntegerVariable ActionID;

	public override void OnEnter()
	{
		int num = NPCEx.NPCIDToNew(NPCID.Value);
		if (NpcJieSuanManager.inst.IsDeath(num))
		{
			Debug.LogError((object)$"CmdChangeNPCAction命令出错,id为{num}的NPC已死");
			Debug.LogError((object)("talk名称为：" + GetFlowchart().GetParentName()));
			Debug.LogError((object)("block名称为：" + ParentBlock.BlockName));
			Continue();
			return;
		}
		jsonData.instance.AvatarJsonData[num.ToString()].SetField("ActionId", ActionID.Value);
		if (UINPCJiaoHu.Inst.NowJiaoHuNPC != null)
		{
			UINPCJiaoHu.Inst.NowJiaoHuNPC.RefreshData();
			UINPCSVItem.RefreshNPCTaskID = UINPCJiaoHu.Inst.NowJiaoHuNPC.ID;
		}
		Continue();
	}
}
