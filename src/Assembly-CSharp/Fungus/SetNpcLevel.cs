using System;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSNew/Set", "设置NPC境界并更新属性", "设置NPC境界并更新属性", 0)]
[AddComponentMenu("")]
public class SetNpcLevel : Command
{
	[Tooltip("NpcId")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable NpcId;

	[Tooltip("NpcId")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable Level;

	public override void OnEnter()
	{
		FactoryManager.inst.npcFactory.SetNpcLevel(NpcId.Value, Level.Value);
		Continue();
	}
}
