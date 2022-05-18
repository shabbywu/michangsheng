using System;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000E33 RID: 3635
	public class Goblins : MonoBehaviour
	{
		// Token: 0x0600577B RID: 22395 RVA: 0x0003E84C File Offset: 0x0003CA4C
		public void Start()
		{
			this.skeletonAnimation = base.GetComponent<SkeletonAnimation>();
			this.headBone = this.skeletonAnimation.Skeleton.FindBone("head");
			this.skeletonAnimation.UpdateLocal += new UpdateBonesDelegate(this.UpdateLocal);
		}

		// Token: 0x0600577C RID: 22396 RVA: 0x0003E88C File Offset: 0x0003CA8C
		public void UpdateLocal(ISkeletonAnimation skeletonRenderer)
		{
			this.headBone.Rotation += this.extraRotation;
		}

		// Token: 0x0600577D RID: 22397 RVA: 0x002450E4 File Offset: 0x002432E4
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

		// Token: 0x04005766 RID: 22374
		private SkeletonAnimation skeletonAnimation;

		// Token: 0x04005767 RID: 22375
		private Bone headBone;

		// Token: 0x04005768 RID: 22376
		private bool girlSkin;

		// Token: 0x04005769 RID: 22377
		[Range(-360f, 360f)]
		public float extraRotation;
	}
}
