using System;
using System.Collections;
using Spine.Unity.Modules;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000E62 RID: 3682
	public class SpineboyPole : MonoBehaviour
	{
		// Token: 0x06005838 RID: 22584 RVA: 0x0003F12C File Offset: 0x0003D32C
		private IEnumerator Start()
		{
			AnimationState state = this.skeletonAnimation.state;
			for (;;)
			{
				this.SetXPosition(this.startX);
				this.separator.enabled = false;
				state.SetAnimation(0, this.run, true);
				state.TimeScale = 1.5f;
				while (base.transform.localPosition.x < this.endX)
				{
					base.transform.Translate(Vector3.right * 18f * Time.deltaTime);
					yield return null;
				}
				this.SetXPosition(this.endX);
				this.separator.enabled = true;
				TrackEntry trackEntry = state.SetAnimation(0, this.pole, false);
				yield return new WaitForSpineAnimationComplete(trackEntry);
				yield return new WaitForSeconds(1f);
			}
			yield break;
		}

		// Token: 0x06005839 RID: 22585 RVA: 0x00247500 File Offset: 0x00245700
		private void SetXPosition(float x)
		{
			Vector3 localPosition = base.transform.localPosition;
			localPosition.x = x;
			base.transform.localPosition = localPosition;
		}

		// Token: 0x04005865 RID: 22629
		public SkeletonAnimation skeletonAnimation;

		// Token: 0x04005866 RID: 22630
		public SkeletonRenderSeparator separator;

		// Token: 0x04005867 RID: 22631
		[Space(18f)]
		public AnimationReferenceAsset run;

		// Token: 0x04005868 RID: 22632
		public AnimationReferenceAsset pole;

		// Token: 0x04005869 RID: 22633
		public float startX;

		// Token: 0x0400586A RID: 22634
		public float endX;

		// Token: 0x0400586B RID: 22635
		private const float Speed = 18f;

		// Token: 0x0400586C RID: 22636
		private const float RunTimeScale = 1.5f;
	}
}
