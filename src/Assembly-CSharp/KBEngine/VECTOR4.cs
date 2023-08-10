using UnityEngine;

namespace KBEngine;

public struct VECTOR4
{
	private Vector4 value;

	public float x
	{
		get
		{
			return value.x;
		}
		set
		{
			this.value.x = value;
		}
	}

	public float y
	{
		get
		{
			return value.y;
		}
		set
		{
			this.value.y = value;
		}
	}

	public float z
	{
		get
		{
			return value.z;
		}
		set
		{
			this.value.z = value;
		}
	}

	public float w
	{
		get
		{
			return value.w;
		}
		set
		{
			this.value.w = value;
		}
	}

	private VECTOR4(Vector4 value)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		this.value = value;
	}

	public static implicit operator Vector4(VECTOR4 value)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		return value.value;
	}

	public static implicit operator VECTOR4(Vector4 value)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return new VECTOR4(value);
	}
}
