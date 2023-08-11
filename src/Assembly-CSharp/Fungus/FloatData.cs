using System;
using UnityEngine;

namespace Fungus;

[Serializable]
public struct FloatData
{
	[SerializeField]
	[VariableProperty("<Value>", new Type[] { typeof(FloatVariable) })]
	public FloatVariable floatRef;

	[SerializeField]
	public float floatVal;

	public float Value
	{
		get
		{
			if (!((Object)(object)floatRef == (Object)null))
			{
				return floatRef.Value;
			}
			return floatVal;
		}
		set
		{
			if ((Object)(object)floatRef == (Object)null)
			{
				floatVal = value;
			}
			else
			{
				floatRef.Value = value;
			}
		}
	}

	public FloatData(float v)
	{
		floatVal = v;
		floatRef = null;
	}

	public static implicit operator float(FloatData floatData)
	{
		return floatData.Value;
	}

	public string GetDescription()
	{
		if ((Object)(object)floatRef == (Object)null)
		{
			return floatVal.ToString();
		}
		return floatRef.Key;
	}
}
