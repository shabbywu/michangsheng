using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x0200060B RID: 1547
	[Serializable]
	public class TreeCreator
	{
		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06003180 RID: 12672 RVA: 0x0015F6E8 File Offset: 0x0015D8E8
		public int PrototypeIndex
		{
			get
			{
				return this.m_PrototypeIndex;
			}
		}

		// Token: 0x06003181 RID: 12673 RVA: 0x0015F6F0 File Offset: 0x0015D8F0
		public MineableObject CreateTree(Terrain terrain, TreeInstance treeInstance, int treeIndex)
		{
			if (!this.m_Prefab)
			{
				Debug.LogError("[TreeCreator] - This tree creator has no tree prefab assigned! | Prototype Index: " + this.m_PrototypeIndex);
				return null;
			}
			Transform transform;
			if (terrain.transform.FindDeepChild("Trees") != null)
			{
				transform = terrain.transform.FindDeepChild("Trees");
			}
			else
			{
				transform = new GameObject("Trees").transform;
				transform.position = terrain.transform.position;
				transform.SetParent(terrain.transform, true);
			}
			Vector3 vector = Vector3.Scale(treeInstance.position, terrain.terrainData.size);
			MineableObject mineableObject = Object.Instantiate<MineableObject>(this.m_Prefab, vector + this.m_PositionOffset, Quaternion.Euler(this.m_RotationOffset), transform);
			mineableObject.Destroyed.AddListener(delegate
			{
				this.On_TreeDestroyed(terrain, treeInstance, treeIndex);
			});
			return mineableObject;
		}

		// Token: 0x06003182 RID: 12674 RVA: 0x0015F814 File Offset: 0x0015DA14
		private void On_TreeDestroyed(Terrain terrain, TreeInstance treeInstance, int treeIndex)
		{
			treeInstance.widthScale = (treeInstance.heightScale = 0f);
			terrain.terrainData.SetTreeInstance(treeIndex, treeInstance);
		}

		// Token: 0x04002BB5 RID: 11189
		[SerializeField]
		[Clamp(0f, 100f)]
		private int m_PrototypeIndex;

		// Token: 0x04002BB6 RID: 11190
		[SerializeField]
		private MineableObject m_Prefab;

		// Token: 0x04002BB7 RID: 11191
		[SerializeField]
		private Vector3 m_PositionOffset;

		// Token: 0x04002BB8 RID: 11192
		[SerializeField]
		private Vector3 m_RotationOffset;
	}
}
