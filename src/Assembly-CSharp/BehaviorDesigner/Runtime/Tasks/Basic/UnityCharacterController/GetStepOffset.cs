using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityCharacterController
{
	// Token: 0x02001149 RID: 4425
	[TaskCategory("Basic/CharacterController")]
	[TaskDescription("Stores the step offset of the CharacterController. Returns Success.")]
	public class GetStepOffset : Action
	{
		// Token: 0x060075BA RID: 30138 RVA: 0x002B4FCC File Offset: 0x002B31CC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.characterController = defaultGameObject.GetComponent<CharacterController>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060075BB RID: 30139 RVA: 0x002B500C File Offset: 0x002B320C
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

		// Token: 0x060075BC RID: 30140 RVA: 0x002B503F File Offset: 0x002B323F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x04006142 RID: 24898
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006143 RID: 24899
		[Tooltip("The step offset of the CharacterController")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x04006144 RID: 24900
		private CharacterController characterController;

		// Token: 0x04006145 RID: 24901
		private GameObject prevGameObject;
	}
}
