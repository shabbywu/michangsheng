using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityCharacterController
{
	// Token: 0x02001610 RID: 5648
	[TaskCategory("Basic/CharacterController")]
	[TaskDescription("Sets the slope limit of the CharacterController. Returns Success.")]
	public class SetSlopeLimit : Action
	{
		// Token: 0x060083D5 RID: 33749 RVA: 0x002CEFA0 File Offset: 0x002CD1A0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.characterController = defaultGameObject.GetComponent<CharacterController>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060083D6 RID: 33750 RVA: 0x0005ADC9 File Offset: 0x00058FC9
		public override TaskStatus OnUpdate()
		{
			if (this.characterController == null)
			{
				Debug.LogWarning("CharacterController is null");
				return 1;
			}
			this.characterController.slopeLimit = this.slopeLimit.Value;
			return 2;
		}

		// Token: 0x060083D7 RID: 33751 RVA: 0x0005ADFC File Offset: 0x00058FFC
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.slopeLimit = 0f;
		}

		// Token: 0x04007084 RID: 28804
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04007085 RID: 28805
		[Tooltip("The slope limit of the CharacterController")]
		public SharedFloat slopeLimit;

		// Token: 0x04007086 RID: 28806
		private CharacterController characterController;

		// Token: 0x04007087 RID: 28807
		private GameObject prevGameObject;
	}
}
