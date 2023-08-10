using System;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSTools", "增加Npc下一次突破概率", "增加Npc下一次突破概率", 0)]
[AddComponentMenu("")]
public class AddNpcNextToPoLv : Command
{
	[Tooltip("NpcId")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable NpcId;

	[Tooltip("增加概率")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable AddLV;

	public override void OnEnter()
	{
		NPCEx.AddNpcNextToPoLv(NpcId.Value, AddLV.Value);
		Continue();
	}
}
