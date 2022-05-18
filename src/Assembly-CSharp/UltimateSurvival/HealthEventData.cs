using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x02000879 RID: 2169
	public class HealthEventData
	{
		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06003819 RID: 14361 RVA: 0x00028C74 File Offset: 0x00026E74
		// (set) Token: 0x0600381A RID: 14362 RVA: 0x00028C7C File Offset: 0x00026E7C
		public float Delta { get; set; }

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x0600381B RID: 14363 RVA: 0x00028C85 File Offset: 0x00026E85
		// (set) Token: 0x0600381C RID: 14364 RVA: 0x00028C8D File Offset: 0x00026E8D
		public EntityEventHandler Damager { get; private set; }

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x0600381D RID: 14365 RVA: 0x00028C96 File Offset: 0x00026E96
		// (set) Token: 0x0600381E RID: 14366 RVA: 0x00028C9E File Offset: 0x00026E9E
		public Vector3 HitPoint { get; private set; }

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x0600381F RID: 14367 RVA: 0x00028CA7 File Offset: 0x00026EA7
		// (set) Token: 0x06003820 RID: 14368 RVA: 0x00028CAF File Offset: 0x00026EAF
		public Vector3 HitDirection { get; private set; }

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x06003821 RID: 14369 RVA: 0x00028CB8 File Offset: 0x00026EB8
		// (set) Token: 0x06003822 RID: 14370 RVA: 0x00028CC0 File Offset: 0x00026EC0
		public float HitImpulse { get; private set; }

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x06003823 RID: 14371 RVA: 0x00028CC9 File Offset: 0x00026EC9
		// (set) Token: 0x06003824 RID: 14372 RVA: 0x00028CD1 File Offset: 0x00026ED1
		public Collider AffectedCollider { get; private set; }

		// Token: 0x06003825 RID: 14373 RVA: 0x00028CDA File Offset: 0x00026EDA
		public HealthEventData(float delta, EntityEventHandler damager = null, Vector3 hitPoint = default(Vector3), Vector3 hitDirection = default(Vector3), float hitImpulse = 0f)
		{
			this.Delta = delta;
			this.Damager = damager;
			this.HitPoint = hitPoint;
			this.HitDirection = hitDirection;
			this.HitImpulse = hitImpulse;
		}
	}
}
