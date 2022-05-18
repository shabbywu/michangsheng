using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityLight
{
	// Token: 0x020015D3 RID: 5587
	[TaskCategory("Basic/Light")]
	[TaskDescription("Stores the color of the light.")]
	public class GetShadowStrength : Action
	{
		// Token: 0x06008300 RID: 33536 RVA: 0x002CE56C File Offset: 0x002CC76C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008301 RID: 33537 RVA: 0x00059F2B File Offset: 0x0005812B
		public override TaskStatus OnUpdate()
		{
			if (this.light == null)
			{
				Debug.LogWarning("Light is null");
				return 1;
			}
			this.storeValue = this.light.shadowStrength;
			return 2;
		}

		// Token: 0x06008302 RID: 33538 RVA: 0x00059F5E File Offset: 0x0005815E
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x04006FD3 RID: 28627
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006FD4 RID: 28628
		[RequiredField]
		[Tooltip("The color to store")]
		public SharedFloat storeValue;

		// Token: 0x04006FD5 RID: 28629
		private Light light;

		// Token: 0x04006FD6 RID: 28630
		private GameObject prevGameObject;
	}
}
