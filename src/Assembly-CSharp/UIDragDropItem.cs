using System;
using UnityEngine;

// Token: 0x0200007C RID: 124
[AddComponentMenu("NGUI/Interaction/Drag and Drop Item")]
public class UIDragDropItem : MonoBehaviour
{
	// Token: 0x0600052E RID: 1326 RVA: 0x000089E6 File Offset: 0x00006BE6
	protected virtual void Start()
	{
		this.mTrans = base.transform;
		this.mCollider = base.GetComponent<Collider>();
		this.mButton = base.GetComponent<UIButton>();
		this.mDragScrollView = base.GetComponent<UIDragScrollView>();
	}

	// Token: 0x0600052F RID: 1327 RVA: 0x00008A18 File Offset: 0x00006C18
	private void OnPress(bool isPressed)
	{
		if (isPressed)
		{
			this.mPressTime = RealTime.time;
		}
	}

	// Token: 0x06000530 RID: 1328 RVA: 0x000713C8 File Offset: 0x0006F5C8
	private void OnDragStart()
	{
		if (!base.enabled || this.mTouchID != -2147483648)
		{
			return;
		}
		if (this.restriction != UIDragDropItem.Restriction.None)
		{
			if (this.restriction == UIDragDropItem.Restriction.Horizontal)
			{
				Vector2 totalDelta = UICamera.currentTouch.totalDelta;
				if (Mathf.Abs(totalDelta.x) < Mathf.Abs(totalDelta.y))
				{
					return;
				}
			}
			else if (this.restriction == UIDragDropItem.Restriction.Vertical)
			{
				Vector2 totalDelta2 = UICamera.currentTouch.totalDelta;
				if (Mathf.Abs(totalDelta2.x) > Mathf.Abs(totalDelta2.y))
				{
					return;
				}
			}
			else if (this.restriction == UIDragDropItem.Restriction.PressAndHold && this.mPressTime + this.pressAndHoldDelay > RealTime.time)
			{
				return;
			}
		}
		if (this.cloneOnDrag)
		{
			GameObject gameObject = NGUITools.AddChild(base.transform.parent.gameObject, base.gameObject);
			gameObject.transform.localPosition = base.transform.localPosition;
			gameObject.transform.localRotation = base.transform.localRotation;
			gameObject.transform.localScale = base.transform.localScale;
			UIButtonColor component = gameObject.GetComponent<UIButtonColor>();
			if (component != null)
			{
				component.defaultColor = base.GetComponent<UIButtonColor>().defaultColor;
			}
			UICamera.currentTouch.dragged = gameObject;
			UIDragDropItem component2 = gameObject.GetComponent<UIDragDropItem>();
			component2.Start();
			component2.OnDragDropStart();
			return;
		}
		this.OnDragDropStart();
	}

	// Token: 0x06000531 RID: 1329 RVA: 0x00008A28 File Offset: 0x00006C28
	private void OnDrag(Vector2 delta)
	{
		if (!base.enabled || this.mTouchID != UICamera.currentTouchID)
		{
			return;
		}
		this.OnDragDropMove(delta * this.mRoot.pixelSizeAdjustment);
	}

	// Token: 0x06000532 RID: 1330 RVA: 0x00008A5C File Offset: 0x00006C5C
	private void OnDragEnd()
	{
		if (!base.enabled || this.mTouchID != UICamera.currentTouchID)
		{
			return;
		}
		this.OnDragDropRelease(UICamera.hoveredObject);
	}

	// Token: 0x06000533 RID: 1331 RVA: 0x00071518 File Offset: 0x0006F718
	protected virtual void OnDragDropStart()
	{
		if (this.mDragScrollView != null)
		{
			this.mDragScrollView.enabled = false;
		}
		if (this.mButton != null)
		{
			this.mButton.isEnabled = false;
		}
		else if (this.mCollider != null)
		{
			this.mCollider.enabled = false;
		}
		this.mTouchID = UICamera.currentTouchID;
		this.mParent = this.mTrans.parent;
		this.mRoot = NGUITools.FindInParents<UIRoot>(this.mParent);
		this.mGrid = NGUITools.FindInParents<UIGrid>(this.mParent);
		this.mTable = NGUITools.FindInParents<UITable>(this.mParent);
		if (UIDragDropRoot.root != null)
		{
			this.mTrans.parent = UIDragDropRoot.root;
		}
		Vector3 localPosition = this.mTrans.localPosition;
		localPosition.z = 0f;
		this.mTrans.localPosition = localPosition;
		TweenPosition component = base.GetComponent<TweenPosition>();
		if (component != null)
		{
			component.enabled = false;
		}
		SpringPosition component2 = base.GetComponent<SpringPosition>();
		if (component2 != null)
		{
			component2.enabled = false;
		}
		NGUITools.MarkParentAsChanged(base.gameObject);
		if (this.mTable != null)
		{
			this.mTable.repositionNow = true;
		}
		if (this.mGrid != null)
		{
			this.mGrid.repositionNow = true;
		}
	}

	// Token: 0x06000534 RID: 1332 RVA: 0x00008A7F File Offset: 0x00006C7F
	protected virtual void OnDragDropMove(Vector3 delta)
	{
		this.mTrans.localPosition += delta;
	}

	// Token: 0x06000535 RID: 1333 RVA: 0x00071674 File Offset: 0x0006F874
	protected virtual void OnDragDropRelease(GameObject surface)
	{
		if (!this.cloneOnDrag)
		{
			this.mTouchID = int.MinValue;
			if (this.mButton != null)
			{
				this.mButton.isEnabled = true;
			}
			else if (this.mCollider != null)
			{
				this.mCollider.enabled = true;
			}
			UIDragDropContainer uidragDropContainer = surface ? NGUITools.FindInParents<UIDragDropContainer>(surface) : null;
			if (uidragDropContainer != null)
			{
				this.mTrans.parent = ((uidragDropContainer.reparentTarget != null) ? uidragDropContainer.reparentTarget : uidragDropContainer.transform);
				Vector3 localPosition = this.mTrans.localPosition;
				localPosition.z = 0f;
				this.mTrans.localPosition = localPosition;
			}
			else
			{
				this.mTrans.parent = this.mParent;
			}
			this.mParent = this.mTrans.parent;
			this.mGrid = NGUITools.FindInParents<UIGrid>(this.mParent);
			this.mTable = NGUITools.FindInParents<UITable>(this.mParent);
			if (this.mDragScrollView != null)
			{
				this.mDragScrollView.enabled = true;
			}
			NGUITools.MarkParentAsChanged(base.gameObject);
			if (this.mTable != null)
			{
				this.mTable.repositionNow = true;
			}
			if (this.mGrid != null)
			{
				this.mGrid.repositionNow = true;
				return;
			}
		}
		else
		{
			NGUITools.Destroy(base.gameObject);
		}
	}

	// Token: 0x04000373 RID: 883
	public UIDragDropItem.Restriction restriction;

	// Token: 0x04000374 RID: 884
	public bool cloneOnDrag;

	// Token: 0x04000375 RID: 885
	[HideInInspector]
	public float pressAndHoldDelay = 1f;

	// Token: 0x04000376 RID: 886
	protected Transform mTrans;

	// Token: 0x04000377 RID: 887
	protected Transform mParent;

	// Token: 0x04000378 RID: 888
	protected Collider mCollider;

	// Token: 0x04000379 RID: 889
	protected UIButton mButton;

	// Token: 0x0400037A RID: 890
	protected UIRoot mRoot;

	// Token: 0x0400037B RID: 891
	protected UIGrid mGrid;

	// Token: 0x0400037C RID: 892
	protected UITable mTable;

	// Token: 0x0400037D RID: 893
	protected int mTouchID = int.MinValue;

	// Token: 0x0400037E RID: 894
	protected float mPressTime;

	// Token: 0x0400037F RID: 895
	protected UIDragScrollView mDragScrollView;

	// Token: 0x0200007D RID: 125
	public enum Restriction
	{
		// Token: 0x04000381 RID: 897
		None,
		// Token: 0x04000382 RID: 898
		Horizontal,
		// Token: 0x04000383 RID: 899
		Vertical,
		// Token: 0x04000384 RID: 900
		PressAndHold
	}
}
