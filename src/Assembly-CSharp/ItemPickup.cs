using System;
using UnityEngine;

// Token: 0x020000EA RID: 234
public class ItemPickup : MonoBehaviour
{
	// Token: 0x06000B73 RID: 2931 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x06000B74 RID: 2932 RVA: 0x00045A90 File Offset: 0x00043C90
	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.GetComponent<CharacterInventory>())
		{
			other.gameObject.GetComponent<CharacterInventory>().AddItem(this.IndexItem, this.Num);
			if (this.SoundPickup)
			{
				AudioSource.PlayClipAtPoint(this.SoundPickup, Camera.main.gameObject.transform.position);
			}
			if (this.DestroyWhenPickup)
			{
				if (base.gameObject.transform.parent)
				{
					Object.Destroy(base.gameObject.transform.parent.gameObject);
					return;
				}
				Object.Destroy(base.gameObject);
			}
		}
	}

	// Token: 0x040007A9 RID: 1961
	public bool DestroyWhenPickup = true;

	// Token: 0x040007AA RID: 1962
	public AudioClip SoundPickup;

	// Token: 0x040007AB RID: 1963
	public int IndexItem;

	// Token: 0x040007AC RID: 1964
	public int Num = 1;
}
