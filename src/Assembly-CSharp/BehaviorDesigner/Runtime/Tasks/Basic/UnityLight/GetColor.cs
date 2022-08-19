using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityLight
{
	// Token: 0x0200110F RID: 4367
	[TaskCategory("Basic/Light")]
	[TaskDescription("Stores the color of the light.")]
	public class GetColor : Action
	{
		// Token: 0x060074F2 RID: 29938 RVA: 0x002B389C File Offset: 0x002B1A9C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060074F3 RID: 29939 RVA: 0x002B38DC File Offset: 0x002B1ADC
		public override TaskStatus OnUpdate()
		{
			if (this.light == null)
			{
				Debug.LogWarning("Light is null");
				return 1;
			}
			this.storeValue = this.light.color;
			return 2;
		}

		// Token: 0x060074F4 RID: 29940 RVA: 0x002B390F File Offset: 0x002B1B0F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Color.white;
		}

		// Token: 0x0400609C RID: 24732
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400609D RID: 24733
		[RequiredField]
		[Tooltip("The color to store")]
		public SharedColor storeValue;

		// Token: 0x0400609E RID: 24734
		private Light light;

		// Token: 0x0400609F RID: 24735
		private GameObject prevGameObject;
	}
}
