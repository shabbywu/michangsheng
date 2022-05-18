using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityLight
{
	// Token: 0x020015CE RID: 5582
	[TaskCategory("Basic/Light")]
	[TaskDescription("Stores the color of the light.")]
	public class GetColor : Action
	{
		// Token: 0x060082EC RID: 33516 RVA: 0x002CE42C File Offset: 0x002CC62C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060082ED RID: 33517 RVA: 0x00059DAF File Offset: 0x00057FAF
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

		// Token: 0x060082EE RID: 33518 RVA: 0x00059DE2 File Offset: 0x00057FE2
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Color.white;
		}

		// Token: 0x04006FBF RID: 28607
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006FC0 RID: 28608
		[RequiredField]
		[Tooltip("The color to store")]
		public SharedColor storeValue;

		// Token: 0x04006FC1 RID: 28609
		private Light light;

		// Token: 0x04006FC2 RID: 28610
		private GameObject prevGameObject;
	}
}
