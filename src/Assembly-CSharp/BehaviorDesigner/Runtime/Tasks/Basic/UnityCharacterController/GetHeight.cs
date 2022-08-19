using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityCharacterController
{
	// Token: 0x02001146 RID: 4422
	[TaskCategory("Basic/CharacterController")]
	[TaskDescription("Stores the height of the CharacterController. Returns Success.")]
	public class GetHeight : Action
	{
		// Token: 0x060075AE RID: 30126 RVA: 0x002B4E28 File Offset: 0x002B3028
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.characterController = defaultGameObject.GetComponent<CharacterController>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060075AF RID: 30127 RVA: 0x002B4E68 File Offset: 0x002B3068
		public override TaskStatus OnUpdate()
		{
			if (this.characterController == null)
			{
				Debug.LogWarning("CharacterController is null");
				return 1;
			}
			this.storeValue.Value = this.characterController.height;
			return 2;
		}

		// Token: 0x060075B0 RID: 30128 RVA: 0x002B4E9B File Offset: 0x002B309B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x04006136 RID: 24886
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006137 RID: 24887
		[Tooltip("The height of the CharacterController")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x04006138 RID: 24888
		private CharacterController characterController;

		// Token: 0x04006139 RID: 24889
		private GameObject prevGameObject;
	}
}
