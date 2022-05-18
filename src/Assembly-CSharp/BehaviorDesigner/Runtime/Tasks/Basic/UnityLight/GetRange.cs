using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityLight
{
	// Token: 0x020015D1 RID: 5585
	[TaskCategory("Basic/Light")]
	[TaskDescription("Stores the range of the light.")]
	public class GetRange : Action
	{
		// Token: 0x060082F8 RID: 33528 RVA: 0x002CE4EC File Offset: 0x002CC6EC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060082F9 RID: 33529 RVA: 0x00059E93 File Offset: 0x00058093
		public override TaskStatus OnUpdate()
		{
			if (this.light == null)
			{
				Debug.LogWarning("Light is null");
				return 1;
			}
			this.storeValue = this.light.range;
			return 2;
		}

		// Token: 0x060082FA RID: 33530 RVA: 0x00059EC6 File Offset: 0x000580C6
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x04006FCB RID: 28619
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006FCC RID: 28620
		[RequiredField]
		[Tooltip("The range to store")]
		public SharedFloat storeValue;

		// Token: 0x04006FCD RID: 28621
		private Light light;

		// Token: 0x04006FCE RID: 28622
		private GameObject prevGameObject;
	}
}
