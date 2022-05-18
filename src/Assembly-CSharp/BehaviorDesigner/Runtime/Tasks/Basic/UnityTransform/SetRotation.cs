using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
	// Token: 0x020014F4 RID: 5364
	[TaskCategory("Basic/Transform")]
	[TaskDescription("Sets the rotation of the Transform. Returns Success.")]
	public class SetRotation : Action
	{
		// Token: 0x06007FFD RID: 32765 RVA: 0x002CAC14 File Offset: 0x002C8E14
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007FFE RID: 32766 RVA: 0x00056F5D File Offset: 0x0005515D
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return 1;
			}
			this.targetTransform.rotation = this.rotation.Value;
			return 2;
		}

		// Token: 0x06007FFF RID: 32767 RVA: 0x00056F90 File Offset: 0x00055190
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.rotation = Quaternion.identity;
		}

		// Token: 0x04006CE4 RID: 27876
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006CE5 RID: 27877
		[Tooltip("The rotation of the Transform")]
		public SharedQuaternion rotation;

		// Token: 0x04006CE6 RID: 27878
		private Transform targetTransform;

		// Token: 0x04006CE7 RID: 27879
		private GameObject prevGameObject;
	}
}
