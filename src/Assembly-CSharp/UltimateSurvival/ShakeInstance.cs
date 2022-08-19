using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005B3 RID: 1459
	public class ShakeInstance
	{
		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06002F6E RID: 12142 RVA: 0x00157604 File Offset: 0x00155804
		// (set) Token: 0x06002F6F RID: 12143 RVA: 0x0015760C File Offset: 0x0015580C
		public bool IsWeapon { get; set; }

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06002F70 RID: 12144 RVA: 0x00157615 File Offset: 0x00155815
		// (set) Token: 0x06002F71 RID: 12145 RVA: 0x0015761D File Offset: 0x0015581D
		public float ScaleRoughness
		{
			get
			{
				return this.roughMod;
			}
			set
			{
				this.roughMod = value;
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06002F72 RID: 12146 RVA: 0x00157626 File Offset: 0x00155826
		// (set) Token: 0x06002F73 RID: 12147 RVA: 0x0015762E File Offset: 0x0015582E
		public float ScaleMagnitude
		{
			get
			{
				return this.magnMod;
			}
			set
			{
				this.magnMod = value;
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06002F74 RID: 12148 RVA: 0x00157637 File Offset: 0x00155837
		public float NormalizedFadeTime
		{
			get
			{
				return this.currentFadeTime;
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06002F75 RID: 12149 RVA: 0x0015763F File Offset: 0x0015583F
		private bool IsShaking
		{
			get
			{
				return this.currentFadeTime > 0f || this.sustain;
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06002F76 RID: 12150 RVA: 0x00157656 File Offset: 0x00155856
		private bool IsFadingOut
		{
			get
			{
				return !this.sustain && this.currentFadeTime > 0f;
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06002F77 RID: 12151 RVA: 0x0015766F File Offset: 0x0015586F
		private bool IsFadingIn
		{
			get
			{
				return this.currentFadeTime < 1f && this.sustain && this.fadeInDuration > 0f;
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06002F78 RID: 12152 RVA: 0x00157695 File Offset: 0x00155895
		public ShakeState CurrentState
		{
			get
			{
				if (this.IsFadingIn)
				{
					return ShakeState.FadingIn;
				}
				if (this.IsFadingOut)
				{
					return ShakeState.FadingOut;
				}
				if (this.IsShaking)
				{
					return ShakeState.Sustained;
				}
				return ShakeState.Inactive;
			}
		}

		// Token: 0x06002F79 RID: 12153 RVA: 0x001576B8 File Offset: 0x001558B8
		public ShakeInstance(float magnitude, float roughness, float fadeInTime, float fadeOutTime)
		{
			this.Magnitude = magnitude;
			this.fadeOutDuration = fadeOutTime;
			this.fadeInDuration = fadeInTime;
			this.Roughness = roughness;
			if (fadeInTime > 0f)
			{
				this.sustain = true;
				this.currentFadeTime = 0f;
			}
			else
			{
				this.sustain = false;
				this.currentFadeTime = 1f;
			}
			this.tick = (float)Random.Range(-100, 100);
		}

		// Token: 0x06002F7A RID: 12154 RVA: 0x00157744 File Offset: 0x00155944
		public ShakeInstance(float magnitude, float roughness)
		{
			this.Magnitude = magnitude;
			this.Roughness = roughness;
			this.sustain = true;
			this.tick = (float)Random.Range(-100, 100);
		}

		// Token: 0x06002F7B RID: 12155 RVA: 0x0015779C File Offset: 0x0015599C
		public Vector3 UpdateShake()
		{
			if (this.IsWeapon)
			{
				this.amt.x = -1f;
			}
			else
			{
				this.amt.x = Mathf.PerlinNoise(this.tick, 0f) - 0.5f;
			}
			this.amt.y = Mathf.PerlinNoise(0f, this.tick) - 0.5f;
			this.amt.z = Mathf.PerlinNoise(this.tick, this.tick) - 0.5f;
			if (this.fadeInDuration > 0f && this.sustain)
			{
				if (this.currentFadeTime < 1f)
				{
					this.currentFadeTime += Time.fixedDeltaTime / this.fadeInDuration;
				}
				else if (this.fadeOutDuration > 0f)
				{
					this.sustain = false;
				}
			}
			if (!this.sustain)
			{
				this.currentFadeTime -= Time.fixedDeltaTime / this.fadeOutDuration;
			}
			if (this.sustain)
			{
				this.tick += Time.fixedDeltaTime * this.Roughness * this.roughMod;
			}
			else
			{
				this.tick += Time.fixedDeltaTime * this.Roughness * this.roughMod * this.currentFadeTime;
			}
			return this.amt * this.Magnitude * this.magnMod * this.currentFadeTime;
		}

		// Token: 0x06002F7C RID: 12156 RVA: 0x00157911 File Offset: 0x00155B11
		public void StartFadeOut(float fadeOutTime)
		{
			if (fadeOutTime == 0f)
			{
				this.currentFadeTime = 0f;
			}
			this.fadeOutDuration = fadeOutTime;
			this.fadeInDuration = 0f;
			this.sustain = false;
		}

		// Token: 0x06002F7D RID: 12157 RVA: 0x0015793F File Offset: 0x00155B3F
		public void StartFadeIn(float fadeInTime)
		{
			if (fadeInTime == 0f)
			{
				this.currentFadeTime = 1f;
			}
			this.fadeInDuration = fadeInTime;
			this.fadeOutDuration = 0f;
			this.sustain = true;
		}

		// Token: 0x040029B2 RID: 10674
		public float Magnitude;

		// Token: 0x040029B3 RID: 10675
		public float Roughness;

		// Token: 0x040029B4 RID: 10676
		public Vector3 PositionInfluence;

		// Token: 0x040029B5 RID: 10677
		public Vector3 RotationInfluence;

		// Token: 0x040029B6 RID: 10678
		public bool DeleteOnInactive = true;

		// Token: 0x040029B7 RID: 10679
		private float roughMod = 1f;

		// Token: 0x040029B8 RID: 10680
		private float magnMod = 1f;

		// Token: 0x040029B9 RID: 10681
		private float fadeOutDuration;

		// Token: 0x040029BA RID: 10682
		private float fadeInDuration;

		// Token: 0x040029BB RID: 10683
		private bool sustain;

		// Token: 0x040029BC RID: 10684
		private float currentFadeTime;

		// Token: 0x040029BD RID: 10685
		private float tick;

		// Token: 0x040029BE RID: 10686
		private Vector3 amt;
	}
}
