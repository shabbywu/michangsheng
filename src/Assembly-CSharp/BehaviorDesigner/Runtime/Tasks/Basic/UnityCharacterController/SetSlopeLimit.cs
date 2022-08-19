using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityCharacterController
{
	// Token: 0x02001151 RID: 4433
	[TaskCategory("Basic/CharacterController")]
	[TaskDescription("Sets the slope limit of the CharacterController. Returns Success.")]
	public class SetSlopeLimit : Action
	{
		// Token: 0x060075DB RID: 30171 RVA: 0x002B5434 File Offset: 0x002B3634
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.characterController = defaultGameObject.GetComponent<CharacterController>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060075DC RID: 30172 RVA: 0x002B5474 File Offset: 0x002B3674
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

		// Token: 0x060075DD RID: 30173 RVA: 0x002B54A7 File Offset: 0x002B36A7
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.slopeLimit = 0f;
		}

		// Token: 0x04006161 RID: 24929
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006162 RID: 24930
		[Tooltip("The slope limit of the CharacterController")]
		public SharedFloat slopeLimit;

		// Token: 0x04006163 RID: 24931
		private CharacterController characterController;

		// Token: 0x04006164 RID: 24932
		private GameObject prevGameObject;
	}
}
