using System;
using UnityEngine;

// Token: 0x0200011D RID: 285
public class ETFXProjectileScript : MonoBehaviour
{
	// Token: 0x06000D99 RID: 3481 RVA: 0x00051424 File Offset: 0x0004F624
	private void Start()
	{
		this.projectileParticle = Object.Instantiate<GameObject>(this.projectileParticle, base.transform.position, base.transform.rotation);
		this.projectileParticle.transform.parent = base.transform;
		if (this.muzzleParticle)
		{
			this.muzzleParticle = Object.Instantiate<GameObject>(this.muzzleParticle, base.transform.position, base.transform.rotation);
			Object.Destroy(this.muzzleParticle, 1.5f);
		}
	}

	// Token: 0x06000D9A RID: 3482 RVA: 0x000514B4 File Offset: 0x0004F6B4
	private void OnCollisionEnter(Collision hit)
	{
		if (!this.hasCollided)
		{
			this.hasCollided = true;
			this.impactParticle = Object.Instantiate<GameObject>(this.impactParticle, base.transform.position, Quaternion.FromToRotation(Vector3.up, this.impactNormal));
			if (hit.gameObject.tag == "Destructible")
			{
				Object.Destroy(hit.gameObject);
			}
			foreach (GameObject gameObject in this.trailParticles)
			{
				GameObject gameObject2 = base.transform.Find(this.projectileParticle.name + "/" + gameObject.name).gameObject;
				gameObject2.transform.parent = null;
				Object.Destroy(gameObject2, 3f);
			}
			Object.Destroy(this.projectileParticle, 3f);
			Object.Destroy(this.impactParticle, 5f);
			Object.Destroy(base.gameObject);
			ParticleSystem[] componentsInChildren = base.GetComponentsInChildren<ParticleSystem>();
			for (int j = 1; j < componentsInChildren.Length; j++)
			{
				ParticleSystem particleSystem = componentsInChildren[j];
				if (particleSystem.gameObject.name.Contains("Trail"))
				{
					particleSystem.transform.SetParent(null);
					Object.Destroy(particleSystem.gameObject, 2f);
				}
			}
		}
	}

	// Token: 0x0400099A RID: 2458
	public GameObject impactParticle;

	// Token: 0x0400099B RID: 2459
	public GameObject projectileParticle;

	// Token: 0x0400099C RID: 2460
	public GameObject muzzleParticle;

	// Token: 0x0400099D RID: 2461
	public GameObject[] trailParticles;

	// Token: 0x0400099E RID: 2462
	[HideInInspector]
	public Vector3 impactNormal;

	// Token: 0x0400099F RID: 2463
	private bool hasCollided;
}
