using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityLight
{
	// Token: 0x0200111A RID: 4378
	[TaskCategory("Basic/Light")]
	[TaskDescription("Sets the intensity of the light.")]
	public class SetIntensity : Action
	{
		// Token: 0x0600751E RID: 29982 RVA: 0x002B3E90 File Offset: 0x002B2090
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600751F RID: 29983 RVA: 0x002B3ED0 File Offset: 0x002B20D0
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

		// Token: 0x06007520 RID: 29984 RVA: 0x002B3F03 File Offset: 0x002B2103
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.intensity = 0f;
		}

		// Token: 0x040060C8 RID: 24776
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040060C9 RID: 24777
		[Tooltip("The intensity to set")]
		public SharedFloat intensity;

		// Token: 0x040060CA RID: 24778
		private Light light;

		// Token: 0x040060CB RID: 24779
		private GameObject prevGameObject;
	}
}
