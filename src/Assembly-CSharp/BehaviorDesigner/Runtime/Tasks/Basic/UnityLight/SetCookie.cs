using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityLight
{
	// Token: 0x02001117 RID: 4375
	[TaskCategory("Basic/Light")]
	[TaskDescription("Sets the cookie of the light.")]
	public class SetCookie : Action
	{
		// Token: 0x06007512 RID: 29970 RVA: 0x002B3CFC File Offset: 0x002B1EFC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007513 RID: 29971 RVA: 0x002B3D3C File Offset: 0x002B1F3C
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

		// Token: 0x06007514 RID: 29972 RVA: 0x002B3D6A File Offset: 0x002B1F6A
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.cookie = null;
		}

		// Token: 0x040060BC RID: 24764
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040060BD RID: 24765
		[Tooltip("The cookie to set")]
		public Texture2D cookie;

		// Token: 0x040060BE RID: 24766
		private Light light;

		// Token: 0x040060BF RID: 24767
		private GameObject prevGameObject;
	}
}
