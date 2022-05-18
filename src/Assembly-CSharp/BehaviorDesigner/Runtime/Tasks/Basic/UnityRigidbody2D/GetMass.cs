using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody2D
{
	// Token: 0x02001535 RID: 5429
	[TaskCategory("Basic/Rigidbody2D")]
	[TaskDescription("Stores the mass of the Rigidbody2D. Returns Success.")]
	public class GetMass : Action
	{
		// Token: 0x060080D2 RID: 32978 RVA: 0x002CB824 File Offset: 0x002C9A24
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060080D3 RID: 32979 RVA: 0x00057C46 File Offset: 0x00055E46
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody2D is null");
				return 1;
			}
			this.storeValue.Value = this.rigidbody2D.mass;
			return 2;
		}

		// Token: 0x060080D4 RID: 32980 RVA: 0x00057C79 File Offset: 0x00055E79
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x04006D88 RID: 28040
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006D89 RID: 28041
		[Tooltip("The mass of the Rigidbody2D")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x04006D8A RID: 28042
		private Rigidbody2D rigidbody2D;

		// Token: 0x04006D8B RID: 28043
		private GameObject prevGameObject;
	}
}
