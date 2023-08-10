using System;
using UnityEngine;

namespace Fungus;

[CommandInfo("Variable", "Load Variable", "Loads a saved value and stores it in a Boolean, Integer, Float or String variable. If the key is not found then the variable is not modified.", 0)]
[AddComponentMenu("")]
public class LoadVariable : Command
{
	[Tooltip("Name of the saved value. Supports variable substition e.g. \"player_{$PlayerNumber}\"")]
	[SerializeField]
	protected string key = "";

	[Tooltip("Variable to store the value in.")]
	[VariableProperty(new Type[]
	{
		typeof(BooleanVariable),
		typeof(IntegerVariable),
		typeof(FloatVariable),
		typeof(StringVariable)
	})]
	[SerializeField]
	protected Variable variable;

	public override void OnEnter()
	{
		if (key == "" || (Object)(object)variable == (Object)null)
		{
			Continue();
			return;
		}
		Flowchart flowchart = GetFlowchart();
		string text = SetSaveProfile.SaveProfile + "_" + flowchart.SubstituteVariables(key);
		Type type = ((object)variable).GetType();
		if (type == typeof(BooleanVariable))
		{
			BooleanVariable booleanVariable = variable as BooleanVariable;
			if ((Object)(object)booleanVariable != (Object)null)
			{
				booleanVariable.Value = PlayerPrefs.GetInt(text) == 1;
			}
		}
		else if (type == typeof(IntegerVariable))
		{
			IntegerVariable integerVariable = variable as IntegerVariable;
			if ((Object)(object)integerVariable != (Object)null)
			{
				integerVariable.Value = PlayerPrefs.GetInt(text);
			}
		}
		else if (type == typeof(FloatVariable))
		{
			FloatVariable floatVariable = variable as FloatVariable;
			if ((Object)(object)floatVariable != (Object)null)
			{
				floatVariable.Value = PlayerPrefs.GetFloat(text);
			}
		}
		else if (type == typeof(StringVariable))
		{
			StringVariable stringVariable = variable as StringVariable;
			if ((Object)(object)stringVariable != (Object)null)
			{
				stringVariable.Value = PlayerPrefs.GetString(text);
			}
		}
		Continue();
	}

	public override string GetSummary()
	{
		if (key.Length == 0)
		{
			return "Error: No stored value key selected";
		}
		if ((Object)(object)variable == (Object)null)
		{
			return "Error: No variable selected";
		}
		return "'" + key + "' into " + variable.Key;
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)235, (byte)191, (byte)217, byte.MaxValue));
	}

	public override bool HasReference(Variable in_variable)
	{
		if (!((Object)(object)variable == (Object)(object)in_variable))
		{
			return base.HasReference(in_variable);
		}
		return true;
	}
}
