using System;
using System.Collections.Generic;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000AEC RID: 2796
	public class SkeletonAnimationHandleExample : MonoBehaviour
	{
		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x06004E17 RID: 19991 RVA: 0x002154DA File Offset: 0x002136DA
		// (set) Token: 0x06004E18 RID: 19992 RVA: 0x002154E2 File Offset: 0x002136E2
		public Animation TargetAnimation { get; private set; }

		// Token: 0x06004E19 RID: 19993 RVA: 0x002154EC File Offset: 0x002136EC
		private void Awake()
		{
			foreach (SkeletonAnimationHandleExample.StateNameToAnimationReference stateNameToAnimationReference in this.statesAndAnimations)
			{
				stateNameToAnimationReference.animation.Initialize();
			}
			foreach (SkeletonAnimationHandleExample.AnimationTransition animationTransition in this.transitions)
			{
				animationTransition.from.Initialize();
				animationTransition.to.Initialize();
				animationTransition.transition.Initialize();
			}
		}

		// Token: 0x06004E1A RID: 19994 RVA: 0x0021559C File Offset: 0x0021379C
		public void SetFlip(float horizontal)
		{
			if (horizontal != 0f)
			{
				this.skeletonAnimation.Skeleton.ScaleX = ((horizontal > 0f) ? 1f : -1f);
			}
		}

		// Token: 0x06004E1B RID: 19995 RVA: 0x002155CA File Offset: 0x002137CA
		public void PlayAnimationForState(string stateShortName, int layerIndex)
		{
			this.PlayAnimationForState(this.StringToHash(stateShortName), layerIndex);
		}

		// Token: 0x06004E1C RID: 19996 RVA: 0x002155DC File Offset: 0x002137DC
		public void PlayAnimationForState(int shortNameHash, int layerIndex)
		{
			Animation animationForState = this.GetAnimationForState(shortNameHash);
			if (animationForState == null)
			{
				return;
			}
			this.PlayNewAnimation(animationForState, layerIndex);
		}

		// Token: 0x06004E1D RID: 19997 RVA: 0x002155FD File Offset: 0x002137FD
		public Animation GetAnimationForState(string stateShortName)
		{
			return this.GetAnimationForState(this.StringToHash(stateShortName));
		}

		// Token: 0x06004E1E RID: 19998 RVA: 0x0021560C File Offset: 0x0021380C
		public Animation GetAnimationForState(int shortNameHash)
		{
			SkeletonAnimationHandleExample.StateNameToAnimationReference stateNameToAnimationReference = this.statesAndAnimations.Find((SkeletonAnimationHandleExample.StateNameToAnimationReference entry) => this.StringToHash(entry.stateName) == shortNameHash);
			return (stateNameToAnimationReference == null) ? null : stateNameToAnimationReference.animation;
		}

		// Token: 0x06004E1F RID: 19999 RVA: 0x00215658 File Offset: 0x00213858
		public void PlayNewAnimation(Animation target, int layerIndex)
		{
			Animation animation = null;
			Animation currentAnimation = this.GetCurrentAnimation(layerIndex);
			if (currentAnimation != null)
			{
				animation = this.TryGetTransition(currentAnimation, target);
			}
			if (animation != null)
			{
				this.skeletonAnimation.AnimationState.SetAnimation(layerIndex, animation, false);
				this.skeletonAnimation.AnimationState.AddAnimation(layerIndex, target, true, 0f);
			}
			else
			{
				this.skeletonAnimation.AnimationState.SetAnimation(layerIndex, target, true);
			}
			this.TargetAnimation = target;
		}

		// Token: 0x06004E20 RID: 20000 RVA: 0x002156CC File Offset: 0x002138CC
		public void PlayOneShot(Animation oneShot, int layerIndex)
		{
			AnimationState animationState = this.skeletonAnimation.AnimationState;
			animationState.SetAnimation(0, oneShot, false);
			Animation animation = this.TryGetTransition(oneShot, this.TargetAnimation);
			if (animation != null)
			{
				animationState.AddAnimation(0, animation, false, 0f);
			}
			animationState.AddAnimation(0, this.TargetAnimation, true, 0f);
		}

		// Token: 0x06004E21 RID: 20001 RVA: 0x00215724 File Offset: 0x00213924
		private Animation TryGetTransition(Animation from, Animation to)
		{
			foreach (SkeletonAnimationHandleExample.AnimationTransition animationTransition in this.transitions)
			{
				if (animationTransition.from.Animation == from && animationTransition.to.Animation == to)
				{
					return animationTransition.transition.Animation;
				}
			}
			return null;
		}

		// Token: 0x06004E22 RID: 20002 RVA: 0x002157A0 File Offset: 0x002139A0
		private Animation GetCurrentAnimation(int layerIndex)
		{
			TrackEntry current = this.skeletonAnimation.AnimationState.GetCurrent(layerIndex);
			if (current == null)
			{
				return null;
			}
			return current.Animation;
		}

		// Token: 0x06004E23 RID: 20003 RVA: 0x002157CA File Offset: 0x002139CA
		private int StringToHash(string s)
		{
			return Animator.StringToHash(s);
		}

		// Token: 0x04004D83 RID: 19843
		public SkeletonAnimation skeletonAnimation;

		// Token: 0x04004D84 RID: 19844
		public List<SkeletonAnimationHandleExample.StateNameToAnimationReference> statesAndAnimations = new List<SkeletonAnimationHandleExample.StateNameToAnimationReference>();

		// Token: 0x04004D85 RID: 19845
		public List<SkeletonAnimationHandleExample.AnimationTransition> transitions = new List<SkeletonAnimationHandleExample.AnimationTransition>();

		// Token: 0x020015C5 RID: 5573
		[Serializable]
		public class StateNameToAnimationReference
		{
			// Token: 0x04007067 RID: 28775
			public string stateName;

			// Token: 0x04007068 RID: 28776
			public AnimationReferenceAsset animation;
		}

		// Token: 0x020015C6 RID: 5574
		[Serializable]
		public class AnimationTransition
		{
			// Token: 0x04007069 RID: 28777
			public AnimationReferenceAsset from;

			// Token: 0x0400706A RID: 28778
			public AnimationReferenceAsset to;

			// Token: 0x0400706B RID: 28779
			public AnimationReferenceAsset transition;
		}
	}
}
