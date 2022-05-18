using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityLight
{
	// Token: 0x020015D5 RID: 5589
	[TaskCategory("Basic/Light")]
	[TaskDescription("Sets the color of the light.")]
	public class SetColor : Action
	{
		// Token: 0x06008308 RID: 33544 RVA: 0x002CE5EC File Offset: 0x002CC7EC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008309 RID: 33545 RVA: 0x00059FC3 File Offset: 0x000581C3
		public override TaskStatus OnUpdate()
		{
			if (this.light == null)
			{
				Debug.LogWarning("Light is null");
				return 1;
			}
			this.light.color = this.color.Value;
			return 2;
		}

		// Token: 0x0600830A RID: 33546 RVA: 0x00059FF6 File Offset: 0x000581F6
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.color = Color.white;
		}

		// Token: 0x04006FDB RID: 28635
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006FDC RID: 28636
		[Tooltip("The color to set")]
		public SharedColor color;

		// Token: 0x04006FDD RID: 28637
		private Light light;

		// Token: 0x04006FDE RID: 28638
		private GameObject prevGameObject;
	}
}
