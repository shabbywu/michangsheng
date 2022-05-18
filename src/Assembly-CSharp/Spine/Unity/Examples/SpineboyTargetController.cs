using System;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000E30 RID: 3632
	public class SpineboyTargetController : MonoBehaviour
	{
		// Token: 0x06005774 RID: 22388 RVA: 0x0003E7F4 File Offset: 0x0003C9F4
		private void OnValidate()
		{
			if (this.skeletonAnimation == null)
			{
				this.skeletonAnimation = base.GetComponent<SkeletonAnimation>();
			}
		}

		// Token: 0x06005775 RID: 22389 RVA: 0x0003E810 File Offset: 0x0003CA10
		private void Start()
		{
			this.bone = this.skeletonAnimation.Skeleton.FindBone(this.boneName);
		}

		// Token: 0x06005776 RID: 22390 RVA: 0x00244FC4 File Offset: 0x002431C4
		private void Update()
		{
			Vector3 mousePosition = Input.mousePosition;
			Vector3 vector = this.camera.ScreenToWorldPoint(mousePosition);
			Vector3 vector2 = this.skeletonAnimation.transform.InverseTransformPoint(vector);
			vector2.x *= this.skeletonAnimation.Skeleton.ScaleX;
			vector2.y *= this.skeletonAnimation.Skeleton.ScaleY;
			SkeletonExtensions.SetLocalPosition(this.bone, vector2);
		}

		// Token: 0x0400575D RID: 22365
		public SkeletonAnimation skeletonAnimation;

		// Token: 0x0400575E RID: 22366
		[SpineBone("", "skeletonAnimation", true, false)]
		public string boneName;

		// Token: 0x0400575F RID: 22367
		public Camera camera;

		// Token: 0x04005760 RID: 22368
		private Bone bone;
	}
}
