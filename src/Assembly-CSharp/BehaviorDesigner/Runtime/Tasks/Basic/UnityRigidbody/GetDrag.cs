using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x0200154F RID: 5455
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Stores the drag of the Rigidbody. Returns Success.")]
	public class GetDrag : Action
	{
		// Token: 0x0600813A RID: 33082 RVA: 0x002CBFAC File Offset: 0x002CA1AC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600813B RID: 33083 RVA: 0x00058350 File Offset: 0x00056550
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			this.storeValue.Value = this.rigidbody.drag;
			return 2;
		}

		// Token: 0x0600813C RID: 33084 RVA: 0x00058383 File Offset: 0x00056583
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x04006DF6 RID: 28150
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006DF7 RID: 28151
		[Tooltip("The drag of the Rigidbody")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x04006DF8 RID: 28152
		private Rigidbody rigidbody;

		// Token: 0x04006DF9 RID: 28153
		private GameObject prevGameObject;
	}
}
