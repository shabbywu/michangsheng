using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnitySphereCollider
{
	// Token: 0x02001507 RID: 5383
	[TaskCategory("Basic/SphereCollider")]
	[TaskDescription("Sets the center of the SphereCollider. Returns Success.")]
	public class SetCenter : Action
	{
		// Token: 0x0600803C RID: 32828 RVA: 0x002CAEE0 File Offset: 0x002C90E0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.sphereCollider = defaultGameObject.GetComponent<SphereCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600803D RID: 32829 RVA: 0x000573A9 File Offset: 0x000555A9
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

		// Token: 0x0600803E RID: 32830 RVA: 0x000573DC File Offset: 0x000555DC
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.center = Vector3.zero;
		}

		// Token: 0x04006D16 RID: 27926
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006D17 RID: 27927
		[Tooltip("The center of the SphereCollider")]
		public SharedVector3 center;

		// Token: 0x04006D18 RID: 27928
		private SphereCollider sphereCollider;

		// Token: 0x04006D19 RID: 27929
		private GameObject prevGameObject;
	}
}
