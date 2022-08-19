using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
	// Token: 0x02001024 RID: 4132
	[TaskCategory("Basic/Transform")]
	[TaskDescription("Stores the local euler angles of the Transform. Returns Success.")]
	public class GetLocalEulerAngles : Action
	{
		// Token: 0x060071AB RID: 29099 RVA: 0x002AC538 File Offset: 0x002AA738
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060071AC RID: 29100 RVA: 0x002AC578 File Offset: 0x002AA778
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return 1;
			}
			this.storeValue.Value = this.targetTransform.localEulerAngles;
			return 2;
		}

		// Token: 0x060071AD RID: 29101 RVA: 0x002AC5AB File Offset: 0x002AA7AB
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x04005D87 RID: 23943
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005D88 RID: 23944
		[Tooltip("The local euler angles of the Transform")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x04005D89 RID: 23945
		private Transform targetTransform;

		// Token: 0x04005D8A RID: 23946
		private GameObject prevGameObject;
	}
}
