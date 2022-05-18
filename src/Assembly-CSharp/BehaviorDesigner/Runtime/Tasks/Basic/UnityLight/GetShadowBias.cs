using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityLight
{
	// Token: 0x020015D2 RID: 5586
	[TaskCategory("Basic/Light")]
	[TaskDescription("Stores the shadow bias of the light.")]
	public class GetShadowBias : Action
	{
		// Token: 0x060082FC RID: 33532 RVA: 0x002CE52C File Offset: 0x002CC72C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060082FD RID: 33533 RVA: 0x00059EDF File Offset: 0x000580DF
		public override TaskStatus OnUpdate()
		{
			if (this.light == null)
			{
				Debug.LogWarning("Light is null");
				return 1;
			}
			this.storeValue = this.light.shadowBias;
			return 2;
		}

		// Token: 0x060082FE RID: 33534 RVA: 0x00059F12 File Offset: 0x00058112
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x04006FCF RID: 28623
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006FD0 RID: 28624
		[RequiredField]
		[Tooltip("The shadow bias to store")]
		public SharedFloat storeValue;

		// Token: 0x04006FD1 RID: 28625
		private Light light;

		// Token: 0x04006FD2 RID: 28626
		private GameObject prevGameObject;
	}
}
