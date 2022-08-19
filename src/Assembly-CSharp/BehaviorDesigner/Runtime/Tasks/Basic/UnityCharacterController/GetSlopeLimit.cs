using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityCharacterController
{
	// Token: 0x02001148 RID: 4424
	[TaskCategory("Basic/CharacterController")]
	[TaskDescription("Stores the slope limit of the CharacterController. Returns Success.")]
	public class GetSlopeLimit : Action
	{
		// Token: 0x060075B6 RID: 30134 RVA: 0x002B4F40 File Offset: 0x002B3140
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.characterController = defaultGameObject.GetComponent<CharacterController>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060075B7 RID: 30135 RVA: 0x002B4F80 File Offset: 0x002B3180
		public override TaskStatus OnUpdate()
		{
			if (this.characterController == null)
			{
				Debug.LogWarning("CharacterController is null");
				return 1;
			}
			this.storeValue.Value = this.characterController.slopeLimit;
			return 2;
		}

		// Token: 0x060075B8 RID: 30136 RVA: 0x002B4FB3 File Offset: 0x002B31B3
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x0400613E RID: 24894
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400613F RID: 24895
		[Tooltip("The slope limit of the CharacterController")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x04006140 RID: 24896
		private CharacterController characterController;

		// Token: 0x04006141 RID: 24897
		private GameObject prevGameObject;
	}
}
