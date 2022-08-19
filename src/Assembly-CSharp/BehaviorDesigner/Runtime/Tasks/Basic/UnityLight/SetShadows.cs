using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityLight
{
	// Token: 0x0200111D RID: 4381
	[TaskCategory("Basic/Light")]
	[TaskDescription("Sets the shadow type of the light.")]
	public class SetShadows : Action
	{
		// Token: 0x0600752A RID: 29994 RVA: 0x002B4034 File Offset: 0x002B2234
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600752B RID: 29995 RVA: 0x002B4074 File Offset: 0x002B2274
		public override TaskStatus OnUpdate()
		{
			if (this.light == null)
			{
				Debug.LogWarning("Light is null");
				return 1;
			}
			this.light.shadows = this.shadows;
			return 2;
		}

		// Token: 0x0600752C RID: 29996 RVA: 0x002B40A2 File Offset: 0x002B22A2
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x040060D4 RID: 24788
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040060D5 RID: 24789
		[Tooltip("The shadow type to set")]
		public LightShadows shadows;

		// Token: 0x040060D6 RID: 24790
		private Light light;

		// Token: 0x040060D7 RID: 24791
		private GameObject prevGameObject;
	}
}
