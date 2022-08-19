using System;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000AE5 RID: 2789
	public class Goblins : MonoBehaviour
	{
		// Token: 0x06004DFF RID: 19967 RVA: 0x00214F2D File Offset: 0x0021312D
		public void Start()
		{
			this.skeletonAnimation = base.GetComponent<SkeletonAnimation>();
			this.headBone = this.skeletonAnimation.Skeleton.FindBone("head");
			this.skeletonAnimation.UpdateLocal += new UpdateBonesDelegate(this.UpdateLocal);
		}

		// Token: 0x06004E00 RID: 19968 RVA: 0x00214F6D File Offset: 0x0021316D
		public void UpdateLocal(ISkeletonAnimation skeletonRenderer)
		{
			this.headBone.Rotation += this.extraRotation;
		}

		// Token: 0x06004E01 RID: 19969 RVA: 0x00214F88 File Offset: 0x00213188
		public void OnMouseDown()
		{
			this.skeletonAnimation.Skeleton.SetSkin(this.girlSkin ? "goblin" : "goblingirl");
			this.skeletonAnimation.Skeleton.SetSlotsToSetupPose();
			this.girlSkin = !this.girlSkin;
			if (this.girlSkin)
			{
				this.skeletonAnimation.Skeleton.SetAttachment("right hand item", null);
				this.skeletonAnimation.Skeleton.SetAttachment("left hand item", "spear");
				return;
			}
			this.skeletonAnimation.Skeleton.SetAttachment("left hand item", "dagger");
		}

		// Token: 0x04004D5A RID: 19802
		private SkeletonAnimation skeletonAnimation;

		// Token: 0x04004D5B RID: 19803
		private Bone headBone;

		// Token: 0x04004D5C RID: 19804
		private bool girlSkin;

		// Token: 0x04004D5D RID: 19805
		[Range(-360f, 360f)]
		public float extraRotation;
	}
}
