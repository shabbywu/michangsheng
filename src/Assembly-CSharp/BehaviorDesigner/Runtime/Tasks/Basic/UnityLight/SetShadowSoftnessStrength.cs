using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityLight
{
	// Token: 0x020015DD RID: 5597
	[TaskCategory("Basic/Light")]
	[TaskDescription("Sets the shadow strength of the light.")]
	public class SetShadowSoftnessStrength : Action
	{
		// Token: 0x06008328 RID: 33576 RVA: 0x002CE7EC File Offset: 0x002CC9EC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008329 RID: 33577 RVA: 0x0005A1FC File Offset: 0x000583FC
		public override TaskStatus OnUpdate()
		{
			if (this.light == null)
			{
				Debug.LogWarning("Light is null");
				return 1;
			}
			this.light.shadowStrength = this.shadowStrength.Value;
			return 2;
		}

		// Token: 0x0600832A RID: 33578 RVA: 0x0005A22F File Offset: 0x0005842F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.shadowStrength = 0f;
		}

		// Token: 0x04006FFB RID: 28667
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006FFC RID: 28668
		[Tooltip("The shadow strength to set")]
		public SharedFloat shadowStrength;

		// Token: 0x04006FFD RID: 28669
		private Light light;

		// Token: 0x04006FFE RID: 28670
		private GameObject prevGameObject;
	}
}
