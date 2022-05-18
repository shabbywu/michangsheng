using System;
using UnityEngine;

// Token: 0x02000161 RID: 353
public class ItemPickup : MonoBehaviour
{
	// Token: 0x06000C62 RID: 3170 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x06000C63 RID: 3171 RVA: 0x000975AC File Offset: 0x000957AC
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

	// Token: 0x04000984 RID: 2436
	public bool DestroyWhenPickup = true;

	// Token: 0x04000985 RID: 2437
	public AudioClip SoundPickup;

	// Token: 0x04000986 RID: 2438
	public int IndexItem;

	// Token: 0x04000987 RID: 2439
	public int Num = 1;
}
