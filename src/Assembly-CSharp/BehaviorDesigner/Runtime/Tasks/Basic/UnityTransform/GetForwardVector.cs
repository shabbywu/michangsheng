using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
	// Token: 0x020014DD RID: 5341
	[TaskCategory("Basic/Transform")]
	[TaskDescription("Stores the forward vector of the Transform. Returns Success.")]
	public class GetForwardVector : Action
	{
		// Token: 0x06007FA1 RID: 32673 RVA: 0x002CA58C File Offset: 0x002C878C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007FA2 RID: 32674 RVA: 0x000568B8 File Offset: 0x00054AB8
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return 1;
			}
			this.storeValue.Value = this.targetTransform.forward;
			return 2;
		}

		// Token: 0x06007FA3 RID: 32675 RVA: 0x000568EB File Offset: 0x00054AEB
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x04006C83 RID: 27779
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006C84 RID: 27780
		[Tooltip("The position of the Transform")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x04006C85 RID: 27781
		private Transform targetTransform;

		// Token: 0x04006C86 RID: 27782
		private GameObject prevGameObject;
	}
}
