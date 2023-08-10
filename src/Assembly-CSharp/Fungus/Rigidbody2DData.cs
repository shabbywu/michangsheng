using System;
using UnityEngine;

namespace Fungus;

[Serializable]
public struct Rigidbody2DData
{
	[SerializeField]
	[VariableProperty("<Value>", new Type[] { typeof(Rigidbody2DVariable) })]
	public Rigidbody2DVariable rigidbody2DRef;

	[SerializeField]
	public Rigidbody2D rigidbody2DVal;

	public Rigidbody2D Value
	{
		get
		{
			if (!((Object)(object)rigidbody2DRef == (Object)null))
			{
				return rigidbody2DRef.Value;
			}
			return rigidbody2DVal;
		}
		set
		{
			if ((Object)(object)rigidbody2DRef == (Object)null)
			{
				rigidbody2DVal = value;
			}
			else
			{
				rigidbody2DRef.Value = value;
			}
		}
	}

	public static implicit operator Rigidbody2D(Rigidbody2DData rigidbody2DData)
	{
		return rigidbody2DData.Value;
	}

	public Rigidbody2DData(Rigidbody2D v)
	{
		rigidbody2DVal = v;
		rigidbody2DRef = null;
	}

	public string GetDescription()
	{
		if ((Object)(object)rigidbody2DRef == (Object)null)
		{
			return ((object)rigidbody2DVal).ToString();
		}
		return rigidbody2DRef.Key;
	}
}
