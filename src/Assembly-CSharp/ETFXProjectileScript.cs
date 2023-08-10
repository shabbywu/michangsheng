using UnityEngine;

public class ETFXProjectileScript : MonoBehaviour
{
	public GameObject impactParticle;

	public GameObject projectileParticle;

	public GameObject muzzleParticle;

	public GameObject[] trailParticles;

	[HideInInspector]
	public Vector3 impactNormal;

	private bool hasCollided;

	private void Start()
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		projectileParticle = Object.Instantiate<GameObject>(projectileParticle, ((Component)this).transform.position, ((Component)this).transform.rotation);
		projectileParticle.transform.parent = ((Component)this).transform;
		if (Object.op_Implicit((Object)(object)muzzleParticle))
		{
			muzzleParticle = Object.Instantiate<GameObject>(muzzleParticle, ((Component)this).transform.position, ((Component)this).transform.rotation);
			Object.Destroy((Object)(object)muzzleParticle, 1.5f);
		}
	}

	private void OnCollisionEnter(Collision hit)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		if (hasCollided)
		{
			return;
		}
		hasCollided = true;
		impactParticle = Object.Instantiate<GameObject>(impactParticle, ((Component)this).transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal));
		if (hit.gameObject.tag == "Destructible")
		{
			Object.Destroy((Object)(object)hit.gameObject);
		}
		GameObject[] array = trailParticles;
		foreach (GameObject val in array)
		{
			GameObject gameObject = ((Component)((Component)this).transform.Find(((Object)projectileParticle).name + "/" + ((Object)val).name)).gameObject;
			gameObject.transform.parent = null;
			Object.Destroy((Object)(object)gameObject, 3f);
		}
		Object.Destroy((Object)(object)projectileParticle, 3f);
		Object.Destroy((Object)(object)impactParticle, 5f);
		Object.Destroy((Object)(object)((Component)this).gameObject);
		ParticleSystem[] componentsInChildren = ((Component)this).GetComponentsInChildren<ParticleSystem>();
		for (int j = 1; j < componentsInChildren.Length; j++)
		{
			ParticleSystem val2 = componentsInChildren[j];
			if (((Object)((Component)val2).gameObject).name.Contains("Trail"))
			{
				((Component)val2).transform.SetParent((Transform)null);
				Object.Destroy((Object)(object)((Component)val2).gameObject, 2f);
			}
		}
	}
}
