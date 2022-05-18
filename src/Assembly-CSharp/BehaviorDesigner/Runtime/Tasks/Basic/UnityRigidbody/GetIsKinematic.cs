using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x02001551 RID: 5457
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Stores the is kinematic value of the Rigidbody. Returns Success.")]
	public class GetIsKinematic : Action
	{
		// Token: 0x06008142 RID: 33090 RVA: 0x002CC02C File Offset: 0x002CA22C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008143 RID: 33091 RVA: 0x000583E4 File Offset: 0x000565E4
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			this.storeValue.Value = this.rigidbody.isKinematic;
			return 2;
		}

		// Token: 0x06008144 RID: 33092 RVA: 0x00058417 File Offset: 0x00056617
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = false;
		}

		// Token: 0x04006DFE RID: 28158
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006DFF RID: 28159
		[Tooltip("The is kinematic value of the Rigidbody")]
		[RequiredField]
		public SharedBool storeValue;

		// Token: 0x04006E00 RID: 28160
		private Rigidbody rigidbody;

		// Token: 0x04006E01 RID: 28161
		private GameObject prevGameObject;
	}
}
