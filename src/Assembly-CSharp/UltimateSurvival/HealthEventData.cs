using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005BB RID: 1467
	public class HealthEventData
	{
		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06002F95 RID: 12181 RVA: 0x0015827F File Offset: 0x0015647F
		// (set) Token: 0x06002F96 RID: 12182 RVA: 0x00158287 File Offset: 0x00156487
		public float Delta { get; set; }

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06002F97 RID: 12183 RVA: 0x00158290 File Offset: 0x00156490
		// (set) Token: 0x06002F98 RID: 12184 RVA: 0x00158298 File Offset: 0x00156498
		public EntityEventHandler Damager { get; private set; }

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06002F99 RID: 12185 RVA: 0x001582A1 File Offset: 0x001564A1
		// (set) Token: 0x06002F9A RID: 12186 RVA: 0x001582A9 File Offset: 0x001564A9
		public Vector3 HitPoint { get; private set; }

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06002F9B RID: 12187 RVA: 0x001582B2 File Offset: 0x001564B2
		// (set) Token: 0x06002F9C RID: 12188 RVA: 0x001582BA File Offset: 0x001564BA
		public Vector3 HitDirection { get; private set; }

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06002F9D RID: 12189 RVA: 0x001582C3 File Offset: 0x001564C3
		// (set) Token: 0x06002F9E RID: 12190 RVA: 0x001582CB File Offset: 0x001564CB
		public float HitImpulse { get; private set; }

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06002F9F RID: 12191 RVA: 0x001582D4 File Offset: 0x001564D4
		// (set) Token: 0x06002FA0 RID: 12192 RVA: 0x001582DC File Offset: 0x001564DC
		public Collider AffectedCollider { get; private set; }

		// Token: 0x06002FA1 RID: 12193 RVA: 0x001582E5 File Offset: 0x001564E5
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
