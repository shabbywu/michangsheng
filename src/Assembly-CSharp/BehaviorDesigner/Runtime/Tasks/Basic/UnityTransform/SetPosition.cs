using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
	// Token: 0x02001038 RID: 4152
	[TaskCategory("Basic/Transform")]
	[TaskDescription("Sets the position of the Transform. Returns Success.")]
	public class SetPosition : Action
	{
		// Token: 0x060071FB RID: 29179 RVA: 0x002AD0C8 File Offset: 0x002AB2C8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060071FC RID: 29180 RVA: 0x002AD108 File Offset: 0x002AB308
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return 1;
			}
			this.targetTransform.position = this.position.Value;
			return 2;
		}

		// Token: 0x060071FD RID: 29181 RVA: 0x002AD13B File Offset: 0x002AB33B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.position = Vector3.zero;
		}

		// Token: 0x04005DDC RID: 24028
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005DDD RID: 24029
		[Tooltip("The position of the Transform")]
		public SharedVector3 position;

		// Token: 0x04005DDE RID: 24030
		private Transform targetTransform;

		// Token: 0x04005DDF RID: 24031
		private GameObject prevGameObject;
	}
}
