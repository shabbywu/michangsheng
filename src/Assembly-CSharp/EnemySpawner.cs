using System;
using UnityEngine;

// Token: 0x02000159 RID: 345
public class EnemySpawner : MonoBehaviour
{
	// Token: 0x06000C4E RID: 3150 RVA: 0x0000E516 File Offset: 0x0000C716
	private void Start()
	{
		this.timetemp = Time.time;
	}

	// Token: 0x06000C4F RID: 3151 RVA: 0x00096F3C File Offset: 0x0009513C
	private void Update()
	{
		int num = 0;
		if (Random.Range(0, 10) > 8)
		{
			num = 1;
		}
		if (GameObject.FindGameObjectsWithTag(this.Objectman[num].tag).Length < this.enemyCount && Time.time > this.timetemp + this.timeSpawn)
		{
			this.timetemp = Time.time;
			Object.Instantiate<GameObject>(this.Objectman[num], base.transform.position + new Vector3((float)Random.Range(-this.radius, this.radius), base.transform.position.y, (float)Random.Range(-this.radius, this.radius)), Quaternion.identity);
		}
	}

	// Token: 0x04000968 RID: 2408
	public GameObject[] Objectman;

	// Token: 0x04000969 RID: 2409
	public float timeSpawn = 3f;

	// Token: 0x0400096A RID: 2410
	public int enemyCount = 10;

	// Token: 0x0400096B RID: 2411
	public int radius;

	// Token: 0x0400096C RID: 2412
	private float timetemp;
}
