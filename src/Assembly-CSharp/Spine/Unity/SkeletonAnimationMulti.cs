using System;
using System.Collections.Generic;
using UnityEngine;

namespace Spine.Unity
{
	// Token: 0x02000E10 RID: 3600
	public class SkeletonAnimationMulti : MonoBehaviour
	{
		// Token: 0x060056F6 RID: 22262 RVA: 0x00243470 File Offset: 0x00241670
		private void Clear()
		{
			foreach (SkeletonAnimation skeletonAnimation in this.skeletonAnimations)
			{
				Object.Destroy(skeletonAnimation.gameObject);
			}
			this.skeletonAnimations.Clear();
			this.animationNameTable.Clear();
			this.animationSkeletonTable.Clear();
		}

		// Token: 0x060056F7 RID: 22263 RVA: 0x002434E8 File Offset: 0x002416E8
		private void SetActiveSkeleton(SkeletonAnimation skeletonAnimation)
		{
			foreach (SkeletonAnimation skeletonAnimation2 in this.skeletonAnimations)
			{
				skeletonAnimation2.gameObject.SetActive(skeletonAnimation2 == skeletonAnimation);
			}
			this.currentSkeletonAnimation = skeletonAnimation;
		}

		// Token: 0x060056F8 RID: 22264 RVA: 0x0003E2B4 File Offset: 0x0003C4B4
		private void Awake()
		{
			this.Initialize(false);
		}

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x060056F9 RID: 22265 RVA: 0x0003E2BD File Offset: 0x0003C4BD
		public Dictionary<Animation, SkeletonAnimation> AnimationSkeletonTable
		{
			get
			{
				return this.animationSkeletonTable;
			}
		}

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x060056FA RID: 22266 RVA: 0x0003E2C5 File Offset: 0x0003C4C5
		public Dictionary<string, Animation> AnimationNameTable
		{
			get
			{
				return this.animationNameTable;
			}
		}

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x060056FB RID: 22267 RVA: 0x0003E2CD File Offset: 0x0003C4CD
		public SkeletonAnimation CurrentSkeletonAnimation
		{
			get
			{
				return this.currentSkeletonAnimation;
			}
		}

		// Token: 0x060056FC RID: 22268 RVA: 0x00243550 File Offset: 0x00241750
		public void Initialize(bool overwrite)
		{
			if (this.skeletonAnimations.Count != 0 && !overwrite)
			{
				return;
			}
			this.Clear();
			MeshGenerator.Settings meshSettings = this.meshGeneratorSettings;
			Transform transform = base.transform;
			foreach (SkeletonDataAsset skeletonDataAsset in this.skeletonDataAssets)
			{
				SkeletonAnimation skeletonAnimation = SkeletonAnimation.NewSkeletonAnimationGameObject(skeletonDataAsset);
				skeletonAnimation.transform.SetParent(transform, false);
				skeletonAnimation.SetMeshSettings(meshSettings);
				skeletonAnimation.initialFlipX = this.initialFlipX;
				skeletonAnimation.initialFlipY = this.initialFlipY;
				Skeleton skeleton = skeletonAnimation.skeleton;
				skeleton.ScaleX = (float)(this.initialFlipX ? -1 : 1);
				skeleton.ScaleY = (float)(this.initialFlipY ? -1 : 1);
				skeletonAnimation.Initialize(false);
				this.skeletonAnimations.Add(skeletonAnimation);
			}
			Dictionary<string, Animation> dictionary = this.animationNameTable;
			Dictionary<Animation, SkeletonAnimation> dictionary2 = this.animationSkeletonTable;
			foreach (SkeletonAnimation skeletonAnimation2 in this.skeletonAnimations)
			{
				foreach (Animation animation in skeletonAnimation2.Skeleton.Data.Animations)
				{
					dictionary[animation.Name] = animation;
					dictionary2[animation] = skeletonAnimation2;
				}
			}
			this.SetActiveSkeleton(this.skeletonAnimations[0]);
			this.SetAnimation(this.initialAnimation, this.initialLoop);
		}

		// Token: 0x060056FD RID: 22269 RVA: 0x00243710 File Offset: 0x00241910
		public Animation FindAnimation(string animationName)
		{
			Animation result;
			this.animationNameTable.TryGetValue(animationName, out result);
			return result;
		}

		// Token: 0x060056FE RID: 22270 RVA: 0x0003E2D5 File Offset: 0x0003C4D5
		public TrackEntry SetAnimation(string animationName, bool loop)
		{
			return this.SetAnimation(this.FindAnimation(animationName), loop);
		}

		// Token: 0x060056FF RID: 22271 RVA: 0x00243730 File Offset: 0x00241930
		public TrackEntry SetAnimation(Animation animation, bool loop)
		{
			if (animation == null)
			{
				return null;
			}
			SkeletonAnimation skeletonAnimation;
			this.animationSkeletonTable.TryGetValue(animation, out skeletonAnimation);
			if (skeletonAnimation != null)
			{
				this.SetActiveSkeleton(skeletonAnimation);
				skeletonAnimation.skeleton.SetToSetupPose();
				return skeletonAnimation.state.SetAnimation(0, animation, loop);
			}
			return null;
		}

		// Token: 0x06005700 RID: 22272 RVA: 0x0003E2E5 File Offset: 0x0003C4E5
		public void SetEmptyAnimation(float mixDuration)
		{
			this.currentSkeletonAnimation.state.SetEmptyAnimation(0, mixDuration);
		}

		// Token: 0x06005701 RID: 22273 RVA: 0x0003E2FA File Offset: 0x0003C4FA
		public void ClearAnimation()
		{
			this.currentSkeletonAnimation.state.ClearTrack(0);
		}

		// Token: 0x06005702 RID: 22274 RVA: 0x0003E30D File Offset: 0x0003C50D
		public TrackEntry GetCurrent()
		{
			return this.currentSkeletonAnimation.state.GetCurrent(0);
		}

		// Token: 0x040056A6 RID: 22182
		private const int MainTrackIndex = 0;

		// Token: 0x040056A7 RID: 22183
		public bool initialFlipX;

		// Token: 0x040056A8 RID: 22184
		public bool initialFlipY;

		// Token: 0x040056A9 RID: 22185
		public string initialAnimation;

		// Token: 0x040056AA RID: 22186
		public bool initialLoop;

		// Token: 0x040056AB RID: 22187
		[Space]
		public List<SkeletonDataAsset> skeletonDataAssets = new List<SkeletonDataAsset>();

		// Token: 0x040056AC RID: 22188
		[Header("Settings")]
		public MeshGenerator.Settings meshGeneratorSettings = MeshGenerator.Settings.Default;

		// Token: 0x040056AD RID: 22189
		private readonly List<SkeletonAnimation> skeletonAnimations = new List<SkeletonAnimation>();

		// Token: 0x040056AE RID: 22190
		private readonly Dictionary<string, Animation> animationNameTable = new Dictionary<string, Animation>();

		// Token: 0x040056AF RID: 22191
		private readonly Dictionary<Animation, SkeletonAnimation> animationSkeletonTable = new Dictionary<Animation, SkeletonAnimation>();

		// Token: 0x040056B0 RID: 22192
		private SkeletonAnimation currentSkeletonAnimation;
	}
}
