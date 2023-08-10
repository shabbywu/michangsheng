using System;
using System.Globalization;
using UnityEngine;

namespace Fungus;

[CommandInfo("Variable", "From String", "Attempts to parse a string into a given fungus variable type, such as integer or float", 0)]
[AddComponentMenu("")]
public class FromString : Command
{
	[Tooltip("Source of string data to parse into another variables value")]
	[VariableProperty(new Type[] { typeof(StringVariable) })]
	[SerializeField]
	protected StringVariable sourceString;

	[Tooltip("The variable type to be parsed and value stored within")]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable),
		typeof(FloatVariable)
	})]
	[SerializeField]
	protected Variable outValue;

	public override void OnEnter()
	{
		if ((Object)(object)sourceString != (Object)null && (Object)(object)outValue != (Object)null)
		{
			double num = 0.0;
			try
			{
				num = Convert.ToDouble(sourceString.Value, CultureInfo.CurrentCulture);
			}
			catch (Exception)
			{
				Debug.LogWarning((object)("Failed to parse as number: " + sourceString.Value));
			}
			IntegerVariable integerVariable = outValue as IntegerVariable;
			if ((Object)(object)integerVariable != (Object)null)
			{
				integerVariable.Value = (int)num;
			}
			else
			{
				FloatVariable floatVariable = outValue as FloatVariable;
				if ((Object)(object)floatVariable != (Object)null)
				{
					floatVariable.Value = (float)num;
				}
			}
		}
		Continue();
	}

	public override string GetSummary()
	{
		if ((Object)(object)sourceString == (Object)null)
		{
			return "Error: No source string selected";
		}
		if ((Object)(object)outValue == (Object)null)
		{
			return "Error: No type and storage variable selected";
		}
		return outValue.Key + ".Parse " + sourceString.Key;
	}

	public override bool HasReference(Variable variable)
	{
		if (!((Object)(object)variable == (Object)(object)sourceString))
		{
			return (Object)(object)variable == (Object)(object)outValue;
		}
		return true;
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)253, (byte)253, (byte)150, byte.MaxValue));
	}
}
