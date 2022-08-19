using System;
using UnityEngine;

namespace WXB
{
	// Token: 0x020006A4 RID: 1700
	public class Tweener
	{
		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x060035AD RID: 13741 RVA: 0x001719B8 File Offset: 0x0016FBB8
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

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x060035AE RID: 13742 RVA: 0x00171A1B File Offset: 0x0016FC1B
		// (set) Token: 0x060035AF RID: 13743 RVA: 0x00171A23 File Offset: 0x0016FC23
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

		// Token: 0x060035B0 RID: 13744 RVA: 0x00171A34 File Offset: 0x0016FC34
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

		// Token: 0x060035B1 RID: 13745 RVA: 0x00171B80 File Offset: 0x0016FD80
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

		// Token: 0x060035B2 RID: 13746 RVA: 0x00171C38 File Offset: 0x0016FE38
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

		// Token: 0x060035B3 RID: 13747 RVA: 0x00171CBD File Offset: 0x0016FEBD
		public void Play(bool forward)
		{
			this.mAmountPerDelta = Mathf.Abs(this.amountPerDelta);
			if (!forward)
			{
				this.mAmountPerDelta = -this.mAmountPerDelta;
			}
		}

		// Token: 0x060035B4 RID: 13748 RVA: 0x00171CE0 File Offset: 0x0016FEE0
		public void ResetToBeginning()
		{
			this.mFactor = ((this.amountPerDelta < 0f) ? 1f : 0f);
			this.Sample(this.mFactor, false);
		}

		// Token: 0x060035B5 RID: 13749 RVA: 0x00171D0E File Offset: 0x0016FF0E
		public void Toggle()
		{
			if (this.mFactor > 0f)
			{
				this.mAmountPerDelta = -this.amountPerDelta;
				return;
			}
			this.mAmountPerDelta = Mathf.Abs(this.amountPerDelta);
		}

		// Token: 0x04002F15 RID: 12053
		public Tweener.Method method;

		// Token: 0x04002F16 RID: 12054
		public Tweener.Style style;

		// Token: 0x04002F17 RID: 12055
		public float duration = 1f;

		// Token: 0x04002F18 RID: 12056
		private float mDuration;

		// Token: 0x04002F19 RID: 12057
		private float mAmountPerDelta = 1000f;

		// Token: 0x04002F1A RID: 12058
		private float mFactor;

		// Token: 0x04002F1B RID: 12059
		public Action<float, bool> OnUpdate;

		// Token: 0x020014FF RID: 5375
		public enum Method
		{
			// Token: 0x04006DFE RID: 28158
			Linear,
			// Token: 0x04006DFF RID: 28159
			EaseIn,
			// Token: 0x04006E00 RID: 28160
			EaseOut,
			// Token: 0x04006E01 RID: 28161
			EaseInOut,
			// Token: 0x04006E02 RID: 28162
			BounceIn,
			// Token: 0x04006E03 RID: 28163
			BounceOut
		}

		// Token: 0x02001500 RID: 5376
		public enum Style
		{
			// Token: 0x04006E05 RID: 28165
			Once,
			// Token: 0x04006E06 RID: 28166
			Loop,
			// Token: 0x04006E07 RID: 28167
			PingPong
		}
	}
}
