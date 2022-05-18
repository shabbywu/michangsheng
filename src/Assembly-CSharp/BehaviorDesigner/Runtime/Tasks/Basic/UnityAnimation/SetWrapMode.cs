using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation
{
	// Token: 0x02001672 RID: 5746
	[TaskCategory("Basic/Animation")]
	[TaskDescription("Sets the wrap mode to the specified value. Returns Success.")]
	public class SetWrapMode : Action
	{
		// Token: 0x06008564 RID: 34148 RVA: 0x002D0E04 File Offset: 0x002CF004
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animation = defaultGameObject.GetComponent<Animation>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008565 RID: 34149 RVA: 0x0005C8EA File Offset: 0x0005AAEA
		public override TaskStatus OnUpdate()
		{
			if (this.animation == null)
			{
				Debug.LogWarning("Animation is null");
				return 1;
			}
			this.animation.wrapMode = this.wrapMode;
			return 2;
		}

		// Token: 0x06008566 RID: 34150 RVA: 0x0005C918 File Offset: 0x0005AB18
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.wrapMode = 0;
		}

		// Token: 0x04007226 RID: 29222
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04007227 RID: 29223
		[Tooltip("How should time beyond the playback range of the clip be treated?")]
		public WrapMode wrapMode;

		// Token: 0x04007228 RID: 29224
		private Animation animation;

		// Token: 0x04007229 RID: 29225
		private GameObject prevGameObject;
	}
}
