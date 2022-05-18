using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityCharacterController
{
	// Token: 0x02001608 RID: 5640
	[TaskCategory("Basic/CharacterController")]
	[TaskDescription("Stores the step offset of the CharacterController. Returns Success.")]
	public class GetStepOffset : Action
	{
		// Token: 0x060083B4 RID: 33716 RVA: 0x002CED8C File Offset: 0x002CCF8C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.characterController = defaultGameObject.GetComponent<CharacterController>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060083B5 RID: 33717 RVA: 0x0005AB7D File Offset: 0x00058D7D
		public override TaskStatus OnUpdate()
		{
			if (this.characterController == null)
			{
				Debug.LogWarning("CharacterController is null");
				return 1;
			}
			this.storeValue.Value = this.characterController.stepOffset;
			return 2;
		}

		// Token: 0x060083B6 RID: 33718 RVA: 0x0005ABB0 File Offset: 0x00058DB0
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x04007065 RID: 28773
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04007066 RID: 28774
		[Tooltip("The step offset of the CharacterController")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x04007067 RID: 28775
		private CharacterController characterController;

		// Token: 0x04007068 RID: 28776
		private GameObject prevGameObject;
	}
}
