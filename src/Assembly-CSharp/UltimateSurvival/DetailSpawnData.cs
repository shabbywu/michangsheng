using System;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival;

public class DetailSpawnData : ScriptableObject
{
	[Serializable]
	public class DetailSpawn
	{
		public GameObject Object;

		[Range(0f, 100f)]
		public int SpawnProbability;

		public Vector3 PositionOffset;

		public Vector3 RandomRotation;
	}

	public DetailSpawn[] m_TreeList;

	public DetailSpawn[] m_RockList;

	public DetailSpawn GetTreePrefab()
	{
		int num = ProbabilityUtils.RandomChoiceFollowingDistribution(GetSpawnProbabilities(m_TreeList));
		return m_TreeList[num];
	}

	public DetailSpawn GetRockPrefab()
	{
		int num = ProbabilityUtils.RandomChoiceFollowingDistribution(GetSpawnProbabilities(m_RockList));
		return m_RockList[num];
	}

	private List<float> GetSpawnProbabilities(DetailSpawn[] array)
	{
		List<float> list = new List<float>();
		for (int i = 0; i < array.Length; i++)
		{
			list.Add(array[i].SpawnProbability);
		}
		return list;
	}
}
