using UnityEngine;
using UnityEngine.UI;

namespace Fungus;

[CommandInfo("UI", "Set Toggle State", "Sets the state of a toggle UI object", 0)]
public class SetToggleState : Command
{
	[Tooltip("Target toggle object to set the state on")]
	[SerializeField]
	protected Toggle toggle;

	[Tooltip("Boolean value to set the toggle state to.")]
	[SerializeField]
	protected BooleanData value;

	public override void OnEnter()
	{
		if ((Object)(object)toggle != (Object)null)
		{
			toggle.isOn = value.Value;
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
		return ((Object)toggle).name + " = " + value.GetDescription();
	}

	public override bool HasReference(Variable variable)
	{
		if (!((Object)(object)value.booleanRef == (Object)(object)variable))
		{
			return base.HasReference(variable);
		}
		return true;
	}
}
