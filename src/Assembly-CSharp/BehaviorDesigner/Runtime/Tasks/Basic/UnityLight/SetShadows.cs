using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityLight
{
	// Token: 0x020015DC RID: 5596
	[TaskCategory("Basic/Light")]
	[TaskDescription("Sets the shadow type of the light.")]
	public class SetShadows : Action
	{
		// Token: 0x06008324 RID: 33572 RVA: 0x002CE7AC File Offset: 0x002CC9AC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008325 RID: 33573 RVA: 0x0005A1C5 File Offset: 0x000583C5
		public override TaskStatus OnUpdate()
		{
			if (this.light == null)
			{
				Debug.LogWarning("Light is null");
				return 1;
			}
			this.light.shadows = this.shadows;
			return 2;
		}

		// Token: 0x06008326 RID: 33574 RVA: 0x0005A1F3 File Offset: 0x000583F3
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04006FF7 RID: 28663
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006FF8 RID: 28664
		[Tooltip("The shadow type to set")]
		public LightShadows shadows;

		// Token: 0x04006FF9 RID: 28665
		private Light light;

		// Token: 0x04006FFA RID: 28666
		private GameObject prevGameObject;
	}
}
