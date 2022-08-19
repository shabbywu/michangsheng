using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityLight
{
	// Token: 0x02001112 RID: 4370
	[TaskCategory("Basic/Light")]
	[TaskDescription("Stores the range of the light.")]
	public class GetRange : Action
	{
		// Token: 0x060074FE RID: 29950 RVA: 0x002B3A40 File Offset: 0x002B1C40
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060074FF RID: 29951 RVA: 0x002B3A80 File Offset: 0x002B1C80
		public override TaskStatus OnUpdate()
		{
			if (this.light == null)
			{
				Debug.LogWarning("Light is null");
				return 1;
			}
			this.storeValue = this.light.range;
			return 2;
		}

		// Token: 0x06007500 RID: 29952 RVA: 0x002B3AB3 File Offset: 0x002B1CB3
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x040060A8 RID: 24744
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040060A9 RID: 24745
		[RequiredField]
		[Tooltip("The range to store")]
		public SharedFloat storeValue;

		// Token: 0x040060AA RID: 24746
		private Light light;

		// Token: 0x040060AB RID: 24747
		private GameObject prevGameObject;
	}
}
