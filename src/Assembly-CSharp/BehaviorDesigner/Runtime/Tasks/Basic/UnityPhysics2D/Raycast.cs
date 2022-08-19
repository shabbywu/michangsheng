using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityPhysics2D
{
	// Token: 0x020010C9 RID: 4297
	[TaskCategory("Basic/Physics2D")]
	[TaskDescription("Casts a ray against all colliders in the scene. Returns success if a collider was hit.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=118")]
	public class Raycast : Action
	{
		// Token: 0x060073F7 RID: 29687 RVA: 0x002B10D8 File Offset: 0x002AF2D8
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

		// Token: 0x060073F8 RID: 29688 RVA: 0x002B11F8 File Offset: 0x002AF3F8
		public override void OnReset()
		{
			this.originGameObject = null;
			this.originPosition = Vector2.zero;
			this.direction = Vector2.zero;
			this.distance = -1f;
			this.layerMask = -1;
			this.space = 1;
		}

		// Token: 0x04005FA3 RID: 24483
		[Tooltip("Starts the ray at the GameObject's position. If null the originPosition will be used.")]
		public SharedGameObject originGameObject;

		// Token: 0x04005FA4 RID: 24484
		[Tooltip("Starts the ray at the position. Only used if originGameObject is null.")]
		public SharedVector2 originPosition;

		// Token: 0x04005FA5 RID: 24485
		[Tooltip("The direction of the ray")]
		public SharedVector2 direction;

		// Token: 0x04005FA6 RID: 24486
		[Tooltip("The length of the ray. Set to -1 for infinity.")]
		public SharedFloat distance = -1f;

		// Token: 0x04005FA7 RID: 24487
		[Tooltip("Selectively ignore colliders.")]
		public LayerMask layerMask = -1;

		// Token: 0x04005FA8 RID: 24488
		[Tooltip("Cast the ray in world or local space. The direction is in world space if no GameObject is specified.")]
		public Space space = 1;

		// Token: 0x04005FA9 RID: 24489
		[SharedRequired]
		[Tooltip("Stores the hit object of the raycast.")]
		public SharedGameObject storeHitObject;

		// Token: 0x04005FAA RID: 24490
		[SharedRequired]
		[Tooltip("Stores the hit point of the raycast.")]
		public SharedVector2 storeHitPoint;

		// Token: 0x04005FAB RID: 24491
		[SharedRequired]
		[Tooltip("Stores the hit normal of the raycast.")]
		public SharedVector2 storeHitNormal;

		// Token: 0x04005FAC RID: 24492
		[SharedRequired]
		[Tooltip("Stores the hit distance of the raycast.")]
		public SharedFloat storeHitDistance;
	}
}
