using UnityEngine;

namespace UltimateSurvival;

public class DamageArea : MonoBehaviour
{
	[SerializeField]
	private Vector2 m_DamagePerSecond = new Vector2(3f, 5f);

	public bool Active { get; set; }

	private void OnTriggerStay(Collider other)
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		if (Active)
		{
			EntityEventHandler component = ((Component)other).GetComponent<EntityEventHandler>();
			if (Object.op_Implicit((Object)(object)component))
			{
				HealthEventData arg = new HealthEventData((0f - Random.Range(m_DamagePerSecond.x, m_DamagePerSecond.y)) * Time.deltaTime);
				component.ChangeHealth.Try(arg);
			}
		}
	}
}
