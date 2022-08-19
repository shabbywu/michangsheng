using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityPhysics
{
	// Token: 0x020010CC RID: 4300
	[TaskCategory("Basic/Physics")]
	[TaskDescription("Casts a sphere against all colliders in the scene. Returns success if a collider was hit.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=117")]
	public class SphereCast : Action
	{
		// Token: 0x06007400 RID: 29696 RVA: 0x002B1474 File Offset: 0x002AF674
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

		// Token: 0x06007401 RID: 29697 RVA: 0x002B1584 File Offset: 0x002AF784
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

		// Token: 0x04005FBA RID: 24506
		[Tooltip("Starts the spherecast at the GameObject's position. If null the originPosition will be used")]
		public SharedGameObject originGameObject;

		// Token: 0x04005FBB RID: 24507
		[Tooltip("Starts the sherecast at the position. Only used if originGameObject is null")]
		public SharedVector3 originPosition;

		// Token: 0x04005FBC RID: 24508
		[Tooltip("The radius of the spherecast")]
		public SharedFloat radius;

		// Token: 0x04005FBD RID: 24509
		[Tooltip("The direction of the spherecast")]
		public SharedVector3 direction;

		// Token: 0x04005FBE RID: 24510
		[Tooltip("The length of the spherecast. Set to -1 for infinity")]
		public SharedFloat distance = -1f;

		// Token: 0x04005FBF RID: 24511
		[Tooltip("Selectively ignore colliders")]
		public LayerMask layerMask = -1;

		// Token: 0x04005FC0 RID: 24512
		[Tooltip("Use world or local space. The direction is in world space if no GameObject is specified")]
		public Space space = 1;

		// Token: 0x04005FC1 RID: 24513
		[SharedRequired]
		[Tooltip("Stores the hit object of the spherecast")]
		public SharedGameObject storeHitObject;

		// Token: 0x04005FC2 RID: 24514
		[SharedRequired]
		[Tooltip("Stores the hit point of the spherecast")]
		public SharedVector3 storeHitPoint;

		// Token: 0x04005FC3 RID: 24515
		[SharedRequired]
		[Tooltip("Stores the hit normal of the spherecast")]
		public SharedVector3 storeHitNormal;

		// Token: 0x04005FC4 RID: 24516
		[SharedRequired]
		[Tooltip("Stores the hit distance of the spherecast")]
		public SharedFloat storeHitDistance;
	}
}
