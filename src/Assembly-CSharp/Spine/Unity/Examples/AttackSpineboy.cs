using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Spine.Unity.Examples
{
	// Token: 0x02000E1C RID: 3612
	public class AttackSpineboy : MonoBehaviour
	{
		// Token: 0x0600571B RID: 22299 RVA: 0x00243D84 File Offset: 0x00241F84
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

		// Token: 0x040056D4 RID: 22228
		public SkeletonAnimation spineboy;

		// Token: 0x040056D5 RID: 22229
		public SkeletonAnimation attackerSpineboy;

		// Token: 0x040056D6 RID: 22230
		public SpineGauge gauge;

		// Token: 0x040056D7 RID: 22231
		public Text healthText;

		// Token: 0x040056D8 RID: 22232
		private int currentHealth = 100;

		// Token: 0x040056D9 RID: 22233
		private const int maxHealth = 100;

		// Token: 0x040056DA RID: 22234
		public AnimationReferenceAsset shoot;

		// Token: 0x040056DB RID: 22235
		public AnimationReferenceAsset hit;

		// Token: 0x040056DC RID: 22236
		public AnimationReferenceAsset idle;

		// Token: 0x040056DD RID: 22237
		public AnimationReferenceAsset death;

		// Token: 0x040056DE RID: 22238
		public UnityEvent onAttack;
	}
}
