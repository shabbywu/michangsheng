using System;
using UnityEngine;

[Serializable]
public struct SphericalVector
{
	public float Length;

	public float Zenith;

	public float Azimuth;

	public Vector3 Position => Length * Direction;

	public Vector3 Direction
	{
		get
		{
			//IL_0052: Unknown result type (might be due to invalid IL or missing references)
			float num = Zenith * (float)Math.PI / 2f;
			Vector3 result = default(Vector3);
			result.y = Mathf.Sin(num);
			float num2 = Mathf.Cos(num);
			float num3 = Azimuth * (float)Math.PI;
			result.x = num2 * Mathf.Sin(num3);
			result.z = num2 * Mathf.Cos(num3);
			return result;
		}
	}

	public SphericalVector(float azimuth, float zenith, float length)
	{
		Length = length;
		Zenith = zenith;
		Azimuth = azimuth;
	}

	public override string ToString()
	{
		return $"Azimuth {Azimuth:0.0000} : Zenith {Zenith:0.0000} : Length {Length:0.0000}]";
	}
}
