using System;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000AE3 RID: 2787
	public class SpineboyTargetController : MonoBehaviour
	{
		// Token: 0x06004DF8 RID: 19960 RVA: 0x00214DB4 File Offset: 0x00212FB4
		private void OnValidate()
		{
			if (this.skeletonAnimation == null)
			{
				this.skeletonAnimation = base.GetComponent<SkeletonAnimation>();
			}
		}

		// Token: 0x06004DF9 RID: 19961 RVA: 0x00214DD0 File Offset: 0x00212FD0
		private void Start()
		{
			this.bone = this.skeletonAnimation.Skeleton.FindBone(this.boneName);
		}

		// Token: 0x06004DFA RID: 19962 RVA: 0x00214DF0 File Offset: 0x00212FF0
		private void Update()
		{
			Vector3 mousePosition = Input.mousePosition;
			Vector3 vector = this.camera.ScreenToWorldPoint(mousePosition);
			Vector3 vector2 = this.skeletonAnimation.transform.InverseTransformPoint(vector);
			vector2.x *= this.skeletonAnimation.Skeleton.ScaleX;
			vector2.y *= this.skeletonAnimation.Skeleton.ScaleY;
			SkeletonExtensions.SetLocalPosition(this.bone, vector2);
		}

		// Token: 0x04004D54 RID: 19796
		public SkeletonAnimation skeletonAnimation;

		// Token: 0x04004D55 RID: 19797
		[SpineBone("", "skeletonAnimation", true, false)]
		public string boneName;

		// Token: 0x04004D56 RID: 19798
		public Camera camera;

		// Token: 0x04004D57 RID: 19799
		private Bone bone;
	}
}
