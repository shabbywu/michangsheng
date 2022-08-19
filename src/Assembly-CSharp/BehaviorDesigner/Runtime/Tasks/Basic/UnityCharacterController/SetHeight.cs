using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityCharacterController
{
	// Token: 0x0200114F RID: 4431
	[TaskCategory("Basic/CharacterController")]
	[TaskDescription("Sets the height of the CharacterController. Returns Success.")]
	public class SetHeight : Action
	{
		// Token: 0x060075D3 RID: 30163 RVA: 0x002B531C File Offset: 0x002B351C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.characterController = defaultGameObject.GetComponent<CharacterController>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060075D4 RID: 30164 RVA: 0x002B535C File Offset: 0x002B355C
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

		// Token: 0x060075D5 RID: 30165 RVA: 0x002B538F File Offset: 0x002B358F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.height = 0f;
		}

		// Token: 0x04006159 RID: 24921
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400615A RID: 24922
		[Tooltip("The height of the CharacterController")]
		public SharedFloat height;

		// Token: 0x0400615B RID: 24923
		private CharacterController characterController;

		// Token: 0x0400615C RID: 24924
		private GameObject prevGameObject;
	}
}
