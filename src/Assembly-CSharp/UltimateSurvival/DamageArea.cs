using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005B7 RID: 1463
	public class DamageArea : MonoBehaviour
	{
		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06002F83 RID: 12163 RVA: 0x00157E32 File Offset: 0x00156032
		// (set) Token: 0x06002F84 RID: 12164 RVA: 0x00157E3A File Offset: 0x0015603A
		public bool Active { get; set; }

		// Token: 0x06002F85 RID: 12165 RVA: 0x00157E44 File Offset: 0x00156044
		private void OnTriggerStay(Collider other)
		{
			if (!this.Active)
			{
				return;
			}
			EntityEventHandler component = other.GetComponent<EntityEventHandler>();
			if (component)
			{
				HealthEventData arg = new HealthEventData(-Random.Range(this.m_DamagePerSecond.x, this.m_DamagePerSecond.y) * Time.deltaTime, null, default(Vector3), default(Vector3), 0f);
				component.ChangeHealth.Try(arg);
			}
		}

		// Token: 0x040029D4 RID: 10708
		[SerializeField]
		private Vector2 m_DamagePerSecond = new Vector2(3f, 5f);
	}
}
