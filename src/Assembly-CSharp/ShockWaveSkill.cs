using System;
using UnityEngine;

// Token: 0x0200016C RID: 364
public class ShockWaveSkill : MonoBehaviour
{
	// Token: 0x06000C7E RID: 3198 RVA: 0x0000E653 File Offset: 0x0000C853
	private void Start()
	{
		Object.Destroy(base.gameObject, this.LifeTime);
		ShakeCamera.Shake(0.2f, 0.7f);
	}

	// Token: 0x06000C7F RID: 3199 RVA: 0x000984D8 File Offset: 0x000966D8
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

	// Token: 0x040009AF RID: 2479
	public GameObject Skill;

	// Token: 0x040009B0 RID: 2480
	public float Speed;

	// Token: 0x040009B1 RID: 2481
	public float Spawnrate;

	// Token: 0x040009B2 RID: 2482
	public float LifeTime = 1f;

	// Token: 0x040009B3 RID: 2483
	private float timeTemp;
}
