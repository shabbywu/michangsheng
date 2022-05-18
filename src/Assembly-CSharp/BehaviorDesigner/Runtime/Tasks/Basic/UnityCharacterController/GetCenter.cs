using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityCharacterController
{
	// Token: 0x02001604 RID: 5636
	[TaskCategory("Basic/CharacterController")]
	[TaskDescription("Stores the center of the CharacterController. Returns Success.")]
	public class GetCenter : Action
	{
		// Token: 0x060083A4 RID: 33700 RVA: 0x002CEC8C File Offset: 0x002CCE8C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.characterController = defaultGameObject.GetComponent<CharacterController>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060083A5 RID: 33701 RVA: 0x0005AA4D File Offset: 0x00058C4D
		public override TaskStatus OnUpdate()
		{
			if (this.characterController == null)
			{
				Debug.LogWarning("CharacterController is null");
				return 1;
			}
			this.storeValue.Value = this.characterController.center;
			return 2;
		}

		// Token: 0x060083A6 RID: 33702 RVA: 0x0005AA80 File Offset: 0x00058C80
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x04007055 RID: 28757
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04007056 RID: 28758
		[Tooltip("The center of the CharacterController")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x04007057 RID: 28759
		private CharacterController characterController;

		// Token: 0x04007058 RID: 28760
		private GameObject prevGameObject;
	}
}
