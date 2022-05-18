using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityPhysics
{
	// Token: 0x02001585 RID: 5509
	[TaskCategory("Basic/Physics")]
	[TaskDescription("Casts a ray against all colliders in the scene. Returns success if a collider was hit.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=117")]
	public class Raycast : Action
	{
		// Token: 0x060081F7 RID: 33271 RVA: 0x002CCB88 File Offset: 0x002CAD88
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
			if (Physics.Raycast(vector2, vector, ref raycastHit, (this.distance.Value == -1f) ? float.PositiveInfinity : this.distance.Value, this.layerMask))
			{
				this.storeHitObject.Value = raycastHit.collider.gameObject;
				this.storeHitPoint.Value = raycastHit.point;
				this.storeHitNormal.Value = raycastHit.normal;
				this.storeHitDistance.Value = raycastHit.distance;
				return 2;
			}
			return 1;
		}

		// Token: 0x060081F8 RID: 33272 RVA: 0x002CCC8C File Offset: 0x002CAE8C
		public override void OnReset()
		{
			this.originGameObject = null;
			this.originPosition = Vector3.zero;
			this.direction = Vector3.zero;
			this.distance = -1f;
			this.layerMask = -1;
			this.space = 1;
		}

		// Token: 0x04006EB0 RID: 28336
		[Tooltip("Starts the ray at the GameObject's position. If null the originPosition will be used")]
		public SharedGameObject originGameObject;

		// Token: 0x04006EB1 RID: 28337
		[Tooltip("Starts the ray at the position. Only used if originGameObject is null")]
		public SharedVector3 originPosition;

		// Token: 0x04006EB2 RID: 28338
		[Tooltip("The direction of the ray")]
		public SharedVector3 direction;

		// Token: 0x04006EB3 RID: 28339
		[Tooltip("The length of the ray. Set to -1 for infinity")]
		public SharedFloat distance = -1f;

		// Token: 0x04006EB4 RID: 28340
		[Tooltip("Selectively ignore colliders")]
		public LayerMask layerMask = -1;

		// Token: 0x04006EB5 RID: 28341
		[Tooltip("Cast the ray in world or local space. The direction is in world space if no GameObject is specified")]
		public Space space = 1;

		// Token: 0x04006EB6 RID: 28342
		[SharedRequired]
		[Tooltip("Stores the hit object of the raycast")]
		public SharedGameObject storeHitObject;

		// Token: 0x04006EB7 RID: 28343
		[SharedRequired]
		[Tooltip("Stores the hit point of the raycast")]
		public SharedVector3 storeHitPoint;

		// Token: 0x04006EB8 RID: 28344
		[SharedRequired]
		[Tooltip("Stores the hit normal of the raycast")]
		public SharedVector3 storeHitNormal;

		// Token: 0x04006EB9 RID: 28345
		[SharedRequired]
		[Tooltip("Stores the hit distance of the raycast")]
		public SharedFloat storeHitDistance;
	}
}
