using UnityEngine;

namespace UltimateSurvival;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class HitBox : MonoBehaviour, IDamageable
{
	[SerializeField]
	[Clamp(0f, float.PositiveInfinity)]
	private float m_DamageMultiplier = 1f;

	private Collider m_Collider;

	private Rigidbody m_Rigidbody;

	private EntityEventHandler m_ParentEntity;

	public Collider Collider => m_Collider;

	public void ReceiveDamage(HealthEventData damageData)
	{
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		if (((Behaviour)this).enabled)
		{
			if (m_ParentEntity.Health.Get() > 0f)
			{
				damageData.Delta *= m_DamageMultiplier;
				m_ParentEntity.ChangeHealth.Try(damageData);
			}
			if (m_ParentEntity.Health.Get() == 0f)
			{
				m_Rigidbody.AddForceAtPosition(damageData.HitDirection * damageData.HitImpulse, damageData.HitPoint, (ForceMode)1);
			}
		}
	}

	private void Awake()
	{
		m_ParentEntity = ((Component)this).GetComponentInParent<EntityEventHandler>();
		if (!Object.op_Implicit((Object)(object)m_ParentEntity))
		{
			Debug.LogErrorFormat((Object)(object)this, "[This HitBox is not under an entity, like a player, animal, etc, it has no purpose.", new object[1] { ((Object)this).name });
			((Behaviour)this).enabled = false;
		}
		else
		{
			m_Collider = ((Component)this).GetComponent<Collider>();
			m_Rigidbody = ((Component)this).GetComponent<Rigidbody>();
		}
	}
}
