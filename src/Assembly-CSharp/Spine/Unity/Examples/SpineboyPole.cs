using System;
using System.Collections;
using Spine.Unity.Modules;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000B03 RID: 2819
	public class SpineboyPole : MonoBehaviour
	{
		// Token: 0x06004E7A RID: 20090 RVA: 0x002171B9 File Offset: 0x002153B9
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

		// Token: 0x06004E7B RID: 20091 RVA: 0x002171C8 File Offset: 0x002153C8
		private void SetXPosition(float x)
		{
			Vector3 localPosition = base.transform.localPosition;
			localPosition.x = x;
			base.transform.localPosition = localPosition;
		}

		// Token: 0x04004E20 RID: 20000
		public SkeletonAnimation skeletonAnimation;

		// Token: 0x04004E21 RID: 20001
		public SkeletonRenderSeparator separator;

		// Token: 0x04004E22 RID: 20002
		[Space(18f)]
		public AnimationReferenceAsset run;

		// Token: 0x04004E23 RID: 20003
		public AnimationReferenceAsset pole;

		// Token: 0x04004E24 RID: 20004
		public float startX;

		// Token: 0x04004E25 RID: 20005
		public float endX;

		// Token: 0x04004E26 RID: 20006
		private const float Speed = 18f;

		// Token: 0x04004E27 RID: 20007
		private const float RunTimeScale = 1.5f;
	}
}
