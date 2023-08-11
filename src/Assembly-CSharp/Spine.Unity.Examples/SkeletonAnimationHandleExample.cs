using System;
using System.Collections.Generic;
using UnityEngine;

namespace Spine.Unity.Examples;

public class SkeletonAnimationHandleExample : MonoBehaviour
{
	[Serializable]
	public class StateNameToAnimationReference
	{
		public string stateName;

		public AnimationReferenceAsset animation;
	}

	[Serializable]
	public class AnimationTransition
	{
		public AnimationReferenceAsset from;

		public AnimationReferenceAsset to;

		public AnimationReferenceAsset transition;
	}

	public SkeletonAnimation skeletonAnimation;

	public List<StateNameToAnimationReference> statesAndAnimations = new List<StateNameToAnimationReference>();

	public List<AnimationTransition> transitions = new List<AnimationTransition>();

	public Animation TargetAnimation { get; private set; }

	private void Awake()
	{
		foreach (StateNameToAnimationReference statesAndAnimation in statesAndAnimations)
		{
			statesAndAnimation.animation.Initialize();
		}
		foreach (AnimationTransition transition in transitions)
		{
			transition.from.Initialize();
			transition.to.Initialize();
			transition.transition.Initialize();
		}
	}

	public void SetFlip(float horizontal)
	{
		if (horizontal != 0f)
		{
			((SkeletonRenderer)skeletonAnimation).Skeleton.ScaleX = ((horizontal > 0f) ? 1f : (-1f));
		}
	}

	public void PlayAnimationForState(string stateShortName, int layerIndex)
	{
		PlayAnimationForState(StringToHash(stateShortName), layerIndex);
	}

	public void PlayAnimationForState(int shortNameHash, int layerIndex)
	{
		Animation animationForState = GetAnimationForState(shortNameHash);
		if (animationForState != null)
		{
			PlayNewAnimation(animationForState, layerIndex);
		}
	}

	public Animation GetAnimationForState(string stateShortName)
	{
		return GetAnimationForState(StringToHash(stateShortName));
	}

	public Animation GetAnimationForState(int shortNameHash)
	{
		return AnimationReferenceAsset.op_Implicit(statesAndAnimations.Find((StateNameToAnimationReference entry) => StringToHash(entry.stateName) == shortNameHash)?.animation);
	}

	public void PlayNewAnimation(Animation target, int layerIndex)
	{
		Animation val = null;
		Animation val2 = null;
		val2 = GetCurrentAnimation(layerIndex);
		if (val2 != null)
		{
			val = TryGetTransition(val2, target);
		}
		if (val != null)
		{
			skeletonAnimation.AnimationState.SetAnimation(layerIndex, val, false);
			skeletonAnimation.AnimationState.AddAnimation(layerIndex, target, true, 0f);
		}
		else
		{
			skeletonAnimation.AnimationState.SetAnimation(layerIndex, target, true);
		}
		TargetAnimation = target;
	}

	public void PlayOneShot(Animation oneShot, int layerIndex)
	{
		AnimationState animationState = skeletonAnimation.AnimationState;
		animationState.SetAnimation(0, oneShot, false);
		Animation val = TryGetTransition(oneShot, TargetAnimation);
		if (val != null)
		{
			animationState.AddAnimation(0, val, false, 0f);
		}
		animationState.AddAnimation(0, TargetAnimation, true, 0f);
	}

	private Animation TryGetTransition(Animation from, Animation to)
	{
		foreach (AnimationTransition transition in transitions)
		{
			if (transition.from.Animation == from && transition.to.Animation == to)
			{
				return transition.transition.Animation;
			}
		}
		return null;
	}

	private Animation GetCurrentAnimation(int layerIndex)
	{
		TrackEntry current = skeletonAnimation.AnimationState.GetCurrent(layerIndex);
		if (current == null)
		{
			return null;
		}
		return current.Animation;
	}

	private int StringToHash(string s)
	{
		return Animator.StringToHash(s);
	}
}
