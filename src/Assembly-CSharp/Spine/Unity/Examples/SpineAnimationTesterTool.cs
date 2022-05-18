using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Spine.Unity.Examples
{
	// Token: 0x02000E19 RID: 3609
	public class SpineAnimationTesterTool : MonoBehaviour, IHasSkeletonDataAsset, IHasSkeletonComponent
	{
		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x06005714 RID: 22292 RVA: 0x0003E3F5 File Offset: 0x0003C5F5
		public SkeletonDataAsset SkeletonDataAsset
		{
			get
			{
				return this.skeletonAnimation.SkeletonDataAsset;
			}
		}

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x06005715 RID: 22293 RVA: 0x0003E402 File Offset: 0x0003C602
		public ISkeletonComponent SkeletonComponent
		{
			get
			{
				return this.skeletonAnimation;
			}
		}

		// Token: 0x06005716 RID: 22294 RVA: 0x00243B0C File Offset: 0x00241D0C
		private void OnValidate()
		{
			if (this.skeletonNameText != null && this.skeletonAnimation != null && this.skeletonAnimation.skeletonDataAsset != null)
			{
				this.skeletonNameText.text = this.SkeletonDataAsset.name.Replace("_SkeletonData", "");
			}
			if (this.boundAnimationsText != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine("Animation Controls:");
				for (int i = 0; i < this.trackControls.Count; i++)
				{
					if (i > 0)
					{
						stringBuilder.AppendLine();
					}
					stringBuilder.AppendFormat("---- Track {0} ---- \n", i);
					foreach (SpineAnimationTesterTool.AnimationControl animationControl in this.trackControls[i].controls)
					{
						string text = animationControl.animationName;
						if (string.IsNullOrEmpty(text))
						{
							text = "SetEmptyAnimation";
						}
						stringBuilder.AppendFormat("[{0}]  {1}\n", animationControl.key.ToString(), text);
					}
				}
				this.boundAnimationsText.text = stringBuilder.ToString();
			}
		}

		// Token: 0x06005717 RID: 22295 RVA: 0x0003E40A File Offset: 0x0003C60A
		private void Start()
		{
			if (this.useOverrideMixDuration)
			{
				this.skeletonAnimation.AnimationState.Data.DefaultMix = this.overrideMixDuration;
			}
		}

		// Token: 0x06005718 RID: 22296 RVA: 0x00243C5C File Offset: 0x00241E5C
		private void Update()
		{
			AnimationState animationState = this.skeletonAnimation.AnimationState;
			for (int i = 0; i < this.trackControls.Count; i++)
			{
				foreach (SpineAnimationTesterTool.AnimationControl animationControl in this.trackControls[i].controls)
				{
					if (Input.GetKeyDown(animationControl.key))
					{
						TrackEntry trackEntry;
						if (!string.IsNullOrEmpty(animationControl.animationName))
						{
							trackEntry = animationState.SetAnimation(i, animationControl.animationName, animationControl.loop);
						}
						else
						{
							float num = animationControl.useCustomMixDuration ? animationControl.mixDuration : animationState.Data.DefaultMix;
							trackEntry = animationState.SetEmptyAnimation(i, num);
						}
						if (trackEntry == null)
						{
							break;
						}
						if (animationControl.useCustomMixDuration)
						{
							trackEntry.MixDuration = animationControl.mixDuration;
						}
						if (this.useOverrideAttachmentThreshold)
						{
							trackEntry.AttachmentThreshold = this.attachmentThreshold;
						}
						if (this.useOverrideDrawOrderThreshold)
						{
							trackEntry.DrawOrderThreshold = this.drawOrderThreshold;
							break;
						}
						break;
					}
				}
			}
		}

		// Token: 0x040056C4 RID: 22212
		public SkeletonAnimation skeletonAnimation;

		// Token: 0x040056C5 RID: 22213
		public bool useOverrideMixDuration;

		// Token: 0x040056C6 RID: 22214
		public float overrideMixDuration = 0.2f;

		// Token: 0x040056C7 RID: 22215
		public bool useOverrideAttachmentThreshold = true;

		// Token: 0x040056C8 RID: 22216
		[Range(0f, 1f)]
		public float attachmentThreshold = 0.5f;

		// Token: 0x040056C9 RID: 22217
		public bool useOverrideDrawOrderThreshold;

		// Token: 0x040056CA RID: 22218
		[Range(0f, 1f)]
		public float drawOrderThreshold = 0.5f;

		// Token: 0x040056CB RID: 22219
		[Space]
		public List<SpineAnimationTesterTool.ControlledTrack> trackControls = new List<SpineAnimationTesterTool.ControlledTrack>();

		// Token: 0x040056CC RID: 22220
		[Header("UI")]
		public Text boundAnimationsText;

		// Token: 0x040056CD RID: 22221
		public Text skeletonNameText;

		// Token: 0x02000E1A RID: 3610
		[Serializable]
		public struct AnimationControl
		{
			// Token: 0x040056CE RID: 22222
			[SpineAnimation("", "", true, false)]
			public string animationName;

			// Token: 0x040056CF RID: 22223
			public bool loop;

			// Token: 0x040056D0 RID: 22224
			public KeyCode key;

			// Token: 0x040056D1 RID: 22225
			[Space]
			public bool useCustomMixDuration;

			// Token: 0x040056D2 RID: 22226
			public float mixDuration;
		}

		// Token: 0x02000E1B RID: 3611
		[Serializable]
		public class ControlledTrack
		{
			// Token: 0x040056D3 RID: 22227
			public List<SpineAnimationTesterTool.AnimationControl> controls = new List<SpineAnimationTesterTool.AnimationControl>();
		}
	}
}
