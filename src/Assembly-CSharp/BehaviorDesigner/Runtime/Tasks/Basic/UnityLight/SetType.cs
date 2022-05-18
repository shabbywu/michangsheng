using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityLight
{
	// Token: 0x020015DF RID: 5599
	[TaskCategory("Basic/Light")]
	[TaskDescription("Sets the type of the light.")]
	public class SetType : Action
	{
		// Token: 0x06008330 RID: 33584 RVA: 0x002CE86C File Offset: 0x002CCA6C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008331 RID: 33585 RVA: 0x0005A294 File Offset: 0x00058494
		public override TaskStatus OnUpdate()
		{
			if (this.light == null)
			{
				Debug.LogWarning("Light is null");
				return 1;
			}
			this.light.type = this.type;
			return 2;
		}

		// Token: 0x06008332 RID: 33586 RVA: 0x0005A2C2 File Offset: 0x000584C2
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04007003 RID: 28675
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04007004 RID: 28676
		[Tooltip("The type to set")]
		public LightType type;

		// Token: 0x04007005 RID: 28677
		private Light light;

		// Token: 0x04007006 RID: 28678
		private GameObject prevGameObject;
	}
}
