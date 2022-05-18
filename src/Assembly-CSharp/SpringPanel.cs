using System;
using UnityEngine;

// Token: 0x020000C8 RID: 200
[RequireComponent(typeof(UIPanel))]
[AddComponentMenu("NGUI/Internal/Spring Panel")]
public class SpringPanel : MonoBehaviour
{
	// Token: 0x060007DD RID: 2013 RVA: 0x0000A8E7 File Offset: 0x00008AE7
	private void Start()
	{
		this.mPanel = base.GetComponent<UIPanel>();
		this.mDrag = base.GetComponent<UIScrollView>();
		this.mTrans = base.transform;
	}

	// Token: 0x060007DE RID: 2014 RVA: 0x0000A90D File Offset: 0x00008B0D
	private void Update()
	{
		this.AdvanceTowardsPosition();
	}

	// Token: 0x060007DF RID: 2015 RVA: 0x000805EC File Offset: 0x0007E7EC
	protected virtual void AdvanceTowardsPosition()
	{
		float deltaTime = RealTime.deltaTime;
		bool flag = false;
		Vector3 localPosition = this.mTrans.localPosition;
		Vector3 vector = NGUIMath.SpringLerp(this.mTrans.localPosition, this.target, this.strength, deltaTime);
		if ((vector - this.target).sqrMagnitude < 0.01f)
		{
			vector = this.target;
			base.enabled = false;
			flag = true;
		}
		this.mTrans.localPosition = vector;
		Vector3 vector2 = vector - localPosition;
		Vector2 clipOffset = this.mPanel.clipOffset;
		clipOffset.x -= vector2.x;
		clipOffset.y -= vector2.y;
		this.mPanel.clipOffset = clipOffset;
		if (this.mDrag != null)
		{
			this.mDrag.UpdateScrollbars(false);
		}
		if (flag && this.onFinished != null)
		{
			SpringPanel.current = this;
			this.onFinished();
			SpringPanel.current = null;
		}
	}

	// Token: 0x060007E0 RID: 2016 RVA: 0x000806E8 File Offset: 0x0007E8E8
	public static SpringPanel Begin(GameObject go, Vector3 pos, float strength)
	{
		SpringPanel springPanel = go.GetComponent<SpringPanel>();
		if (springPanel == null)
		{
			springPanel = go.AddComponent<SpringPanel>();
		}
		springPanel.target = pos;
		springPanel.strength = strength;
		springPanel.onFinished = null;
		springPanel.enabled = true;
		return springPanel;
	}

	// Token: 0x0400057E RID: 1406
	public static SpringPanel current;

	// Token: 0x0400057F RID: 1407
	public Vector3 target = Vector3.zero;

	// Token: 0x04000580 RID: 1408
	public float strength = 10f;

	// Token: 0x04000581 RID: 1409
	public SpringPanel.OnFinished onFinished;

	// Token: 0x04000582 RID: 1410
	private UIPanel mPanel;

	// Token: 0x04000583 RID: 1411
	private Transform mTrans;

	// Token: 0x04000584 RID: 1412
	private UIScrollView mDrag;

	// Token: 0x020000C9 RID: 201
	// (Invoke) Token: 0x060007E3 RID: 2019
	public delegate void OnFinished();
}
