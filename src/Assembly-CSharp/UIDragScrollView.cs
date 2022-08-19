using System;
using UnityEngine;

// Token: 0x02000065 RID: 101
[AddComponentMenu("NGUI/Interaction/Drag Scroll View")]
public class UIDragScrollView : MonoBehaviour
{
	// Token: 0x06000504 RID: 1284 RVA: 0x0001BBD0 File Offset: 0x00019DD0
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

	// Token: 0x06000505 RID: 1285 RVA: 0x0001BC3C File Offset: 0x00019E3C
	private void Start()
	{
		this.mStarted = true;
		this.FindScrollView();
	}

	// Token: 0x06000506 RID: 1286 RVA: 0x0001BC4C File Offset: 0x00019E4C
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

	// Token: 0x06000507 RID: 1287 RVA: 0x0001BCA4 File Offset: 0x00019EA4
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

	// Token: 0x06000508 RID: 1288 RVA: 0x0001BD35 File Offset: 0x00019F35
	private void OnDrag(Vector2 delta)
	{
		if (this.scrollView && NGUITools.GetActive(this))
		{
			this.scrollView.Drag();
		}
	}

	// Token: 0x06000509 RID: 1289 RVA: 0x0001BD57 File Offset: 0x00019F57
	private void OnScroll(float delta)
	{
		if (this.scrollView && NGUITools.GetActive(this))
		{
			this.scrollView.Scroll(delta);
		}
	}

	// Token: 0x0400032E RID: 814
	public UIScrollView scrollView;

	// Token: 0x0400032F RID: 815
	[HideInInspector]
	[SerializeField]
	private UIScrollView draggablePanel;

	// Token: 0x04000330 RID: 816
	private Transform mTrans;

	// Token: 0x04000331 RID: 817
	private UIScrollView mScroll;

	// Token: 0x04000332 RID: 818
	private bool mAutoFind;

	// Token: 0x04000333 RID: 819
	private bool mStarted;
}
