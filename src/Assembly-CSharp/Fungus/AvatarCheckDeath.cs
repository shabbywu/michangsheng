using System;
using UnityEngine;

namespace Fungus;

[CommandInfo("YS", "CheckAvatarDeath", "检测英雄的死亡，将死亡状态赋值到你设置的IsDeath中设置的变量当中", 0)]
[AddComponentMenu("")]
public class AvatarCheckDeath : Command
{
	[Tooltip("需要检测是否死亡的英雄ID")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable MonstarID;

	[Tooltip("将检测到的值赋给一个变量")]
	[VariableProperty(new Type[] { typeof(BooleanVariable) })]
	[SerializeField]
	protected BooleanVariable TempBool;

	public void setHasVariable(string name, int num, Flowchart flowchart)
	{
		if (flowchart.HasVariable(name))
		{
			flowchart.SetIntegerVariable(name, num);
		}
	}

	public override void OnEnter()
	{
		TempBool.Value = jsonData.instance.MonstarIsDeath(MonstarID.Value);
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
