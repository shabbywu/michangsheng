using System;
using UnityEngine;

namespace WXB
{
	// Token: 0x020009B9 RID: 2489
	public class Tweener
	{
		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x06003F6E RID: 16238 RVA: 0x001B9718 File Offset: 0x001B7918
		public float amountPerDelta
		{
			get
			{
				if (this.duration == 0f)
				{
					return 1000f;
				}
				if (this.mDuration != this.duration)
				{
					this.mDuration = this.duration;
					this.mAmountPerDelta = Mathf.Abs(1f / this.duration) * Mathf.Sign(this.mAmountPerDelta);
				}
				return this.mAmountPerDelta;
			}
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x06003F6F RID: 16239 RVA: 0x0002D921 File Offset: 0x0002BB21
		// (set) Token: 0x06003F70 RID: 16240 RVA: 0x0002D929 File Offset: 0x0002BB29
		public float tweenFactor
		{
			get
			{
				return this.mFactor;
			}
			set
			{
				this.mFactor = Mathf.Clamp01(value);
			}
		}

		// Token: 0x06003F71 RID: 16241 RVA: 0x001B977C File Offset: 0x001B797C
		public void Update(float delta)
		{
			this.mFactor += ((this.duration == 0f) ? 1f : (this.amountPerDelta * delta));
			if (this.style == Tweener.Style.Loop)
			{
				if (this.mFactor > 1f)
				{
					this.mFactor -= Mathf.Floor(this.mFactor);
				}
			}
			else if (this.style == Tweener.Style.PingPong)
			{
				if (this.mFactor > 1f)
				{
					this.mFactor = 1f - (this.mFactor - Mathf.Floor(this.mFactor));
					this.mAmountPerDelta = -this.mAmountPerDelta;
				}
				else if (this.mFactor < 0f)
				{
					this.mFactor = -this.mFactor;
					this.mFactor -= Mathf.Floor(this.mFactor);
					this.mAmountPerDelta = -this.mAmountPerDelta;
				}
			}
			if (this.style == Tweener.Style.Once && (this.duration == 0f || this.mFactor > 1f || this.mFactor < 0f))
			{
				this.mFactor = Mathf.Clamp01(this.mFactor);
				this.Sample(this.mFactor, true);
				return;
			}
			this.Sample(this.mFactor, false);
		}

		// Token: 0x06003F72 RID: 16242 RVA: 0x001B98C8 File Offset: 0x001B7AC8
		public void Sample(float factor, bool isFinished)
		{
			float num = Mathf.Clamp01(factor);
			if (this.method == Tweener.Method.EaseIn)
			{
				num = 1f - Mathf.Sin(1.5707964f * (1f - num));
			}
			else if (this.method == Tweener.Method.EaseOut)
			{
				num = Mathf.Sin(1.5707964f * num);
			}
			else if (this.method == Tweener.Method.EaseInOut)
			{
				num -= Mathf.Sin(num * 6.2831855f) / 6.2831855f;
			}
			else if (this.method == Tweener.Method.BounceIn)
			{
				num = this.BounceLogic(num);
			}
			else if (this.method == Tweener.Method.BounceOut)
			{
				num = 1f - this.BounceLogic(1f - num);
			}
			if (this.OnUpdate != null)
			{
				this.OnUpdate(num, isFinished);
			}
		}

		// Token: 0x06003F73 RID: 16243 RVA: 0x00087150 File Offset: 0x00085350
		private float BounceLogic(float val)
		{
			if (val < 0.363636f)
			{
				val = 7.5685f * val * val;
			}
			else if (val < 0.727272f)
			{
				val = 7.5625f * (val -= 0.545454f) * val + 0.75f;
			}
			else if (val < 0.90909f)
			{
				val = 7.5625f * (val -= 0.818181f) * val + 0.9375f;
			}
			else
			{
				val = 7.5625f * (val -= 0.9545454f) * val + 0.984375f;
			}
			return val;
		}

		// Token: 0x06003F74 RID: 16244 RVA: 0x0002D937 File Offset: 0x0002BB37
		public void Play(bool forward)
		{
			this.mAmountPerDelta = Mathf.Abs(this.amountPerDelta);
			if (!forward)
			{
				this.mAmountPerDelta = -this.mAmountPerDelta;
			}
		}

		// Token: 0x06003F75 RID: 16245 RVA: 0x0002D95A File Offset: 0x0002BB5A
		public void ResetToBeginning()
		{
			this.mFactor = ((this.amountPerDelta < 0f) ? 1f : 0f);
			this.Sample(this.mFactor, false);
		}

		// Token: 0x06003F76 RID: 16246 RVA: 0x0002D988 File Offset: 0x0002BB88
		public void Toggle()
		{
			if (this.mFactor > 0f)
			{
				this.mAmountPerDelta = -this.amountPerDelta;
				return;
			}
			this.mAmountPerDelta = Mathf.Abs(this.amountPerDelta);
		}

		// Token: 0x040038D0 RID: 14544
		public Tweener.Method method;

		// Token: 0x040038D1 RID: 14545
		public Tweener.Style style;

		// Token: 0x040038D2 RID: 14546
		public float duration = 1f;

		// Token: 0x040038D3 RID: 14547
		private float mDuration;

		// Token: 0x040038D4 RID: 14548
		private float mAmountPerDelta = 1000f;

		// Token: 0x040038D5 RID: 14549
		private float mFactor;

		// Token: 0x040038D6 RID: 14550
		public Action<float, bool> OnUpdate;

		// Token: 0x020009BA RID: 2490
		public enum Method
		{
			// Token: 0x040038D8 RID: 14552
			Linear,
			// Token: 0x040038D9 RID: 14553
			EaseIn,
			// Token: 0x040038DA RID: 14554
			EaseOut,
			// Token: 0x040038DB RID: 14555
			EaseInOut,
			// Token: 0x040038DC RID: 14556
			BounceIn,
			// Token: 0x040038DD RID: 14557
			BounceOut
		}

		// Token: 0x020009BB RID: 2491
		public enum Style
		{
			// Token: 0x040038DF RID: 14559
			Once,
			// Token: 0x040038E0 RID: 14560
			Loop,
			// Token: 0x040038E1 RID: 14561
			PingPong
		}
	}
}
