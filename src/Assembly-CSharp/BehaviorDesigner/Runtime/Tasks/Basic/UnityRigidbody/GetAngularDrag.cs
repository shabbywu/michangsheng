using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x0200154C RID: 5452
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Stores the angular drag of the Rigidbody. Returns Success.")]
	public class GetAngularDrag : Action
	{
		// Token: 0x0600812E RID: 33070 RVA: 0x002CBEEC File Offset: 0x002CA0EC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600812F RID: 33071 RVA: 0x0005826C File Offset: 0x0005646C
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			this.storeValue.Value = this.rigidbody.angularDrag;
			return 2;
		}

		// Token: 0x06008130 RID: 33072 RVA: 0x0005829F File Offset: 0x0005649F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x04006DEA RID: 28138
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006DEB RID: 28139
		[Tooltip("The angular drag of the Rigidbody")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x04006DEC RID: 28140
		private Rigidbody rigidbody;

		// Token: 0x04006DED RID: 28141
		private GameObject prevGameObject;
	}
}
