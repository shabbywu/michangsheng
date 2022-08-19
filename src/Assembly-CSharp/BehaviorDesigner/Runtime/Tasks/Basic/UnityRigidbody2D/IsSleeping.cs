using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody2D
{
	// Token: 0x02001080 RID: 4224
	[TaskCategory("Basic/Rigidbody2D")]
	[TaskDescription("Returns Success if the Rigidbody2D is sleeping, otherwise Failure.")]
	public class IsSleeping : Conditional
	{
		// Token: 0x060072EC RID: 29420 RVA: 0x002AED8C File Offset: 0x002ACF8C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060072ED RID: 29421 RVA: 0x002AEDCC File Offset: 0x002ACFCC
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody2D is null");
				return 1;
			}
			if (!this.rigidbody2D.IsSleeping())
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x060072EE RID: 29422 RVA: 0x002AEDF8 File Offset: 0x002ACFF8
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04005E9B RID: 24219
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005E9C RID: 24220
		private Rigidbody2D rigidbody2D;

		// Token: 0x04005E9D RID: 24221
		private GameObject prevGameObject;
	}
}
