using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityLight
{
	// Token: 0x020015CF RID: 5583
	[TaskCategory("Basic/Light")]
	[TaskDescription("Stores the light's cookie size.")]
	public class GetCookieSize : Action
	{
		// Token: 0x060082F0 RID: 33520 RVA: 0x002CE46C File Offset: 0x002CC66C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060082F1 RID: 33521 RVA: 0x00059DFB File Offset: 0x00057FFB
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

		// Token: 0x060082F2 RID: 33522 RVA: 0x00059E2E File Offset: 0x0005802E
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x04006FC3 RID: 28611
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006FC4 RID: 28612
		[RequiredField]
		[Tooltip("The size to store")]
		public SharedFloat storeValue;

		// Token: 0x04006FC5 RID: 28613
		private Light light;

		// Token: 0x04006FC6 RID: 28614
		private GameObject prevGameObject;
	}
}
