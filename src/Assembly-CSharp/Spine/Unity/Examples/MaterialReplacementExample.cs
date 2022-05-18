using System;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000E37 RID: 3639
	public class MaterialReplacementExample : MonoBehaviour
	{
		// Token: 0x06005789 RID: 22409 RVA: 0x0003E960 File Offset: 0x0003CB60
		private void Start()
		{
			this.previousEnabled = this.replacementEnabled;
			this.SetReplacementEnabled(this.replacementEnabled);
			this.mpb = new MaterialPropertyBlock();
		}

		// Token: 0x0600578A RID: 22410 RVA: 0x00245338 File Offset: 0x00243538
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

		// Token: 0x0600578B RID: 22411 RVA: 0x0003E985 File Offset: 0x0003CB85
		private void SetReplacementEnabled(bool active)
		{
			if (this.replacementEnabled)
			{
				this.skeletonAnimation.CustomMaterialOverride[this.originalMaterial] = this.replacementMaterial;
				return;
			}
			this.skeletonAnimation.CustomMaterialOverride.Remove(this.originalMaterial);
		}

		// Token: 0x0400577B RID: 22395
		public Material originalMaterial;

		// Token: 0x0400577C RID: 22396
		public Material replacementMaterial;

		// Token: 0x0400577D RID: 22397
		public bool replacementEnabled = true;

		// Token: 0x0400577E RID: 22398
		public SkeletonAnimation skeletonAnimation;

		// Token: 0x0400577F RID: 22399
		[Space]
		public string phasePropertyName = "_FillPhase";

		// Token: 0x04005780 RID: 22400
		[Range(0f, 1f)]
		public float phase = 1f;

		// Token: 0x04005781 RID: 22401
		private bool previousEnabled;

		// Token: 0x04005782 RID: 22402
		private MaterialPropertyBlock mpb;
	}
}
