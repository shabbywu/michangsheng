using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityCharacterController
{
	// Token: 0x02001145 RID: 4421
	[TaskCategory("Basic/CharacterController")]
	[TaskDescription("Stores the center of the CharacterController. Returns Success.")]
	public class GetCenter : Action
	{
		// Token: 0x060075AA RID: 30122 RVA: 0x002B4D9C File Offset: 0x002B2F9C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.characterController = defaultGameObject.GetComponent<CharacterController>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060075AB RID: 30123 RVA: 0x002B4DDC File Offset: 0x002B2FDC
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

		// Token: 0x060075AC RID: 30124 RVA: 0x002B4E0F File Offset: 0x002B300F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x04006132 RID: 24882
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006133 RID: 24883
		[Tooltip("The center of the CharacterController")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x04006134 RID: 24884
		private CharacterController characterController;

		// Token: 0x04006135 RID: 24885
		private GameObject prevGameObject;
	}
}
