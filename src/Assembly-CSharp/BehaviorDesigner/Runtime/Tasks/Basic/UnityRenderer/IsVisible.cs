using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRenderer
{
	// Token: 0x0200156A RID: 5482
	[TaskCategory("Basic/Renderer")]
	[TaskDescription("Returns Success if the Renderer is visible, otherwise Failure.")]
	public class IsVisible : Conditional
	{
		// Token: 0x060081A6 RID: 33190 RVA: 0x002CC66C File Offset: 0x002CA86C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.renderer = defaultGameObject.GetComponent<Renderer>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060081A7 RID: 33191 RVA: 0x00058AB3 File Offset: 0x00056CB3
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

		// Token: 0x060081A8 RID: 33192 RVA: 0x00058ADF File Offset: 0x00056CDF
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04006E5D RID: 28253
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006E5E RID: 28254
		private Renderer renderer;

		// Token: 0x04006E5F RID: 28255
		private GameObject prevGameObject;
	}
}
