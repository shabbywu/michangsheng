using System;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005C3 RID: 1475
	public class DetailSpawnData : ScriptableObject
	{
		// Token: 0x06002FB9 RID: 12217 RVA: 0x0015893C File Offset: 0x00156B3C
		public DetailSpawnData.DetailSpawn GetTreePrefab()
		{
			int num = ProbabilityUtils.RandomChoiceFollowingDistribution(this.GetSpawnProbabilities(this.m_TreeList));
			return this.m_TreeList[num];
		}

		// Token: 0x06002FBA RID: 12218 RVA: 0x00158964 File Offset: 0x00156B64
		public DetailSpawnData.DetailSpawn GetRockPrefab()
		{
			int num = ProbabilityUtils.RandomChoiceFollowingDistribution(this.GetSpawnProbabilities(this.m_RockList));
			return this.m_RockList[num];
		}

		// Token: 0x06002FBB RID: 12219 RVA: 0x0015898C File Offset: 0x00156B8C
		private List<float> GetSpawnProbabilities(DetailSpawnData.DetailSpawn[] array)
		{
			List<float> list = new List<float>();
			for (int i = 0; i < array.Length; i++)
			{
				list.Add((float)array[i].SpawnProbability);
			}
			return list;
		}

		// Token: 0x04002A0C RID: 10764
		public DetailSpawnData.DetailSpawn[] m_TreeList;

		// Token: 0x04002A0D RID: 10765
		public DetailSpawnData.DetailSpawn[] m_RockList;

		// Token: 0x020014AA RID: 5290
		[Serializable]
		public class DetailSpawn
		{
			// Token: 0x04006CD0 RID: 27856
			public GameObject Object;

			// Token: 0x04006CD1 RID: 27857
			[Range(0f, 100f)]
			public int SpawnProbability;

			// Token: 0x04006CD2 RID: 27858
			public Vector3 PositionOffset;

			// Token: 0x04006CD3 RID: 27859
			public Vector3 RandomRotation;
		}
	}
}
