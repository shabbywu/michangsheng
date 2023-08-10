using System;
using Fungus;
using UnityEngine;

[CommandInfo("YSPlayer", "设置玩家人际关系", "设置玩家人际关系", 0)]
[AddComponentMenu("")]
public class CmdSetPlayerRelationship : Command
{
	[Tooltip("是否添加")]
	[SerializeField]
	protected bool IsAdd = true;

	[Tooltip("NPC的ID(新老都行)")]
	[SerializeField]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	protected IntegerVariable NPCID;

	[Tooltip("师傅")]
	[SerializeField]
	protected bool Teather;

	[Tooltip("道侣")]
	[SerializeField]
	protected bool DaoLv;

	[Tooltip("兄弟姐妹")]
	[SerializeField]
	protected bool Brother;

	[Tooltip("徒弟")]
	[SerializeField]
	protected bool TuDi;

	public override void OnEnter()
	{
		int npcid = NPCEx.NPCIDToNew(NPCID.Value);
		if (IsAdd)
		{
			PlayerEx.AddRelationship(npcid, Teather, DaoLv, Brother, TuDi);
		}
		else
		{
			PlayerEx.SubRelationship(npcid, Teather, DaoLv, Brother, TuDi);
		}
		Continue();
	}
}
