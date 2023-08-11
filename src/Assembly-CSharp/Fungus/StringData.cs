using System;
using UnityEngine;

namespace Fungus;

[Serializable]
public struct StringData
{
	[SerializeField]
	[VariableProperty("<Value>", new Type[] { typeof(StringVariable) })]
	public StringVariable stringRef;

	[SerializeField]
	public string stringVal;

	public string Value
	{
		get
		{
			if (stringVal == null)
			{
				stringVal = "";
			}
			if (!((Object)(object)stringRef == (Object)null))
			{
				return stringRef.Value;
			}
			return stringVal;
		}
		set
		{
			if ((Object)(object)stringRef == (Object)null)
			{
				stringVal = value;
			}
			else
			{
				stringRef.Value = value;
			}
		}
	}

	public StringData(string v)
	{
		stringVal = v;
		stringRef = null;
	}

	public static implicit operator string(StringData spriteData)
	{
		return spriteData.Value;
	}

	public string GetDescription()
	{
		if ((Object)(object)stringRef == (Object)null)
		{
			return stringVal;
		}
		return stringRef.Key;
	}
}
