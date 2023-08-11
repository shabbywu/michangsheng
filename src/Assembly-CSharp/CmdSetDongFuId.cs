using System;
using Fungus;
using UnityEngine;

[CommandInfo("YSPlayer", "设置npc去的洞府Id", "设置npc去的洞府Id", 0)]
[AddComponentMenu("")]
public class CmdSetDongFuId : Command
{
	[SerializeField]
	[Tooltip("npcId")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	protected IntegerVariable npc;

	[SerializeField]
	[Tooltip("洞府ID")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	protected IntegerVariable dongFu;

	public override void OnEnter()
	{
		jsonData.instance.AvatarJsonData[npc.Value.ToString()].SetField("DongFuId", dongFu.Value);
		Continue();
	}
}
