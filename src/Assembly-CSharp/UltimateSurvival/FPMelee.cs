using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020008BF RID: 2239
	public class FPMelee : FPWeaponBase
	{
		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x060039A0 RID: 14752 RVA: 0x00029D0D File Offset: 0x00027F0D
		public Message<bool> MeleeAttack
		{
			get
			{
				return this.m_MeleeAttack;
			}
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x060039A1 RID: 14753 RVA: 0x00029D15 File Offset: 0x00027F15
		public Message Miss
		{
			get
			{
				return this.m_Miss;
			}
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x060039A2 RID: 14754 RVA: 0x00029D1D File Offset: 0x00027F1D
		public Message Hit
		{
			get
			{
				return this.m_Hit;
			}
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x060039A3 RID: 14755 RVA: 0x00029D25 File Offset: 0x00027F25
		public float MaxReach
		{
			get
			{
				return this.m_MaxReach;
			}
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x060039A4 RID: 14756 RVA: 0x00029D2D File Offset: 0x00027F2D
		public float DamagePerHit
		{
			get
			{
				return this.m_DamagePerHit;
			}
		}

		// Token: 0x060039A5 RID: 14757 RVA: 0x001A6498 File Offset: 0x001A4698
		public override bool TryAttackOnce(Camera camera)
		{
			if (Time.time < this.m_NextUseTime)
			{
				return false;
			}
			RaycastData raycastData = base.Player.RaycastData.Get();
			if (raycastData)
			{
				bool message = raycastData.HitInfo.distance < this.m_MaxReach;
				this.MeleeAttack.Send(message);
			}
			else
			{
				this.MeleeAttack.Send(false);
			}
			base.Attack.Send();
			this.m_NextUseTime = Time.time + this.m_TimeBetweenAttacks;
			return true;
		}

		// Token: 0x060039A6 RID: 14758 RVA: 0x00029D35 File Offset: 0x00027F35
		public override bool TryAttackContinuously(Camera camera)
		{
			return this.TryAttackOnce(camera);
		}

		// Token: 0x060039A7 RID: 14759 RVA: 0x00029D3E File Offset: 0x00027F3E
		protected virtual void Start()
		{
			this.m_EventHandler.Hit.AddListener(new Action(this.On_Hit));
			this.m_EventHandler.Woosh.AddListener(new Action(this.On_Woosh));
		}

		// Token: 0x060039A8 RID: 14760 RVA: 0x001A651C File Offset: 0x001A471C
		protected virtual void On_Hit()
		{
			RaycastData raycastData = base.Player.RaycastData.Get();
			if (!raycastData)
			{
				return;
			}
			if (GameController.SurfaceDatabase)
			{
				SurfaceData surfaceData = GameController.SurfaceDatabase.GetSurfaceData(raycastData.HitInfo);
				surfaceData.PlaySound(ItemSelectionMethod.Randomly, this.m_SoundType, 1f, raycastData.HitInfo.point);
				if (this.m_SoundType == SoundType.Hit)
				{
					surfaceData.CreateHitFX(raycastData.HitInfo.point, Quaternion.LookRotation(raycastData.HitInfo.normal), null);
				}
				else if (this.m_SoundType == SoundType.Chop)
				{
					surfaceData.CreateChopFX(raycastData.HitInfo.point, Quaternion.LookRotation(raycastData.HitInfo.normal), null);
				}
			}
			IDamageable component = raycastData.GameObject.GetComponent<IDamageable>();
			if (component != null)
			{
				HealthEventData damageData = new HealthEventData(-this.m_DamagePerHit, base.Player, base.transform.position, GameController.WorldCamera.transform.forward, this.m_ImpactForce);
				component.ReceiveDamage(damageData);
			}
			if (this.m_Durability != null)
			{
				ItemProperty.Float @float = this.m_Durability.Float;
				float num = @float.Current;
				@float.Current = num - 1f;
				this.m_Durability.SetValue(ItemProperty.Type.Float, @float);
				if (@float.Current == 0f)
				{
					base.Player.DestroyEquippedItem.Try();
				}
			}
			this.Hit.Send();
		}

		// Token: 0x060039A9 RID: 14761 RVA: 0x00029D79 File Offset: 0x00027F79
		private void On_Woosh()
		{
			this.m_AttackAudio.Play(ItemSelectionMethod.Randomly, this.m_AudioSource, 0.6f);
			this.Miss.Send();
		}

		// Token: 0x040033C8 RID: 13256
		[Header("Melee Setup")]
		[SerializeField]
		[Tooltip("The animation event handler - that picks up animation events from Mecanim.")]
		private FPMeleeEventHandler m_EventHandler;

		// Token: 0x040033C9 RID: 13257
		[Header("Melee Settings")]
		[SerializeField]
		[Tooltip("From how far can this object hit stuff?")]
		private float m_MaxReach = 0.5f;

		// Token: 0x040033CA RID: 13258
		[SerializeField]
		[Tooltip("Useful for limiting the number of hits you can do in a period of time.")]
		private float m_TimeBetweenAttacks = 0.85f;

		// Token: 0x040033CB RID: 13259
		[SerializeField]
		[Range(0f, 1000f)]
		private float m_DamagePerHit = 15f;

		// Token: 0x040033CC RID: 13260
		[Range(0f, 1000f)]
		[SerializeField]
		private float m_ImpactForce = 15f;

		// Token: 0x040033CD RID: 13261
		[Header("Audio")]
		[SerializeField]
		private AudioSource m_AudioSource;

		// Token: 0x040033CE RID: 13262
		[SerializeField]
		private SoundPlayer m_AttackAudio;

		// Token: 0x040033CF RID: 13263
		[SerializeField]
		private SoundType m_SoundType;

		// Token: 0x040033D0 RID: 13264
		private Message<bool> m_MeleeAttack = new Message<bool>();

		// Token: 0x040033D1 RID: 13265
		private Message m_Miss = new Message();

		// Token: 0x040033D2 RID: 13266
		private Message m_Hit = new Message();

		// Token: 0x040033D3 RID: 13267
		private float m_NextUseTime;

		// Token: 0x020008C0 RID: 2240
		public enum HitInvokeMethod
		{
			// Token: 0x040033D5 RID: 13269
			ByTimer,
			// Token: 0x040033D6 RID: 13270
			ByAnimationEvent
		}
	}
}
