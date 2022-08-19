using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityLight
{
	// Token: 0x02001111 RID: 4369
	[TaskCategory("Basic/Light")]
	[TaskDescription("Stores the intensity of the light.")]
	public class GetIntensity : Action
	{
		// Token: 0x060074FA RID: 29946 RVA: 0x002B39B4 File Offset: 0x002B1BB4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060074FB RID: 29947 RVA: 0x002B39F4 File Offset: 0x002B1BF4
		public override TaskStatus OnUpdate()
		{
			if (this.light == null)
			{
				Debug.LogWarning("Light is null");
				return 1;
			}
			this.storeValue = this.light.intensity;
			return 2;
		}

		// Token: 0x060074FC RID: 29948 RVA: 0x002B3A27 File Offset: 0x002B1C27
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x040060A4 RID: 24740
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040060A5 RID: 24741
		[RequiredField]
		[Tooltip("The intensity to store")]
		public SharedFloat storeValue;

		// Token: 0x040060A6 RID: 24742
		private Light light;

		// Token: 0x040060A7 RID: 24743
		private GameObject prevGameObject;
	}
}
