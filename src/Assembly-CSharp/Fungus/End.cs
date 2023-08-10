using UnityEngine;

namespace Fungus;

[CommandInfo("Flow", "End", "Marks the end of a conditional block.", 0)]
[AddComponentMenu("")]
public class End : Command
{
	public virtual bool Loop { get; set; }

	public override void OnEnter()
	{
		if (Loop)
		{
			for (int num = CommandIndex - 1; num >= 0; num--)
			{
				Command command = ParentBlock.CommandList[num];
				if (command.IndentLevel == IndentLevel && ((object)command).GetType() == typeof(While))
				{
					Continue(num);
					return;
				}
			}
		}
		Continue();
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
