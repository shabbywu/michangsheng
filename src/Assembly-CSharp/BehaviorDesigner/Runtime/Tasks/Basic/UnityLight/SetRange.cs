using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityLight
{
	// Token: 0x020015DA RID: 5594
	[TaskCategory("Basic/Light")]
	[TaskDescription("Sets the range of the light.")]
	public class SetRange : Action
	{
		// Token: 0x0600831C RID: 33564 RVA: 0x002CE72C File Offset: 0x002CC92C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600831D RID: 33565 RVA: 0x0005A12D File Offset: 0x0005832D
		public override TaskStatus OnUpdate()
		{
			if (this.light == null)
			{
				Debug.LogWarning("Light is null");
				return 1;
			}
			this.light.range = this.range.Value;
			return 2;
		}

		// Token: 0x0600831E RID: 33566 RVA: 0x0005A160 File Offset: 0x00058360
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.range = 0f;
		}

		// Token: 0x04006FEF RID: 28655
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006FF0 RID: 28656
		[Tooltip("The range to set")]
		public SharedFloat range;

		// Token: 0x04006FF1 RID: 28657
		private Light light;

		// Token: 0x04006FF2 RID: 28658
		private GameObject prevGameObject;
	}
}
