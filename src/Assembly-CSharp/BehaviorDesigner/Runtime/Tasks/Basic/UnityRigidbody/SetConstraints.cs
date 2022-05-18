using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x0200155E RID: 5470
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Sets the constraints of the Rigidbody. Returns Success.")]
	public class SetConstraints : Action
	{
		// Token: 0x06008176 RID: 33142 RVA: 0x002CC36C File Offset: 0x002CA56C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008177 RID: 33143 RVA: 0x0005878A File Offset: 0x0005698A
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			this.rigidbody.constraints = this.constraints;
			return 2;
		}

		// Token: 0x06008178 RID: 33144 RVA: 0x000587B8 File Offset: 0x000569B8
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.constraints = 0;
		}

		// Token: 0x04006E30 RID: 28208
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006E31 RID: 28209
		[Tooltip("The constraints of the Rigidbody")]
		public RigidbodyConstraints constraints;

		// Token: 0x04006E32 RID: 28210
		private Rigidbody rigidbody;

		// Token: 0x04006E33 RID: 28211
		private GameObject prevGameObject;
	}
}
