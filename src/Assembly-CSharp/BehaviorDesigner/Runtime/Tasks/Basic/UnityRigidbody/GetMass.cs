using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x02001552 RID: 5458
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Stores the mass of the Rigidbody. Returns Success.")]
	public class GetMass : Action
	{
		// Token: 0x06008146 RID: 33094 RVA: 0x002CC06C File Offset: 0x002CA26C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008147 RID: 33095 RVA: 0x0005842C File Offset: 0x0005662C
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			this.storeValue.Value = this.rigidbody.mass;
			return 2;
		}

		// Token: 0x06008148 RID: 33096 RVA: 0x0005845F File Offset: 0x0005665F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x04006E02 RID: 28162
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006E03 RID: 28163
		[Tooltip("The mass of the Rigidbody")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x04006E04 RID: 28164
		private Rigidbody rigidbody;

		// Token: 0x04006E05 RID: 28165
		private GameObject prevGameObject;
	}
}
