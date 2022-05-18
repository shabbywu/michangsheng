using System;
using UnityEngine;

// Token: 0x020000E5 RID: 229
[AddComponentMenu("NGUI/Tween/Spring Position")]
public class SpringPosition : MonoBehaviour
{
	// Token: 0x060008DF RID: 2271 RVA: 0x0000B3C8 File Offset: 0x000095C8
	private void Start()
	{
		this.mTrans = base.transform;
		if (this.updateScrollView)
		{
			this.mSv = NGUITools.FindInParents<UIScrollView>(base.gameObject);
		}
	}

	// Token: 0x060008E0 RID: 2272 RVA: 0x000861D0 File Offset: 0x000843D0
	private void Update()
	{
		float deltaTime = this.ignoreTimeScale ? RealTime.deltaTime : Time.deltaTime;
		if (this.worldSpace)
		{
			if (this.mThreshold == 0f)
			{
				this.mThreshold = (this.target - this.mTrans.position).sqrMagnitude * 0.001f;
			}
			this.mTrans.position = NGUIMath.SpringLerp(this.mTrans.position, this.target, this.strength, deltaTime);
			if (this.mThreshold >= (this.target - this.mTrans.position).sqrMagnitude)
			{
				this.mTrans.position = this.target;
				this.NotifyListeners();
				base.enabled = false;
			}
		}
		else
		{
			if (this.mThreshold == 0f)
			{
				this.mThreshold = (this.target - this.mTrans.localPosition).sqrMagnitude * 1E-05f;
			}
			this.mTrans.localPosition = NGUIMath.SpringLerp(this.mTrans.localPosition, this.target, this.strength, deltaTime);
			if (this.mThreshold >= (this.target - this.mTrans.localPosition).sqrMagnitude)
			{
				this.mTrans.localPosition = this.target;
				this.NotifyListeners();
				base.enabled = false;
			}
		}
		if (this.mSv != null)
		{
			this.mSv.UpdateScrollbars(true);
		}
	}

	// Token: 0x060008E1 RID: 2273 RVA: 0x00086368 File Offset: 0x00084568
	private void NotifyListeners()
	{
		SpringPosition.current = this;
		if (this.onFinished != null)
		{
			this.onFinished();
		}
		if (this.eventReceiver != null && !string.IsNullOrEmpty(this.callWhenFinished))
		{
			this.eventReceiver.SendMessage(this.callWhenFinished, this, 1);
		}
		SpringPosition.current = null;
	}

	// Token: 0x060008E2 RID: 2274 RVA: 0x000863C4 File Offset: 0x000845C4
	public static SpringPosition Begin(GameObject go, Vector3 pos, float strength)
	{
		SpringPosition springPosition = go.GetComponent<SpringPosition>();
		if (springPosition == null)
		{
			springPosition = go.AddComponent<SpringPosition>();
		}
		springPosition.target = pos;
		springPosition.strength = strength;
		springPosition.onFinished = null;
		if (!springPosition.enabled)
		{
			springPosition.mThreshold = 0f;
			springPosition.enabled = true;
		}
		return springPosition;
	}

	// Token: 0x0400062F RID: 1583
	public static SpringPosition current;

	// Token: 0x04000630 RID: 1584
	public Vector3 target = Vector3.zero;

	// Token: 0x04000631 RID: 1585
	public float strength = 10f;

	// Token: 0x04000632 RID: 1586
	public bool worldSpace;

	// Token: 0x04000633 RID: 1587
	public bool ignoreTimeScale;

	// Token: 0x04000634 RID: 1588
	public bool updateScrollView;

	// Token: 0x04000635 RID: 1589
	public SpringPosition.OnFinished onFinished;

	// Token: 0x04000636 RID: 1590
	[SerializeField]
	[HideInInspector]
	private GameObject eventReceiver;

	// Token: 0x04000637 RID: 1591
	[SerializeField]
	[HideInInspector]
	public string callWhenFinished;

	// Token: 0x04000638 RID: 1592
	private Transform mTrans;

	// Token: 0x04000639 RID: 1593
	private float mThreshold;

	// Token: 0x0400063A RID: 1594
	private UIScrollView mSv;

	// Token: 0x020000E6 RID: 230
	// (Invoke) Token: 0x060008E5 RID: 2277
	public delegate void OnFinished();
}
