using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005BD RID: 1469
	[RequireComponent(typeof(Collider))]
	[RequireComponent(typeof(Rigidbody))]
	public class HitBox : MonoBehaviour, IDamageable
	{
		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06002FA3 RID: 12195 RVA: 0x00158312 File Offset: 0x00156512
		public Collider Collider
		{
			get
			{
				return this.m_Collider;
			}
		}

		// Token: 0x06002FA4 RID: 12196 RVA: 0x0015831C File Offset: 0x0015651C
		public void ReceiveDamage(HealthEventData damageData)
		{
			if (base.enabled)
			{
				if (this.m_ParentEntity.Health.Get() > 0f)
				{
					damageData.Delta *= this.m_DamageMultiplier;
					this.m_ParentEntity.ChangeHealth.Try(damageData);
				}
				if (this.m_ParentEntity.Health.Get() == 0f)
				{
					this.m_Rigidbody.AddForceAtPosition(damageData.HitDirection * damageData.HitImpulse, damageData.HitPoint, 1);
				}
			}
		}

		// Token: 0x06002FA5 RID: 12197 RVA: 0x001583A8 File Offset: 0x001565A8
		private void Awake()
		{
			this.m_ParentEntity = base.GetComponentInParent<EntityEventHandler>();
			if (!this.m_ParentEntity)
			{
				Debug.LogErrorFormat(this, "[This HitBox is not under an entity, like a player, animal, etc, it has no purpose.", new object[]
				{
					base.name
				});
				base.enabled = false;
				return;
			}
			this.m_Collider = base.GetComponent<Collider>();
			this.m_Rigidbody = base.GetComponent<Rigidbody>();
		}

		// Token: 0x040029ED RID: 10733
		[SerializeField]
		[Clamp(0f, float.PositiveInfinity)]
		private float m_DamageMultiplier = 1f;

		// Token: 0x040029EE RID: 10734
		private Collider m_Collider;

		// Token: 0x040029EF RID: 10735
		private Rigidbody m_Rigidbody;

		// Token: 0x040029F0 RID: 10736
		private EntityEventHandler m_ParentEntity;
	}
}
