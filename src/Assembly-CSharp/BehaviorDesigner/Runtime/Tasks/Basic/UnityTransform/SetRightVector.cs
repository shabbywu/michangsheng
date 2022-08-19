using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
	// Token: 0x02001039 RID: 4153
	[TaskCategory("Basic/Transform")]
	[TaskDescription("Sets the right vector of the Transform. Returns Success.")]
	public class SetRightVector : Action
	{
		// Token: 0x060071FF RID: 29183 RVA: 0x002AD154 File Offset: 0x002AB354
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007200 RID: 29184 RVA: 0x002AD194 File Offset: 0x002AB394
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return 1;
			}
			this.targetTransform.right = this.position.Value;
			return 2;
		}

		// Token: 0x06007201 RID: 29185 RVA: 0x002AD1C7 File Offset: 0x002AB3C7
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.position = Vector3.zero;
		}

		// Token: 0x04005DE0 RID: 24032
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005DE1 RID: 24033
		[Tooltip("The position of the Transform")]
		public SharedVector3 position;

		// Token: 0x04005DE2 RID: 24034
		private Transform targetTransform;

		// Token: 0x04005DE3 RID: 24035
		private GameObject prevGameObject;
	}
}
