using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnitySphereCollider
{
	// Token: 0x0200104D RID: 4173
	[TaskCategory("Basic/SphereCollider")]
	[TaskDescription("Sets the center of the SphereCollider. Returns Success.")]
	public class SetCenter : Action
	{
		// Token: 0x06007242 RID: 29250 RVA: 0x002AD8F8 File Offset: 0x002ABAF8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.sphereCollider = defaultGameObject.GetComponent<SphereCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007243 RID: 29251 RVA: 0x002AD938 File Offset: 0x002ABB38
		public override TaskStatus OnUpdate()
		{
			if (this.sphereCollider == null)
			{
				Debug.LogWarning("SphereCollider is null");
				return 1;
			}
			this.sphereCollider.center = this.center.Value;
			return 2;
		}

		// Token: 0x06007244 RID: 29252 RVA: 0x002AD96B File Offset: 0x002ABB6B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.center = Vector3.zero;
		}

		// Token: 0x04005E16 RID: 24086
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005E17 RID: 24087
		[Tooltip("The center of the SphereCollider")]
		public SharedVector3 center;

		// Token: 0x04005E18 RID: 24088
		private SphereCollider sphereCollider;

		// Token: 0x04005E19 RID: 24089
		private GameObject prevGameObject;
	}
}
