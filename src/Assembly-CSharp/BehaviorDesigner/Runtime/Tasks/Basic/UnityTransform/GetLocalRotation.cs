using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
	// Token: 0x020014E0 RID: 5344
	[TaskCategory("Basic/Transform")]
	[TaskDescription("Stores the local rotation of the Transform. Returns Success.")]
	public class GetLocalRotation : Action
	{
		// Token: 0x06007FAD RID: 32685 RVA: 0x002CA64C File Offset: 0x002C884C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007FAE RID: 32686 RVA: 0x0005699C File Offset: 0x00054B9C
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return 1;
			}
			this.storeValue.Value = this.targetTransform.localRotation;
			return 2;
		}

		// Token: 0x06007FAF RID: 32687 RVA: 0x000569CF File Offset: 0x00054BCF
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Quaternion.identity;
		}

		// Token: 0x04006C8F RID: 27791
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006C90 RID: 27792
		[Tooltip("The local rotation of the Transform")]
		[RequiredField]
		public SharedQuaternion storeValue;

		// Token: 0x04006C91 RID: 27793
		private Transform targetTransform;

		// Token: 0x04006C92 RID: 27794
		private GameObject prevGameObject;
	}
}
