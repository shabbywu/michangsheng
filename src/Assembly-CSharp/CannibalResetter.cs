using System;
using UnityEngine;

// Token: 0x020007AE RID: 1966
public class CannibalResetter : MonoBehaviour
{
	// Token: 0x060031FE RID: 12798 RVA: 0x000247E8 File Offset: 0x000229E8
	private void Start()
	{
		this.ResetCannibals();
	}

	// Token: 0x060031FF RID: 12799 RVA: 0x000247F0 File Offset: 0x000229F0
	private void Update()
	{
		if (Input.GetKeyDown(111))
		{
			this.ResetCannibals();
		}
	}

	// Token: 0x06003200 RID: 12800 RVA: 0x0018D178 File Offset: 0x0018B378
	private void ResetCannibals()
	{
		int childCount = this.m_Container.childCount;
		for (int i = 0; i < childCount; i++)
		{
			Object.Destroy(this.m_Container.GetChild(i).gameObject);
		}
		for (int j = 0; j < this.m_SpawnPoints.Length; j++)
		{
			Object.Instantiate<GameObject>(this.m_CannibalPrefab, this.m_SpawnPoints[j].position, this.m_SpawnPoints[j].rotation, this.m_Container);
		}
	}

	// Token: 0x04002E2A RID: 11818
	[SerializeField]
	private GameObject m_CannibalPrefab;

	// Token: 0x04002E2B RID: 11819
	[SerializeField]
	private Transform[] m_SpawnPoints;

	// Token: 0x04002E2C RID: 11820
	[SerializeField]
	private Transform m_Container;
}
