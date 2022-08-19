using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityLight
{
	// Token: 0x0200111E RID: 4382
	[TaskCategory("Basic/Light")]
	[TaskDescription("Sets the shadow strength of the light.")]
	public class SetShadowSoftnessStrength : Action
	{
		// Token: 0x0600752E RID: 29998 RVA: 0x002B40AC File Offset: 0x002B22AC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600752F RID: 29999 RVA: 0x002B40EC File Offset: 0x002B22EC
		public override TaskStatus OnUpdate()
		{
			if (this.light == null)
			{
				Debug.LogWarning("Light is null");
				return 1;
			}
			this.light.shadowStrength = this.shadowStrength.Value;
			return 2;
		}

		// Token: 0x06007530 RID: 30000 RVA: 0x002B411F File Offset: 0x002B231F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.shadowStrength = 0f;
		}

		// Token: 0x040060D8 RID: 24792
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040060D9 RID: 24793
		[Tooltip("The shadow strength to set")]
		public SharedFloat shadowStrength;

		// Token: 0x040060DA RID: 24794
		private Light light;

		// Token: 0x040060DB RID: 24795
		private GameObject prevGameObject;
	}
}
