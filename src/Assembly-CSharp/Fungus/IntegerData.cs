using System;
using UnityEngine;

namespace Fungus;

[Serializable]
public struct IntegerData
{
	[SerializeField]
	[VariableProperty("<Value>", new Type[] { typeof(IntegerVariable) })]
	public IntegerVariable integerRef;

	[SerializeField]
	public int integerVal;

	public int Value
	{
		get
		{
			if (!((Object)(object)integerRef == (Object)null))
			{
				return integerRef.Value;
			}
			return integerVal;
		}
		set
		{
			if ((Object)(object)integerRef == (Object)null)
			{
				integerVal = value;
			}
			else
			{
				integerRef.Value = value;
			}
		}
	}

	public IntegerData(int v)
	{
		integerVal = v;
		integerRef = null;
	}

	public static implicit operator int(IntegerData integerData)
	{
		return integerData.Value;
	}

	public string GetDescription()
	{
		if ((Object)(object)integerRef == (Object)null)
		{
			return integerVal.ToString();
		}
		return integerRef.Key;
	}
}
