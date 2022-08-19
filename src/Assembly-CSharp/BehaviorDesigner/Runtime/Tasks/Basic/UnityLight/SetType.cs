using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityLight
{
	// Token: 0x02001120 RID: 4384
	[TaskCategory("Basic/Light")]
	[TaskDescription("Sets the type of the light.")]
	public class SetType : Action
	{
		// Token: 0x06007536 RID: 30006 RVA: 0x002B41C4 File Offset: 0x002B23C4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007537 RID: 30007 RVA: 0x002B4204 File Offset: 0x002B2404
		public override TaskStatus OnUpdate()
		{
			if (this.light == null)
			{
				Debug.LogWarning("Light is null");
				return 1;
			}
			this.light.type = this.type;
			return 2;
		}

		// Token: 0x06007538 RID: 30008 RVA: 0x002B4232 File Offset: 0x002B2432
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x040060E0 RID: 24800
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040060E1 RID: 24801
		[Tooltip("The type to set")]
		public LightType type;

		// Token: 0x040060E2 RID: 24802
		private Light light;

		// Token: 0x040060E3 RID: 24803
		private GameObject prevGameObject;
	}
}
