using System;
using UnityEngine;

namespace Spine.Unity
{
	// Token: 0x02000AD0 RID: 2768
	[DisallowMultipleComponent]
	public sealed class SkeletonAnimationFixedTimestep : MonoBehaviour
	{
		// Token: 0x06004DA0 RID: 19872 RVA: 0x00213308 File Offset: 0x00211508
		private void OnValidate()
		{
			this.skeletonAnimation = base.GetComponent<SkeletonAnimation>();
			if (this.frameDeltaTime <= 0f)
			{
				this.frameDeltaTime = 0.016666668f;
			}
			if (this.maxFrameSkip < 1)
			{
				this.maxFrameSkip = 1;
			}
		}

		// Token: 0x06004DA1 RID: 19873 RVA: 0x0021333E File Offset: 0x0021153E
		private void Awake()
		{
			this.requiresNewMesh = true;
			this.accumulatedTime = this.timeOffset;
		}

		// Token: 0x06004DA2 RID: 19874 RVA: 0x00213354 File Offset: 0x00211554
		private void Update()
		{
			if (this.skeletonAnimation.enabled)
			{
				this.skeletonAnimation.enabled = false;
			}
			this.accumulatedTime += Time.deltaTime;
			float num = 0f;
			while (this.accumulatedTime >= this.frameDeltaTime)
			{
				num += 1f;
				if (num > (float)this.maxFrameSkip)
				{
					break;
				}
				this.accumulatedTime -= this.frameDeltaTime;
			}
			if (num > 0f)
			{
				this.skeletonAnimation.Update(num * this.frameDeltaTime);
				this.requiresNewMesh = true;
			}
		}

		// Token: 0x06004DA3 RID: 19875 RVA: 0x002133E9 File Offset: 0x002115E9
		private void LateUpdate()
		{
			if (this.frameskipMeshUpdate && !this.requiresNewMesh)
			{
				return;
			}
			this.skeletonAnimation.LateUpdate();
			this.requiresNewMesh = false;
		}

		// Token: 0x04004CC4 RID: 19652
		public SkeletonAnimation skeletonAnimation;

		// Token: 0x04004CC5 RID: 19653
		[Tooltip("The duration of each frame in seconds. For 12 fps: enter '1/12' in the Unity inspector.")]
		public float frameDeltaTime = 0.06666667f;

		// Token: 0x04004CC6 RID: 19654
		[Header("Advanced")]
		[Tooltip("The maximum number of fixed timesteps. If the game framerate drops below the If the framerate is consistently faster than the limited frames, this does nothing.")]
		public int maxFrameSkip = 4;

		// Token: 0x04004CC7 RID: 19655
		[Tooltip("If enabled, the Skeleton mesh will be updated only on the same frame when the animation and skeleton are updated. Disable this or call SkeletonAnimation.LateUpdate yourself if you are modifying the Skeleton using other components that don't run in the same fixed timestep.")]
		public bool frameskipMeshUpdate = true;

		// Token: 0x04004CC8 RID: 19656
		[Tooltip("This is the amount the internal accumulator starts with. Set it to some fraction of your frame delta time if you want to stagger updates between multiple skeletons.")]
		public float timeOffset;

		// Token: 0x04004CC9 RID: 19657
		private float accumulatedTime;

		// Token: 0x04004CCA RID: 19658
		private bool requiresNewMesh;
	}
}
