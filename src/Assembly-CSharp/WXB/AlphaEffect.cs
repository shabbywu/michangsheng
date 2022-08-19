using System;
using UnityEngine;

namespace WXB
{
	// Token: 0x0200067F RID: 1663
	public class AlphaEffect : IEffect
	{
		// Token: 0x060034CA RID: 13514 RVA: 0x0016EFD0 File Offset: 0x0016D1D0
		public void UpdateEffect(Draw draw, float deltaTime)
		{
			if (this.last_update_time == -1f)
			{
				this.last_update_time = Time.realtimeSinceStartup;
				return;
			}
			float realtimeSinceStartup = Time.realtimeSinceStartup;
			float num = realtimeSinceStartup - this.last_update_time;
			if (num <= this.space_timer)
			{
				return;
			}
			this.space_timer = 0.05f;
			num = this.space_timer;
			this.last_update_time = realtimeSinceStartup;
			if (this.isFoward)
			{
				this.alpha += num;
				if (this.alpha > 1f)
				{
					this.alpha = 1f;
					this.isFoward = false;
					this.space_timer = 0.5f;
				}
			}
			else
			{
				this.alpha -= num;
				if (this.alpha < 0f)
				{
					this.alpha = -this.alpha;
					this.isFoward = true;
				}
			}
			draw.canvasRenderer.SetAlpha(this.alpha);
		}

		// Token: 0x060034CB RID: 13515 RVA: 0x0016F0AA File Offset: 0x0016D2AA
		public void Release()
		{
			this.last_update_time = -1f;
			this.isFoward = false;
			this.space_timer = 0.05f;
			this.alpha = 1f;
		}

		// Token: 0x04002EBD RID: 11965
		protected float last_update_time = -1f;

		// Token: 0x04002EBE RID: 11966
		protected bool isFoward;

		// Token: 0x04002EBF RID: 11967
		protected float space_timer = 0.05f;

		// Token: 0x04002EC0 RID: 11968
		protected float alpha = 1f;
	}
}
