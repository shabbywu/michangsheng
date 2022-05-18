using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
	// Token: 0x02001636 RID: 5686
	[TaskCategory("Basic/AudioSource")]
	[TaskDescription("Sets the clip value of the AudioSource. Returns Success.")]
	public class SetAudioClip : Action
	{
		// Token: 0x0600846A RID: 33898 RVA: 0x002CF9A8 File Offset: 0x002CDBA8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.audioSource = defaultGameObject.GetComponent<AudioSource>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600846B RID: 33899 RVA: 0x0005B83D File Offset: 0x00059A3D
		public override TaskStatus OnUpdate()
		{
			if (this.audioSource == null)
			{
				Debug.LogWarning("AudioSource is null");
				return 1;
			}
			this.audioSource.clip = this.audioClip;
			return 2;
		}

		// Token: 0x0600846C RID: 33900 RVA: 0x0005B86B File Offset: 0x00059A6B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.audioClip = null;
		}

		// Token: 0x04007113 RID: 28947
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04007114 RID: 28948
		[Tooltip("The AudioSource clip")]
		public AudioClip audioClip;

		// Token: 0x04007115 RID: 28949
		private AudioSource audioSource;

		// Token: 0x04007116 RID: 28950
		private GameObject prevGameObject;
	}
}
