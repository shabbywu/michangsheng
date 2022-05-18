using System;
using UnityEngine;

namespace Spine.Unity
{
	// Token: 0x02000E0F RID: 3599
	[DisallowMultipleComponent]
	public sealed class SkeletonAnimationFixedTimestep : MonoBehaviour
	{
		// Token: 0x060056F1 RID: 22257 RVA: 0x0003E223 File Offset: 0x0003C423
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

		// Token: 0x060056F2 RID: 22258 RVA: 0x0003E259 File Offset: 0x0003C459
		private void Awake()
		{
			this.requiresNewMesh = true;
			this.accumulatedTime = this.timeOffset;
		}

		// Token: 0x060056F3 RID: 22259 RVA: 0x002433D8 File Offset: 0x002415D8
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

		// Token: 0x060056F4 RID: 22260 RVA: 0x0003E26E File Offset: 0x0003C46E
		private void LateUpdate()
		{
			if (this.frameskipMeshUpdate && !this.requiresNewMesh)
			{
				return;
			}
			this.skeletonAnimation.LateUpdate();
			this.requiresNewMesh = false;
		}

		// Token: 0x0400569F RID: 22175
		public SkeletonAnimation skeletonAnimation;

		// Token: 0x040056A0 RID: 22176
		[Tooltip("The duration of each frame in seconds. For 12 fps: enter '1/12' in the Unity inspector.")]
		public float frameDeltaTime = 0.06666667f;

		// Token: 0x040056A1 RID: 22177
		[Header("Advanced")]
		[Tooltip("The maximum number of fixed timesteps. If the game framerate drops below the If the framerate is consistently faster than the limited frames, this does nothing.")]
		public int maxFrameSkip = 4;

		// Token: 0x040056A2 RID: 22178
		[Tooltip("If enabled, the Skeleton mesh will be updated only on the same frame when the animation and skeleton are updated. Disable this or call SkeletonAnimation.LateUpdate yourself if you are modifying the Skeleton using other components that don't run in the same fixed timestep.")]
		public bool frameskipMeshUpdate = true;

		// Token: 0x040056A3 RID: 22179
		[Tooltip("This is the amount the internal accumulator starts with. Set it to some fraction of your frame delta time if you want to stagger updates between multiple skeletons.")]
		public float timeOffset;

		// Token: 0x040056A4 RID: 22180
		private float accumulatedTime;

		// Token: 0x040056A5 RID: 22181
		private bool requiresNewMesh;
	}
}
