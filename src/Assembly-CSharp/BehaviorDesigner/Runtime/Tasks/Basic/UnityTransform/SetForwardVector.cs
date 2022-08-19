using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
	// Token: 0x02001032 RID: 4146
	[TaskCategory("Basic/Transform")]
	[TaskDescription("Sets the forward vector of the Transform. Returns Success.")]
	public class SetForwardVector : Action
	{
		// Token: 0x060071E3 RID: 29155 RVA: 0x002ACD88 File Offset: 0x002AAF88
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060071E4 RID: 29156 RVA: 0x002ACDC8 File Offset: 0x002AAFC8
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return 1;
			}
			this.targetTransform.forward = this.position.Value;
			return 2;
		}

		// Token: 0x060071E5 RID: 29157 RVA: 0x002ACDFB File Offset: 0x002AAFFB
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.position = Vector3.zero;
		}

		// Token: 0x04005DC4 RID: 24004
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005DC5 RID: 24005
		[Tooltip("The position of the Transform")]
		public SharedVector3 position;

		// Token: 0x04005DC6 RID: 24006
		private Transform targetTransform;

		// Token: 0x04005DC7 RID: 24007
		private GameObject prevGameObject;
	}
}
