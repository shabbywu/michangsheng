using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnitySphereCollider
{
	// Token: 0x02001508 RID: 5384
	[TaskCategory("Basic/SphereCollider")]
	[TaskDescription("Sets the radius of the SphereCollider. Returns Success.")]
	public class SetRadius : Action
	{
		// Token: 0x06008040 RID: 32832 RVA: 0x002CAF20 File Offset: 0x002C9120
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.sphereCollider = defaultGameObject.GetComponent<SphereCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008041 RID: 32833 RVA: 0x000573F5 File Offset: 0x000555F5
		public override TaskStatus OnUpdate()
		{
			if (this.sphereCollider == null)
			{
				Debug.LogWarning("SphereCollider is null");
				return 1;
			}
			this.sphereCollider.radius = this.radius.Value;
			return 2;
		}

		// Token: 0x06008042 RID: 32834 RVA: 0x00057428 File Offset: 0x00055628
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.radius = 0f;
		}

		// Token: 0x04006D1A RID: 27930
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006D1B RID: 27931
		[Tooltip("The radius of the SphereCollider")]
		public SharedFloat radius;

		// Token: 0x04006D1C RID: 27932
		private SphereCollider sphereCollider;

		// Token: 0x04006D1D RID: 27933
		private GameObject prevGameObject;
	}
}
