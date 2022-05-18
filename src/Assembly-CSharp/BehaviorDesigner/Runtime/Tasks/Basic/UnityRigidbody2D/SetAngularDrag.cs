using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody2D
{
	// Token: 0x0200153D RID: 5437
	[TaskCategory("Basic/Rigidbody2D")]
	[TaskDescription("Sets the angular drag of the Rigidbody2D. Returns Success.")]
	public class SetAngularDrag : Action
	{
		// Token: 0x060080F2 RID: 33010 RVA: 0x002CBA24 File Offset: 0x002C9C24
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060080F3 RID: 33011 RVA: 0x00057E78 File Offset: 0x00056078
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody2D is null");
				return 1;
			}
			this.rigidbody2D.angularDrag = this.angularDrag.Value;
			return 2;
		}

		// Token: 0x060080F4 RID: 33012 RVA: 0x00057EAB File Offset: 0x000560AB
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.angularDrag = 0f;
		}

		// Token: 0x04006DA6 RID: 28070
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006DA7 RID: 28071
		[Tooltip("The angular drag of the Rigidbody2D")]
		public SharedFloat angularDrag;

		// Token: 0x04006DA8 RID: 28072
		private Rigidbody2D rigidbody2D;

		// Token: 0x04006DA9 RID: 28073
		private GameObject prevGameObject;
	}
}
