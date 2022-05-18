using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
	// Token: 0x020014E4 RID: 5348
	[TaskCategory("Basic/Transform")]
	[TaskDescription("Stores the right vector of the Transform. Returns Success.")]
	public class GetRightVector : Action
	{
		// Token: 0x06007FBD RID: 32701 RVA: 0x002CA74C File Offset: 0x002C894C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007FBE RID: 32702 RVA: 0x00056AC3 File Offset: 0x00054CC3
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

		// Token: 0x06007FBF RID: 32703 RVA: 0x00056AF6 File Offset: 0x00054CF6
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x04006C9F RID: 27807
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006CA0 RID: 27808
		[Tooltip("The position of the Transform")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x04006CA1 RID: 27809
		private Transform targetTransform;

		// Token: 0x04006CA2 RID: 27810
		private GameObject prevGameObject;
	}
}
