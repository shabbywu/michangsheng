using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020008FD RID: 2301
	[Serializable]
	public class SurfaceData
	{
		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x06003AEF RID: 15087 RVA: 0x0002AC6A File Offset: 0x00028E6A
		public string Name
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x06003AF0 RID: 15088 RVA: 0x0002AC72 File Offset: 0x00028E72
		public bool IsPenetrable
		{
			get
			{
				return this.m_IsPenetrable;
			}
		}

		// Token: 0x06003AF1 RID: 15089 RVA: 0x001AAA5C File Offset: 0x001A8C5C
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

		// Token: 0x06003AF2 RID: 15090 RVA: 0x001AAA90 File Offset: 0x001A8C90
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

		// Token: 0x06003AF3 RID: 15091 RVA: 0x001AAB3C File Offset: 0x001A8D3C
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

		// Token: 0x06003AF4 RID: 15092 RVA: 0x001AABE8 File Offset: 0x001A8DE8
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

		// Token: 0x06003AF5 RID: 15093 RVA: 0x001AAC44 File Offset: 0x001A8E44
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

		// Token: 0x06003AF6 RID: 15094 RVA: 0x001AACA0 File Offset: 0x001A8EA0
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

		// Token: 0x06003AF7 RID: 15095 RVA: 0x001AACFC File Offset: 0x001A8EFC
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

		// Token: 0x04003536 RID: 13622
		private const int BULLET_DECAL_POOL_SIZE = 100;

		// Token: 0x04003537 RID: 13623
		private const int BULLET_IMPACT_FX_POOL_SIZE = 100;

		// Token: 0x04003538 RID: 13624
		private const int CHOP_FX_POOL_SIZE = 25;

		// Token: 0x04003539 RID: 13625
		private const int HIT_FX_POOL_SIZE = 25;

		// Token: 0x0400353A RID: 13626
		[SerializeField]
		private string m_Name;

		// Token: 0x0400353B RID: 13627
		[SerializeField]
		private Texture[] m_Textures;

		// Token: 0x0400353C RID: 13628
		[Header("Footsteps")]
		[SerializeField]
		private SoundPlayer m_FootstepSounds;

		// Token: 0x0400353D RID: 13629
		[SerializeField]
		private SoundPlayer m_JumpSounds;

		// Token: 0x0400353E RID: 13630
		[SerializeField]
		private SoundPlayer m_LandSounds;

		// Token: 0x0400353F RID: 13631
		[Header("Bullet Impact")]
		[SerializeField]
		private SoundPlayer m_BulletImpactSounds;

		// Token: 0x04003540 RID: 13632
		[SerializeField]
		private PooledObject[] m_BulletDecals;

		// Token: 0x04003541 RID: 13633
		[SerializeField]
		private PooledObject[] m_BulletImpactFX;

		// Token: 0x04003542 RID: 13634
		[Header("Chop & Hit")]
		[SerializeField]
		private SoundPlayer m_ChopSounds;

		// Token: 0x04003543 RID: 13635
		[SerializeField]
		private SoundPlayer m_HitSounds;

		// Token: 0x04003544 RID: 13636
		[SerializeField]
		private PooledObject[] m_ChopFX;

		// Token: 0x04003545 RID: 13637
		[SerializeField]
		private PooledObject[] m_HitFX;

		// Token: 0x04003546 RID: 13638
		[Header("Penetration")]
		[SerializeField]
		private bool m_IsPenetrable;

		// Token: 0x04003547 RID: 13639
		[SerializeField]
		private SoundPlayer m_SpearPenetrationSounds;

		// Token: 0x04003548 RID: 13640
		[SerializeField]
		private SoundPlayer m_ArrowPenetrationSounds;

		// Token: 0x04003549 RID: 13641
		private int m_LastPlayedFootstep;
	}
}
