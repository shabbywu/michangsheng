using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityLight
{
	// Token: 0x02001115 RID: 4373
	[TaskCategory("Basic/Light")]
	[TaskDescription("Stores the spot angle of the light.")]
	public class GetSpotAngle : Action
	{
		// Token: 0x0600750A RID: 29962 RVA: 0x002B3BE4 File Offset: 0x002B1DE4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600750B RID: 29963 RVA: 0x002B3C24 File Offset: 0x002B1E24
		public override TaskStatus OnUpdate()
		{
			if (this.light == null)
			{
				Debug.LogWarning("Light is null");
				return 1;
			}
			this.storeValue = this.light.spotAngle;
			return 2;
		}

		// Token: 0x0600750C RID: 29964 RVA: 0x002B3C57 File Offset: 0x002B1E57
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x040060B4 RID: 24756
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040060B5 RID: 24757
		[RequiredField]
		[Tooltip("The spot angle to store")]
		public SharedFloat storeValue;

		// Token: 0x040060B6 RID: 24758
		private Light light;

		// Token: 0x040060B7 RID: 24759
		private GameObject prevGameObject;
	}
}
