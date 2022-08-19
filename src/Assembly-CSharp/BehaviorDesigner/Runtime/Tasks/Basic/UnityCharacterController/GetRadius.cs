using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityCharacterController
{
	// Token: 0x02001147 RID: 4423
	[TaskCategory("Basic/CharacterController")]
	[TaskDescription("Stores the radius of the CharacterController. Returns Success.")]
	public class GetRadius : Action
	{
		// Token: 0x060075B2 RID: 30130 RVA: 0x002B4EB4 File Offset: 0x002B30B4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.characterController = defaultGameObject.GetComponent<CharacterController>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060075B3 RID: 30131 RVA: 0x002B4EF4 File Offset: 0x002B30F4
		public override TaskStatus OnUpdate()
		{
			if (this.characterController == null)
			{
				Debug.LogWarning("CharacterController is null");
				return 1;
			}
			this.storeValue.Value = this.characterController.radius;
			return 2;
		}

		// Token: 0x060075B4 RID: 30132 RVA: 0x002B4F27 File Offset: 0x002B3127
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x0400613A RID: 24890
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400613B RID: 24891
		[Tooltip("The radius of the CharacterController")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x0400613C RID: 24892
		private CharacterController characterController;

		// Token: 0x0400613D RID: 24893
		private GameObject prevGameObject;
	}
}
