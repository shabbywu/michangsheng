using System;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000E4E RID: 3662
	public class BoneLocalOverride : MonoBehaviour
	{
		// Token: 0x060057E8 RID: 22504 RVA: 0x00246090 File Offset: 0x00244290
		private void Awake()
		{
			this.spineComponent = base.GetComponent<ISkeletonAnimation>();
			if (this.spineComponent == null)
			{
				base.enabled = false;
				return;
			}
			this.spineComponent.UpdateLocal += new UpdateBonesDelegate(this.OverrideLocal);
			if (this.bone == null)
			{
				base.enabled = false;
				return;
			}
		}

		// Token: 0x060057E9 RID: 22505 RVA: 0x002460E0 File Offset: 0x002442E0
		private void OverrideLocal(ISkeletonAnimation animated)
		{
			if (this.bone == null || this.bone.Data.Name != this.boneName)
			{
				if (string.IsNullOrEmpty(this.boneName))
				{
					return;
				}
				this.bone = this.spineComponent.Skeleton.FindBone(this.boneName);
				if (this.bone == null)
				{
					Debug.LogFormat("Cannot find bone: '{0}'", new object[]
					{
						this.boneName
					});
					return;
				}
			}
			if (this.overridePosition)
			{
				this.bone.X = Mathf.Lerp(this.bone.X, this.localPosition.x, this.alpha);
				this.bone.Y = Mathf.Lerp(this.bone.Y, this.localPosition.y, this.alpha);
			}
			if (this.overrideRotation)
			{
				this.bone.Rotation = Mathf.Lerp(this.bone.Rotation, this.rotation, this.alpha);
			}
		}

		// Token: 0x040057E6 RID: 22502
		[SpineBone("", "", true, false)]
		public string boneName;

		// Token: 0x040057E7 RID: 22503
		[Space]
		[Range(0f, 1f)]
		public float alpha = 1f;

		// Token: 0x040057E8 RID: 22504
		[Space]
		public bool overridePosition = true;

		// Token: 0x040057E9 RID: 22505
		public Vector2 localPosition;

		// Token: 0x040057EA RID: 22506
		[Space]
		public bool overrideRotation = true;

		// Token: 0x040057EB RID: 22507
		[Range(0f, 360f)]
		public float rotation;

		// Token: 0x040057EC RID: 22508
		private ISkeletonAnimation spineComponent;

		// Token: 0x040057ED RID: 22509
		private Bone bone;
	}
}
