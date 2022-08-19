using System;
using UnityEngine;

// Token: 0x020000E8 RID: 232
public class ItemInventory : MonoBehaviour
{
	// Token: 0x06000B6E RID: 2926 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x06000B6F RID: 2927 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x040007A0 RID: 1952
	public int Damage;

	// Token: 0x040007A1 RID: 1953
	public int Defend;

	// Token: 0x040007A2 RID: 1954
	public int ItemEmbedSlotIndex;

	// Token: 0x040007A3 RID: 1955
	public AudioClip[] SoundHit;

	// Token: 0x040007A4 RID: 1956
	public float SpeedAttack = 1f;
}
