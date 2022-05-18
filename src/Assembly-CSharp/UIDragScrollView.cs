using System;
using UnityEngine;

// Token: 0x02000083 RID: 131
[AddComponentMenu("NGUI/Interaction/Drag Scroll View")]
public class UIDragScrollView : MonoBehaviour
{
	// Token: 0x06000556 RID: 1366 RVA: 0x00072548 File Offset: 0x00070748
	private void OnEnable()
	{
		this.mTrans = base.transform;
		if (this.scrollView == null && this.draggablePanel != null)
		{
			this.scrollView = this.draggablePanel;
			this.draggablePanel = null;
		}
		if (this.mStarted && (this.mAutoFind || this.mScroll == null))
		{
			this.FindScrollView();
		}
	}

	// Token: 0x06000557 RID: 1367 RVA: 0x00008BF5 File Offset: 0x00006DF5
	private void Start()
	{
		this.mStarted = true;
		this.FindScrollView();
	}

	// Token: 0x06000558 RID: 1368 RVA: 0x000725B4 File Offset: 0x000707B4
	private void FindScrollView()
	{
		UIScrollView uiscrollView = NGUITools.FindInParents<UIScrollView>(this.mTrans);
		if (this.scrollView == null)
		{
			this.scrollView = uiscrollView;
			this.mAutoFind = true;
		}
		else if (this.scrollView == uiscrollView)
		{
			this.mAutoFind = true;
		}
		this.mScroll = this.scrollView;
	}

	// Token: 0x06000559 RID: 1369 RVA: 0x0007260C File Offset: 0x0007080C
	private void OnPress(bool pressed)
	{
		if (this.mAutoFind && this.mScroll != this.scrollView)
		{
			this.mScroll = this.scrollView;
			this.mAutoFind = false;
		}
		if (this.scrollView && base.enabled && NGUITools.GetActive(base.gameObject))
		{
			this.scrollView.Press(pressed);
			if (!pressed && this.mAutoFind)
			{
				this.scrollView = NGUITools.FindInParents<UIScrollView>(this.mTrans);
				this.mScroll = this.scrollView;
			}
		}
	}

	// Token: 0x0600055A RID: 1370 RVA: 0x00008C04 File Offset: 0x00006E04
	private void OnDrag(Vector2 delta)
	{
		if (this.scrollView && NGUITools.GetActive(this))
		{
			this.scrollView.Drag();
		}
	}

	// Token: 0x0600055B RID: 1371 RVA: 0x00008C26 File Offset: 0x00006E26
	private void OnScroll(float delta)
	{
		if (this.scrollView && NGUITools.GetActive(this))
		{
			this.scrollView.Scroll(delta);
		}
	}

	// Token: 0x040003B6 RID: 950
	public UIScrollView scrollView;

	// Token: 0x040003B7 RID: 951
	[HideInInspector]
	[SerializeField]
	private UIScrollView draggablePanel;

	// Token: 0x040003B8 RID: 952
	private Transform mTrans;

	// Token: 0x040003B9 RID: 953
	private UIScrollView mScroll;

	// Token: 0x040003BA RID: 954
	private bool mAutoFind;

	// Token: 0x040003BB RID: 955
	private bool mStarted;
}
