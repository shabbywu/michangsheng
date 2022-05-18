using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x0200087B RID: 2171
	[RequireComponent(typeof(Collider))]
	[RequireComponent(typeof(Rigidbody))]
	public class HitBox : MonoBehaviour, IDamageable
	{
		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06003827 RID: 14375 RVA: 0x00028D07 File Offset: 0x00026F07
		public Collider Collider
		{
			get
			{
				return this.m_Collider;
			}
		}

		// Token: 0x06003828 RID: 14376 RVA: 0x001A2498 File Offset: 0x001A0698
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

		// Token: 0x06003829 RID: 14377 RVA: 0x001A2524 File Offset: 0x001A0724
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

		// Token: 0x04003277 RID: 12919
		[SerializeField]
		[Clamp(0f, float.PositiveInfinity)]
		private float m_DamageMultiplier = 1f;

		// Token: 0x04003278 RID: 12920
		private Collider m_Collider;

		// Token: 0x04003279 RID: 12921
		private Rigidbody m_Rigidbody;

		// Token: 0x0400327A RID: 12922
		private EntityEventHandler m_ParentEntity;
	}
}
