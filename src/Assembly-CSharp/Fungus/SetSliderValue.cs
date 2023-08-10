using UnityEngine;
using UnityEngine.UI;

namespace Fungus;

[CommandInfo("UI", "Set Slider Value", "Sets the value property of a slider object", 0)]
public class SetSliderValue : Command
{
	[Tooltip("Target slider object to set the value on")]
	[SerializeField]
	protected Slider slider;

	[Tooltip("Float value to set the slider value to.")]
	[SerializeField]
	protected FloatData value;

	public override void OnEnter()
	{
		if ((Object)(object)slider != (Object)null)
		{
			slider.value = value;
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
		if ((Object)(object)slider == (Object)null)
		{
			return "Error: Slider object not selected";
		}
		return ((Object)slider).name + " = " + value.GetDescription();
	}

	public override bool HasReference(Variable variable)
	{
		if (!((Object)(object)value.floatRef == (Object)(object)variable))
		{
			return base.HasReference(variable);
		}
		return true;
	}
}
