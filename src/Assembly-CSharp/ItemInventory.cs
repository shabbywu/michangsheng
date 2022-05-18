using System;
using UnityEngine;

// Token: 0x0200015F RID: 351
public class ItemInventory : MonoBehaviour
{
	// Token: 0x06000C5D RID: 3165 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x06000C5E RID: 3166 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x0400097B RID: 2427
	public int Damage;

	// Token: 0x0400097C RID: 2428
	public int Defend;

	// Token: 0x0400097D RID: 2429
	public int ItemEmbedSlotIndex;

	// Token: 0x0400097E RID: 2430
	public AudioClip[] SoundHit;

	// Token: 0x0400097F RID: 2431
	public float SpeedAttack = 1f;
}
