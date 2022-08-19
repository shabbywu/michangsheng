using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
	// Token: 0x02001031 RID: 4145
	[TaskCategory("Basic/Transform")]
	[TaskDescription("Sets the euler angles of the Transform. Returns Success.")]
	public class SetEulerAngles : Action
	{
		// Token: 0x060071DF RID: 29151 RVA: 0x002ACCFC File Offset: 0x002AAEFC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060071E0 RID: 29152 RVA: 0x002ACD3C File Offset: 0x002AAF3C
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return 1;
			}
			this.targetTransform.eulerAngles = this.eulerAngles.Value;
			return 2;
		}

		// Token: 0x060071E1 RID: 29153 RVA: 0x002ACD6F File Offset: 0x002AAF6F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.eulerAngles = Vector3.zero;
		}

		// Token: 0x04005DC0 RID: 24000
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005DC1 RID: 24001
		[Tooltip("The euler angles of the Transform")]
		public SharedVector3 eulerAngles;

		// Token: 0x04005DC2 RID: 24002
		private Transform targetTransform;

		// Token: 0x04005DC3 RID: 24003
		private GameObject prevGameObject;
	}
}
