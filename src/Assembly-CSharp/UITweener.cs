using System;
using System.Collections.Generic;
using AnimationOrTween;
using UnityEngine;

// Token: 0x020000A1 RID: 161
public abstract class UITweener : MonoBehaviour
{
	// Token: 0x17000133 RID: 307
	// (get) Token: 0x060008A7 RID: 2215 RVA: 0x00032DE0 File Offset: 0x00030FE0
	public float amountPerDelta
	{
		get
		{
			if (this.mDuration != this.duration)
			{
				this.mDuration = this.duration;
				this.mAmountPerDelta = Mathf.Abs((this.duration > 0f) ? (1f / this.duration) : 1000f) * Mathf.Sign(this.mAmountPerDelta);
			}
			return this.mAmountPerDelta;
		}
	}

	// Token: 0x17000134 RID: 308
	// (get) Token: 0x060008A8 RID: 2216 RVA: 0x00032E44 File Offset: 0x00031044
	// (set) Token: 0x060008A9 RID: 2217 RVA: 0x00032E4C File Offset: 0x0003104C
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

	// Token: 0x17000135 RID: 309
	// (get) Token: 0x060008AA RID: 2218 RVA: 0x00032E5A File Offset: 0x0003105A
	public Direction direction
	{
		get
		{
			if (this.amountPerDelta >= 0f)
			{
				return Direction.Forward;
			}
			return Direction.Reverse;
		}
	}

	// Token: 0x060008AB RID: 2219 RVA: 0x00032E6C File Offset: 0x0003106C
	private void Reset()
	{
		if (!this.mStarted)
		{
			this.SetStartToCurrentValue();
			this.SetEndToCurrentValue();
		}
	}

	// Token: 0x060008AC RID: 2220 RVA: 0x00032E82 File Offset: 0x00031082
	protected virtual void Start()
	{
		this.Update();
	}

	// Token: 0x060008AD RID: 2221 RVA: 0x00032E8C File Offset: 0x0003108C
	private void Update()
	{
		float num = this.ignoreTimeScale ? RealTime.deltaTime : Time.deltaTime;
		float num2 = this.ignoreTimeScale ? RealTime.time : Time.time;
		if (!this.mStarted)
		{
			this.mStarted = true;
			this.mStartTime = num2 + this.delay;
		}
		if (num2 < this.mStartTime)
		{
			return;
		}
		this.mFactor += this.amountPerDelta * num;
		if (this.style == UITweener.Style.Loop)
		{
			if (this.mFactor > 1f)
			{
				this.mFactor -= Mathf.Floor(this.mFactor);
			}
		}
		else if (this.style == UITweener.Style.PingPong)
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
		if (this.style == UITweener.Style.Once && (this.duration == 0f || this.mFactor > 1f || this.mFactor < 0f))
		{
			this.mFactor = Mathf.Clamp01(this.mFactor);
			this.Sample(this.mFactor, true);
			if (this.duration == 0f || (this.mFactor == 1f && this.mAmountPerDelta > 0f) || (this.mFactor == 0f && this.mAmountPerDelta < 0f))
			{
				base.enabled = false;
			}
			if (UITweener.current == null)
			{
				UITweener.current = this;
				if (this.onFinished != null)
				{
					this.mTemp = this.onFinished;
					this.onFinished = new List<EventDelegate>();
					EventDelegate.Execute(this.mTemp);
					for (int i = 0; i < this.mTemp.Count; i++)
					{
						EventDelegate eventDelegate = this.mTemp[i];
						if (eventDelegate != null)
						{
							EventDelegate.Add(this.onFinished, eventDelegate, eventDelegate.oneShot);
						}
					}
					this.mTemp = null;
				}
				if (this.eventReceiver != null && !string.IsNullOrEmpty(this.callWhenFinished))
				{
					this.eventReceiver.SendMessage(this.callWhenFinished, this, 1);
				}
				UITweener.current = null;
				return;
			}
		}
		else
		{
			this.Sample(this.mFactor, false);
		}
	}

	// Token: 0x060008AE RID: 2222 RVA: 0x00033113 File Offset: 0x00031313
	public void SetOnFinished(EventDelegate.Callback del)
	{
		EventDelegate.Set(this.onFinished, del);
	}

	// Token: 0x060008AF RID: 2223 RVA: 0x00033122 File Offset: 0x00031322
	public void SetOnFinished(EventDelegate del)
	{
		EventDelegate.Set(this.onFinished, del);
	}

	// Token: 0x060008B0 RID: 2224 RVA: 0x00033130 File Offset: 0x00031330
	public void AddOnFinished(EventDelegate.Callback del)
	{
		EventDelegate.Add(this.onFinished, del);
	}

	// Token: 0x060008B1 RID: 2225 RVA: 0x0003313F File Offset: 0x0003133F
	public void AddOnFinished(EventDelegate del)
	{
		EventDelegate.Add(this.onFinished, del);
	}

	// Token: 0x060008B2 RID: 2226 RVA: 0x0003314D File Offset: 0x0003134D
	public void RemoveOnFinished(EventDelegate del)
	{
		if (this.onFinished != null)
		{
			this.onFinished.Remove(del);
		}
		if (this.mTemp != null)
		{
			this.mTemp.Remove(del);
		}
	}

	// Token: 0x060008B3 RID: 2227 RVA: 0x00033179 File Offset: 0x00031379
	private void OnDisable()
	{
		this.mStarted = false;
	}

	// Token: 0x060008B4 RID: 2228 RVA: 0x00033184 File Offset: 0x00031384
	public void Sample(float factor, bool isFinished)
	{
		float num = Mathf.Clamp01(factor);
		if (this.method == UITweener.Method.EaseIn)
		{
			num = 1f - Mathf.Sin(1.5707964f * (1f - num));
			if (this.steeperCurves)
			{
				num *= num;
			}
		}
		else if (this.method == UITweener.Method.EaseOut)
		{
			num = Mathf.Sin(1.5707964f * num);
			if (this.steeperCurves)
			{
				num = 1f - num;
				num = 1f - num * num;
			}
		}
		else if (this.method == UITweener.Method.EaseInOut)
		{
			num -= Mathf.Sin(num * 6.2831855f) / 6.2831855f;
			if (this.steeperCurves)
			{
				num = num * 2f - 1f;
				float num2 = Mathf.Sign(num);
				num = 1f - Mathf.Abs(num);
				num = 1f - num * num;
				num = num2 * num * 0.5f + 0.5f;
			}
		}
		else if (this.method == UITweener.Method.BounceIn)
		{
			num = this.BounceLogic(num);
		}
		else if (this.method == UITweener.Method.BounceOut)
		{
			num = 1f - this.BounceLogic(1f - num);
		}
		this.OnUpdate((this.animationCurve != null) ? this.animationCurve.Evaluate(num) : num, isFinished);
	}

	// Token: 0x060008B5 RID: 2229 RVA: 0x000332B8 File Offset: 0x000314B8
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

	// Token: 0x060008B6 RID: 2230 RVA: 0x0003333D File Offset: 0x0003153D
	[Obsolete("Use PlayForward() instead")]
	public void Play()
	{
		this.Play(true);
	}

	// Token: 0x060008B7 RID: 2231 RVA: 0x0003333D File Offset: 0x0003153D
	public void PlayForward()
	{
		this.Play(true);
	}

	// Token: 0x060008B8 RID: 2232 RVA: 0x00033346 File Offset: 0x00031546
	public void PlayReverse()
	{
		this.Play(false);
	}

	// Token: 0x060008B9 RID: 2233 RVA: 0x0003334F File Offset: 0x0003154F
	public void Play(bool forward)
	{
		this.mAmountPerDelta = Mathf.Abs(this.amountPerDelta);
		if (!forward)
		{
			this.mAmountPerDelta = -this.mAmountPerDelta;
		}
		base.enabled = true;
		this.Update();
	}

	// Token: 0x060008BA RID: 2234 RVA: 0x0003337F File Offset: 0x0003157F
	public void ResetToBeginning()
	{
		this.mStarted = false;
		this.mFactor = ((this.amountPerDelta < 0f) ? 1f : 0f);
		this.Sample(this.mFactor, false);
	}

	// Token: 0x060008BB RID: 2235 RVA: 0x000333B4 File Offset: 0x000315B4
	public void Toggle()
	{
		if (this.mFactor > 0f)
		{
			this.mAmountPerDelta = -this.amountPerDelta;
		}
		else
		{
			this.mAmountPerDelta = Mathf.Abs(this.amountPerDelta);
		}
		base.enabled = true;
	}

	// Token: 0x060008BC RID: 2236
	protected abstract void OnUpdate(float factor, bool isFinished);

	// Token: 0x060008BD RID: 2237 RVA: 0x000333EC File Offset: 0x000315EC
	public static T Begin<T>(GameObject go, float duration) where T : UITweener
	{
		T t = go.GetComponent<T>();
		if (t != null && t.tweenGroup != 0)
		{
			t = default(T);
			T[] components = go.GetComponents<T>();
			int i = 0;
			int num = components.Length;
			while (i < num)
			{
				t = components[i];
				if (t != null && t.tweenGroup == 0)
				{
					break;
				}
				t = default(T);
				i++;
			}
		}
		if (t == null)
		{
			t = go.AddComponent<T>();
		}
		t.mStarted = false;
		t.duration = duration;
		t.mFactor = 0f;
		t.mAmountPerDelta = Mathf.Abs(t.amountPerDelta);
		t.style = UITweener.Style.Once;
		t.animationCurve = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0f, 0f, 1f),
			new Keyframe(1f, 1f, 1f, 0f)
		});
		t.eventReceiver = null;
		t.callWhenFinished = null;
		t.enabled = true;
		if (duration <= 0f)
		{
			t.Sample(1f, true);
			t.enabled = false;
		}
		return t;
	}

	// Token: 0x060008BE RID: 2238 RVA: 0x00004095 File Offset: 0x00002295
	public virtual void SetStartToCurrentValue()
	{
	}

	// Token: 0x060008BF RID: 2239 RVA: 0x00004095 File Offset: 0x00002295
	public virtual void SetEndToCurrentValue()
	{
	}

	// Token: 0x04000548 RID: 1352
	public static UITweener current;

	// Token: 0x04000549 RID: 1353
	[HideInInspector]
	public UITweener.Method method;

	// Token: 0x0400054A RID: 1354
	[HideInInspector]
	public UITweener.Style style;

	// Token: 0x0400054B RID: 1355
	[HideInInspector]
	public AnimationCurve animationCurve = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 0f, 0f, 1f),
		new Keyframe(1f, 1f, 1f, 0f)
	});

	// Token: 0x0400054C RID: 1356
	[HideInInspector]
	public bool ignoreTimeScale = true;

	// Token: 0x0400054D RID: 1357
	[HideInInspector]
	public float delay;

	// Token: 0x0400054E RID: 1358
	[HideInInspector]
	public float duration = 1f;

	// Token: 0x0400054F RID: 1359
	[HideInInspector]
	public bool steeperCurves;

	// Token: 0x04000550 RID: 1360
	[HideInInspector]
	public int tweenGroup;

	// Token: 0x04000551 RID: 1361
	[HideInInspector]
	public List<EventDelegate> onFinished = new List<EventDelegate>();

	// Token: 0x04000552 RID: 1362
	[HideInInspector]
	public GameObject eventReceiver;

	// Token: 0x04000553 RID: 1363
	[HideInInspector]
	public string callWhenFinished;

	// Token: 0x04000554 RID: 1364
	private bool mStarted;

	// Token: 0x04000555 RID: 1365
	private float mStartTime;

	// Token: 0x04000556 RID: 1366
	private float mDuration;

	// Token: 0x04000557 RID: 1367
	private float mAmountPerDelta = 1000f;

	// Token: 0x04000558 RID: 1368
	private float mFactor;

	// Token: 0x04000559 RID: 1369
	private List<EventDelegate> mTemp;

	// Token: 0x02001215 RID: 4629
	public enum Method
	{
		// Token: 0x04006473 RID: 25715
		Linear,
		// Token: 0x04006474 RID: 25716
		EaseIn,
		// Token: 0x04006475 RID: 25717
		EaseOut,
		// Token: 0x04006476 RID: 25718
		EaseInOut,
		// Token: 0x04006477 RID: 25719
		BounceIn,
		// Token: 0x04006478 RID: 25720
		BounceOut
	}

	// Token: 0x02001216 RID: 4630
	public enum Style
	{
		// Token: 0x0400647A RID: 25722
		Once,
		// Token: 0x0400647B RID: 25723
		Loop,
		// Token: 0x0400647C RID: 25724
		PingPong
	}
}
