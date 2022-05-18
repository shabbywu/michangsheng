using System;
using System.Collections.Generic;
using AnimationOrTween;
using UnityEngine;

// Token: 0x020000F2 RID: 242
public abstract class UITweener : MonoBehaviour
{
	// Token: 0x17000147 RID: 327
	// (get) Token: 0x0600095F RID: 2399 RVA: 0x00086D30 File Offset: 0x00084F30
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

	// Token: 0x17000148 RID: 328
	// (get) Token: 0x06000960 RID: 2400 RVA: 0x0000BAC0 File Offset: 0x00009CC0
	// (set) Token: 0x06000961 RID: 2401 RVA: 0x0000BAC8 File Offset: 0x00009CC8
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

	// Token: 0x17000149 RID: 329
	// (get) Token: 0x06000962 RID: 2402 RVA: 0x0000BAD6 File Offset: 0x00009CD6
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

	// Token: 0x06000963 RID: 2403 RVA: 0x0000BAE8 File Offset: 0x00009CE8
	private void Reset()
	{
		if (!this.mStarted)
		{
			this.SetStartToCurrentValue();
			this.SetEndToCurrentValue();
		}
	}

	// Token: 0x06000964 RID: 2404 RVA: 0x0000BAFE File Offset: 0x00009CFE
	protected virtual void Start()
	{
		this.Update();
	}

	// Token: 0x06000965 RID: 2405 RVA: 0x00086D94 File Offset: 0x00084F94
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

	// Token: 0x06000966 RID: 2406 RVA: 0x0000BB06 File Offset: 0x00009D06
	public void SetOnFinished(EventDelegate.Callback del)
	{
		EventDelegate.Set(this.onFinished, del);
	}

	// Token: 0x06000967 RID: 2407 RVA: 0x0000BB15 File Offset: 0x00009D15
	public void SetOnFinished(EventDelegate del)
	{
		EventDelegate.Set(this.onFinished, del);
	}

	// Token: 0x06000968 RID: 2408 RVA: 0x0000BB23 File Offset: 0x00009D23
	public void AddOnFinished(EventDelegate.Callback del)
	{
		EventDelegate.Add(this.onFinished, del);
	}

	// Token: 0x06000969 RID: 2409 RVA: 0x0000BB32 File Offset: 0x00009D32
	public void AddOnFinished(EventDelegate del)
	{
		EventDelegate.Add(this.onFinished, del);
	}

	// Token: 0x0600096A RID: 2410 RVA: 0x0000BB40 File Offset: 0x00009D40
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

	// Token: 0x0600096B RID: 2411 RVA: 0x0000BB6C File Offset: 0x00009D6C
	private void OnDisable()
	{
		this.mStarted = false;
	}

	// Token: 0x0600096C RID: 2412 RVA: 0x0008701C File Offset: 0x0008521C
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

	// Token: 0x0600096D RID: 2413 RVA: 0x00087150 File Offset: 0x00085350
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

	// Token: 0x0600096E RID: 2414 RVA: 0x0000BB75 File Offset: 0x00009D75
	[Obsolete("Use PlayForward() instead")]
	public void Play()
	{
		this.Play(true);
	}

	// Token: 0x0600096F RID: 2415 RVA: 0x0000BB75 File Offset: 0x00009D75
	public void PlayForward()
	{
		this.Play(true);
	}

	// Token: 0x06000970 RID: 2416 RVA: 0x0000BB7E File Offset: 0x00009D7E
	public void PlayReverse()
	{
		this.Play(false);
	}

	// Token: 0x06000971 RID: 2417 RVA: 0x0000BB87 File Offset: 0x00009D87
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

	// Token: 0x06000972 RID: 2418 RVA: 0x0000BBB7 File Offset: 0x00009DB7
	public void ResetToBeginning()
	{
		this.mStarted = false;
		this.mFactor = ((this.amountPerDelta < 0f) ? 1f : 0f);
		this.Sample(this.mFactor, false);
	}

	// Token: 0x06000973 RID: 2419 RVA: 0x0000BBEC File Offset: 0x00009DEC
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

	// Token: 0x06000974 RID: 2420
	protected abstract void OnUpdate(float factor, bool isFinished);

	// Token: 0x06000975 RID: 2421 RVA: 0x000871D8 File Offset: 0x000853D8
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

	// Token: 0x06000976 RID: 2422 RVA: 0x000042DD File Offset: 0x000024DD
	public virtual void SetStartToCurrentValue()
	{
	}

	// Token: 0x06000977 RID: 2423 RVA: 0x000042DD File Offset: 0x000024DD
	public virtual void SetEndToCurrentValue()
	{
	}

	// Token: 0x0400066B RID: 1643
	public static UITweener current;

	// Token: 0x0400066C RID: 1644
	[HideInInspector]
	public UITweener.Method method;

	// Token: 0x0400066D RID: 1645
	[HideInInspector]
	public UITweener.Style style;

	// Token: 0x0400066E RID: 1646
	[HideInInspector]
	public AnimationCurve animationCurve = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 0f, 0f, 1f),
		new Keyframe(1f, 1f, 1f, 0f)
	});

	// Token: 0x0400066F RID: 1647
	[HideInInspector]
	public bool ignoreTimeScale = true;

	// Token: 0x04000670 RID: 1648
	[HideInInspector]
	public float delay;

	// Token: 0x04000671 RID: 1649
	[HideInInspector]
	public float duration = 1f;

	// Token: 0x04000672 RID: 1650
	[HideInInspector]
	public bool steeperCurves;

	// Token: 0x04000673 RID: 1651
	[HideInInspector]
	public int tweenGroup;

	// Token: 0x04000674 RID: 1652
	[HideInInspector]
	public List<EventDelegate> onFinished = new List<EventDelegate>();

	// Token: 0x04000675 RID: 1653
	[HideInInspector]
	public GameObject eventReceiver;

	// Token: 0x04000676 RID: 1654
	[HideInInspector]
	public string callWhenFinished;

	// Token: 0x04000677 RID: 1655
	private bool mStarted;

	// Token: 0x04000678 RID: 1656
	private float mStartTime;

	// Token: 0x04000679 RID: 1657
	private float mDuration;

	// Token: 0x0400067A RID: 1658
	private float mAmountPerDelta = 1000f;

	// Token: 0x0400067B RID: 1659
	private float mFactor;

	// Token: 0x0400067C RID: 1660
	private List<EventDelegate> mTemp;

	// Token: 0x020000F3 RID: 243
	public enum Method
	{
		// Token: 0x0400067E RID: 1662
		Linear,
		// Token: 0x0400067F RID: 1663
		EaseIn,
		// Token: 0x04000680 RID: 1664
		EaseOut,
		// Token: 0x04000681 RID: 1665
		EaseInOut,
		// Token: 0x04000682 RID: 1666
		BounceIn,
		// Token: 0x04000683 RID: 1667
		BounceOut
	}

	// Token: 0x020000F4 RID: 244
	public enum Style
	{
		// Token: 0x04000685 RID: 1669
		Once,
		// Token: 0x04000686 RID: 1670
		Loop,
		// Token: 0x04000687 RID: 1671
		PingPong
	}
}
