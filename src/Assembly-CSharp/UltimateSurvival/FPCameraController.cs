using System;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005AE RID: 1454
	public class FPCameraController : MonoSingleton<FPCameraController>
	{
		// Token: 0x06002F51 RID: 12113 RVA: 0x00156B2F File Offset: 0x00154D2F
		public ShakeInstance Shake(ShakeInstance shake)
		{
			this.m_ShakeInstances.Add(shake);
			return shake;
		}

		// Token: 0x06002F52 RID: 12114 RVA: 0x00156B40 File Offset: 0x00154D40
		public ShakeInstance ShakeOnce(float magnitude, float roughness, float fadeInTime, float fadeOutTime)
		{
			ShakeInstance shakeInstance = new ShakeInstance(magnitude, roughness, fadeInTime, fadeOutTime);
			shakeInstance.PositionInfluence = this.m_DefaultPosInfluence;
			shakeInstance.RotationInfluence = this.m_DefaultRotInfluence;
			this.m_ShakeInstances.Add(shakeInstance);
			return shakeInstance;
		}

		// Token: 0x06002F53 RID: 12115 RVA: 0x00156B80 File Offset: 0x00154D80
		public ShakeInstance ShakeOnce(float magnitude, float roughness, float fadeInTime, float fadeOutTime, Vector3 posInfluence, Vector3 rotInfluence)
		{
			ShakeInstance shakeInstance = new ShakeInstance(magnitude, roughness, fadeInTime, fadeOutTime);
			shakeInstance.PositionInfluence = posInfluence;
			shakeInstance.RotationInfluence = rotInfluence;
			this.m_ShakeInstances.Add(shakeInstance);
			return shakeInstance;
		}

		// Token: 0x06002F54 RID: 12116 RVA: 0x00156BB8 File Offset: 0x00154DB8
		public ShakeInstance StartShake(float magnitude, float roughness, float fadeInTime)
		{
			ShakeInstance shakeInstance = new ShakeInstance(magnitude, roughness);
			shakeInstance.PositionInfluence = this.m_DefaultPosInfluence;
			shakeInstance.RotationInfluence = this.m_DefaultRotInfluence;
			shakeInstance.StartFadeIn(fadeInTime);
			this.m_ShakeInstances.Add(shakeInstance);
			return shakeInstance;
		}

		// Token: 0x06002F55 RID: 12117 RVA: 0x00156BFC File Offset: 0x00154DFC
		public ShakeInstance StartShake(float magnitude, float roughness, float fadeInTime, Vector3 posInfluence, Vector3 rotInfluence)
		{
			ShakeInstance shakeInstance = new ShakeInstance(magnitude, roughness);
			shakeInstance.PositionInfluence = posInfluence;
			shakeInstance.RotationInfluence = rotInfluence;
			shakeInstance.StartFadeIn(fadeInTime);
			this.m_ShakeInstances.Add(shakeInstance);
			return shakeInstance;
		}

		// Token: 0x06002F56 RID: 12118 RVA: 0x00156C35 File Offset: 0x00154E35
		private void Awake()
		{
			this.m_Player.ChangeHealth.AddListener(new Action<HealthEventData>(this.OnSuccess_PlayerHealthChanged));
			this.m_Player.Land.AddListener(new Action<float>(this.On_PlayerLanded));
		}

		// Token: 0x06002F57 RID: 12119 RVA: 0x00156C6F File Offset: 0x00154E6F
		private void OnSuccess_PlayerHealthChanged(HealthEventData healthEventData)
		{
			if (healthEventData.Delta < 0f && healthEventData.Delta < -8f)
			{
				this.m_DamageShake.Shake(Mathf.Abs(healthEventData.Delta / 100f));
			}
		}

		// Token: 0x06002F58 RID: 12120 RVA: 0x00156CA7 File Offset: 0x00154EA7
		private void On_PlayerLanded(float landSpeed)
		{
			if (landSpeed > this.m_LandThreeshold)
			{
				this.m_LandShake.Shake();
			}
		}

		// Token: 0x06002F59 RID: 12121 RVA: 0x00156CC0 File Offset: 0x00154EC0
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

		// Token: 0x04002981 RID: 10625
		[Tooltip("The default position influence of all shakes created on the fly.")]
		[SerializeField]
		private Vector3 m_DefaultPosInfluence = new Vector3(0.15f, 0.15f, 0.15f);

		// Token: 0x04002982 RID: 10626
		[Tooltip("The default rotation influence of all shakes created on the fly.")]
		[SerializeField]
		private Vector3 m_DefaultRotInfluence = new Vector3(1f, 1f, 1f);

		// Token: 0x04002983 RID: 10627
		[Header("Headbobs")]
		[SerializeField]
		private PlayerEventHandler m_Player;

		// Token: 0x04002984 RID: 10628
		[SerializeField]
		private TrigonometricBob m_WalkHeadbob;

		// Token: 0x04002985 RID: 10629
		[SerializeField]
		private TrigonometricBob m_RunHeadbob;

		// Token: 0x04002986 RID: 10630
		[Header("Shakes")]
		[SerializeField]
		private GenericShake m_DamageShake;

		// Token: 0x04002987 RID: 10631
		[SerializeField]
		private WeaponShake m_LandShake;

		// Token: 0x04002988 RID: 10632
		[SerializeField]
		private float m_LandThreeshold = 3f;

		// Token: 0x04002989 RID: 10633
		private Vector3 m_PositionAddShake;

		// Token: 0x0400298A RID: 10634
		private Vector3 m_RotationAddShake;

		// Token: 0x0400298B RID: 10635
		private Vector3 m_PositionAddBob;

		// Token: 0x0400298C RID: 10636
		private Vector3 m_RotationAddBob;

		// Token: 0x0400298D RID: 10637
		private List<ShakeInstance> m_ShakeInstances = new List<ShakeInstance>();
	}
}
