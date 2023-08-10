using UnityEngine;

namespace Fungus;

[CommandInfo("Narrative", "Set Say Dialog", "Sets a custom say dialog to use when displaying story text", 0)]
[AddComponentMenu("")]
public class SetSayDialog : Command
{
	[Tooltip("The Say Dialog to use for displaying Say story text")]
	[SerializeField]
	protected SayDialog sayDialog;

	public override void OnEnter()
	{
		if ((Object)(object)sayDialog != (Object)null)
		{
			SayDialog.ActiveSayDialog = sayDialog;
		}
		Continue();
	}

	public override string GetSummary()
	{
		if ((Object)(object)sayDialog == (Object)null)
		{
			return "Error: No say dialog selected";
		}
		return ((Object)sayDialog).name;
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)184, (byte)210, (byte)235, byte.MaxValue));
	}
}
