using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityBoxCollider
{
	// Token: 0x0200161F RID: 5663
	[TaskCategory("Basic/BoxCollider")]
	[TaskDescription("Sets the center of the BoxCollider. Returns Success.")]
	public class SetCenter : Action
	{
		// Token: 0x06008411 RID: 33809 RVA: 0x002CF360 File Offset: 0x002CD560
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.boxCollider = defaultGameObject.GetComponent<BoxCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008412 RID: 33810 RVA: 0x0005B236 File Offset: 0x00059436
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

		// Token: 0x06008413 RID: 33811 RVA: 0x0005B269 File Offset: 0x00059469
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.center = Vector3.zero;
		}

		// Token: 0x040070C0 RID: 28864
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040070C1 RID: 28865
		[Tooltip("The center of the BoxCollider")]
		public SharedVector3 center;

		// Token: 0x040070C2 RID: 28866
		private BoxCollider boxCollider;

		// Token: 0x040070C3 RID: 28867
		private GameObject prevGameObject;
	}
}
