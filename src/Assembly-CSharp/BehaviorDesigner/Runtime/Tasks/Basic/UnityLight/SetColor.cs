using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityLight
{
	// Token: 0x02001116 RID: 4374
	[TaskCategory("Basic/Light")]
	[TaskDescription("Sets the color of the light.")]
	public class SetColor : Action
	{
		// Token: 0x0600750E RID: 29966 RVA: 0x002B3C70 File Offset: 0x002B1E70
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600750F RID: 29967 RVA: 0x002B3CB0 File Offset: 0x002B1EB0
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

		// Token: 0x06007510 RID: 29968 RVA: 0x002B3CE3 File Offset: 0x002B1EE3
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.color = Color.white;
		}

		// Token: 0x040060B8 RID: 24760
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040060B9 RID: 24761
		[Tooltip("The color to set")]
		public SharedColor color;

		// Token: 0x040060BA RID: 24762
		private Light light;

		// Token: 0x040060BB RID: 24763
		private GameObject prevGameObject;
	}
}
