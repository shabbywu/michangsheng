using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Spine.Unity.Examples
{
	// Token: 0x02000AD5 RID: 2773
	public class SpineAnimationTesterTool : MonoBehaviour, IHasSkeletonDataAsset, IHasSkeletonComponent
	{
		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06004DBD RID: 19901 RVA: 0x00213BD2 File Offset: 0x00211DD2
		public SkeletonDataAsset SkeletonDataAsset
		{
			get
			{
				return this.skeletonAnimation.SkeletonDataAsset;
			}
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x06004DBE RID: 19902 RVA: 0x00213BDF File Offset: 0x00211DDF
		public ISkeletonComponent SkeletonComponent
		{
			get
			{
				return this.skeletonAnimation;
			}
		}

		// Token: 0x06004DBF RID: 19903 RVA: 0x00213BE8 File Offset: 0x00211DE8
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

		// Token: 0x06004DC0 RID: 19904 RVA: 0x00213D38 File Offset: 0x00211F38
		private void Start()
		{
			if (this.useOverrideMixDuration)
			{
				this.skeletonAnimation.AnimationState.Data.DefaultMix = this.overrideMixDuration;
			}
		}

		// Token: 0x06004DC1 RID: 19905 RVA: 0x00213D60 File Offset: 0x00211F60
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

		// Token: 0x04004CDF RID: 19679
		public SkeletonAnimation skeletonAnimation;

		// Token: 0x04004CE0 RID: 19680
		public bool useOverrideMixDuration;

		// Token: 0x04004CE1 RID: 19681
		public float overrideMixDuration = 0.2f;

		// Token: 0x04004CE2 RID: 19682
		public bool useOverrideAttachmentThreshold = true;

		// Token: 0x04004CE3 RID: 19683
		[Range(0f, 1f)]
		public float attachmentThreshold = 0.5f;

		// Token: 0x04004CE4 RID: 19684
		public bool useOverrideDrawOrderThreshold;

		// Token: 0x04004CE5 RID: 19685
		[Range(0f, 1f)]
		public float drawOrderThreshold = 0.5f;

		// Token: 0x04004CE6 RID: 19686
		[Space]
		public List<SpineAnimationTesterTool.ControlledTrack> trackControls = new List<SpineAnimationTesterTool.ControlledTrack>();

		// Token: 0x04004CE7 RID: 19687
		[Header("UI")]
		public Text boundAnimationsText;

		// Token: 0x04004CE8 RID: 19688
		public Text skeletonNameText;

		// Token: 0x020015BA RID: 5562
		[Serializable]
		public struct AnimationControl
		{
			// Token: 0x0400703C RID: 28732
			[SpineAnimation("", "", true, false)]
			public string animationName;

			// Token: 0x0400703D RID: 28733
			public bool loop;

			// Token: 0x0400703E RID: 28734
			public KeyCode key;

			// Token: 0x0400703F RID: 28735
			[Space]
			public bool useCustomMixDuration;

			// Token: 0x04007040 RID: 28736
			public float mixDuration;
		}

		// Token: 0x020015BB RID: 5563
		[Serializable]
		public class ControlledTrack
		{
			// Token: 0x04007041 RID: 28737
			public List<SpineAnimationTesterTool.AnimationControl> controls = new List<SpineAnimationTesterTool.AnimationControl>();
		}
	}
}
