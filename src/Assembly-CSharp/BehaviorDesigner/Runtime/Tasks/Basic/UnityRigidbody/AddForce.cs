using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x02001547 RID: 5447
	[RequiredComponent(typeof(Rigidbody))]
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Applies a force to the rigidbody. Returns Success.")]
	public class AddForce : Action
	{
		// Token: 0x0600811A RID: 33050 RVA: 0x002CBD5C File Offset: 0x002C9F5C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600811B RID: 33051 RVA: 0x000580EA File Offset: 0x000562EA
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			this.rigidbody.AddForce(this.force.Value, this.forceMode);
			return 2;
		}

		// Token: 0x0600811C RID: 33052 RVA: 0x00058123 File Offset: 0x00056323
		public override void OnReset()
		{
			this.targetGameObject = null;
			if (this.force != null)
			{
				this.force.Value = Vector3.zero;
			}
			this.forceMode = 0;
		}

		// Token: 0x04006DD0 RID: 28112
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006DD1 RID: 28113
		[Tooltip("The amount of force to apply")]
		public SharedVector3 force;

		// Token: 0x04006DD2 RID: 28114
		[Tooltip("The type of force")]
		public ForceMode forceMode;

		// Token: 0x04006DD3 RID: 28115
		private Rigidbody rigidbody;

		// Token: 0x04006DD4 RID: 28116
		private GameObject prevGameObject;
	}
}
