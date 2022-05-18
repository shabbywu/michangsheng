using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityLight
{
	// Token: 0x020015D4 RID: 5588
	[TaskCategory("Basic/Light")]
	[TaskDescription("Stores the spot angle of the light.")]
	public class GetSpotAngle : Action
	{
		// Token: 0x06008304 RID: 33540 RVA: 0x002CE5AC File Offset: 0x002CC7AC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008305 RID: 33541 RVA: 0x00059F77 File Offset: 0x00058177
		public override TaskStatus OnUpdate()
		{
			if (this.light == null)
			{
				Debug.LogWarning("Light is null");
				return 1;
			}
			this.storeValue = this.light.spotAngle;
			return 2;
		}

		// Token: 0x06008306 RID: 33542 RVA: 0x00059FAA File Offset: 0x000581AA
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x04006FD7 RID: 28631
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006FD8 RID: 28632
		[RequiredField]
		[Tooltip("The spot angle to store")]
		public SharedFloat storeValue;

		// Token: 0x04006FD9 RID: 28633
		private Light light;

		// Token: 0x04006FDA RID: 28634
		private GameObject prevGameObject;
	}
}
