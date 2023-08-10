using System;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSTools", "检查npc是否死亡", "检查是否能截杀", 0)]
[AddComponentMenu("")]
public class CheckNpcDeath : Command
{
	[Tooltip("npcId")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable npcId;

	[Tooltip("是否死亡")]
	[VariableProperty(new Type[] { typeof(BooleanVariable) })]
	[SerializeField]
	protected BooleanVariable IsDeath;

	public override void OnEnter()
	{
		if (NpcJieSuanManager.inst.IsDeath(npcId.Value))
		{
			IsDeath.Value = true;
		}
		else
		{
			IsDeath.Value = false;
		}
		Continue();
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)184, (byte)210, (byte)235, byte.MaxValue));
	}

	public override void OnReset()
	{
	}
}
