using System;
using UnityEngine;

namespace UltimateSurvival.AI
{
	// Token: 0x0200066A RID: 1642
	public class AISettings : AIBehaviour
	{
		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06003437 RID: 13367 RVA: 0x0016D632 File Offset: 0x0016B832
		public EntityMovement Movement
		{
			get
			{
				return this.m_Movement;
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06003438 RID: 13368 RVA: 0x0016D63A File Offset: 0x0016B83A
		public EntityDetection Detection
		{
			get
			{
				return this.m_Detection;
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06003439 RID: 13369 RVA: 0x0016D642 File Offset: 0x0016B842
		public EntityVitals Vitals
		{
			get
			{
				return this.m_Vitals;
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x0600343A RID: 13370 RVA: 0x0016D64A File Offset: 0x0016B84A
		public EntityAnimation Animation
		{
			get
			{
				return this.m_Animation;
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x0600343B RID: 13371 RVA: 0x0016D652 File Offset: 0x0016B852
		public AudioSource AudioSource
		{
			get
			{
				return this.m_AudioSource;
			}
		}

		// Token: 0x0600343C RID: 13372 RVA: 0x0016D65C File Offset: 0x0016B85C
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

		// Token: 0x0600343D RID: 13373 RVA: 0x0016D7BC File Offset: 0x0016B9BC
		public void PlayAttackSounds()
		{
			this.m_AttackSounds.Play(ItemSelectionMethod.RandomlyButExcludeLast, this.m_AudioSource, 1f);
		}

		// Token: 0x0600343E RID: 13374 RVA: 0x0016D7D8 File Offset: 0x0016B9D8
		private void Start()
		{
			this.m_Brain = base.GetComponent<AIBrain>();
			this.m_Movement.Initialize(this.m_Brain);
			this.m_Detection.Initialize(base.transform);
			this.m_Animation = new EntityAnimation();
			this.m_Animation.Initialize(this.m_Brain);
		}

		// Token: 0x0600343F RID: 13375 RVA: 0x0016D82F File Offset: 0x0016BA2F
		private void Update()
		{
			this.m_Movement.Update(base.transform);
			this.m_Detection.Update(this.m_Brain);
			this.m_Vitals.Update(this.m_Brain);
		}

		// Token: 0x04002E71 RID: 11889
		[SerializeField]
		private EntityMovement m_Movement;

		// Token: 0x04002E72 RID: 11890
		[SerializeField]
		private EntityDetection m_Detection;

		// Token: 0x04002E73 RID: 11891
		[SerializeField]
		private EntityVitals m_Vitals;

		// Token: 0x04002E74 RID: 11892
		[Header("Combat")]
		[SerializeField]
		[Clamp(0f, 500f)]
		private float m_HitDamage = 25f;

		// Token: 0x04002E75 RID: 11893
		[SerializeField]
		[Clamp(0f, 3f)]
		private float m_MaxAttackDistance = 2f;

		// Token: 0x04002E76 RID: 11894
		[Header("Audio")]
		[SerializeField]
		private AudioSource m_AudioSource;

		// Token: 0x04002E77 RID: 11895
		[SerializeField]
		private SoundPlayer m_AttackSounds;

		// Token: 0x04002E78 RID: 11896
		private EntityAnimation m_Animation;

		// Token: 0x04002E79 RID: 11897
		private AIBrain m_Brain;
	}
}
