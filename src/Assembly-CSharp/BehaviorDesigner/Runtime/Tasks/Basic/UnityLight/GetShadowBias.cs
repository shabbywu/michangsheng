using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityLight
{
	// Token: 0x02001113 RID: 4371
	[TaskCategory("Basic/Light")]
	[TaskDescription("Stores the shadow bias of the light.")]
	public class GetShadowBias : Action
	{
		// Token: 0x06007502 RID: 29954 RVA: 0x002B3ACC File Offset: 0x002B1CCC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007503 RID: 29955 RVA: 0x002B3B0C File Offset: 0x002B1D0C
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

		// Token: 0x06007504 RID: 29956 RVA: 0x002B3B3F File Offset: 0x002B1D3F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x040060AC RID: 24748
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040060AD RID: 24749
		[RequiredField]
		[Tooltip("The shadow bias to store")]
		public SharedFloat storeValue;

		// Token: 0x040060AE RID: 24750
		private Light light;

		// Token: 0x040060AF RID: 24751
		private GameObject prevGameObject;
	}
}
