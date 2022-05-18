using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityLight
{
	// Token: 0x020015DB RID: 5595
	[TaskCategory("Basic/Light")]
	[TaskDescription("Sets the shadow bias of the light.")]
	public class SetShadowBias : Action
	{
		// Token: 0x06008320 RID: 33568 RVA: 0x002CE76C File Offset: 0x002CC96C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008321 RID: 33569 RVA: 0x0005A179 File Offset: 0x00058379
		public override TaskStatus OnUpdate()
		{
			if (this.light == null)
			{
				Debug.LogWarning("Light is null");
				return 1;
			}
			this.light.shadowBias = this.shadowBias.Value;
			return 2;
		}

		// Token: 0x06008322 RID: 33570 RVA: 0x0005A1AC File Offset: 0x000583AC
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.shadowBias = 0f;
		}

		// Token: 0x04006FF3 RID: 28659
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006FF4 RID: 28660
		[Tooltip("The shadow bias to set")]
		public SharedFloat shadowBias;

		// Token: 0x04006FF5 RID: 28661
		private Light light;

		// Token: 0x04006FF6 RID: 28662
		private GameObject prevGameObject;
	}
}
