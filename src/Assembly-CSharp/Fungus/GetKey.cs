using System;
using UnityEngine;

namespace Fungus;

[CommandInfo("Input", "GetKey", "Store Input.GetKey in a variable. Supports an optional Negative key input. A negative value will be overridden by a positive one, they do not add.", 0)]
[AddComponentMenu("")]
public class GetKey : Command
{
	public enum InputKeyQueryType
	{
		Down,
		Up,
		State
	}

	[SerializeField]
	protected KeyCode keyCode;

	[Tooltip("Optional, secondary or negative keycode. For booleans will also set to true, for int and float will set to -1.")]
	[SerializeField]
	protected KeyCode keyCodeNegative;

	[SerializeField]
	[Tooltip("Only used if KeyCode is KeyCode.None, expects a name of the key to use.")]
	protected StringData keyCodeName = new StringData(string.Empty);

	[SerializeField]
	[Tooltip("Optional, secondary or negative keycode. For booleans will also set to true, for int and float will set to -1.Only used if KeyCode is KeyCode.None, expects a name of the key to use.")]
	protected StringData keyCodeNameNegative = new StringData(string.Empty);

	[Tooltip("Do we want an Input.GetKeyDown, GetKeyUp or GetKey")]
	[SerializeField]
	protected InputKeyQueryType keyQueryType = InputKeyQueryType.State;

	[Tooltip("Will store true or false or 0 or 1 depending on type. Sets true or -1 for negative key values.")]
	[SerializeField]
	[VariableProperty(new Type[]
	{
		typeof(FloatVariable),
		typeof(BooleanVariable),
		typeof(IntegerVariable)
	})]
	protected Variable outValue;

	public override void OnEnter()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		FillOutValue(0);
		if ((int)keyCodeNegative != 0)
		{
			DoKeyCode(keyCodeNegative, -1);
		}
		else if (!string.IsNullOrEmpty(keyCodeNameNegative))
		{
			DoKeyName(keyCodeNameNegative, -1);
		}
		if ((int)keyCode != 0)
		{
			DoKeyCode(keyCode, 1);
		}
		else if (!string.IsNullOrEmpty(keyCodeName))
		{
			DoKeyName(keyCodeName, 1);
		}
		Continue();
	}

	private void DoKeyCode(KeyCode key, int trueVal)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		switch (keyQueryType)
		{
		case InputKeyQueryType.Down:
			if (Input.GetKeyDown(key))
			{
				FillOutValue(trueVal);
			}
			break;
		case InputKeyQueryType.Up:
			if (Input.GetKeyUp(key))
			{
				FillOutValue(trueVal);
			}
			break;
		case InputKeyQueryType.State:
			if (Input.GetKey(key))
			{
				FillOutValue(trueVal);
			}
			break;
		}
	}

	private void DoKeyName(string key, int trueVal)
	{
		switch (keyQueryType)
		{
		case InputKeyQueryType.Down:
			if (Input.GetKeyDown(key))
			{
				FillOutValue(trueVal);
			}
			break;
		case InputKeyQueryType.Up:
			if (Input.GetKeyUp(key))
			{
				FillOutValue(trueVal);
			}
			break;
		case InputKeyQueryType.State:
			if (Input.GetKey(key))
			{
				FillOutValue(trueVal);
			}
			break;
		}
	}

	private void FillOutValue(int v)
	{
		FloatVariable floatVariable = outValue as FloatVariable;
		if ((Object)(object)floatVariable != (Object)null)
		{
			floatVariable.Value = v;
			return;
		}
		BooleanVariable booleanVariable = outValue as BooleanVariable;
		if ((Object)(object)booleanVariable != (Object)null)
		{
			booleanVariable.Value = ((v != 0) ? true : false);
			return;
		}
		IntegerVariable integerVariable = outValue as IntegerVariable;
		if ((Object)(object)integerVariable != (Object)null)
		{
			integerVariable.Value = v;
		}
	}

	public override string GetSummary()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)outValue == (Object)null)
		{
			return "Error: no outvalue set";
		}
		return (((int)keyCode != 0) ? ((object)(KeyCode)(ref keyCode)).ToString() : ((string)keyCodeName)) + " in " + outValue.Key;
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)235, (byte)191, (byte)217, byte.MaxValue));
	}

	public override bool HasReference(Variable variable)
	{
		if ((Object)(object)keyCodeName.stringRef == (Object)(object)variable || (Object)(object)outValue == (Object)(object)variable || (Object)(object)keyCodeNameNegative.stringRef == (Object)(object)variable)
		{
			return true;
		}
		return false;
	}
}
