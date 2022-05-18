using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityCharacterController
{
	// Token: 0x02001606 RID: 5638
	[TaskCategory("Basic/CharacterController")]
	[TaskDescription("Stores the radius of the CharacterController. Returns Success.")]
	public class GetRadius : Action
	{
		// Token: 0x060083AC RID: 33708 RVA: 0x002CED0C File Offset: 0x002CCF0C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.characterController = defaultGameObject.GetComponent<CharacterController>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060083AD RID: 33709 RVA: 0x0005AAE5 File Offset: 0x00058CE5
		public override TaskStatus OnUpdate()
		{
			if (this.characterController == null)
			{
				Debug.LogWarning("CharacterController is null");
				return 1;
			}
			this.storeValue.Value = this.characterController.radius;
			return 2;
		}

		// Token: 0x060083AE RID: 33710 RVA: 0x0005AB18 File Offset: 0x00058D18
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x0400705D RID: 28765
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400705E RID: 28766
		[Tooltip("The radius of the CharacterController")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x0400705F RID: 28767
		private CharacterController characterController;

		// Token: 0x04007060 RID: 28768
		private GameObject prevGameObject;
	}
}
