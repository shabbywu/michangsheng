using System;
using UnityEngine;

// Token: 0x0200016B RID: 363
public class MoverMissile : MonoBehaviour
{
	// Token: 0x06000C7B RID: 3195 RVA: 0x0000E640 File Offset: 0x0000C840
	private void Start()
	{
		Object.Destroy(base.gameObject, this.LifeTime);
	}

	// Token: 0x06000C7C RID: 3196 RVA: 0x000981F4 File Offset: 0x000963F4
	private void Update()
	{
		if (this.Seeker)
		{
			if (this.timetorock > this.DurationLock)
			{
				if (!this.locked && !this.target)
				{
					float num = 2.1474836E+09f;
					if (GameObject.FindGameObjectsWithTag(this.TargetTag).Length != 0)
					{
						foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag(this.TargetTag))
						{
							if (gameObject)
							{
								float num2 = Vector3.Distance(gameObject.transform.position, base.gameObject.transform.position);
								if ((float)this.distanceLock > num2)
								{
									if (num > num2)
									{
										num = num2;
										this.target = gameObject;
									}
									this.locked = true;
								}
							}
						}
					}
				}
			}
			else
			{
				this.timetorock++;
			}
			if (this.target)
			{
				this.damping += 0.9f;
				Quaternion quaternion = Quaternion.LookRotation(this.target.transform.position - base.transform.transform.position);
				base.transform.rotation = Quaternion.Slerp(base.transform.rotation, quaternion, Time.deltaTime * this.damping);
				if (Vector3.Dot((this.target.transform.position - base.transform.position).normalized, base.transform.forward) < this.targetlockdirection)
				{
					this.target = null;
				}
			}
			else
			{
				this.locked = false;
			}
		}
		this.Speed += this.SpeedMult;
		if (this.Speed > this.SpeedMax)
		{
			this.Speed = this.SpeedMax;
		}
		base.GetComponent<Rigidbody>().velocity = this.Speed * Time.deltaTime * base.gameObject.transform.forward;
		base.GetComponent<Rigidbody>().velocity += new Vector3(Random.Range(-this.Noise.x, this.Noise.x), Random.Range(-this.Noise.y, this.Noise.y), Random.Range(-this.Noise.z, this.Noise.z));
	}

	// Token: 0x040009A1 RID: 2465
	public GameObject target;

	// Token: 0x040009A2 RID: 2466
	public string TargetTag;

	// Token: 0x040009A3 RID: 2467
	public float damping = 3f;

	// Token: 0x040009A4 RID: 2468
	public float Speed = 500f;

	// Token: 0x040009A5 RID: 2469
	public float SpeedMax = 1000f;

	// Token: 0x040009A6 RID: 2470
	public float SpeedMult = 1f;

	// Token: 0x040009A7 RID: 2471
	public Vector3 Noise = new Vector3(20f, 20f, 20f);

	// Token: 0x040009A8 RID: 2472
	public int distanceLock = 70;

	// Token: 0x040009A9 RID: 2473
	public int DurationLock = 40;

	// Token: 0x040009AA RID: 2474
	public float targetlockdirection = 0.5f;

	// Token: 0x040009AB RID: 2475
	public bool Seeker;

	// Token: 0x040009AC RID: 2476
	public float LifeTime = 5f;

	// Token: 0x040009AD RID: 2477
	private int timetorock;

	// Token: 0x040009AE RID: 2478
	private bool locked;
}
