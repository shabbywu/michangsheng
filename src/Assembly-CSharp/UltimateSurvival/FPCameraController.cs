using System;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x0200086A RID: 2154
	public class FPCameraController : MonoSingleton<FPCameraController>
	{
		// Token: 0x060037CF RID: 14287 RVA: 0x0002885A File Offset: 0x00026A5A
		public ShakeInstance Shake(ShakeInstance shake)
		{
			this.m_ShakeInstances.Add(shake);
			return shake;
		}

		// Token: 0x060037D0 RID: 14288 RVA: 0x001A1094 File Offset: 0x0019F294
		public ShakeInstance ShakeOnce(float magnitude, float roughness, float fadeInTime, float fadeOutTime)
		{
			ShakeInstance shakeInstance = new ShakeInstance(magnitude, roughness, fadeInTime, fadeOutTime);
			shakeInstance.PositionInfluence = this.m_DefaultPosInfluence;
			shakeInstance.RotationInfluence = this.m_DefaultRotInfluence;
			this.m_ShakeInstances.Add(shakeInstance);
			return shakeInstance;
		}

		// Token: 0x060037D1 RID: 14289 RVA: 0x001A10D4 File Offset: 0x0019F2D4
		public ShakeInstance ShakeOnce(float magnitude, float roughness, float fadeInTime, float fadeOutTime, Vector3 posInfluence, Vector3 rotInfluence)
		{
			ShakeInstance shakeInstance = new ShakeInstance(magnitude, roughness, fadeInTime, fadeOutTime);
			shakeInstance.PositionInfluence = posInfluence;
			shakeInstance.RotationInfluence = rotInfluence;
			this.m_ShakeInstances.Add(shakeInstance);
			return shakeInstance;
		}

		// Token: 0x060037D2 RID: 14290 RVA: 0x001A110C File Offset: 0x0019F30C
		public ShakeInstance StartShake(float magnitude, float roughness, float fadeInTime)
		{
			ShakeInstance shakeInstance = new ShakeInstance(magnitude, roughness);
			shakeInstance.PositionInfluence = this.m_DefaultPosInfluence;
			shakeInstance.RotationInfluence = this.m_DefaultRotInfluence;
			shakeInstance.StartFadeIn(fadeInTime);
			this.m_ShakeInstances.Add(shakeInstance);
			return shakeInstance;
		}

		// Token: 0x060037D3 RID: 14291 RVA: 0x001A1150 File Offset: 0x0019F350
		public ShakeInstance StartShake(float magnitude, float roughness, float fadeInTime, Vector3 posInfluence, Vector3 rotInfluence)
		{
			ShakeInstance shakeInstance = new ShakeInstance(magnitude, roughness);
			shakeInstance.PositionInfluence = posInfluence;
			shakeInstance.RotationInfluence = rotInfluence;
			shakeInstance.StartFadeIn(fadeInTime);
			this.m_ShakeInstances.Add(shakeInstance);
			return shakeInstance;
		}

		// Token: 0x060037D4 RID: 14292 RVA: 0x00028869 File Offset: 0x00026A69
		private void Awake()
		{
			this.m_Player.ChangeHealth.AddListener(new Action<HealthEventData>(this.OnSuccess_PlayerHealthChanged));
			this.m_Player.Land.AddListener(new Action<float>(this.On_PlayerLanded));
		}

		// Token: 0x060037D5 RID: 14293 RVA: 0x000288A3 File Offset: 0x00026AA3
		private void OnSuccess_PlayerHealthChanged(HealthEventData healthEventData)
		{
			if (healthEventData.Delta < 0f && healthEventData.Delta < -8f)
			{
				this.m_DamageShake.Shake(Mathf.Abs(healthEventData.Delta / 100f));
			}
		}

		// Token: 0x060037D6 RID: 14294 RVA: 0x000288DB File Offset: 0x00026ADB
		private void On_PlayerLanded(float landSpeed)
		{
			if (landSpeed > this.m_LandThreeshold)
			{
				this.m_LandShake.Shake();
			}
		}

		// Token: 0x060037D7 RID: 14295 RVA: 0x001A118C File Offset: 0x0019F38C
		private void LateUpdate()
		{
			this.m_PositionAddShake = Vector3.zero;
			this.m_RotationAddShake = Vector3.zero;
			int num = 0;
			while (num < this.m_ShakeInstances.Count && num < this.m_ShakeInstances.Count)
			{
				ShakeInstance shakeInstance = this.m_ShakeInstances[num];
				if (shakeInstance.CurrentState == ShakeState.Inactive && shakeInstance.DeleteOnInactive)
				{
					this.m_ShakeInstances.RemoveAt(num);
					num--;
				}
				else if (shakeInstance.CurrentState != ShakeState.Inactive)
				{
					this.m_PositionAddShake += Vector3.Scale(shakeInstance.UpdateShake(), shakeInstance.PositionInfluence);
					this.m_RotationAddShake += Vector3.Scale(shakeInstance.UpdateShake(), shakeInstance.RotationInfluence);
				}
				num++;
			}
			float magnitude = this.m_Player.Velocity.Get().magnitude;
			if (this.m_Player.Walk.Active)
			{
				this.m_PositionAddBob = this.m_WalkHeadbob.CalculateBob(magnitude, Time.deltaTime);
			}
			else
			{
				this.m_PositionAddBob = this.m_WalkHeadbob.Cooldown(Time.deltaTime);
			}
			if (this.m_Player.Run.Active)
			{
				this.m_PositionAddBob += this.m_RunHeadbob.CalculateBob(magnitude, Time.deltaTime);
			}
			else
			{
				this.m_PositionAddBob += this.m_RunHeadbob.Cooldown(Time.deltaTime);
			}
			base.transform.localPosition = this.m_PositionAddShake + this.m_PositionAddBob;
			base.transform.localEulerAngles = this.m_RotationAddShake;
		}

		// Token: 0x04003202 RID: 12802
		[Tooltip("The default position influence of all shakes created on the fly.")]
		[SerializeField]
		private Vector3 m_DefaultPosInfluence = new Vector3(0.15f, 0.15f, 0.15f);

		// Token: 0x04003203 RID: 12803
		[Tooltip("The default rotation influence of all shakes created on the fly.")]
		[SerializeField]
		private Vector3 m_DefaultRotInfluence = new Vector3(1f, 1f, 1f);

		// Token: 0x04003204 RID: 12804
		[Header("Headbobs")]
		[SerializeField]
		private PlayerEventHandler m_Player;

		// Token: 0x04003205 RID: 12805
		[SerializeField]
		private TrigonometricBob m_WalkHeadbob;

		// Token: 0x04003206 RID: 12806
		[SerializeField]
		private TrigonometricBob m_RunHeadbob;

		// Token: 0x04003207 RID: 12807
		[Header("Shakes")]
		[SerializeField]
		private GenericShake m_DamageShake;

		// Token: 0x04003208 RID: 12808
		[SerializeField]
		private WeaponShake m_LandShake;

		// Token: 0x04003209 RID: 12809
		[SerializeField]
		private float m_LandThreeshold = 3f;

		// Token: 0x0400320A RID: 12810
		private Vector3 m_PositionAddShake;

		// Token: 0x0400320B RID: 12811
		private Vector3 m_RotationAddShake;

		// Token: 0x0400320C RID: 12812
		private Vector3 m_PositionAddBob;

		// Token: 0x0400320D RID: 12813
		private Vector3 m_RotationAddBob;

		// Token: 0x0400320E RID: 12814
		private List<ShakeInstance> m_ShakeInstances = new List<ShakeInstance>();
	}
}
