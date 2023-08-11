using System;
using UnityEngine;

namespace Fungus;

[CommandInfo("Variable", "Save Variable", "Save an Boolean, Integer, Float or String variable to persistent storage using a string key. The value can be loaded again later using the Load Variable command. You can also use the Set Save Profile command to manage separate save profiles for multiple players.", 0)]
[AddComponentMenu("")]
public class SaveVariable : Command
{
	[Tooltip("Name of the saved value. Supports variable substition e.g. \"player_{$PlayerNumber}")]
	[SerializeField]
	protected string key = "";

	[Tooltip("Variable to read the value from. Only Boolean, Integer, Float and String are supported.")]
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
				PlayerPrefs.SetInt(text, booleanVariable.Value ? 1 : 0);
			}
		}
		else if (type == typeof(IntegerVariable))
		{
			IntegerVariable integerVariable = variable as IntegerVariable;
			if ((Object)(object)integerVariable != (Object)null)
			{
				PlayerPrefs.SetInt(text, integerVariable.Value);
			}
		}
		else if (type == typeof(FloatVariable))
		{
			FloatVariable floatVariable = variable as FloatVariable;
			if ((Object)(object)floatVariable != (Object)null)
			{
				PlayerPrefs.SetFloat(text, floatVariable.Value);
			}
		}
		else if (type == typeof(StringVariable))
		{
			StringVariable stringVariable = variable as StringVariable;
			if ((Object)(object)stringVariable != (Object)null)
			{
				PlayerPrefs.SetString(text, stringVariable.Value);
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
		return variable.Key + " into '" + key + "'";
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
