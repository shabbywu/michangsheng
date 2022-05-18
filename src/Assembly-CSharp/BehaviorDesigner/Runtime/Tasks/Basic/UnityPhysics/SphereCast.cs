using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityPhysics
{
	// Token: 0x02001586 RID: 5510
	[TaskCategory("Basic/Physics")]
	[TaskDescription("Casts a sphere against all colliders in the scene. Returns success if a collider was hit.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=117")]
	public class SphereCast : Action
	{
		// Token: 0x060081FA RID: 33274 RVA: 0x002CCCE4 File Offset: 0x002CAEE4
		public override TaskStatus OnUpdate()
		{
			Vector3 vector = this.direction.Value;
			Vector3 vector2;
			if (this.originGameObject.Value != null)
			{
				vector2 = this.originGameObject.Value.transform.position;
				if (this.space == 1)
				{
					vector = this.originGameObject.Value.transform.TransformDirection(this.direction.Value);
				}
			}
			else
			{
				vector2 = this.originPosition.Value;
			}
			RaycastHit raycastHit;
			if (Physics.SphereCast(vector2, this.radius.Value, vector, ref raycastHit, (this.distance.Value == -1f) ? float.PositiveInfinity : this.distance.Value, this.layerMask))
			{
				this.storeHitObject.Value = raycastHit.collider.gameObject;
				this.storeHitPoint.Value = raycastHit.point;
				this.storeHitNormal.Value = raycastHit.normal;
				this.storeHitDistance.Value = raycastHit.distance;
				return 2;
			}
			return 1;
		}

		// Token: 0x060081FB RID: 33275 RVA: 0x002CCDF4 File Offset: 0x002CAFF4
		public override void OnReset()
		{
			this.originGameObject = null;
			this.originPosition = Vector3.zero;
			this.radius = 0f;
			this.direction = Vector3.zero;
			this.distance = -1f;
			this.layerMask = -1;
			this.space = 1;
		}

		// Token: 0x04006EBA RID: 28346
		[Tooltip("Starts the spherecast at the GameObject's position. If null the originPosition will be used")]
		public SharedGameObject originGameObject;

		// Token: 0x04006EBB RID: 28347
		[Tooltip("Starts the sherecast at the position. Only used if originGameObject is null")]
		public SharedVector3 originPosition;

		// Token: 0x04006EBC RID: 28348
		[Tooltip("The radius of the spherecast")]
		public SharedFloat radius;

		// Token: 0x04006EBD RID: 28349
		[Tooltip("The direction of the spherecast")]
		public SharedVector3 direction;

		// Token: 0x04006EBE RID: 28350
		[Tooltip("The length of the spherecast. Set to -1 for infinity")]
		public SharedFloat distance = -1f;

		// Token: 0x04006EBF RID: 28351
		[Tooltip("Selectively ignore colliders")]
		public LayerMask layerMask = -1;

		// Token: 0x04006EC0 RID: 28352
		[Tooltip("Use world or local space. The direction is in world space if no GameObject is specified")]
		public Space space = 1;

		// Token: 0x04006EC1 RID: 28353
		[SharedRequired]
		[Tooltip("Stores the hit object of the spherecast")]
		public SharedGameObject storeHitObject;

		// Token: 0x04006EC2 RID: 28354
		[SharedRequired]
		[Tooltip("Stores the hit point of the spherecast")]
		public SharedVector3 storeHitPoint;

		// Token: 0x04006EC3 RID: 28355
		[SharedRequired]
		[Tooltip("Stores the hit normal of the spherecast")]
		public SharedVector3 storeHitNormal;

		// Token: 0x04006EC4 RID: 28356
		[SharedRequired]
		[Tooltip("Stores the hit distance of the spherecast")]
		public SharedFloat storeHitDistance;
	}
}
