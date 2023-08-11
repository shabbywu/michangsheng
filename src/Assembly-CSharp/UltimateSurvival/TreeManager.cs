using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival;

[RequireComponent(typeof(Terrain))]
public class TreeManager : MonoBehaviour
{
	[SerializeField]
	private TreeCreator[] m_TreeCreators;

	private Terrain m_Terrain;

	private List<MineableObject> m_Trees = new List<MineableObject>();

	private TreeInstance[] m_InitialTrees;

	private void Awake()
	{
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		m_Terrain = ((Component)this).GetComponent<Terrain>();
		m_InitialTrees = m_Terrain.terrainData.treeInstances;
		TreeInstance[] treeInstances = m_Terrain.terrainData.treeInstances;
		for (int i = 0; i < treeInstances.Length; i++)
		{
			TreeCreator treeCreator = GetTreeCreator(treeInstances[i].prototypeIndex);
			if (treeCreator != null)
			{
				m_Trees.Add(treeCreator.CreateTree(m_Terrain, treeInstances[i], i));
			}
		}
	}

	private TreeCreator GetTreeCreator(int prototypeIndex)
	{
		for (int i = 0; i < m_TreeCreators.Length; i++)
		{
			if (m_TreeCreators[i].PrototypeIndex == prototypeIndex)
			{
				return m_TreeCreators[i];
			}
		}
		return null;
	}

	private void OnApplicationQuit()
	{
		m_Terrain.terrainData.treeInstances = m_InitialTrees;
	}
}
