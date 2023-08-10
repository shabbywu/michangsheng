using UnityEngine;

namespace Fungus;

[CommandInfo("Sprite", "Set Clickable 2D", "Sets a Clickable2D component to be clickable / non-clickable.", 0)]
[AddComponentMenu("")]
public class SetClickable2D : Command
{
	[Tooltip("Reference to Clickable2D component on a gameobject")]
	[SerializeField]
	protected Clickable2D targetClickable2D;

	[Tooltip("Set to true to enable the component")]
	[SerializeField]
	protected BooleanData activeState;

	public override void OnEnter()
	{
		if ((Object)(object)targetClickable2D != (Object)null)
		{
			targetClickable2D.ClickEnabled = activeState.Value;
		}
		Continue();
	}

	public override string GetSummary()
	{
		if ((Object)(object)targetClickable2D == (Object)null)
		{
			return "Error: No Clickable2D component selected";
		}
		return ((Object)((Component)targetClickable2D).gameObject).name;
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)235, (byte)191, (byte)217, byte.MaxValue));
	}

	public override bool HasReference(Variable variable)
	{
		if (!((Object)(object)activeState.booleanRef == (Object)(object)variable))
		{
			return base.HasReference(variable);
		}
		return true;
	}
}
