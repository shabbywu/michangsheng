using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004B8 RID: 1208
public class LevelPrefabProperties : MonoBehaviour
{
	// Token: 0x06002642 RID: 9794 RVA: 0x00109E24 File Offset: 0x00108024
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

	// Token: 0x04001F7C RID: 8060
	public int slobodanTeren = 2;

	// Token: 0x04001F7D RID: 8061
	public int enemies_Slots_Count;

	// Token: 0x04001F7E RID: 8062
	public int environment_Slots_Count;

	// Token: 0x04001F7F RID: 8063
	public int coins_Slots_Count;

	// Token: 0x04001F80 RID: 8064
	public int special_Slots_Count;

	// Token: 0x04001F81 RID: 8065
	public List<Transform> environmentsSlots;

	// Token: 0x04001F82 RID: 8066
	public List<Transform> enemiesSlots;

	// Token: 0x04001F83 RID: 8067
	public List<Transform> coinsSlots;

	// Token: 0x04001F84 RID: 8068
	public List<Transform> specialSlots;

	// Token: 0x04001F85 RID: 8069
	public int minimumLevel;

	// Token: 0x04001F86 RID: 8070
	public int maximumLevel;

	// Token: 0x04001F87 RID: 8071
	public int tipTerena;

	// Token: 0x04001F88 RID: 8072
	public int[] moguDaSeNakace;

	// Token: 0x04001F89 RID: 8073
	[HideInInspector]
	public Vector3 originalPosition;

	// Token: 0x04001F8A RID: 8074
	private Transform tipSlota;

	// Token: 0x04001F8B RID: 8075
	public int brojUNizu;
}
