using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityLight
{
	// Token: 0x02001118 RID: 4376
	[TaskCategory("Basic/Light")]
	[TaskDescription("Sets the light's cookie size.")]
	public class SetCookieSize : Action
	{
		// Token: 0x06007516 RID: 29974 RVA: 0x002B3D7C File Offset: 0x002B1F7C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007517 RID: 29975 RVA: 0x002B3DBC File Offset: 0x002B1FBC
		public override TaskStatus OnUpdate()
		{
			if (this.light == null)
			{
				Debug.LogWarning("Light is null");
				return 1;
			}
			this.light.cookieSize = this.cookieSize.Value;
			return 2;
		}

		// Token: 0x06007518 RID: 29976 RVA: 0x002B3DEF File Offset: 0x002B1FEF
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.cookieSize = 0f;
		}

		// Token: 0x040060C0 RID: 24768
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040060C1 RID: 24769
		[Tooltip("The size to set")]
		public SharedFloat cookieSize;

		// Token: 0x040060C2 RID: 24770
		private Light light;

		// Token: 0x040060C3 RID: 24771
		private GameObject prevGameObject;
	}
}
