using UnityEngine;

namespace Fungus;

[CommandInfo("Flow", "Label", "Marks a position in the command list for execution to jump to.", 0)]
[AddComponentMenu("")]
public class Label : Command
{
	[Tooltip("Display name for the label")]
	[SerializeField]
	protected string key = "";

	public virtual string Key => key;

	public override void OnEnter()
	{
		Continue();
	}

	public override string GetSummary()
	{
		return key;
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)200, (byte)200, (byte)253, byte.MaxValue));
	}
}
