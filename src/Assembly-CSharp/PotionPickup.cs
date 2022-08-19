using System;
using UnityEngine;

// Token: 0x020000ED RID: 237
public class PotionPickup : MonoBehaviour
{
	// Token: 0x06000B79 RID: 2937 RVA: 0x00045C00 File Offset: 0x00043E00
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

	// Token: 0x040007B0 RID: 1968
	public GameObject ParticlePotion;

	// Token: 0x040007B1 RID: 1969
	public AudioClip SoundPickup;
}
