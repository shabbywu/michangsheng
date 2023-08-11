using UnityEngine;

public class PotionPickup : MonoBehaviour
{
	public GameObject ParticlePotion;

	public AudioClip SoundPickup;

	private void OnTriggerEnter(Collider other)
	{
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		if (((Component)other).tag == "Player" && Object.op_Implicit((Object)(object)((Component)other).gameObject.GetComponent<CharacterStatus>()))
		{
			((Component)other).gameObject.GetComponent<CharacterStatus>().HP += 20;
			if (Object.op_Implicit((Object)(object)ParticlePotion))
			{
				Object.Instantiate<GameObject>(ParticlePotion, ((Component)this).transform.position, Quaternion.identity);
			}
			if (Object.op_Implicit((Object)(object)SoundPickup))
			{
				AudioSource.PlayClipAtPoint(SoundPickup, ((Component)this).transform.position);
			}
			Object.Destroy((Object)(object)((Component)((Component)this).gameObject.transform.parent).gameObject);
		}
	}
}
