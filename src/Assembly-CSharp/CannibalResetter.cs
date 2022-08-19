using System;
using UnityEngine;

// Token: 0x0200051A RID: 1306
public class CannibalResetter : MonoBehaviour
{
	// Token: 0x060029EB RID: 10731 RVA: 0x0013FF0D File Offset: 0x0013E10D
	private void Start()
	{
		this.ResetCannibals();
	}

	// Token: 0x060029EC RID: 10732 RVA: 0x0013FF15 File Offset: 0x0013E115
	private void Update()
	{
		if (Input.GetKeyDown(111))
		{
			this.ResetCannibals();
		}
	}

	// Token: 0x060029ED RID: 10733 RVA: 0x0013FF28 File Offset: 0x0013E128
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

	// Token: 0x0400263A RID: 9786
	[SerializeField]
	private GameObject m_CannibalPrefab;

	// Token: 0x0400263B RID: 9787
	[SerializeField]
	private Transform[] m_SpawnPoints;

	// Token: 0x0400263C RID: 9788
	[SerializeField]
	private Transform m_Container;
}
