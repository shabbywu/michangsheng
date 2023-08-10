using System;
using UnityEngine;

namespace Fungus;

[Serializable]
public struct ObjectData
{
	[SerializeField]
	[VariableProperty("<Value>", new Type[] { typeof(ObjectVariable) })]
	public ObjectVariable objectRef;

	[SerializeField]
	public Object objectVal;

	public Object Value
	{
		get
		{
			if (!((Object)(object)objectRef == (Object)null))
			{
				return objectRef.Value;
			}
			return objectVal;
		}
		set
		{
			if ((Object)(object)objectRef == (Object)null)
			{
				objectVal = value;
			}
			else
			{
				objectRef.Value = value;
			}
		}
	}

	public ObjectData(Object v)
	{
		objectVal = v;
		objectRef = null;
	}

	public static implicit operator Object(ObjectData objectData)
	{
		return objectData.Value;
	}

	public string GetDescription()
	{
		if ((Object)(object)objectRef == (Object)null)
		{
			return ((object)objectVal).ToString();
		}
		return objectRef.Key;
	}
}
