using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
	// Token: 0x0200102D RID: 4141
	[TaskCategory("Basic/Transform")]
	[TaskDescription("Returns Success if the transform is a child of the specified GameObject.")]
	public class IsChildOf : Conditional
	{
		// Token: 0x060071CF RID: 29135 RVA: 0x002ACA1C File Offset: 0x002AAC1C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060071D0 RID: 29136 RVA: 0x002ACA5C File Offset: 0x002AAC5C
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return 1;
			}
			if (!this.targetTransform.IsChildOf(this.transformName.Value))
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x060071D1 RID: 29137 RVA: 0x002ACA93 File Offset: 0x002AAC93
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.transformName = null;
		}

		// Token: 0x04005DAB RID: 23979
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005DAC RID: 23980
		[Tooltip("The interested transform")]
		public SharedTransform transformName;

		// Token: 0x04005DAD RID: 23981
		private Transform targetTransform;

		// Token: 0x04005DAE RID: 23982
		private GameObject prevGameObject;
	}
}
