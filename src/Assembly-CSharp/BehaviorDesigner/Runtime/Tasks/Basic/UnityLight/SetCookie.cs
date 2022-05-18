using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityLight
{
	// Token: 0x020015D6 RID: 5590
	[TaskCategory("Basic/Light")]
	[TaskDescription("Sets the cookie of the light.")]
	public class SetCookie : Action
	{
		// Token: 0x0600830C RID: 33548 RVA: 0x002CE62C File Offset: 0x002CC82C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600830D RID: 33549 RVA: 0x0005A00F File Offset: 0x0005820F
		public override TaskStatus OnUpdate()
		{
			if (this.light == null)
			{
				Debug.LogWarning("Light is null");
				return 1;
			}
			this.light.cookie = this.cookie;
			return 2;
		}

		// Token: 0x0600830E RID: 33550 RVA: 0x0005A03D File Offset: 0x0005823D
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.cookie = null;
		}

		// Token: 0x04006FDF RID: 28639
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006FE0 RID: 28640
		[Tooltip("The cookie to set")]
		public Texture2D cookie;

		// Token: 0x04006FE1 RID: 28641
		private Light light;

		// Token: 0x04006FE2 RID: 28642
		private GameObject prevGameObject;
	}
}
