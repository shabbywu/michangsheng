using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
	// Token: 0x020014DF RID: 5343
	[TaskCategory("Basic/Transform")]
	[TaskDescription("Stores the local position of the Transform. Returns Success.")]
	public class GetLocalPosition : Action
	{
		// Token: 0x06007FA9 RID: 32681 RVA: 0x002CA60C File Offset: 0x002C880C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007FAA RID: 32682 RVA: 0x00056950 File Offset: 0x00054B50
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return 1;
			}
			this.storeValue.Value = this.targetTransform.localPosition;
			return 2;
		}

		// Token: 0x06007FAB RID: 32683 RVA: 0x00056983 File Offset: 0x00054B83
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x04006C8B RID: 27787
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006C8C RID: 27788
		[Tooltip("The local position of the Transform")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x04006C8D RID: 27789
		private Transform targetTransform;

		// Token: 0x04006C8E RID: 27790
		private GameObject prevGameObject;
	}
}
