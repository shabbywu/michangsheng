using System;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x02000626 RID: 1574
	public class ProbabilityUtils : MonoBehaviour
	{
		// Token: 0x06003201 RID: 12801 RVA: 0x001620E0 File Offset: 0x001602E0
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
