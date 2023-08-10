using System;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSNew/Add", "第三代传音符发送", "第三代传音符发送", 0)]
[AddComponentMenu("")]
public class AddCyF3 : Command
{
	[Tooltip("发送传音符IdnpcId")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable CyId;

	public override void OnEnter()
	{
		NpcJieSuanManager.inst.SendFungusCy(CyId.Value);
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
