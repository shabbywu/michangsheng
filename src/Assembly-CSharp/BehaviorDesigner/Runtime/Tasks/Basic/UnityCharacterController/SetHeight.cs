using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityCharacterController
{
	// Token: 0x0200160E RID: 5646
	[TaskCategory("Basic/CharacterController")]
	[TaskDescription("Sets the height of the CharacterController. Returns Success.")]
	public class SetHeight : Action
	{
		// Token: 0x060083CD RID: 33741 RVA: 0x002CEF20 File Offset: 0x002CD120
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.characterController = defaultGameObject.GetComponent<CharacterController>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060083CE RID: 33742 RVA: 0x0005AD31 File Offset: 0x00058F31
		public override TaskStatus OnUpdate()
		{
			if (this.characterController == null)
			{
				Debug.LogWarning("CharacterController is null");
				return 1;
			}
			this.characterController.height = this.height.Value;
			return 2;
		}

		// Token: 0x060083CF RID: 33743 RVA: 0x0005AD64 File Offset: 0x00058F64
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.height = 0f;
		}

		// Token: 0x0400707C RID: 28796
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400707D RID: 28797
		[Tooltip("The height of the CharacterController")]
		public SharedFloat height;

		// Token: 0x0400707E RID: 28798
		private CharacterController characterController;

		// Token: 0x0400707F RID: 28799
		private GameObject prevGameObject;
	}
}
