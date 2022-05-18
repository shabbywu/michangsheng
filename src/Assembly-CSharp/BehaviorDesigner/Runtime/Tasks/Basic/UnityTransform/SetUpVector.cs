using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
	// Token: 0x020014F5 RID: 5365
	[TaskCategory("Basic/Transform")]
	[TaskDescription("Sets the up vector of the Transform. Returns Success.")]
	public class SetUpVector : Action
	{
		// Token: 0x06008001 RID: 32769 RVA: 0x002CAC54 File Offset: 0x002C8E54
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008002 RID: 32770 RVA: 0x00056FA9 File Offset: 0x000551A9
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return 1;
			}
			this.targetTransform.up = this.position.Value;
			return 2;
		}

		// Token: 0x06008003 RID: 32771 RVA: 0x00056FDC File Offset: 0x000551DC
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.position = Vector3.zero;
		}

		// Token: 0x04006CE8 RID: 27880
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006CE9 RID: 27881
		[Tooltip("The position of the Transform")]
		public SharedVector3 position;

		// Token: 0x04006CEA RID: 27882
		private Transform targetTransform;

		// Token: 0x04006CEB RID: 27883
		private GameObject prevGameObject;
	}
}
