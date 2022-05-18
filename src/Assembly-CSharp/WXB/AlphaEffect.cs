using System;
using UnityEngine;

namespace WXB
{
	// Token: 0x0200098C RID: 2444
	public class AlphaEffect : IEffect
	{
		// Token: 0x06003E7A RID: 15994 RVA: 0x001B73D0 File Offset: 0x001B55D0
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

		// Token: 0x06003E7B RID: 15995 RVA: 0x0002CFFD File Offset: 0x0002B1FD
		public void Release()
		{
			this.last_update_time = -1f;
			this.isFoward = false;
			this.space_timer = 0.05f;
			this.alpha = 1f;
		}

		// Token: 0x04003863 RID: 14435
		protected float last_update_time = -1f;

		// Token: 0x04003864 RID: 14436
		protected bool isFoward;

		// Token: 0x04003865 RID: 14437
		protected float space_timer = 0.05f;

		// Token: 0x04003866 RID: 14438
		protected float alpha = 1f;
	}
}
