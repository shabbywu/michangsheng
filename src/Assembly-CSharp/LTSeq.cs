using System;
using UnityEngine;

// Token: 0x02000023 RID: 35
public class LTSeq
{
	// Token: 0x17000039 RID: 57
	// (get) Token: 0x060002DC RID: 732 RVA: 0x0000EE96 File Offset: 0x0000D096
	public int id
	{
		get
		{
			return (int)(this._id | this.counter << 16);
		}
	}

	// Token: 0x060002DD RID: 733 RVA: 0x0000EEA8 File Offset: 0x0000D0A8
	public void reset()
	{
		this.previous = null;
		this.tween = null;
		this.totalDelay = 0f;
	}

	// Token: 0x060002DE RID: 734 RVA: 0x0000EEC3 File Offset: 0x0000D0C3
	public void init(uint id, uint global_counter)
	{
		this.reset();
		this._id = id;
		this.counter = global_counter;
		this.current = this;
	}

	// Token: 0x060002DF RID: 735 RVA: 0x0000EEE0 File Offset: 0x0000D0E0
	private LTSeq addOn()
	{
		this.current.toggle = true;
		LTSeq ltseq = this.current;
		this.current = LeanTween.sequence(true);
		Debug.Log(string.Concat(new object[]
		{
			"this.current:",
			this.current.id,
			" lastCurrent:",
			ltseq.id
		}));
		this.current.previous = ltseq;
		ltseq.toggle = false;
		this.current.totalDelay = ltseq.totalDelay;
		this.current.debugIter = ltseq.debugIter + 1;
		return this.current;
	}

	// Token: 0x060002E0 RID: 736 RVA: 0x0000EF8C File Offset: 0x0000D18C
	private float addPreviousDelays()
	{
		LTSeq ltseq = this.current.previous;
		if (ltseq != null && ltseq.tween != null)
		{
			return this.current.totalDelay + ltseq.tween.time;
		}
		return this.current.totalDelay;
	}

	// Token: 0x060002E1 RID: 737 RVA: 0x0000EFD3 File Offset: 0x0000D1D3
	public LTSeq append(float delay)
	{
		this.current.totalDelay += delay;
		return this.current;
	}

	// Token: 0x060002E2 RID: 738 RVA: 0x0000EFF0 File Offset: 0x0000D1F0
	public LTSeq append(Action callback)
	{
		LTDescr ltdescr = LeanTween.delayedCall(0f, callback);
		this.append(ltdescr);
		return this.addOn();
	}

	// Token: 0x060002E3 RID: 739 RVA: 0x0000F017 File Offset: 0x0000D217
	public LTSeq append(Action<object> callback, object obj)
	{
		this.append(LeanTween.delayedCall(0f, callback).setOnCompleteParam(obj));
		return this.addOn();
	}

	// Token: 0x060002E4 RID: 740 RVA: 0x0000F037 File Offset: 0x0000D237
	public LTSeq append(GameObject gameObject, Action callback)
	{
		this.append(LeanTween.delayedCall(gameObject, 0f, callback));
		return this.addOn();
	}

	// Token: 0x060002E5 RID: 741 RVA: 0x0000F052 File Offset: 0x0000D252
	public LTSeq append(GameObject gameObject, Action<object> callback, object obj)
	{
		this.append(LeanTween.delayedCall(gameObject, 0f, callback).setOnCompleteParam(obj));
		return this.addOn();
	}

	// Token: 0x060002E6 RID: 742 RVA: 0x0000F073 File Offset: 0x0000D273
	public LTSeq append(LTDescr tween)
	{
		this.current.tween = tween;
		this.current.totalDelay = this.addPreviousDelays();
		tween.setDelay(this.current.totalDelay);
		return this.addOn();
	}

	// Token: 0x060002E7 RID: 743 RVA: 0x0000F0AA File Offset: 0x0000D2AA
	public LTSeq insert(LTDescr tween)
	{
		this.current.tween = tween;
		tween.setDelay(this.addPreviousDelays());
		return this.addOn();
	}

	// Token: 0x060002E8 RID: 744 RVA: 0x0000F0CB File Offset: 0x0000D2CB
	public LTSeq setScale(float timeScale)
	{
		this.setScaleRecursive(this.current, timeScale, 500);
		return this.addOn();
	}

	// Token: 0x060002E9 RID: 745 RVA: 0x0000F0E8 File Offset: 0x0000D2E8
	private void setScaleRecursive(LTSeq seq, float timeScale, int count)
	{
		if (count > 0)
		{
			this.timeScale = timeScale;
			seq.totalDelay *= timeScale;
			if (seq.tween != null)
			{
				if (seq.tween.time != 0f)
				{
					seq.tween.setTime(seq.tween.time * timeScale);
				}
				seq.tween.setDelay(seq.tween.delay * timeScale);
			}
			if (seq.previous != null)
			{
				this.setScaleRecursive(seq.previous, timeScale, count - 1);
			}
		}
	}

	// Token: 0x060002EA RID: 746 RVA: 0x0000F172 File Offset: 0x0000D372
	public LTSeq reverse()
	{
		return this.addOn();
	}

	// Token: 0x04000169 RID: 361
	public LTSeq previous;

	// Token: 0x0400016A RID: 362
	public LTSeq current;

	// Token: 0x0400016B RID: 363
	public LTDescr tween;

	// Token: 0x0400016C RID: 364
	public float totalDelay;

	// Token: 0x0400016D RID: 365
	public float timeScale;

	// Token: 0x0400016E RID: 366
	private int debugIter;

	// Token: 0x0400016F RID: 367
	public uint counter;

	// Token: 0x04000170 RID: 368
	public bool toggle;

	// Token: 0x04000171 RID: 369
	private uint _id;
}
