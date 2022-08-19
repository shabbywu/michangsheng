using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
	// Token: 0x02001033 RID: 4147
	[TaskCategory("Basic/Transform")]
	[TaskDescription("Sets the local euler angles of the Transform. Returns Success.")]
	public class SetLocalEulerAngles : Action
	{
		// Token: 0x060071E7 RID: 29159 RVA: 0x002ACE14 File Offset: 0x002AB014
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060071E8 RID: 29160 RVA: 0x002ACE54 File Offset: 0x002AB054
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return 1;
			}
			this.targetTransform.localEulerAngles = this.localEulerAngles.Value;
			return 2;
		}

		// Token: 0x060071E9 RID: 29161 RVA: 0x002ACE87 File Offset: 0x002AB087
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.localEulerAngles = Vector3.zero;
		}

		// Token: 0x04005DC8 RID: 24008
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005DC9 RID: 24009
		[Tooltip("The local euler angles of the Transform")]
		public SharedVector3 localEulerAngles;

		// Token: 0x04005DCA RID: 24010
		private Transform targetTransform;

		// Token: 0x04005DCB RID: 24011
		private GameObject prevGameObject;
	}
}
