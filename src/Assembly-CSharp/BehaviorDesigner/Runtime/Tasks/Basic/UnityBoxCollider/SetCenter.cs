using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityBoxCollider
{
	// Token: 0x02001160 RID: 4448
	[TaskCategory("Basic/BoxCollider")]
	[TaskDescription("Sets the center of the BoxCollider. Returns Success.")]
	public class SetCenter : Action
	{
		// Token: 0x06007617 RID: 30231 RVA: 0x002B5C64 File Offset: 0x002B3E64
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.boxCollider = defaultGameObject.GetComponent<BoxCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007618 RID: 30232 RVA: 0x002B5CA4 File Offset: 0x002B3EA4
		public override TaskStatus OnUpdate()
		{
			if (this.boxCollider == null)
			{
				Debug.LogWarning("BoxCollider is null");
				return 1;
			}
			this.boxCollider.center = this.center.Value;
			return 2;
		}

		// Token: 0x06007619 RID: 30233 RVA: 0x002B5CD7 File Offset: 0x002B3ED7
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.center = Vector3.zero;
		}

		// Token: 0x0400619D RID: 24989
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400619E RID: 24990
		[Tooltip("The center of the BoxCollider")]
		public SharedVector3 center;

		// Token: 0x0400619F RID: 24991
		private BoxCollider boxCollider;

		// Token: 0x040061A0 RID: 24992
		private GameObject prevGameObject;
	}
}
