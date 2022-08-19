using System;
using System.Collections.Generic;
using UnityEngine;

namespace Spine.Unity
{
	// Token: 0x02000AD1 RID: 2769
	public class SkeletonAnimationMulti : MonoBehaviour
	{
		// Token: 0x06004DA5 RID: 19877 RVA: 0x00213430 File Offset: 0x00211630
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

		// Token: 0x06004DA6 RID: 19878 RVA: 0x002134A8 File Offset: 0x002116A8
		private void SetActiveSkeleton(SkeletonAnimation skeletonAnimation)
		{
			foreach (SkeletonAnimation skeletonAnimation2 in this.skeletonAnimations)
			{
				skeletonAnimation2.gameObject.SetActive(skeletonAnimation2 == skeletonAnimation);
			}
			this.currentSkeletonAnimation = skeletonAnimation;
		}

		// Token: 0x06004DA7 RID: 19879 RVA: 0x00213510 File Offset: 0x00211710
		private void Awake()
		{
			this.Initialize(false);
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06004DA8 RID: 19880 RVA: 0x00213519 File Offset: 0x00211719
		public Dictionary<Animation, SkeletonAnimation> AnimationSkeletonTable
		{
			get
			{
				return this.animationSkeletonTable;
			}
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06004DA9 RID: 19881 RVA: 0x00213521 File Offset: 0x00211721
		public Dictionary<string, Animation> AnimationNameTable
		{
			get
			{
				return this.animationNameTable;
			}
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06004DAA RID: 19882 RVA: 0x00213529 File Offset: 0x00211729
		public SkeletonAnimation CurrentSkeletonAnimation
		{
			get
			{
				return this.currentSkeletonAnimation;
			}
		}

		// Token: 0x06004DAB RID: 19883 RVA: 0x00213534 File Offset: 0x00211734
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

		// Token: 0x06004DAC RID: 19884 RVA: 0x002136F4 File Offset: 0x002118F4
		public Animation FindAnimation(string animationName)
		{
			Animation result;
			this.animationNameTable.TryGetValue(animationName, out result);
			return result;
		}

		// Token: 0x06004DAD RID: 19885 RVA: 0x00213711 File Offset: 0x00211911
		public TrackEntry SetAnimation(string animationName, bool loop)
		{
			return this.SetAnimation(this.FindAnimation(animationName), loop);
		}

		// Token: 0x06004DAE RID: 19886 RVA: 0x00213724 File Offset: 0x00211924
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

		// Token: 0x06004DAF RID: 19887 RVA: 0x00213770 File Offset: 0x00211970
		public void SetEmptyAnimation(float mixDuration)
		{
			this.currentSkeletonAnimation.state.SetEmptyAnimation(0, mixDuration);
		}

		// Token: 0x06004DB0 RID: 19888 RVA: 0x00213785 File Offset: 0x00211985
		public void ClearAnimation()
		{
			this.currentSkeletonAnimation.state.ClearTrack(0);
		}

		// Token: 0x06004DB1 RID: 19889 RVA: 0x00213798 File Offset: 0x00211998
		public TrackEntry GetCurrent()
		{
			return this.currentSkeletonAnimation.state.GetCurrent(0);
		}

		// Token: 0x04004CCB RID: 19659
		private const int MainTrackIndex = 0;

		// Token: 0x04004CCC RID: 19660
		public bool initialFlipX;

		// Token: 0x04004CCD RID: 19661
		public bool initialFlipY;

		// Token: 0x04004CCE RID: 19662
		public string initialAnimation;

		// Token: 0x04004CCF RID: 19663
		public bool initialLoop;

		// Token: 0x04004CD0 RID: 19664
		[Space]
		public List<SkeletonDataAsset> skeletonDataAssets = new List<SkeletonDataAsset>();

		// Token: 0x04004CD1 RID: 19665
		[Header("Settings")]
		public MeshGenerator.Settings meshGeneratorSettings = MeshGenerator.Settings.Default;

		// Token: 0x04004CD2 RID: 19666
		private readonly List<SkeletonAnimation> skeletonAnimations = new List<SkeletonAnimation>();

		// Token: 0x04004CD3 RID: 19667
		private readonly Dictionary<string, Animation> animationNameTable = new Dictionary<string, Animation>();

		// Token: 0x04004CD4 RID: 19668
		private readonly Dictionary<Animation, SkeletonAnimation> animationSkeletonTable = new Dictionary<Animation, SkeletonAnimation>();

		// Token: 0x04004CD5 RID: 19669
		private SkeletonAnimation currentSkeletonAnimation;
	}
}
