using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200000F RID: 15
public class ParticleCollisionInstance : MonoBehaviour
{
	// Token: 0x06000047 RID: 71 RVA: 0x0000332A File Offset: 0x0000152A
	private void Start()
	{
		this.part = base.GetComponent<ParticleSystem>();
	}

	// Token: 0x06000048 RID: 72 RVA: 0x00003338 File Offset: 0x00001538
	private void OnParticleCollision(GameObject other)
	{
		int num = ParticlePhysicsExtensions.GetCollisionEvents(this.part, other, this.collisionEvents);
		for (int i = 0; i < num; i++)
		{
			GameObject[] effectsOnCollision = this.EffectsOnCollision;
			for (int j = 0; j < effectsOnCollision.Length; j++)
			{
				GameObject gameObject = Object.Instantiate<GameObject>(effectsOnCollision[j], this.collisionEvents[i].intersection + this.collisionEvents[i].normal * this.Offset, default(Quaternion));
				if (!this.UseWorldSpacePosition)
				{
					gameObject.transform.parent = base.transform;
				}
				if (this.UseFirePointRotation)
				{
					gameObject.transform.LookAt(base.transform.position);
				}
				else if (this.rotationOffset != Vector3.zero && this.useOnlyRotationOffset)
				{
					gameObject.transform.rotation = Quaternion.Euler(this.rotationOffset);
				}
				else
				{
					gameObject.transform.LookAt(this.collisionEvents[i].intersection + this.collisionEvents[i].normal);
					gameObject.transform.rotation *= Quaternion.Euler(this.rotationOffset);
				}
				Object.Destroy(gameObject, this.DestroyTimeDelay);
			}
		}
		if (this.DestoyMainEffect)
		{
			Object.Destroy(base.gameObject, this.DestroyTimeDelay + 0.5f);
		}
	}

	// Token: 0x04000035 RID: 53
	public GameObject[] EffectsOnCollision;

	// Token: 0x04000036 RID: 54
	public float DestroyTimeDelay = 5f;

	// Token: 0x04000037 RID: 55
	public bool UseWorldSpacePosition;

	// Token: 0x04000038 RID: 56
	public float Offset;

	// Token: 0x04000039 RID: 57
	public Vector3 rotationOffset = new Vector3(0f, 0f, 0f);

	// Token: 0x0400003A RID: 58
	public bool useOnlyRotationOffset = true;

	// Token: 0x0400003B RID: 59
	public bool UseFirePointRotation;

	// Token: 0x0400003C RID: 60
	public bool DestoyMainEffect = true;

	// Token: 0x0400003D RID: 61
	private ParticleSystem part;

	// Token: 0x0400003E RID: 62
	private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

	// Token: 0x0400003F RID: 63
	private ParticleSystem ps;
}
