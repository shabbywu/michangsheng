using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x0200152A RID: 5418
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Gets the Transform from the GameObject. Returns Success.")]
	public class SharedGameObjectToTransform : Action
	{
		// Token: 0x060080A8 RID: 32936 RVA: 0x00057927 File Offset: 0x00055B27
		public override TaskStatus OnUpdate()
		{
			if (this.sharedGameObject.Value == null)
			{
				return 1;
			}
			this.sharedTransform.Value = this.sharedGameObject.Value.GetComponent<Transform>();
			return 2;
		}

		// Token: 0x060080A9 RID: 32937 RVA: 0x0005795A File Offset: 0x00055B5A
		public override void OnReset()
		{
			this.sharedGameObject = null;
			this.sharedTransform = null;
		}

		// Token: 0x04006D61 RID: 28001
		[Tooltip("The GameObject to get the Transform of")]
		public SharedGameObject sharedGameObject;

		// Token: 0x04006D62 RID: 28002
		[RequiredField]
		[Tooltip("The Transform to set")]
		public SharedTransform sharedTransform;
	}
}
