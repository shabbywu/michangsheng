using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityLight
{
	// Token: 0x02001114 RID: 4372
	[TaskCategory("Basic/Light")]
	[TaskDescription("Stores the color of the light.")]
	public class GetShadowStrength : Action
	{
		// Token: 0x06007506 RID: 29958 RVA: 0x002B3B58 File Offset: 0x002B1D58
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007507 RID: 29959 RVA: 0x002B3B98 File Offset: 0x002B1D98
		public override TaskStatus OnUpdate()
		{
			if (this.light == null)
			{
				Debug.LogWarning("Light is null");
				return 1;
			}
			this.storeValue = this.light.shadowStrength;
			return 2;
		}

		// Token: 0x06007508 RID: 29960 RVA: 0x002B3BCB File Offset: 0x002B1DCB
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x040060B0 RID: 24752
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040060B1 RID: 24753
		[RequiredField]
		[Tooltip("The color to store")]
		public SharedFloat storeValue;

		// Token: 0x040060B2 RID: 24754
		private Light light;

		// Token: 0x040060B3 RID: 24755
		private GameObject prevGameObject;
	}
}
