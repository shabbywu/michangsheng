using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityCharacterController
{
	// Token: 0x0200160F RID: 5647
	[TaskCategory("Basic/CharacterController")]
	[TaskDescription("Sets the radius of the CharacterController. Returns Success.")]
	public class SetRadius : Action
	{
		// Token: 0x060083D1 RID: 33745 RVA: 0x002CEF60 File Offset: 0x002CD160
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.characterController = defaultGameObject.GetComponent<CharacterController>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060083D2 RID: 33746 RVA: 0x0005AD7D File Offset: 0x00058F7D
		public override TaskStatus OnUpdate()
		{
			if (this.characterController == null)
			{
				Debug.LogWarning("CharacterController is null");
				return 1;
			}
			this.characterController.radius = this.radius.Value;
			return 2;
		}

		// Token: 0x060083D3 RID: 33747 RVA: 0x0005ADB0 File Offset: 0x00058FB0
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.radius = 0f;
		}

		// Token: 0x04007080 RID: 28800
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04007081 RID: 28801
		[Tooltip("The radius of the CharacterController")]
		public SharedFloat radius;

		// Token: 0x04007082 RID: 28802
		private CharacterController characterController;

		// Token: 0x04007083 RID: 28803
		private GameObject prevGameObject;
	}
}
