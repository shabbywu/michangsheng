using System;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x0200090E RID: 2318
	public class ProbabilityUtils : MonoBehaviour
	{
		// Token: 0x06003B3B RID: 15163 RVA: 0x001ABA4C File Offset: 0x001A9C4C
		public static int RandomChoiceFollowingDistribution(List<float> probabilities)
		{
			float[] array = new float[probabilities.Count];
			float num = 0f;
			for (int i = 0; i < probabilities.Count; i++)
			{
				array[i] = num + probabilities[i];
				num = array[i];
			}
			float value = Random.Range(0f, array[probabilities.Count - 1]);
			int num2 = Array.BinarySearch<float>(array, value);
			if (num2 < 0)
			{
				num2 = ~num2;
			}
			return num2;
		}
	}
}
