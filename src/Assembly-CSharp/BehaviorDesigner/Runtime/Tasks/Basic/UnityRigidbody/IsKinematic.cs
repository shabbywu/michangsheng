using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x02001557 RID: 5463
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Returns Success if the Rigidbody is kinematic, otherwise Failure.")]
	public class IsKinematic : Conditional
	{
		// Token: 0x0600815A RID: 33114 RVA: 0x002CC1AC File Offset: 0x002CA3AC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600815B RID: 33115 RVA: 0x000585A4 File Offset: 0x000567A4
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			if (!this.rigidbody.isKinematic)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x0600815C RID: 33116 RVA: 0x000585D0 File Offset: 0x000567D0
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04006E16 RID: 28182
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006E17 RID: 28183
		private Rigidbody rigidbody;

		// Token: 0x04006E18 RID: 28184
		private GameObject prevGameObject;
	}
}
