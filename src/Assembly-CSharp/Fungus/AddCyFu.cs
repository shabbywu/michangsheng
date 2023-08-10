using System;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSNew/Add", "发送第二代传音符", "发送第二代传音符", 0)]
[AddComponentMenu("")]
public class AddCyFu : Command
{
	[Tooltip("增加npcId")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable CyType;

	public override void OnEnter()
	{
		NpcJieSuanManager.inst.SendFungusCyFu(CyType.Value);
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
