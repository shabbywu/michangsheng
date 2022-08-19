using System;
using UnityEngine;

// Token: 0x0200008B RID: 139
[RequireComponent(typeof(UIPanel))]
[AddComponentMenu("NGUI/Internal/Spring Panel")]
public class SpringPanel : MonoBehaviour
{
	// Token: 0x0600075A RID: 1882 RVA: 0x0002B6FD File Offset: 0x000298FD
	private void Start()
	{
		this.mPanel = base.GetComponent<UIPanel>();
		this.mDrag = base.GetComponent<UIScrollView>();
		this.mTrans = base.transform;
	}

	// Token: 0x0600075B RID: 1883 RVA: 0x0002B723 File Offset: 0x00029923
	private void Update()
	{
		this.AdvanceTowardsPosition();
	}

	// Token: 0x0600075C RID: 1884 RVA: 0x0002B72C File Offset: 0x0002992C
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

	// Token: 0x0600075D RID: 1885 RVA: 0x0002B828 File Offset: 0x00029A28
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

	// Token: 0x0400048A RID: 1162
	public static SpringPanel current;

	// Token: 0x0400048B RID: 1163
	public Vector3 target = Vector3.zero;

	// Token: 0x0400048C RID: 1164
	public float strength = 10f;

	// Token: 0x0400048D RID: 1165
	public SpringPanel.OnFinished onFinished;

	// Token: 0x0400048E RID: 1166
	private UIPanel mPanel;

	// Token: 0x0400048F RID: 1167
	private Transform mTrans;

	// Token: 0x04000490 RID: 1168
	private UIScrollView mDrag;

	// Token: 0x02001201 RID: 4609
	// (Invoke) Token: 0x0600783A RID: 30778
	public delegate void OnFinished();
}
