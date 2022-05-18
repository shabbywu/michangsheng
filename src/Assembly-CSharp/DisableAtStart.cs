using System;
using UnityEngine;

// Token: 0x020007B2 RID: 1970
public class DisableAtStart : MonoBehaviour
{
	// Token: 0x0600320E RID: 12814 RVA: 0x0018D3D0 File Offset: 0x0018B5D0
	private void Start()
	{
		GameObject[] objects = this.m_Objects;
		for (int i = 0; i < objects.Length; i++)
		{
			objects[i].SetActive(false);
		}
	}

	// Token: 0x04002E38 RID: 11832
	[SerializeField]
	private GameObject[] m_Objects;
}
