using System;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000AF5 RID: 2805
	public class BoneLocalOverride : MonoBehaviour
	{
		// Token: 0x06004E47 RID: 20039 RVA: 0x00216034 File Offset: 0x00214234
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

		// Token: 0x06004E48 RID: 20040 RVA: 0x00216084 File Offset: 0x00214284
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

		// Token: 0x04004DBA RID: 19898
		[SpineBone("", "", true, false)]
		public string boneName;

		// Token: 0x04004DBB RID: 19899
		[Space]
		[Range(0f, 1f)]
		public float alpha = 1f;

		// Token: 0x04004DBC RID: 19900
		[Space]
		public bool overridePosition = true;

		// Token: 0x04004DBD RID: 19901
		public Vector2 localPosition;

		// Token: 0x04004DBE RID: 19902
		[Space]
		public bool overrideRotation = true;

		// Token: 0x04004DBF RID: 19903
		[Range(0f, 360f)]
		public float rotation;

		// Token: 0x04004DC0 RID: 19904
		private ISkeletonAnimation spineComponent;

		// Token: 0x04004DC1 RID: 19905
		private Bone bone;
	}
}
