using System;
using UnityEngine;

namespace Fungus;

[Serializable]
public struct TransformData
{
	[SerializeField]
	[VariableProperty("<Value>", new Type[] { typeof(TransformVariable) })]
	public TransformVariable transformRef;

	[SerializeField]
	public Transform transformVal;

	public Transform Value
	{
		get
		{
			if (!((Object)(object)transformRef == (Object)null))
			{
				return transformRef.Value;
			}
			return transformVal;
		}
		set
		{
			if ((Object)(object)transformRef == (Object)null)
			{
				transformVal = value;
			}
			else
			{
				transformRef.Value = value;
			}
		}
	}

	public TransformData(Transform v)
	{
		transformVal = v;
		transformRef = null;
	}

	public static implicit operator Transform(TransformData vector3Data)
	{
		return vector3Data.Value;
	}

	public string GetDescription()
	{
		if ((Object)(object)transformRef == (Object)null)
		{
			return ((object)transformVal).ToString();
		}
		return transformRef.Key;
	}
}
