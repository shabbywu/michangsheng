using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityCharacterController
{
	// Token: 0x02001607 RID: 5639
	[TaskCategory("Basic/CharacterController")]
	[TaskDescription("Stores the slope limit of the CharacterController. Returns Success.")]
	public class GetSlopeLimit : Action
	{
		// Token: 0x060083B0 RID: 33712 RVA: 0x002CED4C File Offset: 0x002CCF4C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.characterController = defaultGameObject.GetComponent<CharacterController>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060083B1 RID: 33713 RVA: 0x0005AB31 File Offset: 0x00058D31
		public override TaskStatus OnUpdate()
		{
			if (this.characterController == null)
			{
				Debug.LogWarning("CharacterController is null");
				return 1;
			}
			this.storeValue.Value = this.characterController.slopeLimit;
			return 2;
		}

		// Token: 0x060083B2 RID: 33714 RVA: 0x0005AB64 File Offset: 0x00058D64
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x04007061 RID: 28769
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04007062 RID: 28770
		[Tooltip("The slope limit of the CharacterController")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x04007063 RID: 28771
		private CharacterController characterController;

		// Token: 0x04007064 RID: 28772
		private GameObject prevGameObject;
	}
}
