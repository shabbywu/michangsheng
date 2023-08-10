using System;
using System.Collections.Generic;
using UnityEngine;

namespace Spine.Unity;

public class SkeletonAnimationMulti : MonoBehaviour
{
	private const int MainTrackIndex = 0;

	public bool initialFlipX;

	public bool initialFlipY;

	public string initialAnimation;

	public bool initialLoop;

	[Space]
	public List<SkeletonDataAsset> skeletonDataAssets = new List<SkeletonDataAsset>();

	[Header("Settings")]
	public Settings meshGeneratorSettings = Settings.Default;

	private readonly List<SkeletonAnimation> skeletonAnimations = new List<SkeletonAnimation>();

	private readonly Dictionary<string, Animation> animationNameTable = new Dictionary<string, Animation>();

	private readonly Dictionary<Animation, SkeletonAnimation> animationSkeletonTable = new Dictionary<Animation, SkeletonAnimation>();

	private SkeletonAnimation currentSkeletonAnimation;

	public Dictionary<Animation, SkeletonAnimation> AnimationSkeletonTable => animationSkeletonTable;

	public Dictionary<string, Animation> AnimationNameTable => animationNameTable;

	public SkeletonAnimation CurrentSkeletonAnimation => currentSkeletonAnimation;

	private void Clear()
	{
		foreach (SkeletonAnimation skeletonAnimation in skeletonAnimations)
		{
			Object.Destroy((Object)(object)((Component)skeletonAnimation).gameObject);
		}
		skeletonAnimations.Clear();
		animationNameTable.Clear();
		animationSkeletonTable.Clear();
	}

	private void SetActiveSkeleton(SkeletonAnimation skeletonAnimation)
	{
		foreach (SkeletonAnimation skeletonAnimation2 in skeletonAnimations)
		{
			((Component)skeletonAnimation2).gameObject.SetActive((Object)(object)skeletonAnimation2 == (Object)(object)skeletonAnimation);
		}
		currentSkeletonAnimation = skeletonAnimation;
	}

	private void Awake()
	{
		Initialize(overwrite: false);
	}

	public void Initialize(bool overwrite)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		if (skeletonAnimations.Count != 0 && !overwrite)
		{
			return;
		}
		Clear();
		Settings meshSettings = meshGeneratorSettings;
		Transform transform = ((Component)this).transform;
		foreach (SkeletonDataAsset skeletonDataAsset in skeletonDataAssets)
		{
			SkeletonAnimation val = SkeletonAnimation.NewSkeletonAnimationGameObject(skeletonDataAsset);
			((Component)val).transform.SetParent(transform, false);
			((SkeletonRenderer)val).SetMeshSettings(meshSettings);
			((SkeletonRenderer)val).initialFlipX = initialFlipX;
			((SkeletonRenderer)val).initialFlipY = initialFlipY;
			Skeleton skeleton = ((SkeletonRenderer)val).skeleton;
			skeleton.ScaleX = ((!initialFlipX) ? 1 : (-1));
			skeleton.ScaleY = ((!initialFlipY) ? 1 : (-1));
			((SkeletonRenderer)val).Initialize(false);
			skeletonAnimations.Add(val);
		}
		Dictionary<string, Animation> dictionary = animationNameTable;
		Dictionary<Animation, SkeletonAnimation> dictionary2 = animationSkeletonTable;
		foreach (SkeletonAnimation skeletonAnimation in skeletonAnimations)
		{
			Enumerator<Animation> enumerator3 = ((SkeletonRenderer)skeletonAnimation).Skeleton.Data.Animations.GetEnumerator();
			try
			{
				while (enumerator3.MoveNext())
				{
					Animation current2 = enumerator3.Current;
					dictionary[current2.Name] = current2;
					dictionary2[current2] = skeletonAnimation;
				}
			}
			finally
			{
				((IDisposable)enumerator3).Dispose();
			}
		}
		SetActiveSkeleton(skeletonAnimations[0]);
		SetAnimation(initialAnimation, initialLoop);
	}

	public Animation FindAnimation(string animationName)
	{
		animationNameTable.TryGetValue(animationName, out var value);
		return value;
	}

	public TrackEntry SetAnimation(string animationName, bool loop)
	{
		return SetAnimation(FindAnimation(animationName), loop);
	}

	public TrackEntry SetAnimation(Animation animation, bool loop)
	{
		if (animation == null)
		{
			return null;
		}
		animationSkeletonTable.TryGetValue(animation, out var value);
		if ((Object)(object)value != (Object)null)
		{
			SetActiveSkeleton(value);
			((SkeletonRenderer)value).skeleton.SetToSetupPose();
			return value.state.SetAnimation(0, animation, loop);
		}
		return null;
	}

	public void SetEmptyAnimation(float mixDuration)
	{
		currentSkeletonAnimation.state.SetEmptyAnimation(0, mixDuration);
	}

	public void ClearAnimation()
	{
		currentSkeletonAnimation.state.ClearTrack(0);
	}

	public TrackEntry GetCurrent()
	{
		return currentSkeletonAnimation.state.GetCurrent(0);
	}
}
