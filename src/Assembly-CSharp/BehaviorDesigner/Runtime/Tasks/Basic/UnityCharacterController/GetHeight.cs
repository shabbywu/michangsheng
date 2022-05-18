using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityCharacterController
{
	// Token: 0x02001605 RID: 5637
	[TaskCategory("Basic/CharacterController")]
	[TaskDescription("Stores the height of the CharacterController. Returns Success.")]
	public class GetHeight : Action
	{
		// Token: 0x060083A8 RID: 33704 RVA: 0x002CECCC File Offset: 0x002CCECC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.characterController = defaultGameObject.GetComponent<CharacterController>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060083A9 RID: 33705 RVA: 0x0005AA99 File Offset: 0x00058C99
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

		// Token: 0x060083AA RID: 33706 RVA: 0x0005AACC File Offset: 0x00058CCC
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x04007059 RID: 28761
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400705A RID: 28762
		[Tooltip("The height of the CharacterController")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x0400705B RID: 28763
		private CharacterController characterController;

		// Token: 0x0400705C RID: 28764
		private GameObject prevGameObject;
	}
}
