using System;
using Fungus;
using UnityEngine;

[CommandInfo("YSPlayer", "获取道侣对自己的昵称", "获取道侣对自己的昵称赋值到NickName变量", 0)]
[AddComponentMenu("")]
public class CmdGetDaoLvNickName : Command
{
	[Tooltip("NPC的ID")]
	[SerializeField]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	protected IntegerVariable NPCID;

	[Tooltip("结果")]
	[SerializeField]
	[VariableProperty(new Type[] { typeof(StringVariable) })]
	protected StringVariable NickName;

	public override void OnEnter()
	{
		int npcid = NPCEx.NPCIDToNew(NPCID.Value);
		if (PlayerEx.IsDaoLv(npcid))
		{
			NickName.Value = PlayerEx.GetDaoLvNickName(npcid);
		}
		else
		{
			Debug.LogError((object)$"获取道侣昵称出错，{NPCID}不是玩家的道侣");
		}
		Continue();
	}
}
