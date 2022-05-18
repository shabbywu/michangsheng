using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
	// Token: 0x020014E6 RID: 5350
	[TaskCategory("Basic/Transform")]
	[TaskDescription("Stores the up vector of the Transform. Returns Success.")]
	public class GetUpVector : Action
	{
		// Token: 0x06007FC5 RID: 32709 RVA: 0x002CA7CC File Offset: 0x002C89CC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007FC6 RID: 32710 RVA: 0x00056B5B File Offset: 0x00054D5B
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return 1;
			}
			this.storeValue.Value = this.targetTransform.up;
			return 2;
		}

		// Token: 0x06007FC7 RID: 32711 RVA: 0x00056B8E File Offset: 0x00054D8E
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x04006CA7 RID: 27815
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006CA8 RID: 27816
		[Tooltip("The position of the Transform")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x04006CA9 RID: 27817
		private Transform targetTransform;

		// Token: 0x04006CAA RID: 27818
		private GameObject prevGameObject;
	}
}
