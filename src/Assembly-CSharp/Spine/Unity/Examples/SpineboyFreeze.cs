using System;
using System.Collections;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000B02 RID: 2818
	public class SpineboyFreeze : MonoBehaviour
	{
		// Token: 0x06004E78 RID: 20088 RVA: 0x00217181 File Offset: 0x00215381
		private IEnumerator Start()
		{
			this.block = new MaterialPropertyBlock();
			this.meshRenderer = base.GetComponent<MeshRenderer>();
			this.particles.Stop();
			this.particles.Clear();
			this.particles.main.loop = false;
			AnimationState state = this.skeletonAnimation.AnimationState;
			for (;;)
			{
				yield return new WaitForSeconds(1f);
				state.SetAnimation(0, this.freeze, false);
				yield return new WaitForSeconds(this.freezePoint);
				this.particles.Play();
				this.block.SetColor(this.colorProperty, this.freezeColor);
				this.block.SetColor(this.blackTintProperty, this.freezeBlackColor);
				this.meshRenderer.SetPropertyBlock(this.block);
				yield return new WaitForSeconds(2f);
				state.SetAnimation(0, this.idle, true);
				this.block.SetColor(this.colorProperty, Color.white);
				this.block.SetColor(this.blackTintProperty, Color.black);
				this.meshRenderer.SetPropertyBlock(this.block);
				yield return null;
			}
			yield break;
		}

		// Token: 0x04004E15 RID: 19989
		public SkeletonAnimation skeletonAnimation;

		// Token: 0x04004E16 RID: 19990
		public AnimationReferenceAsset freeze;

		// Token: 0x04004E17 RID: 19991
		public AnimationReferenceAsset idle;

		// Token: 0x04004E18 RID: 19992
		public Color freezeColor;

		// Token: 0x04004E19 RID: 19993
		public Color freezeBlackColor;

		// Token: 0x04004E1A RID: 19994
		public ParticleSystem particles;

		// Token: 0x04004E1B RID: 19995
		public float freezePoint = 0.5f;

		// Token: 0x04004E1C RID: 19996
		public string colorProperty = "_Color";

		// Token: 0x04004E1D RID: 19997
		public string blackTintProperty = "_Black";

		// Token: 0x04004E1E RID: 19998
		private MaterialPropertyBlock block;

		// Token: 0x04004E1F RID: 19999
		private MeshRenderer meshRenderer;
	}
}
