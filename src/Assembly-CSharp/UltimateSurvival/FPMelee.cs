using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005EC RID: 1516
	public class FPMelee : FPWeaponBase
	{
		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x060030C2 RID: 12482 RVA: 0x0015CCF3 File Offset: 0x0015AEF3
		public Message<bool> MeleeAttack
		{
			get
			{
				return this.m_MeleeAttack;
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x060030C3 RID: 12483 RVA: 0x0015CCFB File Offset: 0x0015AEFB
		public Message Miss
		{
			get
			{
				return this.m_Miss;
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x060030C4 RID: 12484 RVA: 0x0015CD03 File Offset: 0x0015AF03
		public Message Hit
		{
			get
			{
				return this.m_Hit;
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x060030C5 RID: 12485 RVA: 0x0015CD0B File Offset: 0x0015AF0B
		public float MaxReach
		{
			get
			{
				return this.m_MaxReach;
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x060030C6 RID: 12486 RVA: 0x0015CD13 File Offset: 0x0015AF13
		public float DamagePerHit
		{
			get
			{
				return this.m_DamagePerHit;
			}
		}

		// Token: 0x060030C7 RID: 12487 RVA: 0x0015CD1C File Offset: 0x0015AF1C
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

		// Token: 0x060030C8 RID: 12488 RVA: 0x0015CDA0 File Offset: 0x0015AFA0
		public override bool TryAttackContinuously(Camera camera)
		{
			return this.TryAttackOnce(camera);
		}

		// Token: 0x060030C9 RID: 12489 RVA: 0x0015CDA9 File Offset: 0x0015AFA9
		protected virtual void Start()
		{
			this.m_EventHandler.Hit.AddListener(new Action(this.On_Hit));
			this.m_EventHandler.Woosh.AddListener(new Action(this.On_Woosh));
		}

		// Token: 0x060030CA RID: 12490 RVA: 0x0015CDE4 File Offset: 0x0015AFE4
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

		// Token: 0x060030CB RID: 12491 RVA: 0x0015CF63 File Offset: 0x0015B163
		private void On_Woosh()
		{
			this.m_AttackAudio.Play(ItemSelectionMethod.Randomly, this.m_AudioSource, 0.6f);
			this.Miss.Send();
		}

		// Token: 0x04002AF5 RID: 10997
		[Header("Melee Setup")]
		[SerializeField]
		[Tooltip("The animation event handler - that picks up animation events from Mecanim.")]
		private FPMeleeEventHandler m_EventHandler;

		// Token: 0x04002AF6 RID: 10998
		[Header("Melee Settings")]
		[SerializeField]
		[Tooltip("From how far can this object hit stuff?")]
		private float m_MaxReach = 0.5f;

		// Token: 0x04002AF7 RID: 10999
		[SerializeField]
		[Tooltip("Useful for limiting the number of hits you can do in a period of time.")]
		private float m_TimeBetweenAttacks = 0.85f;

		// Token: 0x04002AF8 RID: 11000
		[SerializeField]
		[Range(0f, 1000f)]
		private float m_DamagePerHit = 15f;

		// Token: 0x04002AF9 RID: 11001
		[Range(0f, 1000f)]
		[SerializeField]
		private float m_ImpactForce = 15f;

		// Token: 0x04002AFA RID: 11002
		[Header("Audio")]
		[SerializeField]
		private AudioSource m_AudioSource;

		// Token: 0x04002AFB RID: 11003
		[SerializeField]
		private SoundPlayer m_AttackAudio;

		// Token: 0x04002AFC RID: 11004
		[SerializeField]
		private SoundType m_SoundType;

		// Token: 0x04002AFD RID: 11005
		private Message<bool> m_MeleeAttack = new Message<bool>();

		// Token: 0x04002AFE RID: 11006
		private Message m_Miss = new Message();

		// Token: 0x04002AFF RID: 11007
		private Message m_Hit = new Message();

		// Token: 0x04002B00 RID: 11008
		private float m_NextUseTime;

		// Token: 0x020014BD RID: 5309
		public enum HitInvokeMethod
		{
			// Token: 0x04006D16 RID: 27926
			ByTimer,
			// Token: 0x04006D17 RID: 27927
			ByAnimationEvent
		}
	}
}
