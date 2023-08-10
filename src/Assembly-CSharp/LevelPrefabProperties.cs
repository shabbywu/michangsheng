using System.Collections.Generic;
using UnityEngine;

public class LevelPrefabProperties : MonoBehaviour
{
	public int slobodanTeren = 2;

	public int enemies_Slots_Count;

	public int environment_Slots_Count;

	public int coins_Slots_Count;

	public int special_Slots_Count;

	public List<Transform> environmentsSlots;

	public List<Transform> enemiesSlots;

	public List<Transform> coinsSlots;

	public List<Transform> specialSlots;

	public int minimumLevel;

	public int maximumLevel;

	public int tipTerena;

	public int[] moguDaSeNakace;

	[HideInInspector]
	public Vector3 originalPosition;

	private Transform tipSlota;

	public int brojUNizu;

	private void Awake()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		originalPosition = ((Component)this).transform.position;
		tipSlota = ((Component)this).transform.Find("Enemies_Slots");
		if (tipSlota.childCount > 0)
		{
			for (int i = 0; i < tipSlota.childCount; i++)
			{
				enemies_Slots_Count++;
				enemiesSlots.Add(tipSlota.GetChild(i));
			}
		}
		tipSlota = ((Component)this).transform.Find("Environment_Slots");
		if (tipSlota.childCount > 0)
		{
			for (int j = 0; j < tipSlota.childCount; j++)
			{
				environment_Slots_Count++;
				environmentsSlots.Add(tipSlota.GetChild(j));
			}
		}
		tipSlota = ((Component)this).transform.Find("CoinsStart_Slots");
		if (tipSlota.childCount > 0)
		{
			for (int k = 0; k < tipSlota.childCount; k++)
			{
				coins_Slots_Count++;
				coinsSlots.Add(tipSlota.GetChild(k));
			}
		}
		tipSlota = ((Component)this).transform.Find("Special_Slots");
		if (tipSlota.childCount > 0)
		{
			for (int l = 0; l < tipSlota.childCount; l++)
			{
				special_Slots_Count++;
				specialSlots.Add(tipSlota.GetChild(l));
			}
		}
		slobodanTeren = 2;
	}
}
