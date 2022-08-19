using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityLight
{
	// Token: 0x02001110 RID: 4368
	[TaskCategory("Basic/Light")]
	[TaskDescription("Stores the light's cookie size.")]
	public class GetCookieSize : Action
	{
		// Token: 0x060074F6 RID: 29942 RVA: 0x002B3928 File Offset: 0x002B1B28
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060074F7 RID: 29943 RVA: 0x002B3968 File Offset: 0x002B1B68
		public override TaskStatus OnUpdate()
		{
			if (this.light == null)
			{
				Debug.LogWarning("Light is null");
				return 1;
			}
			this.storeValue = this.light.cookieSize;
			return 2;
		}

		// Token: 0x060074F8 RID: 29944 RVA: 0x002B399B File Offset: 0x002B1B9B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x040060A0 RID: 24736
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040060A1 RID: 24737
		[RequiredField]
		[Tooltip("The size to store")]
		public SharedFloat storeValue;

		// Token: 0x040060A2 RID: 24738
		private Light light;

		// Token: 0x040060A3 RID: 24739
		private GameObject prevGameObject;
	}
}
