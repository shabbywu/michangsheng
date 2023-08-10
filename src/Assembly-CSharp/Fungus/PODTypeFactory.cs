using UnityEngine;

namespace Fungus;

public static class PODTypeFactory
{
	public static Color color(float r, float g, float b, float a)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		return new Color(r, g, b, a);
	}

	public static Vector2 vector2(float x, float y)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(x, y);
	}

	public static Vector3 vector3(float x, float y, float z)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		return new Vector3(x, y, z);
	}

	public static Vector4 vector4(float x, float y, float z, float w)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		return new Vector4(x, y, z, w);
	}

	public static Quaternion quaternion(float x, float y, float z)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		return Quaternion.Euler(x, y, z);
	}

	public static Rect rect(float x, float y, float width, float height)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		return new Rect(x, y, width, height);
	}
}
