using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x02001546 RID: 5446
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Applies a force to the rigidbody that simulates explosion effects. Returns Success.")]
	public class AddExplosionForce : Action
	{
		// Token: 0x06008116 RID: 33046 RVA: 0x002CBC64 File Offset: 0x002C9E64
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008117 RID: 33047 RVA: 0x002CBCA4 File Offset: 0x002C9EA4
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

		// Token: 0x06008118 RID: 33048 RVA: 0x002CBD04 File Offset: 0x002C9F04
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.explosionForce = 0f;
			this.explosionPosition = Vector3.zero;
			this.explosionRadius = 0f;
			this.upwardsModifier = 0f;
			this.forceMode = 0;
		}

		// Token: 0x04006DC8 RID: 28104
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006DC9 RID: 28105
		[Tooltip("The force of the explosion")]
		public SharedFloat explosionForce;

		// Token: 0x04006DCA RID: 28106
		[Tooltip("The position of the explosion")]
		public SharedVector3 explosionPosition;

		// Token: 0x04006DCB RID: 28107
		[Tooltip("The radius of the explosion")]
		public SharedFloat explosionRadius;

		// Token: 0x04006DCC RID: 28108
		[Tooltip("Applies the force as if it was applied from beneath the object")]
		public float upwardsModifier;

		// Token: 0x04006DCD RID: 28109
		[Tooltip("The type of force")]
		public ForceMode forceMode;

		// Token: 0x04006DCE RID: 28110
		private Rigidbody rigidbody;

		// Token: 0x04006DCF RID: 28111
		private GameObject prevGameObject;
	}
}
