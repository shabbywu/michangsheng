using System;
using UnityEngine;

namespace Fungus;

[Serializable]
public struct MaterialData
{
	[SerializeField]
	[VariableProperty("<Value>", new Type[] { typeof(MaterialVariable) })]
	public MaterialVariable materialRef;

	[SerializeField]
	public Material materialVal;

	public Material Value
	{
		get
		{
			if (!((Object)(object)materialRef == (Object)null))
			{
				return materialRef.Value;
			}
			return materialVal;
		}
		set
		{
			if ((Object)(object)materialRef == (Object)null)
			{
				materialVal = value;
			}
			else
			{
				materialRef.Value = value;
			}
		}
	}

	public MaterialData(Material v)
	{
		materialVal = v;
		materialRef = null;
	}

	public static implicit operator Material(MaterialData materialData)
	{
		return materialData.Value;
	}

	public string GetDescription()
	{
		if ((Object)(object)materialRef == (Object)null)
		{
			return ((object)materialVal).ToString();
		}
		return materialRef.Key;
	}
}
