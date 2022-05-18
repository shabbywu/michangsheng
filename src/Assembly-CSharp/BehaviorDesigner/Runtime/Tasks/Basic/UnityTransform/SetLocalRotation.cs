using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
	// Token: 0x020014EF RID: 5359
	[TaskCategory("Basic/Transform")]
	[TaskDescription("Sets the local rotation of the Transform. Returns Success.")]
	public class SetLocalRotation : Action
	{
		// Token: 0x06007FE9 RID: 32745 RVA: 0x002CAAD4 File Offset: 0x002C8CD4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007FEA RID: 32746 RVA: 0x00056DEA File Offset: 0x00054FEA
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return 1;
			}
			this.targetTransform.localRotation = this.localRotation.Value;
			return 2;
		}

		// Token: 0x06007FEB RID: 32747 RVA: 0x00056E1D File Offset: 0x0005501D
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.localRotation = Quaternion.identity;
		}

		// Token: 0x04006CD0 RID: 27856
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006CD1 RID: 27857
		[Tooltip("The local rotation of the Transform")]
		public SharedQuaternion localRotation;

		// Token: 0x04006CD2 RID: 27858
		private Transform targetTransform;

		// Token: 0x04006CD3 RID: 27859
		private GameObject prevGameObject;
	}
}
