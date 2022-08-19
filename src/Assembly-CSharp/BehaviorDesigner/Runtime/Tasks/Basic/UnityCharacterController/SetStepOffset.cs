using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityCharacterController
{
	// Token: 0x02001152 RID: 4434
	[TaskCategory("Basic/CharacterController")]
	[TaskDescription("Sets the step offset of the CharacterController. Returns Success.")]
	public class SetStepOffset : Action
	{
		// Token: 0x060075DF RID: 30175 RVA: 0x002B54C0 File Offset: 0x002B36C0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.characterController = defaultGameObject.GetComponent<CharacterController>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060075E0 RID: 30176 RVA: 0x002B5500 File Offset: 0x002B3700
		public override TaskStatus OnUpdate()
		{
			if (this.characterController == null)
			{
				Debug.LogWarning("CharacterController is null");
				return 1;
			}
			this.characterController.stepOffset = this.stepOffset.Value;
			return 2;
		}

		// Token: 0x060075E1 RID: 30177 RVA: 0x002B5533 File Offset: 0x002B3733
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.stepOffset = 0f;
		}

		// Token: 0x04006165 RID: 24933
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006166 RID: 24934
		[Tooltip("The step offset of the CharacterController")]
		public SharedFloat stepOffset;

		// Token: 0x04006167 RID: 24935
		private CharacterController characterController;

		// Token: 0x04006168 RID: 24936
		private GameObject prevGameObject;
	}
}
