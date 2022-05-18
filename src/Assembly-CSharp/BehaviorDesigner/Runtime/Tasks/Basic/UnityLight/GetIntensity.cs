using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityLight
{
	// Token: 0x020015D0 RID: 5584
	[TaskCategory("Basic/Light")]
	[TaskDescription("Stores the intensity of the light.")]
	public class GetIntensity : Action
	{
		// Token: 0x060082F4 RID: 33524 RVA: 0x002CE4AC File Offset: 0x002CC6AC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060082F5 RID: 33525 RVA: 0x00059E47 File Offset: 0x00058047
		public override TaskStatus OnUpdate()
		{
			if (this.light == null)
			{
				Debug.LogWarning("Light is null");
				return 1;
			}
			this.storeValue = this.light.intensity;
			return 2;
		}

		// Token: 0x060082F6 RID: 33526 RVA: 0x00059E7A File Offset: 0x0005807A
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x04006FC7 RID: 28615
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006FC8 RID: 28616
		[RequiredField]
		[Tooltip("The intensity to store")]
		public SharedFloat storeValue;

		// Token: 0x04006FC9 RID: 28617
		private Light light;

		// Token: 0x04006FCA RID: 28618
		private GameObject prevGameObject;
	}
}
