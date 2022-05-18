using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody2D
{
	// Token: 0x02001542 RID: 5442
	[TaskCategory("Basic/Rigidbody2D")]
	[TaskDescription("Sets the mass of the Rigidbody2D. Returns Success.")]
	public class SetMass : Action
	{
		// Token: 0x06008106 RID: 33030 RVA: 0x002CBB64 File Offset: 0x002C9D64
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008107 RID: 33031 RVA: 0x00057FF0 File Offset: 0x000561F0
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody2D is null");
				return 1;
			}
			this.rigidbody2D.mass = this.mass.Value;
			return 2;
		}

		// Token: 0x06008108 RID: 33032 RVA: 0x00058023 File Offset: 0x00056223
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.mass = 0f;
		}

		// Token: 0x04006DBA RID: 28090
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006DBB RID: 28091
		[Tooltip("The mass of the Rigidbody2D")]
		public SharedFloat mass;

		// Token: 0x04006DBC RID: 28092
		private Rigidbody2D rigidbody2D;

		// Token: 0x04006DBD RID: 28093
		private GameObject prevGameObject;
	}
}
