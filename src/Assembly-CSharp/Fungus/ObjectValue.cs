using System;
using MarkerMetro.Unity.WinLegacy.Reflection;
using UnityEngine;

namespace Fungus;

[Serializable]
public class ObjectValue
{
	public string typeAssemblyname;

	public string typeFullname;

	public int intValue;

	public bool boolValue;

	public float floatValue;

	public string stringValue;

	public Color colorValue;

	public GameObject gameObjectValue;

	public Material materialValue;

	public Object objectValue;

	public Sprite spriteValue;

	public Texture textureValue;

	public Vector2 vector2Value;

	public Vector3 vector3Value;

	public object GetValue()
	{
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		switch (typeFullname)
		{
		case "System.Int32":
			return intValue;
		case "System.Boolean":
			return boolValue;
		case "System.Single":
			return floatValue;
		case "System.String":
			return stringValue;
		case "UnityEngine.Color":
			return colorValue;
		case "UnityEngine.GameObject":
			return gameObjectValue;
		case "UnityEngine.Material":
			return materialValue;
		case "UnityEngine.Sprite":
			return spriteValue;
		case "UnityEngine.Texture":
			return textureValue;
		case "UnityEngine.Vector2":
			return vector2Value;
		case "UnityEngine.Vector3":
			return vector3Value;
		default:
		{
			Type type = ReflectionHelper.GetType(typeAssemblyname);
			if (type.IsSubclassOf(typeof(Object)))
			{
				return objectValue;
			}
			if (type.IsEnum())
			{
				return Enum.ToObject(type, intValue);
			}
			return null;
		}
		}
	}
}
