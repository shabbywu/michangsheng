using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000010 RID: 16
public class ParticleCollisionInstance : MonoBehaviour
{
	// Token: 0x06000047 RID: 71 RVA: 0x00004209 File Offset: 0x00002409
	private void Start()
	{
		this.part = base.GetComponent<ParticleSystem>();
	}

	// Token: 0x06000048 RID: 72 RVA: 0x0005DB20 File Offset: 0x0005BD20
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

	// Token: 0x04000038 RID: 56
	public GameObject[] EffectsOnCollision;

	// Token: 0x04000039 RID: 57
	public float DestroyTimeDelay = 5f;

	// Token: 0x0400003A RID: 58
	public bool UseWorldSpacePosition;

	// Token: 0x0400003B RID: 59
	public float Offset;

	// Token: 0x0400003C RID: 60
	public Vector3 rotationOffset = new Vector3(0f, 0f, 0f);

	// Token: 0x0400003D RID: 61
	public bool useOnlyRotationOffset = true;

	// Token: 0x0400003E RID: 62
	public bool UseFirePointRotation;

	// Token: 0x0400003F RID: 63
	public bool DestoyMainEffect = true;

	// Token: 0x04000040 RID: 64
	private ParticleSystem part;

	// Token: 0x04000041 RID: 65
	private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

	// Token: 0x04000042 RID: 66
	private ParticleSystem ps;
}
