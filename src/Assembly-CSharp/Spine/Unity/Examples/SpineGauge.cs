using System;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000B04 RID: 2820
	[ExecuteInEditMode]
	[RequireComponent(typeof(SkeletonRenderer))]
	public class SpineGauge : MonoBehaviour
	{
		// Token: 0x06004E7D RID: 20093 RVA: 0x002171F5 File Offset: 0x002153F5
		private void Awake()
		{
			this.skeletonRenderer = base.GetComponent<SkeletonRenderer>();
		}

		// Token: 0x06004E7E RID: 20094 RVA: 0x00217203 File Offset: 0x00215403
		private void Update()
		{
			this.SetGaugePercent(this.fillPercent);
		}

		// Token: 0x06004E7F RID: 20095 RVA: 0x00217214 File Offset: 0x00215414
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

		// Token: 0x04004E28 RID: 20008
		[Range(0f, 1f)]
		public float fillPercent;

		// Token: 0x04004E29 RID: 20009
		public AnimationReferenceAsset fillAnimation;

		// Token: 0x04004E2A RID: 20010
		private SkeletonRenderer skeletonRenderer;
	}
}
