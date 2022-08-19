using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityCharacterController
{
	// Token: 0x0200114E RID: 4430
	[TaskCategory("Basic/CharacterController")]
	[TaskDescription("Sets the center of the CharacterController. Returns Success.")]
	public class SetCenter : Action
	{
		// Token: 0x060075CF RID: 30159 RVA: 0x002B5290 File Offset: 0x002B3490
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.characterController = defaultGameObject.GetComponent<CharacterController>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060075D0 RID: 30160 RVA: 0x002B52D0 File Offset: 0x002B34D0
		public override TaskStatus OnUpdate()
		{
			if (this.characterController == null)
			{
				Debug.LogWarning("CharacterController is null");
				return 1;
			}
			this.characterController.center = this.center.Value;
			return 2;
		}

		// Token: 0x060075D1 RID: 30161 RVA: 0x002B5303 File Offset: 0x002B3503
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.center = Vector3.zero;
		}

		// Token: 0x04006155 RID: 24917
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006156 RID: 24918
		[Tooltip("The center of the CharacterController")]
		public SharedVector3 center;

		// Token: 0x04006157 RID: 24919
		private CharacterController characterController;

		// Token: 0x04006158 RID: 24920
		private GameObject prevGameObject;
	}
}
