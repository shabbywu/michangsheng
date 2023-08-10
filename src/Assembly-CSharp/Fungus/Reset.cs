using UnityEngine;

namespace Fungus;

[CommandInfo("Variable", "Reset", "Resets the state of all commands and variables in the Flowchart.", 0)]
[AddComponentMenu("")]
public class Reset : Command
{
	[Tooltip("Reset state of all commands in the script")]
	[SerializeField]
	protected bool resetCommands = true;

	[Tooltip("Reset variables back to their default values")]
	[SerializeField]
	protected bool resetVariables = true;

	public override void OnEnter()
	{
		GetFlowchart().Reset(resetCommands, resetVariables);
		Continue();
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)235, (byte)191, (byte)217, byte.MaxValue));
	}
}
