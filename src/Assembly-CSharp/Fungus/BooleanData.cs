using System;
using UnityEngine;

namespace Fungus;

[Serializable]
public struct BooleanData
{
	[SerializeField]
	[VariableProperty("<Value>", new Type[] { typeof(BooleanVariable) })]
	public BooleanVariable booleanRef;

	[SerializeField]
	public bool booleanVal;

	public bool Value
	{
		get
		{
			if (!((Object)(object)booleanRef == (Object)null))
			{
				return booleanRef.Value;
			}
			return booleanVal;
		}
		set
		{
			if ((Object)(object)booleanRef == (Object)null)
			{
				booleanVal = value;
			}
			else
			{
				booleanRef.Value = value;
			}
		}
	}

	public BooleanData(bool v)
	{
		booleanVal = v;
		booleanRef = null;
	}

	public static implicit operator bool(BooleanData booleanData)
	{
		return booleanData.Value;
	}

	public string GetDescription()
	{
		if ((Object)(object)booleanRef == (Object)null)
		{
			return booleanVal.ToString();
		}
		return booleanRef.Key;
	}
}
