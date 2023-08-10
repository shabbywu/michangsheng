using System;
using UnityEngine;

namespace Fungus;

[Serializable]
public struct Vector3Data
{
	[SerializeField]
	[VariableProperty("<Value>", new Type[] { typeof(Vector3Variable) })]
	public Vector3Variable vector3Ref;

	[SerializeField]
	public Vector3 vector3Val;

	public Vector3 Value
	{
		get
		{
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0014: Unknown result type (might be due to invalid IL or missing references)
			if (!((Object)(object)vector3Ref == (Object)null))
			{
				return vector3Ref.Value;
			}
			return vector3Val;
		}
		set
		{
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			if ((Object)(object)vector3Ref == (Object)null)
			{
				vector3Val = value;
			}
			else
			{
				vector3Ref.Value = value;
			}
		}
	}

	public Vector3Data(Vector3 v)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		vector3Val = v;
		vector3Ref = null;
	}

	public static implicit operator Vector3(Vector3Data vector3Data)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		return vector3Data.Value;
	}

	public string GetDescription()
	{
		if ((Object)(object)vector3Ref == (Object)null)
		{
			return ((object)(Vector3)(ref vector3Val)).ToString();
		}
		return vector3Ref.Key;
	}
}
