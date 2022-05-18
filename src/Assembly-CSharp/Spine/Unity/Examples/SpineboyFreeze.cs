using System;
using System.Collections;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000E60 RID: 3680
	public class SpineboyFreeze : MonoBehaviour
	{
		// Token: 0x06005830 RID: 22576 RVA: 0x0003F0DD File Offset: 0x0003D2DD
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

		// Token: 0x04005856 RID: 22614
		public SkeletonAnimation skeletonAnimation;

		// Token: 0x04005857 RID: 22615
		public AnimationReferenceAsset freeze;

		// Token: 0x04005858 RID: 22616
		public AnimationReferenceAsset idle;

		// Token: 0x04005859 RID: 22617
		public Color freezeColor;

		// Token: 0x0400585A RID: 22618
		public Color freezeBlackColor;

		// Token: 0x0400585B RID: 22619
		public ParticleSystem particles;

		// Token: 0x0400585C RID: 22620
		public float freezePoint = 0.5f;

		// Token: 0x0400585D RID: 22621
		public string colorProperty = "_Color";

		// Token: 0x0400585E RID: 22622
		public string blackTintProperty = "_Black";

		// Token: 0x0400585F RID: 22623
		private MaterialPropertyBlock block;

		// Token: 0x04005860 RID: 22624
		private MeshRenderer meshRenderer;
	}
}
