using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRenderer
{
	// Token: 0x020010B0 RID: 4272
	[TaskCategory("Basic/Renderer")]
	[TaskDescription("Returns Success if the Renderer is visible, otherwise Failure.")]
	public class IsVisible : Conditional
	{
		// Token: 0x060073AC RID: 29612 RVA: 0x002B07C0 File Offset: 0x002AE9C0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.renderer = defaultGameObject.GetComponent<Renderer>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060073AD RID: 29613 RVA: 0x002B0800 File Offset: 0x002AEA00
		public override TaskStatus OnUpdate()
		{
			if (this.renderer == null)
			{
				Debug.LogWarning("Renderer is null");
				return 1;
			}
			if (!this.renderer.isVisible)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x060073AE RID: 29614 RVA: 0x002B082C File Offset: 0x002AEA2C
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04005F5D RID: 24413
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005F5E RID: 24414
		private Renderer renderer;

		// Token: 0x04005F5F RID: 24415
		private GameObject prevGameObject;
	}
}
