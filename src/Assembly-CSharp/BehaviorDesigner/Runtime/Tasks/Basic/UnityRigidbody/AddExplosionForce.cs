using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x0200108C RID: 4236
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Applies a force to the rigidbody that simulates explosion effects. Returns Success.")]
	public class AddExplosionForce : Action
	{
		// Token: 0x0600731C RID: 29468 RVA: 0x002AF3D4 File Offset: 0x002AD5D4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600731D RID: 29469 RVA: 0x002AF414 File Offset: 0x002AD614
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			this.rigidbody.AddExplosionForce(this.explosionForce.Value, this.explosionPosition.Value, this.explosionRadius.Value, this.upwardsModifier, this.forceMode);
			return 2;
		}

		// Token: 0x0600731E RID: 29470 RVA: 0x002AF474 File Offset: 0x002AD674
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.explosionForce = 0f;
			this.explosionPosition = Vector3.zero;
			this.explosionRadius = 0f;
			this.upwardsModifier = 0f;
			this.forceMode = 0;
		}

		// Token: 0x04005EC8 RID: 24264
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005EC9 RID: 24265
		[Tooltip("The force of the explosion")]
		public SharedFloat explosionForce;

		// Token: 0x04005ECA RID: 24266
		[Tooltip("The position of the explosion")]
		public SharedVector3 explosionPosition;

		// Token: 0x04005ECB RID: 24267
		[Tooltip("The radius of the explosion")]
		public SharedFloat explosionRadius;

		// Token: 0x04005ECC RID: 24268
		[Tooltip("Applies the force as if it was applied from beneath the object")]
		public float upwardsModifier;

		// Token: 0x04005ECD RID: 24269
		[Tooltip("The type of force")]
		public ForceMode forceMode;

		// Token: 0x04005ECE RID: 24270
		private Rigidbody rigidbody;

		// Token: 0x04005ECF RID: 24271
		private GameObject prevGameObject;
	}
}
