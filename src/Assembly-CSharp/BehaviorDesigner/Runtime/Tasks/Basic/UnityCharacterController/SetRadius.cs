using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityCharacterController
{
	// Token: 0x02001150 RID: 4432
	[TaskCategory("Basic/CharacterController")]
	[TaskDescription("Sets the radius of the CharacterController. Returns Success.")]
	public class SetRadius : Action
	{
		// Token: 0x060075D7 RID: 30167 RVA: 0x002B53A8 File Offset: 0x002B35A8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.characterController = defaultGameObject.GetComponent<CharacterController>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060075D8 RID: 30168 RVA: 0x002B53E8 File Offset: 0x002B35E8
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

		// Token: 0x060075D9 RID: 30169 RVA: 0x002B541B File Offset: 0x002B361B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.radius = 0f;
		}

		// Token: 0x0400615D RID: 24925
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400615E RID: 24926
		[Tooltip("The radius of the CharacterController")]
		public SharedFloat radius;

		// Token: 0x0400615F RID: 24927
		private CharacterController characterController;

		// Token: 0x04006160 RID: 24928
		private GameObject prevGameObject;
	}
}
