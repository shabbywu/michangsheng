using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
	// Token: 0x020014F3 RID: 5363
	[TaskCategory("Basic/Transform")]
	[TaskDescription("Sets the right vector of the Transform. Returns Success.")]
	public class SetRightVector : Action
	{
		// Token: 0x06007FF9 RID: 32761 RVA: 0x002CABD4 File Offset: 0x002C8DD4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007FFA RID: 32762 RVA: 0x00056F11 File Offset: 0x00055111
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return 1;
			}
			this.targetTransform.right = this.position.Value;
			return 2;
		}

		// Token: 0x06007FFB RID: 32763 RVA: 0x00056F44 File Offset: 0x00055144
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.position = Vector3.zero;
		}

		// Token: 0x04006CE0 RID: 27872
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006CE1 RID: 27873
		[Tooltip("The position of the Transform")]
		public SharedVector3 position;

		// Token: 0x04006CE2 RID: 27874
		private Transform targetTransform;

		// Token: 0x04006CE3 RID: 27875
		private GameObject prevGameObject;
	}
}
