using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRenderer
{
	// Token: 0x0200156B RID: 5483
	[TaskCategory("Basic/Renderer")]
	[TaskDescription("Sets the material on the Renderer.")]
	public class SetMaterial : Action
	{
		// Token: 0x060081AA RID: 33194 RVA: 0x002CC6AC File Offset: 0x002CA8AC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.renderer = defaultGameObject.GetComponent<Renderer>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060081AB RID: 33195 RVA: 0x00058AE8 File Offset: 0x00056CE8
		public override TaskStatus OnUpdate()
		{
			if (this.renderer == null)
			{
				Debug.LogWarning("Renderer is null");
				return 1;
			}
			this.renderer.material = this.material.Value;
			return 2;
		}

		// Token: 0x060081AC RID: 33196 RVA: 0x00058B1B File Offset: 0x00056D1B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.material = null;
		}

		// Token: 0x04006E60 RID: 28256
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006E61 RID: 28257
		[Tooltip("The material to set")]
		public SharedMaterial material;

		// Token: 0x04006E62 RID: 28258
		private Renderer renderer;

		// Token: 0x04006E63 RID: 28259
		private GameObject prevGameObject;
	}
}
