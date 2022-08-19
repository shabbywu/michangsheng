using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityLight
{
	// Token: 0x0200111F RID: 4383
	[TaskCategory("Basic/Light")]
	[TaskDescription("Sets the spot angle of the light.")]
	public class SetSpotAngle : Action
	{
		// Token: 0x06007532 RID: 30002 RVA: 0x002B4138 File Offset: 0x002B2338
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007533 RID: 30003 RVA: 0x002B4178 File Offset: 0x002B2378
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

		// Token: 0x06007534 RID: 30004 RVA: 0x002B41AB File Offset: 0x002B23AB
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.spotAngle = 0f;
		}

		// Token: 0x040060DC RID: 24796
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040060DD RID: 24797
		[Tooltip("The spot angle to set")]
		public SharedFloat spotAngle;

		// Token: 0x040060DE RID: 24798
		private Light light;

		// Token: 0x040060DF RID: 24799
		private GameObject prevGameObject;
	}
}
