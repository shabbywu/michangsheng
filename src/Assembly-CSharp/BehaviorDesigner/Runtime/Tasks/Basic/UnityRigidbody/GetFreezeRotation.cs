using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x02001550 RID: 5456
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Stores the freeze rotation value of the Rigidbody. Returns Success.")]
	public class GetFreezeRotation : Action
	{
		// Token: 0x0600813E RID: 33086 RVA: 0x002CBFEC File Offset: 0x002CA1EC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600813F RID: 33087 RVA: 0x0005839C File Offset: 0x0005659C
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			this.storeValue.Value = this.rigidbody.freezeRotation;
			return 2;
		}

		// Token: 0x06008140 RID: 33088 RVA: 0x000583CF File Offset: 0x000565CF
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = false;
		}

		// Token: 0x04006DFA RID: 28154
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006DFB RID: 28155
		[Tooltip("The freeze rotation value of the Rigidbody")]
		[RequiredField]
		public SharedBool storeValue;

		// Token: 0x04006DFC RID: 28156
		private Rigidbody rigidbody;

		// Token: 0x04006DFD RID: 28157
		private GameObject prevGameObject;
	}
}
