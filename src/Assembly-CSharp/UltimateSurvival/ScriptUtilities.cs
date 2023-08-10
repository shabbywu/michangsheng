using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival;

public static class ScriptUtilities
{
	public static List<Transform> GetTransformsByTag(string tag)
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag(tag);
		List<Transform> list = new List<Transform>();
		for (int i = 0; i < array.Length; i++)
		{
			list.Add(array[i].transform);
		}
		return list;
	}

	public static bool GetTransformsPositionsByTag(string tag, out List<Vector3> posS)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		bool result = false;
		GameObject[] array = GameObject.FindGameObjectsWithTag(tag);
		posS = new List<Vector3>();
		for (int i = 0; i < array.Length; i++)
		{
			posS.Add(array[i].transform.position);
		}
		if (posS.Count > 0)
		{
			result = true;
		}
		return result;
	}

	public static List<Vector3> GetRandomPositionsAroundTransform(Transform transform, int amount = 5, int radius = 5, float distanceBtwPoints = 5f)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		List<Vector3> list = new List<Vector3>();
		for (int i = 0; i < amount; i++)
		{
			Vector3 val = Random.insideUnitSphere * (float)radius + transform.position;
			val.y = transform.position.y;
			if (list.IndexIsValid(i - 1))
			{
				float num = Vector3.Distance(val, list[i - 1]);
				if (num < distanceBtwPoints)
				{
					val += new Vector3(num, 0f, num);
				}
			}
			list.Add(val);
		}
		return list;
	}
}
