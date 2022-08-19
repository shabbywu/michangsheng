using System;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000AFE RID: 2814
	public class Spineboy : MonoBehaviour
	{
		// Token: 0x06004E69 RID: 20073 RVA: 0x00216964 File Offset: 0x00214B64
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

		// Token: 0x06004E6A RID: 20074 RVA: 0x002169E4 File Offset: 0x00214BE4
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

		// Token: 0x06004E6B RID: 20075 RVA: 0x00216A47 File Offset: 0x00214C47
		public void OnMouseDown()
		{
			this.skeletonAnimation.AnimationState.SetAnimation(0, "jump", false);
			this.skeletonAnimation.AnimationState.AddAnimation(0, "run", true, 0f);
		}

		// Token: 0x04004DE2 RID: 19938
		private SkeletonAnimation skeletonAnimation;
	}
}
