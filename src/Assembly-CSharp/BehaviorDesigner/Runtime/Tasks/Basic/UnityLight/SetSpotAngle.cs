using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityLight
{
	// Token: 0x020015DE RID: 5598
	[TaskCategory("Basic/Light")]
	[TaskDescription("Sets the spot angle of the light.")]
	public class SetSpotAngle : Action
	{
		// Token: 0x0600832C RID: 33580 RVA: 0x002CE82C File Offset: 0x002CCA2C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600832D RID: 33581 RVA: 0x0005A248 File Offset: 0x00058448
		public override TaskStatus OnUpdate()
		{
			if (this.light == null)
			{
				Debug.LogWarning("Light is null");
				return 1;
			}
			this.light.spotAngle = this.spotAngle.Value;
			return 2;
		}

		// Token: 0x0600832E RID: 33582 RVA: 0x0005A27B File Offset: 0x0005847B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.spotAngle = 0f;
		}

		// Token: 0x04006FFF RID: 28671
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04007000 RID: 28672
		[Tooltip("The spot angle to set")]
		public SharedFloat spotAngle;

		// Token: 0x04007001 RID: 28673
		private Light light;

		// Token: 0x04007002 RID: 28674
		private GameObject prevGameObject;
	}
}
