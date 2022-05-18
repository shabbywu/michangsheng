using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityLight
{
	// Token: 0x020015D9 RID: 5593
	[TaskCategory("Basic/Light")]
	[TaskDescription("Sets the intensity of the light.")]
	public class SetIntensity : Action
	{
		// Token: 0x06008318 RID: 33560 RVA: 0x002CE6EC File Offset: 0x002CC8EC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008319 RID: 33561 RVA: 0x0005A0E1 File Offset: 0x000582E1
		public override TaskStatus OnUpdate()
		{
			if (this.light == null)
			{
				Debug.LogWarning("Light is null");
				return 1;
			}
			this.light.intensity = this.intensity.Value;
			return 2;
		}

		// Token: 0x0600831A RID: 33562 RVA: 0x0005A114 File Offset: 0x00058314
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.intensity = 0f;
		}

		// Token: 0x04006FEB RID: 28651
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006FEC RID: 28652
		[Tooltip("The intensity to set")]
		public SharedFloat intensity;

		// Token: 0x04006FED RID: 28653
		private Light light;

		// Token: 0x04006FEE RID: 28654
		private GameObject prevGameObject;
	}
}
