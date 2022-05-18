using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityBoxCollider2D
{
	// Token: 0x0200161B RID: 5659
	[TaskCategory("Basic/BoxCollider2D")]
	[TaskDescription("Stores the size of the BoxCollider2D. Returns Success.")]
	public class GetSize : Action
	{
		// Token: 0x06008401 RID: 33793 RVA: 0x002CF260 File Offset: 0x002CD460
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.boxCollider2D = defaultGameObject.GetComponent<BoxCollider2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008402 RID: 33794 RVA: 0x0005B106 File Offset: 0x00059306
		public override TaskStatus OnUpdate()
		{
			if (this.boxCollider2D == null)
			{
				Debug.LogWarning("BoxCollider2D is null");
				return 1;
			}
			this.storeValue.Value = this.boxCollider2D.size;
			return 2;
		}

		// Token: 0x06008403 RID: 33795 RVA: 0x0005B139 File Offset: 0x00059339
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector2.zero;
		}

		// Token: 0x040070B0 RID: 28848
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040070B1 RID: 28849
		[Tooltip("The size of the BoxCollider2D")]
		[RequiredField]
		public SharedVector2 storeValue;

		// Token: 0x040070B2 RID: 28850
		private BoxCollider2D boxCollider2D;

		// Token: 0x040070B3 RID: 28851
		private GameObject prevGameObject;
	}
}
