using UnityEngine;

namespace Fungus;

[CommandInfo("Flow", "Break", "Force a loop to terminate immediately.", 0)]
[AddComponentMenu("")]
public class Break : Command
{
	public override void OnEnter()
	{
		int num = -1;
		int num2 = -1;
		for (int num3 = CommandIndex - 1; num3 >= 0; num3--)
		{
			While @while = ParentBlock.CommandList[num3] as While;
			if ((Object)(object)@while != (Object)null)
			{
				num = num3;
				num2 = @while.IndentLevel;
				break;
			}
		}
		if (num == -1)
		{
			Continue();
			return;
		}
		for (int i = num + 1; i < ParentBlock.CommandList.Count; i++)
		{
			End end = ParentBlock.CommandList[i] as End;
			if ((Object)(object)end != (Object)null && end.IndentLevel == num2)
			{
				if (CommandIndex <= num || CommandIndex >= end.CommandIndex)
				{
					break;
				}
				Continue(end.CommandIndex + 1);
				return;
			}
		}
		Continue();
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)253, (byte)253, (byte)150, byte.MaxValue));
	}
}
