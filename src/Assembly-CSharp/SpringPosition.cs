using System;
using UnityEngine;

// Token: 0x02000095 RID: 149
[AddComponentMenu("NGUI/Tween/Spring Position")]
public class SpringPosition : MonoBehaviour
{
	// Token: 0x0600082B RID: 2091 RVA: 0x00031B78 File Offset: 0x0002FD78
	private void Start()
	{
		this.mTrans = base.transform;
		if (this.updateScrollView)
		{
			this.mSv = NGUITools.FindInParents<UIScrollView>(base.gameObject);
		}
	}

	// Token: 0x0600082C RID: 2092 RVA: 0x00031BA0 File Offset: 0x0002FDA0
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

	// Token: 0x0600082D RID: 2093 RVA: 0x00031D38 File Offset: 0x0002FF38
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

	// Token: 0x0600082E RID: 2094 RVA: 0x00031D94 File Offset: 0x0002FF94
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

	// Token: 0x0400050C RID: 1292
	public static SpringPosition current;

	// Token: 0x0400050D RID: 1293
	public Vector3 target = Vector3.zero;

	// Token: 0x0400050E RID: 1294
	public float strength = 10f;

	// Token: 0x0400050F RID: 1295
	public bool worldSpace;

	// Token: 0x04000510 RID: 1296
	public bool ignoreTimeScale;

	// Token: 0x04000511 RID: 1297
	public bool updateScrollView;

	// Token: 0x04000512 RID: 1298
	public SpringPosition.OnFinished onFinished;

	// Token: 0x04000513 RID: 1299
	[SerializeField]
	[HideInInspector]
	private GameObject eventReceiver;

	// Token: 0x04000514 RID: 1300
	[SerializeField]
	[HideInInspector]
	public string callWhenFinished;

	// Token: 0x04000515 RID: 1301
	private Transform mTrans;

	// Token: 0x04000516 RID: 1302
	private float mThreshold;

	// Token: 0x04000517 RID: 1303
	private UIScrollView mSv;

	// Token: 0x02001214 RID: 4628
	// (Invoke) Token: 0x0600786B RID: 30827
	public delegate void OnFinished();
}
