using System;
using UnityEngine;

// Token: 0x020000F3 RID: 243
public class MoverMissile : MonoBehaviour
{
	// Token: 0x06000B8C RID: 2956 RVA: 0x0004679E File Offset: 0x0004499E
	private void Start()
	{
		Object.Destroy(base.gameObject, this.LifeTime);
	}

	// Token: 0x06000B8D RID: 2957 RVA: 0x000467B4 File Offset: 0x000449B4
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

	// Token: 0x040007C3 RID: 1987
	public GameObject target;

	// Token: 0x040007C4 RID: 1988
	public string TargetTag;

	// Token: 0x040007C5 RID: 1989
	public float damping = 3f;

	// Token: 0x040007C6 RID: 1990
	public float Speed = 500f;

	// Token: 0x040007C7 RID: 1991
	public float SpeedMax = 1000f;

	// Token: 0x040007C8 RID: 1992
	public float SpeedMult = 1f;

	// Token: 0x040007C9 RID: 1993
	public Vector3 Noise = new Vector3(20f, 20f, 20f);

	// Token: 0x040007CA RID: 1994
	public int distanceLock = 70;

	// Token: 0x040007CB RID: 1995
	public int DurationLock = 40;

	// Token: 0x040007CC RID: 1996
	public float targetlockdirection = 0.5f;

	// Token: 0x040007CD RID: 1997
	public bool Seeker;

	// Token: 0x040007CE RID: 1998
	public float LifeTime = 5f;

	// Token: 0x040007CF RID: 1999
	private int timetorock;

	// Token: 0x040007D0 RID: 2000
	private bool locked;
}
