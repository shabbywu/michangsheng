using System;
using UnityEngine;

// Token: 0x02000689 RID: 1673
public class EnemyPool : MonoBehaviour
{
	// Token: 0x060029D2 RID: 10706 RVA: 0x000207B7 File Offset: 0x0001E9B7
	private void Start()
	{
		EnemyPool.AvailableEnemies = base.transform.childCount;
	}

	// Token: 0x060029D3 RID: 10707 RVA: 0x000207C9 File Offset: 0x0001E9C9
	public GameObject getEnemy()
	{
		if (base.transform.childCount > 0)
		{
			return base.transform.GetChild(Random.Range(0, base.transform.childCount)).gameObject;
		}
		return null;
	}

	// Token: 0x04002371 RID: 9073
	public static int AvailableEnemies;
}
