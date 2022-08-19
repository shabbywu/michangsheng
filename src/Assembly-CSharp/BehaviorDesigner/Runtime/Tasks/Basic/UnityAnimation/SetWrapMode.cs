using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation
{
	// Token: 0x020011B0 RID: 4528
	[TaskCategory("Basic/Animation")]
	[TaskDescription("Sets the wrap mode to the specified value. Returns Success.")]
	public class SetWrapMode : Action
	{
		// Token: 0x06007758 RID: 30552 RVA: 0x002B8CA4 File Offset: 0x002B6EA4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animation = defaultGameObject.GetComponent<Animation>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007759 RID: 30553 RVA: 0x002B8CE4 File Offset: 0x002B6EE4
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

		// Token: 0x0600775A RID: 30554 RVA: 0x002B8D12 File Offset: 0x002B6F12
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.wrapMode = 0;
		}

		// Token: 0x040062F7 RID: 25335
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040062F8 RID: 25336
		[Tooltip("How should time beyond the playback range of the clip be treated?")]
		public WrapMode wrapMode;

		// Token: 0x040062F9 RID: 25337
		private Animation animation;

		// Token: 0x040062FA RID: 25338
		private GameObject prevGameObject;
	}
}
