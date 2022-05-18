using System;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000E59 RID: 3673
	public class Spineboy : MonoBehaviour
	{
		// Token: 0x06005816 RID: 22550 RVA: 0x002469B4 File Offset: 0x00244BB4
		public void Start()
		{
			this.skeletonAnimation = base.GetComponent<SkeletonAnimation>();
			AnimationState animationState = this.skeletonAnimation.AnimationState;
			animationState.Event += new AnimationState.TrackEntryEventDelegate(this.HandleEvent);
			animationState.End += delegate(TrackEntry entry)
			{
				Debug.Log("start: " + entry.TrackIndex);
			};
			animationState.AddAnimation(0, "jump", false, 2f);
			animationState.AddAnimation(0, "run", true, 0f);
		}

		// Token: 0x06005817 RID: 22551 RVA: 0x00246A34 File Offset: 0x00244C34
		private void HandleEvent(TrackEntry trackEntry, Event e)
		{
			Debug.Log(string.Concat(new object[]
			{
				trackEntry.TrackIndex,
				" ",
				trackEntry.Animation.Name,
				": event ",
				e,
				", ",
				e.Int
			}));
		}

		// Token: 0x06005818 RID: 22552 RVA: 0x0003EFD5 File Offset: 0x0003D1D5
		public void OnMouseDown()
		{
			this.skeletonAnimation.AnimationState.SetAnimation(0, "jump", false);
			this.skeletonAnimation.AnimationState.AddAnimation(0, "run", true, 0f);
		}

		// Token: 0x04005816 RID: 22550
		private SkeletonAnimation skeletonAnimation;
	}
}
