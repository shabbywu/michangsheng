using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x02001555 RID: 5461
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Stores the use gravity value of the Rigidbody. Returns Success.")]
	public class GetUseGravity : Action
	{
		// Token: 0x06008152 RID: 33106 RVA: 0x002CC12C File Offset: 0x002CA32C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008153 RID: 33107 RVA: 0x00058510 File Offset: 0x00056710
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			this.storeValue.Value = this.rigidbody.useGravity;
			return 2;
		}

		// Token: 0x06008154 RID: 33108 RVA: 0x00058543 File Offset: 0x00056743
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = false;
		}

		// Token: 0x04006E0E RID: 28174
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006E0F RID: 28175
		[Tooltip("The use gravity value of the Rigidbody")]
		[RequiredField]
		public SharedBool storeValue;

		// Token: 0x04006E10 RID: 28176
		private Rigidbody rigidbody;

		// Token: 0x04006E11 RID: 28177
		private GameObject prevGameObject;
	}
}
