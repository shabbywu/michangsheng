using System;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x0200090F RID: 2319
	public static class ScriptUtilities
	{
		// Token: 0x06003B3D RID: 15165 RVA: 0x001ABAB8 File Offset: 0x001A9CB8
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

		// Token: 0x06003B3E RID: 15166 RVA: 0x001ABAF0 File Offset: 0x001A9CF0
		public static bool GetTransformsPositionsByTag(string tag, out List<Vector3> posS)
		{
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

		// Token: 0x06003B3F RID: 15167 RVA: 0x001ABB3C File Offset: 0x001A9D3C
		public static List<Vector3> GetRandomPositionsAroundTransform(Transform transform, int amount = 5, int radius = 5, float distanceBtwPoints = 5f)
		{
			List<Vector3> list = new List<Vector3>();
			for (int i = 0; i < amount; i++)
			{
				Vector3 vector = Random.insideUnitSphere * (float)radius + transform.position;
				vector.y = transform.position.y;
				if (list.IndexIsValid(i - 1))
				{
					float num = Vector3.Distance(vector, list[i - 1]);
					if (num < distanceBtwPoints)
					{
						vector += new Vector3(num, 0f, num);
					}
				}
				list.Add(vector);
			}
			return list;
		}
	}
}
