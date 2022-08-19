using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityBoxCollider2D
{
	// Token: 0x0200115C RID: 4444
	[TaskCategory("Basic/BoxCollider2D")]
	[TaskDescription("Stores the size of the BoxCollider2D. Returns Success.")]
	public class GetSize : Action
	{
		// Token: 0x06007607 RID: 30215 RVA: 0x002B5A34 File Offset: 0x002B3C34
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.boxCollider2D = defaultGameObject.GetComponent<BoxCollider2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007608 RID: 30216 RVA: 0x002B5A74 File Offset: 0x002B3C74
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

		// Token: 0x06007609 RID: 30217 RVA: 0x002B5AA7 File Offset: 0x002B3CA7
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector2.zero;
		}

		// Token: 0x0400618D RID: 24973
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400618E RID: 24974
		[Tooltip("The size of the BoxCollider2D")]
		[RequiredField]
		public SharedVector2 storeValue;

		// Token: 0x0400618F RID: 24975
		private BoxCollider2D boxCollider2D;

		// Token: 0x04006190 RID: 24976
		private GameObject prevGameObject;
	}
}
