using System;
using UnityEngine;

// Token: 0x02000060 RID: 96
[AddComponentMenu("NGUI/Interaction/Drag and Drop Item")]
public class UIDragDropItem : MonoBehaviour
{
	// Token: 0x060004DC RID: 1244 RVA: 0x0001A840 File Offset: 0x00018A40
	protected virtual void Start()
	{
		this.mTrans = base.transform;
		this.mCollider = base.GetComponent<Collider>();
		this.mButton = base.GetComponent<UIButton>();
		this.mDragScrollView = base.GetComponent<UIDragScrollView>();
	}

	// Token: 0x060004DD RID: 1245 RVA: 0x0001A872 File Offset: 0x00018A72
	private void OnPress(bool isPressed)
	{
		if (isPressed)
		{
			this.mPressTime = RealTime.time;
		}
	}

	// Token: 0x060004DE RID: 1246 RVA: 0x0001A884 File Offset: 0x00018A84
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

	// Token: 0x060004DF RID: 1247 RVA: 0x0001A9D4 File Offset: 0x00018BD4
	private void OnDrag(Vector2 delta)
	{
		if (!base.enabled || this.mTouchID != UICamera.currentTouchID)
		{
			return;
		}
		this.OnDragDropMove(delta * this.mRoot.pixelSizeAdjustment);
	}

	// Token: 0x060004E0 RID: 1248 RVA: 0x0001AA08 File Offset: 0x00018C08
	private void OnDragEnd()
	{
		if (!base.enabled || this.mTouchID != UICamera.currentTouchID)
		{
			return;
		}
		this.OnDragDropRelease(UICamera.hoveredObject);
	}

	// Token: 0x060004E1 RID: 1249 RVA: 0x0001AA2C File Offset: 0x00018C2C
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

	// Token: 0x060004E2 RID: 1250 RVA: 0x0001AB86 File Offset: 0x00018D86
	protected virtual void OnDragDropMove(Vector3 delta)
	{
		this.mTrans.localPosition += delta;
	}

	// Token: 0x060004E3 RID: 1251 RVA: 0x0001ABA0 File Offset: 0x00018DA0
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

	// Token: 0x040002F4 RID: 756
	public UIDragDropItem.Restriction restriction;

	// Token: 0x040002F5 RID: 757
	public bool cloneOnDrag;

	// Token: 0x040002F6 RID: 758
	[HideInInspector]
	public float pressAndHoldDelay = 1f;

	// Token: 0x040002F7 RID: 759
	protected Transform mTrans;

	// Token: 0x040002F8 RID: 760
	protected Transform mParent;

	// Token: 0x040002F9 RID: 761
	protected Collider mCollider;

	// Token: 0x040002FA RID: 762
	protected UIButton mButton;

	// Token: 0x040002FB RID: 763
	protected UIRoot mRoot;

	// Token: 0x040002FC RID: 764
	protected UIGrid mGrid;

	// Token: 0x040002FD RID: 765
	protected UITable mTable;

	// Token: 0x040002FE RID: 766
	protected int mTouchID = int.MinValue;

	// Token: 0x040002FF RID: 767
	protected float mPressTime;

	// Token: 0x04000300 RID: 768
	protected UIDragScrollView mDragScrollView;

	// Token: 0x020011E0 RID: 4576
	public enum Restriction
	{
		// Token: 0x040063CF RID: 25551
		None,
		// Token: 0x040063D0 RID: 25552
		Horizontal,
		// Token: 0x040063D1 RID: 25553
		Vertical,
		// Token: 0x040063D2 RID: 25554
		PressAndHold
	}
}
