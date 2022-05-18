using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityCharacterController
{
	// Token: 0x0200160D RID: 5645
	[TaskCategory("Basic/CharacterController")]
	[TaskDescription("Sets the center of the CharacterController. Returns Success.")]
	public class SetCenter : Action
	{
		// Token: 0x060083C9 RID: 33737 RVA: 0x002CEEE0 File Offset: 0x002CD0E0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.characterController = defaultGameObject.GetComponent<CharacterController>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060083CA RID: 33738 RVA: 0x0005ACE5 File Offset: 0x00058EE5
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

		// Token: 0x060083CB RID: 33739 RVA: 0x0005AD18 File Offset: 0x00058F18
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.center = Vector3.zero;
		}

		// Token: 0x04007078 RID: 28792
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04007079 RID: 28793
		[Tooltip("The center of the CharacterController")]
		public SharedVector3 center;

		// Token: 0x0400707A RID: 28794
		private CharacterController characterController;

		// Token: 0x0400707B RID: 28795
		private GameObject prevGameObject;
	}
}
