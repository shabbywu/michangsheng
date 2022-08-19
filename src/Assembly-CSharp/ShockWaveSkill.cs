using System;
using UnityEngine;

// Token: 0x020000F4 RID: 244
public class ShockWaveSkill : MonoBehaviour
{
	// Token: 0x06000B8F RID: 2959 RVA: 0x00046A97 File Offset: 0x00044C97
	private void Start()
	{
		Object.Destroy(base.gameObject, this.LifeTime);
		ShakeCamera.Shake(0.2f, 0.7f);
	}

	// Token: 0x06000B90 RID: 2960 RVA: 0x00046ABC File Offset: 0x00044CBC
	private void Update()
	{
		base.transform.position += base.transform.forward * this.Speed * Time.deltaTime;
		if (Time.time > this.timeTemp + this.Spawnrate)
		{
			if (this.Skill)
			{
				Object.Instantiate<GameObject>(this.Skill, base.transform.position, Quaternion.identity);
			}
			this.timeTemp = Time.time;
		}
	}

	// Token: 0x040007D1 RID: 2001
	public GameObject Skill;

	// Token: 0x040007D2 RID: 2002
	public float Speed;

	// Token: 0x040007D3 RID: 2003
	public float Spawnrate;

	// Token: 0x040007D4 RID: 2004
	public float LifeTime = 1f;

	// Token: 0x040007D5 RID: 2005
	private float timeTemp;
}
