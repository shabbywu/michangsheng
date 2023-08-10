using UnityEngine;

namespace Fungus;

[CommandInfo("Flow", "Else If", "Marks the start of a command block to be executed when the preceding If statement is False and the test expression is true.", 0)]
[AddComponentMenu("")]
public class ElseIf : VariableCondition
{
	protected override bool IsElseIf => true;

	public override bool OpenBlock()
	{
		return true;
	}

	public override bool CloseBlock()
	{
		return true;
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)253, (byte)253, (byte)150, byte.MaxValue));
	}
}
