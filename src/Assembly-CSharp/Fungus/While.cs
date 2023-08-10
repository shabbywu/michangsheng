using UnityEngine;

namespace Fungus;

[CommandInfo("Flow", "While", "Continuously loop through a block of commands while the condition is true. Use the Break command to force the loop to terminate immediately.", 0)]
[AddComponentMenu("")]
public class While : If
{
	public override void OnEnter()
	{
		bool flag = true;
		if ((Object)(object)variable != (Object)null)
		{
			flag = EvaluateCondition();
		}
		End end = null;
		for (int i = CommandIndex + 1; i < ParentBlock.CommandList.Count; i++)
		{
			End end2 = ParentBlock.CommandList[i] as End;
			if ((Object)(object)end2 != (Object)null && end2.IndentLevel == indentLevel)
			{
				end = end2;
				break;
			}
		}
		if (flag)
		{
			end.Loop = true;
			Continue();
		}
		else
		{
			Continue(end.CommandIndex + 1);
		}
	}

	public override bool OpenBlock()
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
