using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody2D
{
	// Token: 0x02001536 RID: 5430
	[TaskCategory("Basic/Rigidbody2D")]
	[TaskDescription("Stores the position of the Rigidbody2D. Returns Success.")]
	public class GetPosition : Action
	{
		// Token: 0x060080D6 RID: 32982 RVA: 0x002CB864 File Offset: 0x002C9A64
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060080D7 RID: 32983 RVA: 0x00057C92 File Offset: 0x00055E92
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody2D is null");
				return 1;
			}
			this.storeValue.Value = this.rigidbody2D.position;
			return 2;
		}

		// Token: 0x060080D8 RID: 32984 RVA: 0x00057CC5 File Offset: 0x00055EC5
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector2.zero;
		}

		// Token: 0x04006D8C RID: 28044
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006D8D RID: 28045
		[Tooltip("The velocity of the Rigidbody2D")]
		[RequiredField]
		public SharedVector2 storeValue;

		// Token: 0x04006D8E RID: 28046
		private Rigidbody2D rigidbody2D;

		// Token: 0x04006D8F RID: 28047
		private GameObject prevGameObject;
	}
}
