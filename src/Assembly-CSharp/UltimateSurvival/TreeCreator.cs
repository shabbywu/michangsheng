using System;
using UnityEngine;

namespace UltimateSurvival;

[Serializable]
public class TreeCreator
{
	[SerializeField]
	[Clamp(0f, 100f)]
	private int m_PrototypeIndex;

	[SerializeField]
	private MineableObject m_Prefab;

	[SerializeField]
	private Vector3 m_PositionOffset;

	[SerializeField]
	private Vector3 m_RotationOffset;

	public int PrototypeIndex => m_PrototypeIndex;

	public MineableObject CreateTree(Terrain terrain, TreeInstance treeInstance, int treeIndex)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		if (!Object.op_Implicit((Object)(object)m_Prefab))
		{
			Debug.LogError((object)("[TreeCreator] - This tree creator has no tree prefab assigned! | Prototype Index: " + m_PrototypeIndex));
			return null;
		}
		Transform val = null;
		if ((Object)(object)((Component)terrain).transform.FindDeepChild("Trees") != (Object)null)
		{
			val = ((Component)terrain).transform.FindDeepChild("Trees");
		}
		else
		{
			val = new GameObject("Trees").transform;
			val.position = ((Component)terrain).transform.position;
			val.SetParent(((Component)terrain).transform, true);
		}
		Vector3 val2 = Vector3.Scale(treeInstance.position, terrain.terrainData.size);
		MineableObject mineableObject = Object.Instantiate<MineableObject>(m_Prefab, val2 + m_PositionOffset, Quaternion.Euler(m_RotationOffset), val);
		mineableObject.Destroyed.AddListener(delegate
		{
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			On_TreeDestroyed(terrain, treeInstance, treeIndex);
		});
		return mineableObject;
	}

	private void On_TreeDestroyed(Terrain terrain, TreeInstance treeInstance, int treeIndex)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		treeInstance.widthScale = (treeInstance.heightScale = 0f);
		terrain.terrainData.SetTreeInstance(treeIndex, treeInstance);
	}
}
