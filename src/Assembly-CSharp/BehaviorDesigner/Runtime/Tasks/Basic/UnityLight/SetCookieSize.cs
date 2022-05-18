using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityLight
{
	// Token: 0x020015D7 RID: 5591
	[TaskCategory("Basic/Light")]
	[TaskDescription("Sets the light's cookie size.")]
	public class SetCookieSize : Action
	{
		// Token: 0x06008310 RID: 33552 RVA: 0x002CE66C File Offset: 0x002CC86C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008311 RID: 33553 RVA: 0x0005A04D File Offset: 0x0005824D
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

		// Token: 0x06008312 RID: 33554 RVA: 0x0005A080 File Offset: 0x00058280
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.cookieSize = 0f;
		}

		// Token: 0x04006FE3 RID: 28643
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006FE4 RID: 28644
		[Tooltip("The size to set")]
		public SharedFloat cookieSize;

		// Token: 0x04006FE5 RID: 28645
		private Light light;

		// Token: 0x04006FE6 RID: 28646
		private GameObject prevGameObject;
	}
}
