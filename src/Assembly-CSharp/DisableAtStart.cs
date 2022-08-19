using System;
using UnityEngine;

// Token: 0x0200051E RID: 1310
public class DisableAtStart : MonoBehaviour
{
	// Token: 0x060029FB RID: 10747 RVA: 0x00140208 File Offset: 0x0013E408
	private void Start()
	{
		GameObject[] objects = this.m_Objects;
		for (int i = 0; i < objects.Length; i++)
		{
			objects[i].SetActive(false);
		}
	}

	// Token: 0x04002648 RID: 9800
	[SerializeField]
	private GameObject[] m_Objects;
}
