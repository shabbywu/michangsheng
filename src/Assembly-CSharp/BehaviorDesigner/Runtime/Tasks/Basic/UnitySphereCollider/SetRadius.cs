using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnitySphereCollider
{
	// Token: 0x0200104E RID: 4174
	[TaskCategory("Basic/SphereCollider")]
	[TaskDescription("Sets the radius of the SphereCollider. Returns Success.")]
	public class SetRadius : Action
	{
		// Token: 0x06007246 RID: 29254 RVA: 0x002AD984 File Offset: 0x002ABB84
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.sphereCollider = defaultGameObject.GetComponent<SphereCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007247 RID: 29255 RVA: 0x002AD9C4 File Offset: 0x002ABBC4
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

		// Token: 0x06007248 RID: 29256 RVA: 0x002AD9F7 File Offset: 0x002ABBF7
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.radius = 0f;
		}

		// Token: 0x04005E1A RID: 24090
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005E1B RID: 24091
		[Tooltip("The radius of the SphereCollider")]
		public SharedFloat radius;

		// Token: 0x04005E1C RID: 24092
		private SphereCollider sphereCollider;

		// Token: 0x04005E1D RID: 24093
		private GameObject prevGameObject;
	}
}
