using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation
{
	// Token: 0x020011A7 RID: 4519
	[TaskCategory("Basic/Animation")]
	[TaskDescription("Fades the animation over a period of time and fades other animations out. Returns Success.")]
	public class CrossFade : Action
	{
		// Token: 0x06007734 RID: 30516 RVA: 0x002B86A0 File Offset: 0x002B68A0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animation = defaultGameObject.GetComponent<Animation>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007735 RID: 30517 RVA: 0x002B86E0 File Offset: 0x002B68E0
		public override TaskStatus OnUpdate()
		{
			if (this.animation == null)
			{
				Debug.LogWarning("Animation is null");
				return 1;
			}
			this.animation.CrossFade(this.animationName.Value, this.fadeLength, this.playMode);
			return 2;
		}

		// Token: 0x06007736 RID: 30518 RVA: 0x002B871F File Offset: 0x002B691F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.animationName.Value = "";
			this.fadeLength = 0.3f;
			this.playMode = 0;
		}

		// Token: 0x040062CC RID: 25292
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040062CD RID: 25293
		[Tooltip("The name of the animation")]
		public SharedString animationName;

		// Token: 0x040062CE RID: 25294
		[Tooltip("The amount of time it takes to blend")]
		public float fadeLength = 0.3f;

		// Token: 0x040062CF RID: 25295
		[Tooltip("The play mode of the animation")]
		public PlayMode playMode;

		// Token: 0x040062D0 RID: 25296
		private Animation animation;

		// Token: 0x040062D1 RID: 25297
		private GameObject prevGameObject;
	}
}
