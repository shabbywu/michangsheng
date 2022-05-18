using System;
using UnityEngine;

// Token: 0x02000164 RID: 356
public class PotionPickup : MonoBehaviour
{
	// Token: 0x06000C68 RID: 3176 RVA: 0x000976E0 File Offset: 0x000958E0
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player" && other.gameObject.GetComponent<CharacterStatus>())
		{
			other.gameObject.GetComponent<CharacterStatus>().HP += 20;
			if (this.ParticlePotion)
			{
				Object.Instantiate<GameObject>(this.ParticlePotion, base.transform.position, Quaternion.identity);
			}
			if (this.SoundPickup)
			{
				AudioSource.PlayClipAtPoint(this.SoundPickup, base.transform.position);
			}
			Object.Destroy(base.gameObject.transform.parent.gameObject);
		}
	}

	// Token: 0x0400098B RID: 2443
	public GameObject ParticlePotion;

	// Token: 0x0400098C RID: 2444
	public AudioClip SoundPickup;
}
