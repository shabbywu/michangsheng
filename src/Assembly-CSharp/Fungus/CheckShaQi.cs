using System;
using KBEngine;
using UnityEngine;

namespace Fungus;

[CommandInfo("YS", "CheckShaQi", "检测杀气数量", 0)]
[AddComponentMenu("")]
public class CheckShaQi : Command
{
	[Tooltip("获取到的杀气值存放位置")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable TempValue;

	public override void OnEnter()
	{
		Avatar player = Tools.instance.getPlayer();
		TempValue.Value = (int)player.shaQi;
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
