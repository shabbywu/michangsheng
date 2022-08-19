using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000118 RID: 280
public class ParticleEffectsLibrary : MonoBehaviour
{
	// Token: 0x06000D80 RID: 3456 RVA: 0x00050E34 File Offset: 0x0004F034
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

	// Token: 0x06000D81 RID: 3457 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x06000D82 RID: 3458 RVA: 0x00050EF8 File Offset: 0x0004F0F8
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

	// Token: 0x06000D83 RID: 3459 RVA: 0x00050F5C File Offset: 0x0004F15C
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

	// Token: 0x06000D84 RID: 3460 RVA: 0x00051068 File Offset: 0x0004F268
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

	// Token: 0x06000D85 RID: 3461 RVA: 0x00051174 File Offset: 0x0004F374
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

	// Token: 0x04000985 RID: 2437
	public static ParticleEffectsLibrary GlobalAccess;

	// Token: 0x04000986 RID: 2438
	public int TotalEffects;

	// Token: 0x04000987 RID: 2439
	public int CurrentParticleEffectIndex;

	// Token: 0x04000988 RID: 2440
	public int CurrentParticleEffectNum;

	// Token: 0x04000989 RID: 2441
	public Vector3[] ParticleEffectSpawnOffsets;

	// Token: 0x0400098A RID: 2442
	public float[] ParticleEffectLifetimes;

	// Token: 0x0400098B RID: 2443
	public GameObject[] ParticleEffectPrefabs;

	// Token: 0x0400098C RID: 2444
	private string effectNameString = "";

	// Token: 0x0400098D RID: 2445
	private List<Transform> currentActivePEList;

	// Token: 0x0400098E RID: 2446
	private Vector3 spawnPosition = Vector3.zero;
}
