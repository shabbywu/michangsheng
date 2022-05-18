using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020006B4 RID: 1716
public class LevelPrefabProperties : MonoBehaviour
{
	// Token: 0x06002AF0 RID: 10992 RVA: 0x0014DE6C File Offset: 0x0014C06C
	private void Awake()
	{
		this.originalPosition = base.transform.position;
		this.tipSlota = base.transform.Find("Enemies_Slots");
		if (this.tipSlota.childCount > 0)
		{
			for (int i = 0; i < this.tipSlota.childCount; i++)
			{
				this.enemies_Slots_Count++;
				this.enemiesSlots.Add(this.tipSlota.GetChild(i));
			}
		}
		this.tipSlota = base.transform.Find("Environment_Slots");
		if (this.tipSlota.childCount > 0)
		{
			for (int j = 0; j < this.tipSlota.childCount; j++)
			{
				this.environment_Slots_Count++;
				this.environmentsSlots.Add(this.tipSlota.GetChild(j));
			}
		}
		this.tipSlota = base.transform.Find("CoinsStart_Slots");
		if (this.tipSlota.childCount > 0)
		{
			for (int k = 0; k < this.tipSlota.childCount; k++)
			{
				this.coins_Slots_Count++;
				this.coinsSlots.Add(this.tipSlota.GetChild(k));
			}
		}
		this.tipSlota = base.transform.Find("Special_Slots");
		if (this.tipSlota.childCount > 0)
		{
			for (int l = 0; l < this.tipSlota.childCount; l++)
			{
				this.special_Slots_Count++;
				this.specialSlots.Add(this.tipSlota.GetChild(l));
			}
		}
		this.slobodanTeren = 2;
	}

	// Token: 0x0400251E RID: 9502
	public int slobodanTeren = 2;

	// Token: 0x0400251F RID: 9503
	public int enemies_Slots_Count;

	// Token: 0x04002520 RID: 9504
	public int environment_Slots_Count;

	// Token: 0x04002521 RID: 9505
	public int coins_Slots_Count;

	// Token: 0x04002522 RID: 9506
	public int special_Slots_Count;

	// Token: 0x04002523 RID: 9507
	public List<Transform> environmentsSlots;

	// Token: 0x04002524 RID: 9508
	public List<Transform> enemiesSlots;

	// Token: 0x04002525 RID: 9509
	public List<Transform> coinsSlots;

	// Token: 0x04002526 RID: 9510
	public List<Transform> specialSlots;

	// Token: 0x04002527 RID: 9511
	public int minimumLevel;

	// Token: 0x04002528 RID: 9512
	public int maximumLevel;

	// Token: 0x04002529 RID: 9513
	public int tipTerena;

	// Token: 0x0400252A RID: 9514
	public int[] moguDaSeNakace;

	// Token: 0x0400252B RID: 9515
	[HideInInspector]
	public Vector3 originalPosition;

	// Token: 0x0400252C RID: 9516
	private Transform tipSlota;

	// Token: 0x0400252D RID: 9517
	public int brojUNizu;
}
