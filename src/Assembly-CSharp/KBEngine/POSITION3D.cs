using UnityEngine;

namespace KBEngine;

public struct POSITION3D
{
	private Vector3 value;

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

	private POSITION3D(Vector3 value)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		this.value = value;
	}

	public static implicit operator Vector3(POSITION3D value)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		return value.value;
	}

	public static implicit operator POSITION3D(Vector3 value)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return new POSITION3D(value);
	}
}
