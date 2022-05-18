using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x02001642 RID: 5698
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Sets the spread value of the AudioSource. Returns Success.")]
	public class SetSpread : Action
	{
		// Token: 0x0600849A RID: 33946 RVA: 0x002CFCA8 File Offset: 0x002CDEA8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600849B RID: 33947 RVA: 0x0005BBCF File Offset: 0x00059DCF
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return 1;
			}
			this.audioSource.spread = this.spread.Value;
			return 2;
		}

		// Token: 0x0600849C RID: 33948 RVA: 0x0005BC02 File Offset: 0x00059E02
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.spread = 1f;
		}

		// Token: 0x04007143 RID: 28995
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04007144 RID: 28996
		[Tooltip("The spread value of the AudioSource")]
		public SharedFloat spread;

		// Token: 0x04007145 RID: 28997
		private AudioSource audioSource;

		// Token: 0x04007146 RID: 28998
		private GameObject prevGameObject;
	}
}
