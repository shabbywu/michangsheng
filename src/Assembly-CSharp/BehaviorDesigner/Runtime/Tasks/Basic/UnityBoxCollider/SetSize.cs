using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityBoxCollider
{
	// Token: 0x02001161 RID: 4449
	[TaskCategory("Basic/BoxCollider")]
	[TaskDescription("Sets the size of the BoxCollider. Returns Success.")]
	public class SetSize : Action
	{
		// Token: 0x0600761B RID: 30235 RVA: 0x002B5CF0 File Offset: 0x002B3EF0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.boxCollider = defaultGameObject.GetComponent<BoxCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600761C RID: 30236 RVA: 0x002B5D30 File Offset: 0x002B3F30
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

		// Token: 0x0600761D RID: 30237 RVA: 0x002B5D63 File Offset: 0x002B3F63
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.size = Vector3.zero;
		}

		// Token: 0x040061A1 RID: 24993
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040061A2 RID: 24994
		[Tooltip("The size of the BoxCollider")]
		public SharedVector3 size;

		// Token: 0x040061A3 RID: 24995
		private BoxCollider boxCollider;

		// Token: 0x040061A4 RID: 24996
		private GameObject prevGameObject;
	}
}
