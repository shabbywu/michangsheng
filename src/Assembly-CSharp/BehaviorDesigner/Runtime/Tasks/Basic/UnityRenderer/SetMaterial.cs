using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRenderer
{
	// Token: 0x020010B1 RID: 4273
	[TaskCategory("Basic/Renderer")]
	[TaskDescription("Sets the material on the Renderer.")]
	public class SetMaterial : Action
	{
		// Token: 0x060073B0 RID: 29616 RVA: 0x002B0838 File Offset: 0x002AEA38
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.renderer = defaultGameObject.GetComponent<Renderer>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060073B1 RID: 29617 RVA: 0x002B0878 File Offset: 0x002AEA78
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

		// Token: 0x060073B2 RID: 29618 RVA: 0x002B08AB File Offset: 0x002AEAAB
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.material = null;
		}

		// Token: 0x04005F60 RID: 24416
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005F61 RID: 24417
		[Tooltip("The material to set")]
		public SharedMaterial material;

		// Token: 0x04005F62 RID: 24418
		private Renderer renderer;

		// Token: 0x04005F63 RID: 24419
		private GameObject prevGameObject;
	}
}
