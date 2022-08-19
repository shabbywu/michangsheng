using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityLight
{
	// Token: 0x02001119 RID: 4377
	[TaskCategory("Basic/Light")]
	[TaskDescription("Sets the culling mask of the light.")]
	public class SetCullingMask : Action
	{
		// Token: 0x0600751A RID: 29978 RVA: 0x002B3E08 File Offset: 0x002B2008
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600751B RID: 29979 RVA: 0x002B3E48 File Offset: 0x002B2048
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

		// Token: 0x0600751C RID: 29980 RVA: 0x002B3E7B File Offset: 0x002B207B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.cullingMask = -1;
		}

		// Token: 0x040060C4 RID: 24772
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040060C5 RID: 24773
		[Tooltip("The culling mask to set")]
		public LayerMask cullingMask;

		// Token: 0x040060C6 RID: 24774
		private Light light;

		// Token: 0x040060C7 RID: 24775
		private GameObject prevGameObject;
	}
}
