using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityLight
{
	// Token: 0x020015D8 RID: 5592
	[TaskCategory("Basic/Light")]
	[TaskDescription("Sets the culling mask of the light.")]
	public class SetCullingMask : Action
	{
		// Token: 0x06008314 RID: 33556 RVA: 0x002CE6AC File Offset: 0x002CC8AC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008315 RID: 33557 RVA: 0x0005A099 File Offset: 0x00058299
		public override TaskStatus OnUpdate()
		{
			if (this.light == null)
			{
				Debug.LogWarning("Light is null");
				return 1;
			}
			this.light.cullingMask = this.cullingMask.value;
			return 2;
		}

		// Token: 0x06008316 RID: 33558 RVA: 0x0005A0CC File Offset: 0x000582CC
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.cullingMask = -1;
		}

		// Token: 0x04006FE7 RID: 28647
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006FE8 RID: 28648
		[Tooltip("The culling mask to set")]
		public LayerMask cullingMask;

		// Token: 0x04006FE9 RID: 28649
		private Light light;

		// Token: 0x04006FEA RID: 28650
		private GameObject prevGameObject;
	}
}
