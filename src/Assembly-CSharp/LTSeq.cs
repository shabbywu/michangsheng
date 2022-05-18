using System;
using UnityEngine;

// Token: 0x0200002B RID: 43
public class LTSeq
{
	// Token: 0x1700003B RID: 59
	// (get) Token: 0x060002EE RID: 750 RVA: 0x00006B87 File Offset: 0x00004D87
	public int id
	{
		get
		{
			return (int)(this._id | this.counter << 16);
		}
	}

	// Token: 0x060002EF RID: 751 RVA: 0x00006B99 File Offset: 0x00004D99
	public void reset()
	{
		this.previous = null;
		this.tween = null;
		this.totalDelay = 0f;
	}

	// Token: 0x060002F0 RID: 752 RVA: 0x00006BB4 File Offset: 0x00004DB4
	public void init(uint id, uint global_counter)
	{
		this.reset();
		this._id = id;
		this.counter = global_counter;
		this.current = this;
	}

	// Token: 0x060002F1 RID: 753 RVA: 0x00066C44 File Offset: 0x00064E44
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

	// Token: 0x060002F2 RID: 754 RVA: 0x00066CF0 File Offset: 0x00064EF0
	private float addPreviousDelays()
	{
		LTSeq ltseq = this.current.previous;
		if (ltseq != null && ltseq.tween != null)
		{
			return this.current.totalDelay + ltseq.tween.time;
		}
		return this.current.totalDelay;
	}

	// Token: 0x060002F3 RID: 755 RVA: 0x00006BD1 File Offset: 0x00004DD1
	public LTSeq append(float delay)
	{
		this.current.totalDelay += delay;
		return this.current;
	}

	// Token: 0x060002F4 RID: 756 RVA: 0x00066D38 File Offset: 0x00064F38
	public LTSeq append(Action callback)
	{
		LTDescr ltdescr = LeanTween.delayedCall(0f, callback);
		this.append(ltdescr);
		return this.addOn();
	}

	// Token: 0x060002F5 RID: 757 RVA: 0x00006BEC File Offset: 0x00004DEC
	public LTSeq append(Action<object> callback, object obj)
	{
		this.append(LeanTween.delayedCall(0f, callback).setOnCompleteParam(obj));
		return this.addOn();
	}

	// Token: 0x060002F6 RID: 758 RVA: 0x00006C0C File Offset: 0x00004E0C
	public LTSeq append(GameObject gameObject, Action callback)
	{
		this.append(LeanTween.delayedCall(gameObject, 0f, callback));
		return this.addOn();
	}

	// Token: 0x060002F7 RID: 759 RVA: 0x00006C27 File Offset: 0x00004E27
	public LTSeq append(GameObject gameObject, Action<object> callback, object obj)
	{
		this.append(LeanTween.delayedCall(gameObject, 0f, callback).setOnCompleteParam(obj));
		return this.addOn();
	}

	// Token: 0x060002F8 RID: 760 RVA: 0x00006C48 File Offset: 0x00004E48
	public LTSeq append(LTDescr tween)
	{
		this.current.tween = tween;
		this.current.totalDelay = this.addPreviousDelays();
		tween.setDelay(this.current.totalDelay);
		return this.addOn();
	}

	// Token: 0x060002F9 RID: 761 RVA: 0x00006C7F File Offset: 0x00004E7F
	public LTSeq insert(LTDescr tween)
	{
		this.current.tween = tween;
		tween.setDelay(this.addPreviousDelays());
		return this.addOn();
	}

	// Token: 0x060002FA RID: 762 RVA: 0x00006CA0 File Offset: 0x00004EA0
	public LTSeq setScale(float timeScale)
	{
		this.setScaleRecursive(this.current, timeScale, 500);
		return this.addOn();
	}

	// Token: 0x060002FB RID: 763 RVA: 0x00066D60 File Offset: 0x00064F60
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

	// Token: 0x060002FC RID: 764 RVA: 0x00006CBA File Offset: 0x00004EBA
	public LTSeq reverse()
	{
		return this.addOn();
	}

	// Token: 0x0400017E RID: 382
	public LTSeq previous;

	// Token: 0x0400017F RID: 383
	public LTSeq current;

	// Token: 0x04000180 RID: 384
	public LTDescr tween;

	// Token: 0x04000181 RID: 385
	public float totalDelay;

	// Token: 0x04000182 RID: 386
	public float timeScale;

	// Token: 0x04000183 RID: 387
	private int debugIter;

	// Token: 0x04000184 RID: 388
	public uint counter;

	// Token: 0x04000185 RID: 389
	public bool toggle;

	// Token: 0x04000186 RID: 390
	private uint _id;
}
