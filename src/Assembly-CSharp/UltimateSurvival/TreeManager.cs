using System;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x0200060C RID: 1548
	[RequireComponent(typeof(Terrain))]
	public class TreeManager : MonoBehaviour
	{
		// Token: 0x06003184 RID: 12676 RVA: 0x0015F844 File Offset: 0x0015DA44
		private void Awake()
		{
			this.m_Terrain = base.GetComponent<Terrain>();
			this.m_InitialTrees = this.m_Terrain.terrainData.treeInstances;
			TreeInstance[] treeInstances = this.m_Terrain.terrainData.treeInstances;
			for (int i = 0; i < treeInstances.Length; i++)
			{
				TreeCreator treeCreator = this.GetTreeCreator(treeInstances[i].prototypeIndex);
				if (treeCreator != null)
				{
					this.m_Trees.Add(treeCreator.CreateTree(this.m_Terrain, treeInstances[i], i));
				}
			}
		}

		// Token: 0x06003185 RID: 12677 RVA: 0x0015F8C8 File Offset: 0x0015DAC8
		private TreeCreator GetTreeCreator(int prototypeIndex)
		{
			for (int i = 0; i < this.m_TreeCreators.Length; i++)
			{
				if (this.m_TreeCreators[i].PrototypeIndex == prototypeIndex)
				{
					return this.m_TreeCreators[i];
				}
			}
			return null;
		}

		// Token: 0x06003186 RID: 12678 RVA: 0x0015F902 File Offset: 0x0015DB02
		private void OnApplicationQuit()
		{
			this.m_Terrain.terrainData.treeInstances = this.m_InitialTrees;
		}

		// Token: 0x04002BB9 RID: 11193
		[SerializeField]
		private TreeCreator[] m_TreeCreators;

		// Token: 0x04002BBA RID: 11194
		private Terrain m_Terrain;

		// Token: 0x04002BBB RID: 11195
		private List<MineableObject> m_Trees = new List<MineableObject>();

		// Token: 0x04002BBC RID: 11196
		private TreeInstance[] m_InitialTrees;
	}
}
