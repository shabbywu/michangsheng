using System;
using KBEngine;
using UnityEngine;

namespace Fungus;

[CommandInfo("YS", "CheckExp", "检测经验数量", 0)]
[AddComponentMenu("")]
public class CheckExp : Command
{
	[Tooltip("获取到的修为值存放位置")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable TempValue;

	public override void OnEnter()
	{
		Avatar player = Tools.instance.getPlayer();
		TempValue.Value = (int)player.exp;
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
