using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001DA RID: 474
public class ParticleEffectsLibrary : MonoBehaviour
{
	// Token: 0x06000F59 RID: 3929 RVA: 0x000A1ADC File Offset: 0x0009FCDC
	private void Awake()
	{
		ParticleEffectsLibrary.GlobalAccess = this;
		this.currentActivePEList = new List<Transform>();
		this.TotalEffects = this.ParticleEffectPrefabs.Length;
		this.CurrentParticleEffectNum = 1;
		if (this.ParticleEffectSpawnOffsets.Length != this.TotalEffects)
		{
			Debug.LogError("ParticleEffectsLibrary-ParticleEffectSpawnOffset: Not all arrays match length, double check counts.");
		}
		if (this.ParticleEffectPrefabs.Length != this.TotalEffects)
		{
			Debug.LogError("ParticleEffectsLibrary-ParticleEffectPrefabs: Not all arrays match length, double check counts.");
		}
		this.effectNameString = string.Concat(new string[]
		{
			this.ParticleEffectPrefabs[this.CurrentParticleEffectIndex].name,
			" (",
			this.CurrentParticleEffectNum.ToString(),
			" of ",
			this.TotalEffects.ToString(),
			")"
		});
	}

	// Token: 0x06000F5A RID: 3930 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x06000F5B RID: 3931 RVA: 0x000A1BA0 File Offset: 0x0009FDA0
	public string GetCurrentPENameString()
	{
		return string.Concat(new string[]
		{
			this.ParticleEffectPrefabs[this.CurrentParticleEffectIndex].name,
			" (",
			this.CurrentParticleEffectNum.ToString(),
			" of ",
			this.TotalEffects.ToString(),
			")"
		});
	}

	// Token: 0x06000F5C RID: 3932 RVA: 0x000A1C04 File Offset: 0x0009FE04
	public void PreviousParticleEffect()
	{
		if (this.ParticleEffectLifetimes[this.CurrentParticleEffectIndex] == 0f && this.currentActivePEList.Count > 0)
		{
			for (int i = 0; i < this.currentActivePEList.Count; i++)
			{
				if (this.currentActivePEList[i] != null)
				{
					Object.Destroy(this.currentActivePEList[i].gameObject);
				}
			}
			this.currentActivePEList.Clear();
		}
		if (this.CurrentParticleEffectIndex > 0)
		{
			this.CurrentParticleEffectIndex--;
		}
		else
		{
			this.CurrentParticleEffectIndex = this.TotalEffects - 1;
		}
		this.CurrentParticleEffectNum = this.CurrentParticleEffectIndex + 1;
		this.effectNameString = string.Concat(new string[]
		{
			this.ParticleEffectPrefabs[this.CurrentParticleEffectIndex].name,
			" (",
			this.CurrentParticleEffectNum.ToString(),
			" of ",
			this.TotalEffects.ToString(),
			")"
		});
	}

	// Token: 0x06000F5D RID: 3933 RVA: 0x000A1D10 File Offset: 0x0009FF10
	public void NextParticleEffect()
	{
		if (this.ParticleEffectLifetimes[this.CurrentParticleEffectIndex] == 0f && this.currentActivePEList.Count > 0)
		{
			for (int i = 0; i < this.currentActivePEList.Count; i++)
			{
				if (this.currentActivePEList[i] != null)
				{
					Object.Destroy(this.currentActivePEList[i].gameObject);
				}
			}
			this.currentActivePEList.Clear();
		}
		if (this.CurrentParticleEffectIndex < this.TotalEffects - 1)
		{
			this.CurrentParticleEffectIndex++;
		}
		else
		{
			this.CurrentParticleEffectIndex = 0;
		}
		this.CurrentParticleEffectNum = this.CurrentParticleEffectIndex + 1;
		this.effectNameString = string.Concat(new string[]
		{
			this.ParticleEffectPrefabs[this.CurrentParticleEffectIndex].name,
			" (",
			this.CurrentParticleEffectNum.ToString(),
			" of ",
			this.TotalEffects.ToString(),
			")"
		});
	}

	// Token: 0x06000F5E RID: 3934 RVA: 0x000A1E1C File Offset: 0x000A001C
	public void SpawnParticleEffect(Vector3 positionInWorldToSpawn)
	{
		this.spawnPosition = positionInWorldToSpawn + this.ParticleEffectSpawnOffsets[this.CurrentParticleEffectIndex];
		GameObject gameObject = Object.Instantiate<GameObject>(this.ParticleEffectPrefabs[this.CurrentParticleEffectIndex], this.spawnPosition, this.ParticleEffectPrefabs[this.CurrentParticleEffectIndex].transform.rotation);
		gameObject.name = "PE_" + this.ParticleEffectPrefabs[this.CurrentParticleEffectIndex];
		if (this.ParticleEffectLifetimes[this.CurrentParticleEffectIndex] == 0f)
		{
			this.currentActivePEList.Add(gameObject.transform);
		}
		this.currentActivePEList.Add(gameObject.transform);
		if (this.ParticleEffectLifetimes[this.CurrentParticleEffectIndex] != 0f)
		{
			Object.Destroy(gameObject, this.ParticleEffectLifetimes[this.CurrentParticleEffectIndex]);
		}
	}

	// Token: 0x04000C09 RID: 3081
	public static ParticleEffectsLibrary GlobalAccess;

	// Token: 0x04000C0A RID: 3082
	public int TotalEffects;

	// Token: 0x04000C0B RID: 3083
	public int CurrentParticleEffectIndex;

	// Token: 0x04000C0C RID: 3084
	public int CurrentParticleEffectNum;

	// Token: 0x04000C0D RID: 3085
	public Vector3[] ParticleEffectSpawnOffsets;

	// Token: 0x04000C0E RID: 3086
	public float[] ParticleEffectLifetimes;

	// Token: 0x04000C0F RID: 3087
	public GameObject[] ParticleEffectPrefabs;

	// Token: 0x04000C10 RID: 3088
	private string effectNameString = "";

	// Token: 0x04000C11 RID: 3089
	private List<Transform> currentActivePEList;

	// Token: 0x04000C12 RID: 3090
	private Vector3 spawnPosition = Vector3.zero;
}
