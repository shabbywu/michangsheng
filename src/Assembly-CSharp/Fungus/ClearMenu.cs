using UnityEngine;

namespace Fungus;

[CommandInfo("Narrative", "Clear Menu", "Clears the options from a menu dialogue", 0)]
public class ClearMenu : Command
{
	[Tooltip("Menu Dialog to clear the options on")]
	[SerializeField]
	protected MenuDialog menuDialog;

	public override void OnEnter()
	{
		menuDialog.Clear();
		Continue();
	}

	public override string GetSummary()
	{
		if ((Object)(object)menuDialog == (Object)null)
		{
			return "Error: No menu dialog object selected";
		}
		return ((Object)menuDialog).name;
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)184, (byte)210, (byte)235, byte.MaxValue));
	}
}
