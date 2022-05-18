using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
	// Token: 0x020014E7 RID: 5351
	[TaskCategory("Basic/Transform")]
	[TaskDescription("Returns Success if the transform is a child of the specified GameObject.")]
	public class IsChildOf : Conditional
	{
		// Token: 0x06007FC9 RID: 32713 RVA: 0x002CA80C File Offset: 0x002C8A0C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007FCA RID: 32714 RVA: 0x00056BA7 File Offset: 0x00054DA7
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

		// Token: 0x06007FCB RID: 32715 RVA: 0x00056BDE File Offset: 0x00054DDE
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.transformName = null;
		}

		// Token: 0x04006CAB RID: 27819
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006CAC RID: 27820
		[Tooltip("The interested transform")]
		public SharedTransform transformName;

		// Token: 0x04006CAD RID: 27821
		private Transform targetTransform;

		// Token: 0x04006CAE RID: 27822
		private GameObject prevGameObject;
	}
}
