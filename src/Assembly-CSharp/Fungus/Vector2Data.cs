using System;
using UnityEngine;

namespace Fungus;

[Serializable]
public struct Vector2Data
{
	[SerializeField]
	[VariableProperty("<Value>", new Type[] { typeof(Vector2Variable) })]
	public Vector2Variable vector2Ref;

	[SerializeField]
	public Vector2 vector2Val;

	public Vector2 Value
	{
		get
		{
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0014: Unknown result type (might be due to invalid IL or missing references)
			if (!((Object)(object)vector2Ref == (Object)null))
			{
				return vector2Ref.Value;
			}
			return vector2Val;
		}
		set
		{
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			if ((Object)(object)vector2Ref == (Object)null)
			{
				vector2Val = value;
			}
			else
			{
				vector2Ref.Value = value;
			}
		}
	}

	public Vector2Data(Vector2 v)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		vector2Val = v;
		vector2Ref = null;
	}

	public static implicit operator Vector2(Vector2Data vector2Data)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		return vector2Data.Value;
	}

	public string GetDescription()
	{
		if ((Object)(object)vector2Ref == (Object)null)
		{
			return ((object)(Vector2)(ref vector2Val)).ToString();
		}
		return vector2Ref.Key;
	}
}
