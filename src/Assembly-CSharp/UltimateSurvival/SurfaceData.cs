using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x02000617 RID: 1559
	[Serializable]
	public class SurfaceData
	{
		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x060031C1 RID: 12737 RVA: 0x00161109 File Offset: 0x0015F309
		public string Name
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x060031C2 RID: 12738 RVA: 0x00161111 File Offset: 0x0015F311
		public bool IsPenetrable
		{
			get
			{
				return this.m_IsPenetrable;
			}
		}

		// Token: 0x060031C3 RID: 12739 RVA: 0x0016111C File Offset: 0x0015F31C
		public bool HasTexture(Texture texture)
		{
			for (int i = 0; i < this.m_Textures.Length; i++)
			{
				if (this.m_Textures[i] == texture)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060031C4 RID: 12740 RVA: 0x00161150 File Offset: 0x0015F350
		public void PlaySound(ItemSelectionMethod selectionMethod, SoundType soundType, float volumeFactor = 1f, AudioSource audioSource = null)
		{
			if (soundType == SoundType.BulletImpact)
			{
				this.m_BulletImpactSounds.Play(selectionMethod, audioSource, volumeFactor);
				return;
			}
			if (soundType == SoundType.Footstep)
			{
				this.m_FootstepSounds.Play(selectionMethod, audioSource, volumeFactor);
				return;
			}
			if (soundType == SoundType.Jump)
			{
				this.m_JumpSounds.Play(selectionMethod, audioSource, volumeFactor);
				return;
			}
			if (soundType == SoundType.Land)
			{
				this.m_LandSounds.Play(selectionMethod, audioSource, volumeFactor);
				return;
			}
			if (soundType == SoundType.Chop)
			{
				this.m_ChopSounds.Play(selectionMethod, audioSource, volumeFactor);
				return;
			}
			if (soundType == SoundType.Hit)
			{
				this.m_HitSounds.Play(selectionMethod, audioSource, volumeFactor);
				return;
			}
			if (soundType == SoundType.SpearPenetration)
			{
				this.m_SpearPenetrationSounds.Play(selectionMethod, audioSource, volumeFactor);
				return;
			}
			if (soundType == SoundType.ArrowPenetration)
			{
				this.m_ArrowPenetrationSounds.Play(selectionMethod, audioSource, volumeFactor);
			}
		}

		// Token: 0x060031C5 RID: 12741 RVA: 0x001611FC File Offset: 0x0015F3FC
		public void PlaySound(ItemSelectionMethod selectionMethod, SoundType soundType, float volumeFactor = 1f, Vector3 position = default(Vector3))
		{
			if (soundType == SoundType.BulletImpact)
			{
				this.m_BulletImpactSounds.PlayAtPosition(selectionMethod, position, volumeFactor);
				return;
			}
			if (soundType == SoundType.Footstep)
			{
				this.m_FootstepSounds.PlayAtPosition(selectionMethod, position, volumeFactor);
				return;
			}
			if (soundType == SoundType.Jump)
			{
				this.m_JumpSounds.PlayAtPosition(selectionMethod, position, volumeFactor);
				return;
			}
			if (soundType == SoundType.Land)
			{
				this.m_LandSounds.PlayAtPosition(selectionMethod, position, volumeFactor);
				return;
			}
			if (soundType == SoundType.Chop)
			{
				this.m_ChopSounds.PlayAtPosition(selectionMethod, position, volumeFactor);
				return;
			}
			if (soundType == SoundType.Hit)
			{
				this.m_HitSounds.PlayAtPosition(selectionMethod, position, volumeFactor);
				return;
			}
			if (soundType == SoundType.SpearPenetration)
			{
				this.m_SpearPenetrationSounds.PlayAtPosition(selectionMethod, position, volumeFactor);
				return;
			}
			if (soundType == SoundType.ArrowPenetration)
			{
				this.m_ArrowPenetrationSounds.PlayAtPosition(selectionMethod, position, volumeFactor);
			}
		}

		// Token: 0x060031C6 RID: 12742 RVA: 0x001612A8 File Offset: 0x0015F4A8
		public void CreateBulletDecal(Vector3 position, Quaternion rotation, Transform parent = null)
		{
			if (this.m_BulletDecals.Length == 0)
			{
				return;
			}
			PooledObject pooledObject = this.m_BulletDecals[Random.Range(0, this.m_BulletDecals.Length)];
			if (pooledObject)
			{
				Object.Instantiate<PooledObject>(pooledObject, position, rotation, parent);
				return;
			}
			Debug.LogWarningFormat("[({0}) SurfaceData] - A decal object was found null, please check the surface database for missing decals.", new object[]
			{
				this.Name
			});
		}

		// Token: 0x060031C7 RID: 12743 RVA: 0x00161304 File Offset: 0x0015F504
		public void CreateBulletImpactFX(Vector3 position, Quaternion rotation, Transform parent = null)
		{
			if (this.m_BulletImpactFX.Length == 0)
			{
				return;
			}
			PooledObject pooledObject = this.m_BulletImpactFX[Random.Range(0, this.m_BulletImpactFX.Length)];
			if (pooledObject)
			{
				Object.Instantiate<PooledObject>(pooledObject, position, rotation, parent);
				return;
			}
			Debug.LogWarningFormat("[({0}) SurfaceData] - A bullet impact FX prefab was found null, please check the surface database for missing effects.", new object[]
			{
				this.Name
			});
		}

		// Token: 0x060031C8 RID: 12744 RVA: 0x00161360 File Offset: 0x0015F560
		public void CreateHitFX(Vector3 position, Quaternion rotation, Transform parent = null)
		{
			if (this.m_HitFX.Length == 0)
			{
				return;
			}
			PooledObject pooledObject = this.m_HitFX[Random.Range(0, this.m_HitFX.Length)];
			if (pooledObject)
			{
				Object.Instantiate<PooledObject>(pooledObject, position, rotation, parent);
				return;
			}
			Debug.LogWarningFormat("[({0}) SurfaceData] - A hit FX prefab was found null, please check the surface database for missing effects.", new object[]
			{
				this.Name
			});
		}

		// Token: 0x060031C9 RID: 12745 RVA: 0x001613BC File Offset: 0x0015F5BC
		public void CreateChopFX(Vector3 position, Quaternion rotation, Transform parent = null)
		{
			if (this.m_ChopFX.Length == 0)
			{
				return;
			}
			PooledObject pooledObject = this.m_ChopFX[Random.Range(0, this.m_ChopFX.Length)];
			if (pooledObject)
			{
				Object.Instantiate<PooledObject>(pooledObject, position, rotation, parent);
				return;
			}
			Debug.LogWarningFormat("[({0}) SurfaceData] - A chop FX prefab was found null, please check the surface database for missing effects.", new object[]
			{
				this.Name
			});
		}

		// Token: 0x04002C15 RID: 11285
		private const int BULLET_DECAL_POOL_SIZE = 100;

		// Token: 0x04002C16 RID: 11286
		private const int BULLET_IMPACT_FX_POOL_SIZE = 100;

		// Token: 0x04002C17 RID: 11287
		private const int CHOP_FX_POOL_SIZE = 25;

		// Token: 0x04002C18 RID: 11288
		private const int HIT_FX_POOL_SIZE = 25;

		// Token: 0x04002C19 RID: 11289
		[SerializeField]
		private string m_Name;

		// Token: 0x04002C1A RID: 11290
		[SerializeField]
		private Texture[] m_Textures;

		// Token: 0x04002C1B RID: 11291
		[Header("Footsteps")]
		[SerializeField]
		private SoundPlayer m_FootstepSounds;

		// Token: 0x04002C1C RID: 11292
		[SerializeField]
		private SoundPlayer m_JumpSounds;

		// Token: 0x04002C1D RID: 11293
		[SerializeField]
		private SoundPlayer m_LandSounds;

		// Token: 0x04002C1E RID: 11294
		[Header("Bullet Impact")]
		[SerializeField]
		private SoundPlayer m_BulletImpactSounds;

		// Token: 0x04002C1F RID: 11295
		[SerializeField]
		private PooledObject[] m_BulletDecals;

		// Token: 0x04002C20 RID: 11296
		[SerializeField]
		private PooledObject[] m_BulletImpactFX;

		// Token: 0x04002C21 RID: 11297
		[Header("Chop & Hit")]
		[SerializeField]
		private SoundPlayer m_ChopSounds;

		// Token: 0x04002C22 RID: 11298
		[SerializeField]
		private SoundPlayer m_HitSounds;

		// Token: 0x04002C23 RID: 11299
		[SerializeField]
		private PooledObject[] m_ChopFX;

		// Token: 0x04002C24 RID: 11300
		[SerializeField]
		private PooledObject[] m_HitFX;

		// Token: 0x04002C25 RID: 11301
		[Header("Penetration")]
		[SerializeField]
		private bool m_IsPenetrable;

		// Token: 0x04002C26 RID: 11302
		[SerializeField]
		private SoundPlayer m_SpearPenetrationSounds;

		// Token: 0x04002C27 RID: 11303
		[SerializeField]
		private SoundPlayer m_ArrowPenetrationSounds;

		// Token: 0x04002C28 RID: 11304
		private int m_LastPlayedFootstep;
	}
}
