using System;
using System.Collections.Generic;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000E3B RID: 3643
	public class SkeletonAnimationHandleExample : MonoBehaviour
	{
		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x06005799 RID: 22425 RVA: 0x0003EA47 File Offset: 0x0003CC47
		// (set) Token: 0x0600579A RID: 22426 RVA: 0x0003EA4F File Offset: 0x0003CC4F
		public Animation TargetAnimation { get; private set; }

		// Token: 0x0600579B RID: 22427 RVA: 0x002455C4 File Offset: 0x002437C4
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

		// Token: 0x0600579C RID: 22428 RVA: 0x0003EA58 File Offset: 0x0003CC58
		public void SetFlip(float horizontal)
		{
			if (horizontal != 0f)
			{
				this.skeletonAnimation.Skeleton.ScaleX = ((horizontal > 0f) ? 1f : -1f);
			}
		}

		// Token: 0x0600579D RID: 22429 RVA: 0x0003EA86 File Offset: 0x0003CC86
		public void PlayAnimationForState(string stateShortName, int layerIndex)
		{
			this.PlayAnimationForState(this.StringToHash(stateShortName), layerIndex);
		}

		// Token: 0x0600579E RID: 22430 RVA: 0x00245674 File Offset: 0x00243874
		public void PlayAnimationForState(int shortNameHash, int layerIndex)
		{
			Animation animationForState = this.GetAnimationForState(shortNameHash);
			if (animationForState == null)
			{
				return;
			}
			this.PlayNewAnimation(animationForState, layerIndex);
		}

		// Token: 0x0600579F RID: 22431 RVA: 0x0003EA96 File Offset: 0x0003CC96
		public Animation GetAnimationForState(string stateShortName)
		{
			return this.GetAnimationForState(this.StringToHash(stateShortName));
		}

		// Token: 0x060057A0 RID: 22432 RVA: 0x00245698 File Offset: 0x00243898
		public Animation GetAnimationForState(int shortNameHash)
		{
			SkeletonAnimationHandleExample.StateNameToAnimationReference stateNameToAnimationReference = this.statesAndAnimations.Find((SkeletonAnimationHandleExample.StateNameToAnimationReference entry) => this.StringToHash(entry.stateName) == shortNameHash);
			return (stateNameToAnimationReference == null) ? null : stateNameToAnimationReference.animation;
		}

		// Token: 0x060057A1 RID: 22433 RVA: 0x002456E4 File Offset: 0x002438E4
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

		// Token: 0x060057A2 RID: 22434 RVA: 0x00245758 File Offset: 0x00243958
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

		// Token: 0x060057A3 RID: 22435 RVA: 0x002457B0 File Offset: 0x002439B0
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

		// Token: 0x060057A4 RID: 22436 RVA: 0x0024582C File Offset: 0x00243A2C
		private Animation GetCurrentAnimation(int layerIndex)
		{
			TrackEntry current = this.skeletonAnimation.AnimationState.GetCurrent(layerIndex);
			if (current == null)
			{
				return null;
			}
			return current.Animation;
		}

		// Token: 0x060057A5 RID: 22437 RVA: 0x0003EAA5 File Offset: 0x0003CCA5
		private int StringToHash(string s)
		{
			return Animator.StringToHash(s);
		}

		// Token: 0x04005793 RID: 22419
		public SkeletonAnimation skeletonAnimation;

		// Token: 0x04005794 RID: 22420
		public List<SkeletonAnimationHandleExample.StateNameToAnimationReference> statesAndAnimations = new List<SkeletonAnimationHandleExample.StateNameToAnimationReference>();

		// Token: 0x04005795 RID: 22421
		public List<SkeletonAnimationHandleExample.AnimationTransition> transitions = new List<SkeletonAnimationHandleExample.AnimationTransition>();

		// Token: 0x02000E3C RID: 3644
		[Serializable]
		public class StateNameToAnimationReference
		{
			// Token: 0x04005797 RID: 22423
			public string stateName;

			// Token: 0x04005798 RID: 22424
			public AnimationReferenceAsset animation;
		}

		// Token: 0x02000E3D RID: 3645
		[Serializable]
		public class AnimationTransition
		{
			// Token: 0x04005799 RID: 22425
			public AnimationReferenceAsset from;

			// Token: 0x0400579A RID: 22426
			public AnimationReferenceAsset to;

			// Token: 0x0400579B RID: 22427
			public AnimationReferenceAsset transition;
		}
	}
}
