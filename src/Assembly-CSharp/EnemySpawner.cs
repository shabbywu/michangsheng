using System;
using UnityEngine;

// Token: 0x020000E2 RID: 226
public class EnemySpawner : MonoBehaviour
{
	// Token: 0x06000B5F RID: 2911 RVA: 0x000453BA File Offset: 0x000435BA
	private void Start()
	{
		this.timetemp = Time.time;
	}

	// Token: 0x06000B60 RID: 2912 RVA: 0x000453C8 File Offset: 0x000435C8
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

	// Token: 0x0400078D RID: 1933
	public GameObject[] Objectman;

	// Token: 0x0400078E RID: 1934
	public float timeSpawn = 3f;

	// Token: 0x0400078F RID: 1935
	public int enemyCount = 10;

	// Token: 0x04000790 RID: 1936
	public int radius;

	// Token: 0x04000791 RID: 1937
	private float timetemp;
}
