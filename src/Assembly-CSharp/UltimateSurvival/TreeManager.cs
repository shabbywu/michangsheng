using System;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020008ED RID: 2285
	[RequireComponent(typeof(Terrain))]
	public class TreeManager : MonoBehaviour
	{
		// Token: 0x06003A9B RID: 15003 RVA: 0x001A8E10 File Offset: 0x001A7010
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

		// Token: 0x06003A9C RID: 15004 RVA: 0x001A8E94 File Offset: 0x001A7094
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

		// Token: 0x06003A9D RID: 15005 RVA: 0x0002A95E File Offset: 0x00028B5E
		private void OnApplicationQuit()
		{
			this.m_Terrain.terrainData.treeInstances = this.m_InitialTrees;
		}

		// Token: 0x040034C5 RID: 13509
		[SerializeField]
		private TreeCreator[] m_TreeCreators;

		// Token: 0x040034C6 RID: 13510
		private Terrain m_Terrain;

		// Token: 0x040034C7 RID: 13511
		private List<MineableObject> m_Trees = new List<MineableObject>();

		// Token: 0x040034C8 RID: 13512
		private TreeInstance[] m_InitialTrees;
	}
}
