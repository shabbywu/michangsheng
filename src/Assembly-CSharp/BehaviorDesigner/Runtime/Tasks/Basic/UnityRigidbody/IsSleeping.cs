using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x02001558 RID: 5464
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Returns Success if the Rigidbody is sleeping, otherwise Failure.")]
	public class IsSleeping : Conditional
	{
		// Token: 0x0600815E RID: 33118 RVA: 0x002CC1EC File Offset: 0x002CA3EC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600815F RID: 33119 RVA: 0x000585D9 File Offset: 0x000567D9
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			if (!this.rigidbody.IsSleeping())
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x06008160 RID: 33120 RVA: 0x00058605 File Offset: 0x00056805
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04006E19 RID: 28185
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006E1A RID: 28186
		private Rigidbody rigidbody;

		// Token: 0x04006E1B RID: 28187
		private GameObject prevGameObject;
	}
}
