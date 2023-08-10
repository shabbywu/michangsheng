using System;
using UnityEngine;

namespace Fungus;

[Serializable]
public struct ColorData
{
	[SerializeField]
	[VariableProperty("<Value>", new Type[] { typeof(ColorVariable) })]
	public ColorVariable colorRef;

	[SerializeField]
	public Color colorVal;

	public Color Value
	{
		get
		{
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0014: Unknown result type (might be due to invalid IL or missing references)
			if (!((Object)(object)colorRef == (Object)null))
			{
				return colorRef.Value;
			}
			return colorVal;
		}
		set
		{
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			if ((Object)(object)colorRef == (Object)null)
			{
				colorVal = value;
			}
			else
			{
				colorRef.Value = value;
			}
		}
	}

	public ColorData(Color v)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		colorVal = v;
		colorRef = null;
	}

	public static implicit operator Color(ColorData colorData)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		return colorData.Value;
	}

	public string GetDescription()
	{
		if ((Object)(object)colorRef == (Object)null)
		{
			return ((object)(Color)(ref colorVal)).ToString();
		}
		return colorRef.Key;
	}
}
