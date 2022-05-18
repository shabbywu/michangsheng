using System;
using UnityEngine;

namespace WXB
{
	// Token: 0x020009AB RID: 2475
	public class OffsetEffect : IEffect
	{
		// Token: 0x06003F0F RID: 16143 RVA: 0x001B894C File Offset: 0x001B6B4C
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

		// Token: 0x06003F10 RID: 16144 RVA: 0x001B89C8 File Offset: 0x001B6BC8
		private void UpdateOffset(float val, bool isFin)
		{
			this.offset = Vector2.Lerp(new Vector2(this.xMin, this.yMin), new Vector2(this.xMax, this.yMax), val);
			Tools.UpdateRect(this.current.rectTransform, this.offset);
		}

		// Token: 0x06003F11 RID: 16145 RVA: 0x001B8A1C File Offset: 0x001B6C1C
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

		// Token: 0x040038B1 RID: 14513
		private Vector2 offset = Vector2.zero;

		// Token: 0x040038B2 RID: 14514
		public float xMin = -5f;

		// Token: 0x040038B3 RID: 14515
		public float yMin = -5f;

		// Token: 0x040038B4 RID: 14516
		public float xMax = 5f;

		// Token: 0x040038B5 RID: 14517
		public float yMax = 5f;

		// Token: 0x040038B6 RID: 14518
		public float speed = 2f;

		// Token: 0x040038B7 RID: 14519
		private Tweener tweener;

		// Token: 0x040038B8 RID: 14520
		private Draw current;
	}
}
