using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityLight
{
	// Token: 0x0200111C RID: 4380
	[TaskCategory("Basic/Light")]
	[TaskDescription("Sets the shadow bias of the light.")]
	public class SetShadowBias : Action
	{
		// Token: 0x06007526 RID: 29990 RVA: 0x002B3FA8 File Offset: 0x002B21A8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007527 RID: 29991 RVA: 0x002B3FE8 File Offset: 0x002B21E8
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

		// Token: 0x06007528 RID: 29992 RVA: 0x002B401B File Offset: 0x002B221B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.shadowBias = 0f;
		}

		// Token: 0x040060D0 RID: 24784
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040060D1 RID: 24785
		[Tooltip("The shadow bias to set")]
		public SharedFloat shadowBias;

		// Token: 0x040060D2 RID: 24786
		private Light light;

		// Token: 0x040060D3 RID: 24787
		private GameObject prevGameObject;
	}
}
