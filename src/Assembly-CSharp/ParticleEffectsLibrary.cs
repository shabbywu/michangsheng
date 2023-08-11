using System.Collections.Generic;
using UnityEngine;

public class ParticleEffectsLibrary : MonoBehaviour
{
	public static ParticleEffectsLibrary GlobalAccess;

	public int TotalEffects;

	public int CurrentParticleEffectIndex;

	public int CurrentParticleEffectNum;

	public Vector3[] ParticleEffectSpawnOffsets;

	public float[] ParticleEffectLifetimes;

	public GameObject[] ParticleEffectPrefabs;

	private string effectNameString = "";

	private List<Transform> currentActivePEList;

	private Vector3 spawnPosition = Vector3.zero;

	private void Awake()
	{
		GlobalAccess = this;
		currentActivePEList = new List<Transform>();
		TotalEffects = ParticleEffectPrefabs.Length;
		CurrentParticleEffectNum = 1;
		if (ParticleEffectSpawnOffsets.Length != TotalEffects)
		{
			Debug.LogError((object)"ParticleEffectsLibrary-ParticleEffectSpawnOffset: Not all arrays match length, double check counts.");
		}
		if (ParticleEffectPrefabs.Length != TotalEffects)
		{
			Debug.LogError((object)"ParticleEffectsLibrary-ParticleEffectPrefabs: Not all arrays match length, double check counts.");
		}
		effectNameString = ((Object)ParticleEffectPrefabs[CurrentParticleEffectIndex]).name + " (" + CurrentParticleEffectNum + " of " + TotalEffects + ")";
	}

	private void Start()
	{
	}

	public string GetCurrentPENameString()
	{
		return ((Object)ParticleEffectPrefabs[CurrentParticleEffectIndex]).name + " (" + CurrentParticleEffectNum + " of " + TotalEffects + ")";
	}

	public void PreviousParticleEffect()
	{
		if (ParticleEffectLifetimes[CurrentParticleEffectIndex] == 0f && currentActivePEList.Count > 0)
		{
			for (int i = 0; i < currentActivePEList.Count; i++)
			{
				if ((Object)(object)currentActivePEList[i] != (Object)null)
				{
					Object.Destroy((Object)(object)((Component)currentActivePEList[i]).gameObject);
				}
			}
			currentActivePEList.Clear();
		}
		if (CurrentParticleEffectIndex > 0)
		{
			CurrentParticleEffectIndex--;
		}
		else
		{
			CurrentParticleEffectIndex = TotalEffects - 1;
		}
		CurrentParticleEffectNum = CurrentParticleEffectIndex + 1;
		effectNameString = ((Object)ParticleEffectPrefabs[CurrentParticleEffectIndex]).name + " (" + CurrentParticleEffectNum + " of " + TotalEffects + ")";
	}

	public void NextParticleEffect()
	{
		if (ParticleEffectLifetimes[CurrentParticleEffectIndex] == 0f && currentActivePEList.Count > 0)
		{
			for (int i = 0; i < currentActivePEList.Count; i++)
			{
				if ((Object)(object)currentActivePEList[i] != (Object)null)
				{
					Object.Destroy((Object)(object)((Component)currentActivePEList[i]).gameObject);
				}
			}
			currentActivePEList.Clear();
		}
		if (CurrentParticleEffectIndex < TotalEffects - 1)
		{
			CurrentParticleEffectIndex++;
		}
		else
		{
			CurrentParticleEffectIndex = 0;
		}
		CurrentParticleEffectNum = CurrentParticleEffectIndex + 1;
		effectNameString = ((Object)ParticleEffectPrefabs[CurrentParticleEffectIndex]).name + " (" + CurrentParticleEffectNum + " of " + TotalEffects + ")";
	}

	public void SpawnParticleEffect(Vector3 positionInWorldToSpawn)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		spawnPosition = positionInWorldToSpawn + ParticleEffectSpawnOffsets[CurrentParticleEffectIndex];
		GameObject val = Object.Instantiate<GameObject>(ParticleEffectPrefabs[CurrentParticleEffectIndex], spawnPosition, ParticleEffectPrefabs[CurrentParticleEffectIndex].transform.rotation);
		((Object)val).name = "PE_" + ParticleEffectPrefabs[CurrentParticleEffectIndex];
		if (ParticleEffectLifetimes[CurrentParticleEffectIndex] == 0f)
		{
			currentActivePEList.Add(val.transform);
		}
		currentActivePEList.Add(val.transform);
		if (ParticleEffectLifetimes[CurrentParticleEffectIndex] != 0f)
		{
			Object.Destroy((Object)(object)val, ParticleEffectLifetimes[CurrentParticleEffectIndex]);
		}
	}
}
