using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x02000875 RID: 2165
	public class DamageArea : MonoBehaviour
	{
		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06003807 RID: 14343 RVA: 0x00028B50 File Offset: 0x00026D50
		// (set) Token: 0x06003808 RID: 14344 RVA: 0x00028B58 File Offset: 0x00026D58
		public bool Active { get; set; }

		// Token: 0x06003809 RID: 14345 RVA: 0x001A216C File Offset: 0x001A036C
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

		// Token: 0x0400325E RID: 12894
		[SerializeField]
		private Vector2 m_DamagePerSecond = new Vector2(3f, 5f);
	}
}
