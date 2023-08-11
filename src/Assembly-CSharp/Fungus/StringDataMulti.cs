using System;
using UnityEngine;

namespace Fungus;

[Serializable]
public struct StringDataMulti
{
	[SerializeField]
	[VariableProperty("<Value>", new Type[] { typeof(StringVariable) })]
	public StringVariable stringRef;

	[TextArea(1, 15)]
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

	public StringDataMulti(string v)
	{
		stringVal = v;
		stringRef = null;
	}

	public static implicit operator string(StringDataMulti spriteData)
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
