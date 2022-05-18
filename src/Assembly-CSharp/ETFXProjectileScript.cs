using System;
using UnityEngine;

// Token: 0x020001DF RID: 479
public class ETFXProjectileScript : MonoBehaviour
{
	// Token: 0x06000F72 RID: 3954 RVA: 0x000A1EF0 File Offset: 0x000A00F0
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

	// Token: 0x06000F73 RID: 3955 RVA: 0x000A1F80 File Offset: 0x000A0180
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

	// Token: 0x04000C1E RID: 3102
	public GameObject impactParticle;

	// Token: 0x04000C1F RID: 3103
	public GameObject projectileParticle;

	// Token: 0x04000C20 RID: 3104
	public GameObject muzzleParticle;

	// Token: 0x04000C21 RID: 3105
	public GameObject[] trailParticles;

	// Token: 0x04000C22 RID: 3106
	[HideInInspector]
	public Vector3 impactNormal;

	// Token: 0x04000C23 RID: 3107
	private bool hasCollided;
}
