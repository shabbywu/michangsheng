using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Spine.Unity.Examples;

public class SpineAnimationTesterTool : MonoBehaviour, IHasSkeletonDataAsset, IHasSkeletonComponent
{
	[Serializable]
	public struct AnimationControl
	{
		[SpineAnimation("", "", true, false)]
		public string animationName;

		public bool loop;

		public KeyCode key;

		[Space]
		public bool useCustomMixDuration;

		public float mixDuration;
	}

	[Serializable]
	public class ControlledTrack
	{
		public List<AnimationControl> controls = new List<AnimationControl>();
	}

	public SkeletonAnimation skeletonAnimation;

	public bool useOverrideMixDuration;

	public float overrideMixDuration = 0.2f;

	public bool useOverrideAttachmentThreshold = true;

	[Range(0f, 1f)]
	public float attachmentThreshold = 0.5f;

	public bool useOverrideDrawOrderThreshold;

	[Range(0f, 1f)]
	public float drawOrderThreshold = 0.5f;

	[Space]
	public List<ControlledTrack> trackControls = new List<ControlledTrack>();

	[Header("UI")]
	public Text boundAnimationsText;

	public Text skeletonNameText;

	public SkeletonDataAsset SkeletonDataAsset => ((SkeletonRenderer)skeletonAnimation).SkeletonDataAsset;

	public ISkeletonComponent SkeletonComponent => (ISkeletonComponent)(object)skeletonAnimation;

	private void OnValidate()
	{
		if ((Object)(object)skeletonNameText != (Object)null && (Object)(object)skeletonAnimation != (Object)null && (Object)(object)((SkeletonRenderer)skeletonAnimation).skeletonDataAsset != (Object)null)
		{
			skeletonNameText.text = ((Object)SkeletonDataAsset).name.Replace("_SkeletonData", "");
		}
		if (!((Object)(object)boundAnimationsText != (Object)null))
		{
			return;
		}
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("Animation Controls:");
		for (int i = 0; i < trackControls.Count; i++)
		{
			if (i > 0)
			{
				stringBuilder.AppendLine();
			}
			stringBuilder.AppendFormat("---- Track {0} ---- \n", i);
			foreach (AnimationControl control in trackControls[i].controls)
			{
				AnimationControl current = control;
				string text = current.animationName;
				if (string.IsNullOrEmpty(text))
				{
					text = "SetEmptyAnimation";
				}
				stringBuilder.AppendFormat("[{0}]  {1}\n", ((object)(KeyCode)(ref current.key)).ToString(), text);
			}
		}
		boundAnimationsText.text = stringBuilder.ToString();
	}

	private void Start()
	{
		if (useOverrideMixDuration)
		{
			skeletonAnimation.AnimationState.Data.DefaultMix = overrideMixDuration;
		}
	}

	private void Update()
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		AnimationState animationState = skeletonAnimation.AnimationState;
		for (int i = 0; i < trackControls.Count; i++)
		{
			foreach (AnimationControl control in trackControls[i].controls)
			{
				if (!Input.GetKeyDown(control.key))
				{
					continue;
				}
				TrackEntry val;
				if (!string.IsNullOrEmpty(control.animationName))
				{
					val = animationState.SetAnimation(i, control.animationName, control.loop);
				}
				else
				{
					float num = (control.useCustomMixDuration ? control.mixDuration : animationState.Data.DefaultMix);
					val = animationState.SetEmptyAnimation(i, num);
				}
				if (val != null)
				{
					if (control.useCustomMixDuration)
					{
						val.MixDuration = control.mixDuration;
					}
					if (useOverrideAttachmentThreshold)
					{
						val.AttachmentThreshold = attachmentThreshold;
					}
					if (useOverrideDrawOrderThreshold)
					{
						val.DrawOrderThreshold = drawOrderThreshold;
					}
				}
				break;
			}
		}
	}
}
