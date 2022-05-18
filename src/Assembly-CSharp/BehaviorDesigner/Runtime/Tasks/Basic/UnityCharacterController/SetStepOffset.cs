using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityCharacterController
{
	// Token: 0x02001611 RID: 5649
	[TaskCategory("Basic/CharacterController")]
	[TaskDescription("Sets the step offset of the CharacterController. Returns Success.")]
	public class SetStepOffset : Action
	{
		// Token: 0x060083D9 RID: 33753 RVA: 0x002CEFE0 File Offset: 0x002CD1E0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.characterController = defaultGameObject.GetComponent<CharacterController>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060083DA RID: 33754 RVA: 0x0005AE15 File Offset: 0x00059015
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

		// Token: 0x060083DB RID: 33755 RVA: 0x0005AE48 File Offset: 0x00059048
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.stepOffset = 0f;
		}

		// Token: 0x04007088 RID: 28808
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04007089 RID: 28809
		[Tooltip("The step offset of the CharacterController")]
		public SharedFloat stepOffset;

		// Token: 0x0400708A RID: 28810
		private CharacterController characterController;

		// Token: 0x0400708B RID: 28811
		private GameObject prevGameObject;
	}
}
