using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x02001070 RID: 4208
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Gets the Transform from the GameObject. Returns Success.")]
	public class SharedGameObjectToTransform : Action
	{
		// Token: 0x060072AE RID: 29358 RVA: 0x002AE561 File Offset: 0x002AC761
		public override TaskStatus OnUpdate()
		{
			if (this.sharedGameObject.Value == null)
			{
				return 1;
			}
			this.sharedTransform.Value = this.sharedGameObject.Value.GetComponent<Transform>();
			return 2;
		}

		// Token: 0x060072AF RID: 29359 RVA: 0x002AE594 File Offset: 0x002AC794
		public override void OnReset()
		{
			this.sharedGameObject = null;
			this.sharedTransform = null;
		}

		// Token: 0x04005E61 RID: 24161
		[Tooltip("The GameObject to get the Transform of")]
		public SharedGameObject sharedGameObject;

		// Token: 0x04005E62 RID: 24162
		[RequiredField]
		[Tooltip("The Transform to set")]
		public SharedTransform sharedTransform;
	}
}
