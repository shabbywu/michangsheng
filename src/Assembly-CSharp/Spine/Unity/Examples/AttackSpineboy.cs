using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Spine.Unity.Examples
{
	// Token: 0x02000AD6 RID: 2774
	public class AttackSpineboy : MonoBehaviour
	{
		// Token: 0x06004DC3 RID: 19907 RVA: 0x00213EC4 File Offset: 0x002120C4
		private void Update()
		{
			if (Input.GetKeyDown(32))
			{
				this.currentHealth -= 10;
				this.healthText.text = this.currentHealth + "/" + 100;
				this.attackerSpineboy.AnimationState.SetAnimation(1, this.shoot, false);
				this.attackerSpineboy.AnimationState.AddEmptyAnimation(1, 0.5f, 2f);
				if (this.currentHealth > 0)
				{
					this.spineboy.AnimationState.SetAnimation(0, this.hit, false);
					this.spineboy.AnimationState.AddAnimation(0, this.idle, true, 0f);
					this.gauge.fillPercent = (float)this.currentHealth / 100f;
					this.onAttack.Invoke();
					return;
				}
				if (this.currentHealth >= 0)
				{
					this.gauge.fillPercent = 0f;
					this.spineboy.AnimationState.SetAnimation(0, this.death, false).TrackEnd = float.PositiveInfinity;
				}
			}
		}

		// Token: 0x04004CE9 RID: 19689
		public SkeletonAnimation spineboy;

		// Token: 0x04004CEA RID: 19690
		public SkeletonAnimation attackerSpineboy;

		// Token: 0x04004CEB RID: 19691
		public SpineGauge gauge;

		// Token: 0x04004CEC RID: 19692
		public Text healthText;

		// Token: 0x04004CED RID: 19693
		private int currentHealth = 100;

		// Token: 0x04004CEE RID: 19694
		private const int maxHealth = 100;

		// Token: 0x04004CEF RID: 19695
		public AnimationReferenceAsset shoot;

		// Token: 0x04004CF0 RID: 19696
		public AnimationReferenceAsset hit;

		// Token: 0x04004CF1 RID: 19697
		public AnimationReferenceAsset idle;

		// Token: 0x04004CF2 RID: 19698
		public AnimationReferenceAsset death;

		// Token: 0x04004CF3 RID: 19699
		public UnityEvent onAttack;
	}
}
