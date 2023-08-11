using System;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSTools", "AvatarDeath", "设置英雄死亡", 0)]
[AddComponentMenu("")]
public class AvatarDeath : Command
{
	[Tooltip("死亡的英雄ID")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable MonstarID;

	public void setHasVariable(string name, int num, Flowchart flowchart)
	{
		if (flowchart.HasVariable(name))
		{
			flowchart.SetIntegerVariable(name, num);
		}
	}

	public override void OnEnter()
	{
		jsonData.instance.setMonstarDeath(MonstarID.Value);
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
