using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody2D
{
	// Token: 0x0200107F RID: 4223
	[TaskCategory("Basic/Rigidbody2D")]
	[TaskDescription("Returns Success if the Rigidbody2D is kinematic, otherwise Failure.")]
	public class IsKinematic : Conditional
	{
		// Token: 0x060072E8 RID: 29416 RVA: 0x002AED14 File Offset: 0x002ACF14
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060072E9 RID: 29417 RVA: 0x002AED54 File Offset: 0x002ACF54
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody2D is null");
				return 1;
			}
			if (!this.rigidbody2D.isKinematic)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x060072EA RID: 29418 RVA: 0x002AED80 File Offset: 0x002ACF80
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04005E98 RID: 24216
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005E99 RID: 24217
		private Rigidbody2D rigidbody2D;

		// Token: 0x04005E9A RID: 24218
		private GameObject prevGameObject;
	}
}
