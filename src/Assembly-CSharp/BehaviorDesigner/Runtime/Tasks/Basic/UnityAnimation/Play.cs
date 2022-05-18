using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation
{
	// Token: 0x0200166D RID: 5741
	[TaskCategory("Basic/Animation")]
	[TaskDescription("Plays animation without any blending. Returns Success.")]
	public class Play : Action
	{
		// Token: 0x06008550 RID: 34128 RVA: 0x002D0BFC File Offset: 0x002CEDFC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animation = defaultGameObject.GetComponent<Animation>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008551 RID: 34129 RVA: 0x002D0C3C File Offset: 0x002CEE3C
		public override TaskStatus OnUpdate()
		{
			if (this.animation == null)
			{
				Debug.LogWarning("Animation is null");
				return 1;
			}
			if (string.IsNullOrEmpty(this.animationName.Value))
			{
				this.animation.Play();
			}
			else
			{
				this.animation.Play(this.animationName.Value, this.playMode);
			}
			return 2;
		}

		// Token: 0x06008552 RID: 34130 RVA: 0x0005C7D1 File Offset: 0x0005A9D1
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.animationName.Value = "";
			this.playMode = 0;
		}

		// Token: 0x04007210 RID: 29200
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04007211 RID: 29201
		[Tooltip("The name of the animation")]
		public SharedString animationName;

		// Token: 0x04007212 RID: 29202
		[Tooltip("The play mode of the animation")]
		public PlayMode playMode;

		// Token: 0x04007213 RID: 29203
		private Animation animation;

		// Token: 0x04007214 RID: 29204
		private GameObject prevGameObject;
	}
}
