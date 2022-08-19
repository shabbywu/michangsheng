using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
	// Token: 0x0200102A RID: 4138
	[TaskCategory("Basic/Transform")]
	[TaskDescription("Stores the right vector of the Transform. Returns Success.")]
	public class GetRightVector : Action
	{
		// Token: 0x060071C3 RID: 29123 RVA: 0x002AC878 File Offset: 0x002AAA78
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060071C4 RID: 29124 RVA: 0x002AC8B8 File Offset: 0x002AAAB8
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return 1;
			}
			this.storeValue.Value = this.targetTransform.right;
			return 2;
		}

		// Token: 0x060071C5 RID: 29125 RVA: 0x002AC8EB File Offset: 0x002AAAEB
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x04005D9F RID: 23967
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005DA0 RID: 23968
		[Tooltip("The position of the Transform")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x04005DA1 RID: 23969
		private Transform targetTransform;

		// Token: 0x04005DA2 RID: 23970
		private GameObject prevGameObject;
	}
}
