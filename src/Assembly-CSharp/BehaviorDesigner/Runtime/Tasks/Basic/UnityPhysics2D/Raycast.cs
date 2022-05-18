using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityPhysics2D
{
	// Token: 0x02001583 RID: 5507
	[TaskCategory("Basic/Physics2D")]
	[TaskDescription("Casts a ray against all colliders in the scene. Returns success if a collider was hit.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=118")]
	public class Raycast : Action
	{
		// Token: 0x060081F1 RID: 33265 RVA: 0x002CCA10 File Offset: 0x002CAC10
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
			RaycastHit2D raycastHit2D = Physics2D.Raycast(vector2, vector, (this.distance.Value == -1f) ? float.PositiveInfinity : this.distance.Value, this.layerMask);
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

		// Token: 0x060081F2 RID: 33266 RVA: 0x002CCB30 File Offset: 0x002CAD30
		public override void OnReset()
		{
			this.originGameObject = null;
			this.originPosition = Vector2.zero;
			this.direction = Vector2.zero;
			this.distance = -1f;
			this.layerMask = -1;
			this.space = 1;
		}

		// Token: 0x04006EA3 RID: 28323
		[Tooltip("Starts the ray at the GameObject's position. If null the originPosition will be used.")]
		public SharedGameObject originGameObject;

		// Token: 0x04006EA4 RID: 28324
		[Tooltip("Starts the ray at the position. Only used if originGameObject is null.")]
		public SharedVector2 originPosition;

		// Token: 0x04006EA5 RID: 28325
		[Tooltip("The direction of the ray")]
		public SharedVector2 direction;

		// Token: 0x04006EA6 RID: 28326
		[Tooltip("The length of the ray. Set to -1 for infinity.")]
		public SharedFloat distance = -1f;

		// Token: 0x04006EA7 RID: 28327
		[Tooltip("Selectively ignore colliders.")]
		public LayerMask layerMask = -1;

		// Token: 0x04006EA8 RID: 28328
		[Tooltip("Cast the ray in world or local space. The direction is in world space if no GameObject is specified.")]
		public Space space = 1;

		// Token: 0x04006EA9 RID: 28329
		[SharedRequired]
		[Tooltip("Stores the hit object of the raycast.")]
		public SharedGameObject storeHitObject;

		// Token: 0x04006EAA RID: 28330
		[SharedRequired]
		[Tooltip("Stores the hit point of the raycast.")]
		public SharedVector2 storeHitPoint;

		// Token: 0x04006EAB RID: 28331
		[SharedRequired]
		[Tooltip("Stores the hit normal of the raycast.")]
		public SharedVector2 storeHitNormal;

		// Token: 0x04006EAC RID: 28332
		[SharedRequired]
		[Tooltip("Stores the hit distance of the raycast.")]
		public SharedFloat storeHitDistance;
	}
}
