using System;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x02000883 RID: 2179
	public class DetailSpawnData : ScriptableObject
	{
		// Token: 0x06003842 RID: 14402 RVA: 0x001A2960 File Offset: 0x001A0B60
		public DetailSpawnData.DetailSpawn GetTreePrefab()
		{
			int num = ProbabilityUtils.RandomChoiceFollowingDistribution(this.GetSpawnProbabilities(this.m_TreeList));
			return this.m_TreeList[num];
		}

		// Token: 0x06003843 RID: 14403 RVA: 0x001A2988 File Offset: 0x001A0B88
		public DetailSpawnData.DetailSpawn GetRockPrefab()
		{
			int num = ProbabilityUtils.RandomChoiceFollowingDistribution(this.GetSpawnProbabilities(this.m_RockList));
			return this.m_RockList[num];
		}

		// Token: 0x06003844 RID: 14404 RVA: 0x001A29B0 File Offset: 0x001A0BB0
		private List<float> GetSpawnProbabilities(DetailSpawnData.DetailSpawn[] array)
		{
			List<float> list = new List<float>();
			for (int i = 0; i < array.Length; i++)
			{
				list.Add((float)array[i].SpawnProbability);
			}
			return list;
		}

		// Token: 0x0400329A RID: 12954
		public DetailSpawnData.DetailSpawn[] m_TreeList;

		// Token: 0x0400329B RID: 12955
		public DetailSpawnData.DetailSpawn[] m_RockList;

		// Token: 0x02000884 RID: 2180
		[Serializable]
		public class DetailSpawn
		{
			// Token: 0x0400329C RID: 12956
			public GameObject Object;

			// Token: 0x0400329D RID: 12957
			[Range(0f, 100f)]
			public int SpawnProbability;

			// Token: 0x0400329E RID: 12958
			public Vector3 PositionOffset;

			// Token: 0x0400329F RID: 12959
			public Vector3 RandomRotation;
		}
	}
}
