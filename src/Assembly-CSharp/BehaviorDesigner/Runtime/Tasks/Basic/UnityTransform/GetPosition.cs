using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
	// Token: 0x020014E3 RID: 5347
	[TaskCategory("Basic/Transform")]
	[TaskDescription("Stores the position of the Transform. Returns Success.")]
	public class GetPosition : Action
	{
		// Token: 0x06007FB9 RID: 32697 RVA: 0x002CA70C File Offset: 0x002C890C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007FBA RID: 32698 RVA: 0x00056A77 File Offset: 0x00054C77
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return 1;
			}
			this.storeValue.Value = this.targetTransform.position;
			return 2;
		}

		// Token: 0x06007FBB RID: 32699 RVA: 0x00056AAA File Offset: 0x00054CAA
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x04006C9B RID: 27803
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006C9C RID: 27804
		[Tooltip("Can the target GameObject be empty?")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x04006C9D RID: 27805
		private Transform targetTransform;

		// Token: 0x04006C9E RID: 27806
		private GameObject prevGameObject;
	}
}
