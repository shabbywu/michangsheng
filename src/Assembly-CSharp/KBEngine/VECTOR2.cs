using UnityEngine;

namespace KBEngine;

public struct VECTOR2
{
	private Vector2 value;

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

	private VECTOR2(Vector2 value)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		this.value = value;
	}

	public static implicit operator Vector2(VECTOR2 value)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		return value.value;
	}

	public static implicit operator VECTOR2(Vector2 value)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return new VECTOR2(value);
	}
}
