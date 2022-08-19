using System;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000AE9 RID: 2793
	public class MaterialReplacementExample : MonoBehaviour
	{
		// Token: 0x06004E0D RID: 19981 RVA: 0x00215295 File Offset: 0x00213495
		private void Start()
		{
			this.previousEnabled = this.replacementEnabled;
			this.SetReplacementEnabled(this.replacementEnabled);
			this.mpb = new MaterialPropertyBlock();
		}

		// Token: 0x06004E0E RID: 19982 RVA: 0x002152BC File Offset: 0x002134BC
		private void Update()
		{
			this.mpb.SetFloat(this.phasePropertyName, this.phase);
			base.GetComponent<MeshRenderer>().SetPropertyBlock(this.mpb);
			if (this.previousEnabled != this.replacementEnabled)
			{
				this.SetReplacementEnabled(this.replacementEnabled);
			}
			this.previousEnabled = this.replacementEnabled;
		}

		// Token: 0x06004E0F RID: 19983 RVA: 0x00215317 File Offset: 0x00213517
		private void SetReplacementEnabled(bool active)
		{
			if (this.replacementEnabled)
			{
				this.skeletonAnimation.CustomMaterialOverride[this.originalMaterial] = this.replacementMaterial;
				return;
			}
			this.skeletonAnimation.CustomMaterialOverride.Remove(this.originalMaterial);
		}

		// Token: 0x04004D6F RID: 19823
		public Material originalMaterial;

		// Token: 0x04004D70 RID: 19824
		public Material replacementMaterial;

		// Token: 0x04004D71 RID: 19825
		public bool replacementEnabled = true;

		// Token: 0x04004D72 RID: 19826
		public SkeletonAnimation skeletonAnimation;

		// Token: 0x04004D73 RID: 19827
		[Space]
		public string phasePropertyName = "_FillPhase";

		// Token: 0x04004D74 RID: 19828
		[Range(0f, 1f)]
		public float phase = 1f;

		// Token: 0x04004D75 RID: 19829
		private bool previousEnabled;

		// Token: 0x04004D76 RID: 19830
		private MaterialPropertyBlock mpb;
	}
}
