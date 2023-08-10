using System;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival;

public class ProbabilityUtils : MonoBehaviour
{
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
		int num2 = Array.BinarySearch(array, value);
		if (num2 < 0)
		{
			num2 = ~num2;
		}
		return num2;
	}
}
