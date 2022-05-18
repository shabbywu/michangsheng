using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x02001549 RID: 5449
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Applies a force to the rigidbody relative to its coordinate system. Returns Success.")]
	public class AddRelativeForce : Action
	{
		// Token: 0x06008122 RID: 33058 RVA: 0x002CBE2C File Offset: 0x002CA02C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008123 RID: 33059 RVA: 0x0005817B File Offset: 0x0005637B
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			this.rigidbody.AddRelativeForce(this.force.Value, this.forceMode);
			return 2;
		}

		// Token: 0x06008124 RID: 33060 RVA: 0x000581B4 File Offset: 0x000563B4
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.force = Vector3.zero;
			this.forceMode = 0;
		}

		// Token: 0x04006DDB RID: 28123
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006DDC RID: 28124
		[Tooltip("The amount of force to apply")]
		public SharedVector3 force;

		// Token: 0x04006DDD RID: 28125
		[Tooltip("The type of force")]
		public ForceMode forceMode;

		// Token: 0x04006DDE RID: 28126
		private Rigidbody rigidbody;

		// Token: 0x04006DDF RID: 28127
		private GameObject prevGameObject;
	}
}
