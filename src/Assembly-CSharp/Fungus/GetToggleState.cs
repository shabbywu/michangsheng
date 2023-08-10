using System;
using UnityEngine;
using UnityEngine.UI;

namespace Fungus;

[CommandInfo("UI", "Get Toggle State", "Gets the state of a toggle UI object and stores it in a boolean variable.", 0)]
public class GetToggleState : Command
{
	[Tooltip("Target toggle object to get the value from")]
	[SerializeField]
	protected Toggle toggle;

	[Tooltip("Boolean variable to store the state of the toggle value in.")]
	[VariableProperty(new Type[] { typeof(BooleanVariable) })]
	[SerializeField]
	protected BooleanVariable toggleState;

	public override void OnEnter()
	{
		if ((Object)(object)toggle != (Object)null && (Object)(object)toggleState != (Object)null)
		{
			toggleState.Value = toggle.isOn;
		}
		Continue();
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)235, (byte)191, (byte)217, byte.MaxValue));
	}

	public override string GetSummary()
	{
		if ((Object)(object)toggle == (Object)null)
		{
			return "Error: Toggle object not selected";
		}
		if ((Object)(object)toggleState == (Object)null)
		{
			return "Error: Toggle state variable not selected";
		}
		return ((Object)toggle).name;
	}

	public override bool HasReference(Variable variable)
	{
		if (!((Object)(object)toggleState == (Object)(object)variable))
		{
			return base.HasReference(variable);
		}
		return true;
	}
}
