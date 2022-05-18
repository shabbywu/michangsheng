using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x02000870 RID: 2160
	public class ShakeInstance
	{
		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x060037F2 RID: 14322 RVA: 0x00028A3A File Offset: 0x00026C3A
		// (set) Token: 0x060037F3 RID: 14323 RVA: 0x00028A42 File Offset: 0x00026C42
		public bool IsWeapon { get; set; }

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x060037F4 RID: 14324 RVA: 0x00028A4B File Offset: 0x00026C4B
		// (set) Token: 0x060037F5 RID: 14325 RVA: 0x00028A53 File Offset: 0x00026C53
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

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x060037F6 RID: 14326 RVA: 0x00028A5C File Offset: 0x00026C5C
		// (set) Token: 0x060037F7 RID: 14327 RVA: 0x00028A64 File Offset: 0x00026C64
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

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x060037F8 RID: 14328 RVA: 0x00028A6D File Offset: 0x00026C6D
		public float NormalizedFadeTime
		{
			get
			{
				return this.currentFadeTime;
			}
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x060037F9 RID: 14329 RVA: 0x00028A75 File Offset: 0x00026C75
		private bool IsShaking
		{
			get
			{
				return this.currentFadeTime > 0f || this.sustain;
			}
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x060037FA RID: 14330 RVA: 0x00028A8C File Offset: 0x00026C8C
		private bool IsFadingOut
		{
			get
			{
				return !this.sustain && this.currentFadeTime > 0f;
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x060037FB RID: 14331 RVA: 0x00028AA5 File Offset: 0x00026CA5
		private bool IsFadingIn
		{
			get
			{
				return this.currentFadeTime < 1f && this.sustain && this.fadeInDuration > 0f;
			}
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x060037FC RID: 14332 RVA: 0x00028ACB File Offset: 0x00026CCB
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

		// Token: 0x060037FD RID: 14333 RVA: 0x001A1A54 File Offset: 0x0019FC54
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

		// Token: 0x060037FE RID: 14334 RVA: 0x001A1AE0 File Offset: 0x0019FCE0
		public ShakeInstance(float magnitude, float roughness)
		{
			this.Magnitude = magnitude;
			this.Roughness = roughness;
			this.sustain = true;
			this.tick = (float)Random.Range(-100, 100);
		}

		// Token: 0x060037FF RID: 14335 RVA: 0x001A1B38 File Offset: 0x0019FD38
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

		// Token: 0x06003800 RID: 14336 RVA: 0x00028AEC File Offset: 0x00026CEC
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

		// Token: 0x06003801 RID: 14337 RVA: 0x00028B1A File Offset: 0x00026D1A
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

		// Token: 0x04003238 RID: 12856
		public float Magnitude;

		// Token: 0x04003239 RID: 12857
		public float Roughness;

		// Token: 0x0400323A RID: 12858
		public Vector3 PositionInfluence;

		// Token: 0x0400323B RID: 12859
		public Vector3 RotationInfluence;

		// Token: 0x0400323C RID: 12860
		public bool DeleteOnInactive = true;

		// Token: 0x0400323D RID: 12861
		private float roughMod = 1f;

		// Token: 0x0400323E RID: 12862
		private float magnMod = 1f;

		// Token: 0x0400323F RID: 12863
		private float fadeOutDuration;

		// Token: 0x04003240 RID: 12864
		private float fadeInDuration;

		// Token: 0x04003241 RID: 12865
		private bool sustain;

		// Token: 0x04003242 RID: 12866
		private float currentFadeTime;

		// Token: 0x04003243 RID: 12867
		private float tick;

		// Token: 0x04003244 RID: 12868
		private Vector3 amt;
	}
}
