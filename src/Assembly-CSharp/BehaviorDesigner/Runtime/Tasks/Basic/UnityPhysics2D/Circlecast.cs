using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityPhysics2D
{
	// Token: 0x020010C7 RID: 4295
	[TaskCategory("Basic/Physics2D")]
	[TaskDescription("Casts a circle against all colliders in the scene. Returns success if a collider was hit.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=118")]
	public class Circlecast : Action
	{
		// Token: 0x060073F1 RID: 29681 RVA: 0x002B0EA4 File Offset: 0x002AF0A4
		public override TaskStatus OnUpdate()
		{
			Vector2 vector = this.direction.Value;
			Vector2 vector2;
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
			RaycastHit2D raycastHit2D = Physics2D.CircleCast(vector2, this.radius.Value, vector, (this.distance.Value == -1f) ? float.PositiveInfinity : this.distance.Value, this.layerMask);
			if (raycastHit2D.collider != null)
			{
				this.storeHitObject.Value = raycastHit2D.collider.gameObject;
				this.storeHitPoint.Value = raycastHit2D.point;
				this.storeHitNormal.Value = raycastHit2D.normal;
				this.storeHitDistance.Value = raycastHit2D.distance;
				return 2;
			}
			return 1;
		}

		// Token: 0x060073F2 RID: 29682 RVA: 0x002B0FD0 File Offset: 0x002AF1D0
		public override void OnReset()
		{
			this.originGameObject = null;
			this.originPosition = Vector2.zero;
			this.direction = Vector2.zero;
			this.radius = 0f;
			this.distance = -1f;
			this.layerMask = -1;
			this.space = 1;
		}

		// Token: 0x04005F95 RID: 24469
		[Tooltip("Starts the circlecast at the GameObject's position. If null the originPosition will be used.")]
		public SharedGameObject originGameObject;

		// Token: 0x04005F96 RID: 24470
		[Tooltip("Starts the circlecast at the position. Only used if originGameObject is null.")]
		public SharedVector2 originPosition;

		// Token: 0x04005F97 RID: 24471
		[Tooltip("The radius of the circlecast")]
		public SharedFloat radius;

		// Token: 0x04005F98 RID: 24472
		[Tooltip("The direction of the circlecast")]
		public SharedVector2 direction;

		// Token: 0x04005F99 RID: 24473
		[Tooltip("The length of the ray. Set to -1 for infinity.")]
		public SharedFloat distance = -1f;

		// Token: 0x04005F9A RID: 24474
		[Tooltip("Selectively ignore colliders.")]
		public LayerMask layerMask = -1;

		// Token: 0x04005F9B RID: 24475
		[Tooltip("Use world or local space. The direction is in world space if no GameObject is specified.")]
		public Space space = 1;

		// Token: 0x04005F9C RID: 24476
		[SharedRequired]
		[Tooltip("Stores the hit object of the circlecast.")]
		public SharedGameObject storeHitObject;

		// Token: 0x04005F9D RID: 24477
		[SharedRequired]
		[Tooltip("Stores the hit point of the circlecast.")]
		public SharedVector2 storeHitPoint;

		// Token: 0x04005F9E RID: 24478
		[SharedRequired]
		[Tooltip("Stores the hit normal of the circlecast.")]
		public SharedVector2 storeHitNormal;

		// Token: 0x04005F9F RID: 24479
		[SharedRequired]
		[Tooltip("Stores the hit distance of the circlecast.")]
		public SharedFloat storeHitDistance;
	}
}
