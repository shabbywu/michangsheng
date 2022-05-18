using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityBoxCollider
{
	// Token: 0x02001620 RID: 5664
	[TaskCategory("Basic/BoxCollider")]
	[TaskDescription("Sets the size of the BoxCollider. Returns Success.")]
	public class SetSize : Action
	{
		// Token: 0x06008415 RID: 33813 RVA: 0x002CF3A0 File Offset: 0x002CD5A0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.boxCollider = defaultGameObject.GetComponent<BoxCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008416 RID: 33814 RVA: 0x0005B282 File Offset: 0x00059482
		public override TaskStatus OnUpdate()
		{
			if (this.boxCollider == null)
			{
				Debug.LogWarning("BoxCollider is null");
				return 1;
			}
			this.boxCollider.size = this.size.Value;
			return 2;
		}

		// Token: 0x06008417 RID: 33815 RVA: 0x0005B2B5 File Offset: 0x000594B5
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.size = Vector3.zero;
		}

		// Token: 0x040070C4 RID: 28868
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040070C5 RID: 28869
		[Tooltip("The size of the BoxCollider")]
		public SharedVector3 size;

		// Token: 0x040070C6 RID: 28870
		private BoxCollider boxCollider;

		// Token: 0x040070C7 RID: 28871
		private GameObject prevGameObject;
	}
}
