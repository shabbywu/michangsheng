using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Spine.Unity.Examples;

public class Spineboy : MonoBehaviour
{
	[Serializable]
	[CompilerGenerated]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9 = new _003C_003Ec();

		public static TrackEntryDelegate _003C_003E9__1_0;

		internal void _003CStart_003Eb__1_0(TrackEntry entry)
		{
			Debug.Log((object)("start: " + entry.TrackIndex));
		}
	}

	private SkeletonAnimation skeletonAnimation;

	public void Start()
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Expected O, but got Unknown
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Expected O, but got Unknown
		skeletonAnimation = ((Component)this).GetComponent<SkeletonAnimation>();
		AnimationState animationState = skeletonAnimation.AnimationState;
		animationState.Event += new TrackEntryEventDelegate(HandleEvent);
		object obj = _003C_003Ec._003C_003E9__1_0;
		if (obj == null)
		{
			TrackEntryDelegate val = delegate(TrackEntry entry)
			{
				Debug.Log((object)("start: " + entry.TrackIndex));
			};
			_003C_003Ec._003C_003E9__1_0 = val;
			obj = (object)val;
		}
		animationState.End += (TrackEntryDelegate)obj;
		animationState.AddAnimation(0, "jump", false, 2f);
		animationState.AddAnimation(0, "run", true, 0f);
	}

	private void HandleEvent(TrackEntry trackEntry, Event e)
	{
		Debug.Log((object)string.Concat(trackEntry.TrackIndex, " ", trackEntry.Animation.Name, ": event ", e, ", ", e.Int));
	}

	public void OnMouseDown()
	{
		skeletonAnimation.AnimationState.SetAnimation(0, "jump", false);
		skeletonAnimation.AnimationState.AddAnimation(0, "run", true, 0f);
	}
}
