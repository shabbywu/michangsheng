using System;
using UnityEngine;

namespace UltimateSurvival.AI
{
	// Token: 0x02000975 RID: 2421
	public class AISettings : AIBehaviour
	{
		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x06003DE3 RID: 15843 RVA: 0x0002C8D5 File Offset: 0x0002AAD5
		public EntityMovement Movement
		{
			get
			{
				return this.m_Movement;
			}
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x06003DE4 RID: 15844 RVA: 0x0002C8DD File Offset: 0x0002AADD
		public EntityDetection Detection
		{
			get
			{
				return this.m_Detection;
			}
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x06003DE5 RID: 15845 RVA: 0x0002C8E5 File Offset: 0x0002AAE5
		public EntityVitals Vitals
		{
			get
			{
				return this.m_Vitals;
			}
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06003DE6 RID: 15846 RVA: 0x0002C8ED File Offset: 0x0002AAED
		public EntityAnimation Animation
		{
			get
			{
				return this.m_Animation;
			}
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06003DE7 RID: 15847 RVA: 0x0002C8F5 File Offset: 0x0002AAF5
		public AudioSource AudioSource
		{
			get
			{
				return this.m_AudioSource;
			}
		}

		// Token: 0x06003DE8 RID: 15848 RVA: 0x001B61B8 File Offset: 0x001B43B8
		public void OnAnimationDamage()
		{
			EntityEventHandler component = this.m_Detection.LastChasedTarget.GetComponent<EntityEventHandler>();
			bool flag = Vector3.Distance(component.transform.position, base.transform.position) < this.m_MaxAttackDistance;
			bool flag2 = Vector3.Angle(this.m_Detection.LastChasedTarget.transform.position - base.transform.position, base.transform.forward) < 60f;
			if (component != null && flag && flag2)
			{
				component.ChangeHealth.Try(new HealthEventData(-this.m_HitDamage, base.Entity, base.transform.position + Vector3.up + base.transform.forward * 0.5f, component.transform.position - base.transform.position, 0f));
				Collider component2 = component.GetComponent<Collider>();
				if (component2 != null)
				{
					SurfaceData surfaceData = ScriptableSingleton<SurfaceDatabase>.Instance.GetSurfaceData(component2, component.transform.position + Vector3.up, 0);
					if (surfaceData != null)
					{
						surfaceData.PlaySound(ItemSelectionMethod.RandomlyButExcludeLast, SoundType.Hit, 1f, component.transform.position + Vector3.up * 1.5f);
					}
				}
			}
		}

		// Token: 0x06003DE9 RID: 15849 RVA: 0x0002C8FD File Offset: 0x0002AAFD
		public void PlayAttackSounds()
		{
			this.m_AttackSounds.Play(ItemSelectionMethod.RandomlyButExcludeLast, this.m_AudioSource, 1f);
		}

		// Token: 0x06003DEA RID: 15850 RVA: 0x001B6318 File Offset: 0x001B4518
		private void Start()
		{
			this.m_Brain = base.GetComponent<AIBrain>();
			this.m_Movement.Initialize(this.m_Brain);
			this.m_Detection.Initialize(base.transform);
			this.m_Animation = new EntityAnimation();
			this.m_Animation.Initialize(this.m_Brain);
		}

		// Token: 0x06003DEB RID: 15851 RVA: 0x0002C916 File Offset: 0x0002AB16
		private void Update()
		{
			this.m_Movement.Update(base.transform);
			this.m_Detection.Update(this.m_Brain);
			this.m_Vitals.Update(this.m_Brain);
		}

		// Token: 0x0400380C RID: 14348
		[SerializeField]
		private EntityMovement m_Movement;

		// Token: 0x0400380D RID: 14349
		[SerializeField]
		private EntityDetection m_Detection;

		// Token: 0x0400380E RID: 14350
		[SerializeField]
		private EntityVitals m_Vitals;

		// Token: 0x0400380F RID: 14351
		[Header("Combat")]
		[SerializeField]
		[Clamp(0f, 500f)]
		private float m_HitDamage = 25f;

		// Token: 0x04003810 RID: 14352
		[SerializeField]
		[Clamp(0f, 3f)]
		private float m_MaxAttackDistance = 2f;

		// Token: 0x04003811 RID: 14353
		[Header("Audio")]
		[SerializeField]
		private AudioSource m_AudioSource;

		// Token: 0x04003812 RID: 14354
		[SerializeField]
		private SoundPlayer m_AttackSounds;

		// Token: 0x04003813 RID: 14355
		private EntityAnimation m_Animation;

		// Token: 0x04003814 RID: 14356
		private AIBrain m_Brain;
	}
}
