using System;
using Fungus;
using UnityEngine;
using script.YarnEditor.Command;

[CommandInfo("YSNew/Add", "增加悟道点", "增加悟道点", 0)]
[AddComponentMenu("")]
public class AddWuDaoDian : Command
{
	[Tooltip("增加悟道点数量")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable AddNum;

	public override void OnEnter()
	{
		AddCommand.AddWuDaoDian(AddNum.Value);
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
