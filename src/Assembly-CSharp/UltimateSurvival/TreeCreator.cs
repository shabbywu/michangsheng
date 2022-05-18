using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020008EB RID: 2283
	[Serializable]
	public class TreeCreator
	{
		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06003A95 RID: 14997 RVA: 0x0002A937 File Offset: 0x00028B37
		public int PrototypeIndex
		{
			get
			{
				return this.m_PrototypeIndex;
			}
		}

		// Token: 0x06003A96 RID: 14998 RVA: 0x001A8CBC File Offset: 0x001A6EBC
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

		// Token: 0x06003A97 RID: 14999 RVA: 0x001A8DE0 File Offset: 0x001A6FE0
		private void On_TreeDestroyed(Terrain terrain, TreeInstance treeInstance, int treeIndex)
		{
			treeInstance.widthScale = (treeInstance.heightScale = 0f);
			terrain.terrainData.SetTreeInstance(treeIndex, treeInstance);
		}

		// Token: 0x040034BD RID: 13501
		[SerializeField]
		[Clamp(0f, 100f)]
		private int m_PrototypeIndex;

		// Token: 0x040034BE RID: 13502
		[SerializeField]
		private MineableObject m_Prefab;

		// Token: 0x040034BF RID: 13503
		[SerializeField]
		private Vector3 m_PositionOffset;

		// Token: 0x040034C0 RID: 13504
		[SerializeField]
		private Vector3 m_RotationOffset;
	}
}
