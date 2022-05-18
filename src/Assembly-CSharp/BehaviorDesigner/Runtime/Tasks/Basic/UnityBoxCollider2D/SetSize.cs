using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityBoxCollider2D
{
	// Token: 0x0200161C RID: 5660
	[TaskCategory("Basic/BoxCollider2D")]
	[TaskDescription("Sets the size of the BoxCollider2D. Returns Success.")]
	public class SetSize : Action
	{
		// Token: 0x06008405 RID: 33797 RVA: 0x002CF2A0 File Offset: 0x002CD4A0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.boxCollider2D = defaultGameObject.GetComponent<BoxCollider2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008406 RID: 33798 RVA: 0x0005B152 File Offset: 0x00059352
		public override TaskStatus OnUpdate()
		{
			if (this.boxCollider2D == null)
			{
				Debug.LogWarning("BoxCollider2D is null");
				return 1;
			}
			this.boxCollider2D.size = this.size.Value;
			return 2;
		}

		// Token: 0x06008407 RID: 33799 RVA: 0x0005B185 File Offset: 0x00059385
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.size = Vector2.zero;
		}

		// Token: 0x040070B4 RID: 28852
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040070B5 RID: 28853
		[Tooltip("The size of the BoxCollider2D")]
		public SharedVector2 size;

		// Token: 0x040070B6 RID: 28854
		private BoxCollider2D boxCollider2D;

		// Token: 0x040070B7 RID: 28855
		private GameObject prevGameObject;
	}
}
