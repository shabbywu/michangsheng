using System;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000E64 RID: 3684
	[ExecuteInEditMode]
	[RequireComponent(typeof(SkeletonRenderer))]
	public class SpineGauge : MonoBehaviour
	{
		// Token: 0x06005841 RID: 22593 RVA: 0x0003F152 File Offset: 0x0003D352
		private void Awake()
		{
			this.skeletonRenderer = base.GetComponent<SkeletonRenderer>();
		}

		// Token: 0x06005842 RID: 22594 RVA: 0x0003F160 File Offset: 0x0003D360
		private void Update()
		{
			this.SetGaugePercent(this.fillPercent);
		}

		// Token: 0x06005843 RID: 22595 RVA: 0x00247684 File Offset: 0x00245884
		public void SetGaugePercent(float percent)
		{
			if (this.skeletonRenderer == null)
			{
				return;
			}
			Skeleton skeleton = this.skeletonRenderer.skeleton;
			if (skeleton == null)
			{
				return;
			}
			this.fillAnimation.Animation.Apply(skeleton, 0f, percent, false, null, 1f, 0, 0);
			skeleton.Update(Time.deltaTime);
			skeleton.UpdateWorldTransform();
		}

		// Token: 0x04005871 RID: 22641
		[Range(0f, 1f)]
		public float fillPercent;

		// Token: 0x04005872 RID: 22642
		public AnimationReferenceAsset fillAnimation;

		// Token: 0x04005873 RID: 22643
		private SkeletonRenderer skeletonRenderer;
	}
}
