using UnityEngine;

namespace Fungus;

[CommandInfo("Narrative", "Set Menu Dialog", "Sets a custom menu dialog to use when displaying multiple choice menus", 0)]
[AddComponentMenu("")]
public class SetMenuDialog : Command
{
	[Tooltip("The Menu Dialog to use for displaying menu buttons")]
	[SerializeField]
	protected MenuDialog menuDialog;

	public override void OnEnter()
	{
		if ((Object)(object)menuDialog != (Object)null)
		{
			MenuDialog.ActiveMenuDialog = menuDialog;
		}
		Continue();
	}

	public override string GetSummary()
	{
		if ((Object)(object)menuDialog == (Object)null)
		{
			return "Error: No menu dialog selected";
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
