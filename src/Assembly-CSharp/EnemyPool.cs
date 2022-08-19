using System;
using UnityEngine;

// Token: 0x020004A9 RID: 1193
public class EnemyPool : MonoBehaviour
{
	// Token: 0x06002596 RID: 9622 RVA: 0x00104391 File Offset: 0x00102591
	private void Start()
	{
		EnemyPool.AvailableEnemies = base.transform.childCount;
	}

	// Token: 0x06002597 RID: 9623 RVA: 0x001043A3 File Offset: 0x001025A3
	public GameObject getEnemy()
	{
		if (base.transform.childCount > 0)
		{
			return base.transform.GetChild(Random.Range(0, base.transform.childCount)).gameObject;
		}
		return null;
	}

	// Token: 0x04001E4B RID: 7755
	public static int AvailableEnemies;
}
