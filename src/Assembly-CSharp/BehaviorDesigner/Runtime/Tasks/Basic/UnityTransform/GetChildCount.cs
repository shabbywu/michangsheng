using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
	// Token: 0x02001021 RID: 4129
	[TaskCategory("Basic/Transform")]
	[TaskDescription("Stores the number of children a Transform has. Returns Success.")]
	public class GetChildCount : Action
	{
		// Token: 0x0600719F RID: 29087 RVA: 0x002AC398 File Offset: 0x002AA598
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060071A0 RID: 29088 RVA: 0x002AC3D8 File Offset: 0x002AA5D8
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return 1;
			}
			this.storeValue.Value = this.targetTransform.childCount;
			return 2;
		}

		// Token: 0x060071A1 RID: 29089 RVA: 0x002AC40B File Offset: 0x002AA60B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0;
		}

		// Token: 0x04005D7B RID: 23931
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005D7C RID: 23932
		[Tooltip("The number of children")]
		[RequiredField]
		public SharedInt storeValue;

		// Token: 0x04005D7D RID: 23933
		private Transform targetTransform;

		// Token: 0x04005D7E RID: 23934
		private GameObject prevGameObject;
	}
}
