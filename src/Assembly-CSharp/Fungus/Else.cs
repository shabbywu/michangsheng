using UnityEngine;

namespace Fungus;

[CommandInfo("Flow", "Else", "Marks the start of a command block to be executed when the preceding If statement is False.", 0)]
[AddComponentMenu("")]
public class Else : Command, INoCommand
{
	public override void OnEnter()
	{
		if ((Object)(object)ParentBlock == (Object)null)
		{
			return;
		}
		if (CommandIndex >= ParentBlock.CommandList.Count - 1)
		{
			StopParentBlock();
			return;
		}
		int num = indentLevel;
		for (int i = CommandIndex + 1; i < ParentBlock.CommandList.Count; i++)
		{
			Command command = ParentBlock.CommandList[i];
			if (command.IndentLevel == num && ((object)command).GetType() == typeof(End))
			{
				Continue(command.CommandIndex + 1);
				return;
			}
		}
		StopParentBlock();
	}

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
