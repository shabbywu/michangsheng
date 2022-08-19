using System;
using UnityEngine;

namespace WXB
{
	// Token: 0x02000697 RID: 1687
	public class OffsetEffect : IEffect
	{
		// Token: 0x06003551 RID: 13649 RVA: 0x00170814 File Offset: 0x0016EA14
		public void UpdateEffect(Draw draw, float deltaTime)
		{
			if (this.tweener == null)
			{
				this.tweener = new Tweener();
				this.tweener.method = Tweener.Method.EaseInOut;
				this.tweener.style = Tweener.Style.PingPong;
				this.tweener.duration = 1f;
				this.tweener.OnUpdate = new Action<float, bool>(this.UpdateOffset);
			}
			this.current = draw;
			this.tweener.Update(deltaTime);
			this.current = null;
		}

		// Token: 0x06003552 RID: 13650 RVA: 0x00170890 File Offset: 0x0016EA90
		private void UpdateOffset(float val, bool isFin)
		{
			this.offset = Vector2.Lerp(new Vector2(this.xMin, this.yMin), new Vector2(this.xMax, this.yMax), val);
			Tools.UpdateRect(this.current.rectTransform, this.offset);
		}

		// Token: 0x06003553 RID: 13651 RVA: 0x001708E4 File Offset: 0x0016EAE4
		public void Release()
		{
			if (this.tweener != null)
			{
				this.tweener.method = Tweener.Method.EaseInOut;
				this.tweener.style = Tweener.Style.PingPong;
				this.tweener.duration = 1f;
			}
			this.current = null;
			this.xMin = -5f;
			this.yMin = -5f;
			this.xMax = 5f;
			this.yMax = 5f;
			this.speed = 2f;
			this.offset = Vector2.zero;
		}

		// Token: 0x04002EF8 RID: 12024
		private Vector2 offset = Vector2.zero;

		// Token: 0x04002EF9 RID: 12025
		public float xMin = -5f;

		// Token: 0x04002EFA RID: 12026
		public float yMin = -5f;

		// Token: 0x04002EFB RID: 12027
		public float xMax = 5f;

		// Token: 0x04002EFC RID: 12028
		public float yMax = 5f;

		// Token: 0x04002EFD RID: 12029
		public float speed = 2f;

		// Token: 0x04002EFE RID: 12030
		private Tweener tweener;

		// Token: 0x04002EFF RID: 12031
		private Draw current;
	}
}
