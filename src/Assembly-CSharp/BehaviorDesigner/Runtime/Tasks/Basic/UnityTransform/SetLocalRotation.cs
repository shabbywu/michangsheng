using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
	// Token: 0x02001035 RID: 4149
	[TaskCategory("Basic/Transform")]
	[TaskDescription("Sets the local rotation of the Transform. Returns Success.")]
	public class SetLocalRotation : Action
	{
		// Token: 0x060071EF RID: 29167 RVA: 0x002ACF2C File Offset: 0x002AB12C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060071F0 RID: 29168 RVA: 0x002ACF6C File Offset: 0x002AB16C
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

		// Token: 0x060071F1 RID: 29169 RVA: 0x002ACF9F File Offset: 0x002AB19F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.localRotation = Quaternion.identity;
		}

		// Token: 0x04005DD0 RID: 24016
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005DD1 RID: 24017
		[Tooltip("The local rotation of the Transform")]
		public SharedQuaternion localRotation;

		// Token: 0x04005DD2 RID: 24018
		private Transform targetTransform;

		// Token: 0x04005DD3 RID: 24019
		private GameObject prevGameObject;
	}
}
