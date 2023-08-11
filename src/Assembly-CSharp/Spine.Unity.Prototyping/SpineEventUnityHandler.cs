using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Spine.Unity.Prototyping;

public class SpineEventUnityHandler : MonoBehaviour
{
	[Serializable]
	public class EventPair
	{
		[SpineEvent("", "", true, false, false)]
		public string spineEvent;

		public UnityEvent unityHandler;

		public TrackEntryEventDelegate eventDelegate;
	}

	public List<EventPair> events = new List<EventPair>();

	private ISkeletonComponent skeletonComponent;

	private IAnimationStateComponent animationStateComponent;

	private void Start()
	{
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		skeletonComponent = skeletonComponent ?? ((Component)this).GetComponent<ISkeletonComponent>();
		if (skeletonComponent == null)
		{
			return;
		}
		animationStateComponent = (IAnimationStateComponent)(((object)animationStateComponent) ?? ((object)/*isinst with value type is only supported in some contexts*/));
		if (animationStateComponent == null)
		{
			return;
		}
		Skeleton skeleton = skeletonComponent.Skeleton;
		if (skeleton == null)
		{
			return;
		}
		SkeletonData data = skeleton.Data;
		AnimationState animationState = animationStateComponent.AnimationState;
		foreach (EventPair ep in events)
		{
			EventData eventData = data.FindEvent(ep.spineEvent);
			ep.eventDelegate = (TrackEntryEventDelegate)(((object)ep.eventDelegate) ?? ((object)(TrackEntryEventDelegate)delegate(TrackEntry trackEntry, Event e)
			{
				if (e.Data == eventData)
				{
					ep.unityHandler.Invoke();
				}
			}));
			animationState.Event += ep.eventDelegate;
		}
	}

	private void OnDestroy()
	{
		animationStateComponent = animationStateComponent ?? ((Component)this).GetComponent<IAnimationStateComponent>();
		if (animationStateComponent == null)
		{
			return;
		}
		AnimationState animationState = animationStateComponent.AnimationState;
		foreach (EventPair @event in events)
		{
			if (@event.eventDelegate != null)
			{
				animationState.Event -= @event.eventDelegate;
			}
			@event.eventDelegate = null;
		}
	}
}
