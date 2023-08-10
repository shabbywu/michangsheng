using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Drag and Drop Item")]
public class UIDragDropItem : MonoBehaviour
{
	public enum Restriction
	{
		None,
		Horizontal,
		Vertical,
		PressAndHold
	}

	public Restriction restriction;

	public bool cloneOnDrag;

	[HideInInspector]
	public float pressAndHoldDelay = 1f;

	protected Transform mTrans;

	protected Transform mParent;

	protected Collider mCollider;

	protected UIButton mButton;

	protected UIRoot mRoot;

	protected UIGrid mGrid;

	protected UITable mTable;

	protected int mTouchID = int.MinValue;

	protected float mPressTime;

	protected UIDragScrollView mDragScrollView;

	protected virtual void Start()
	{
		mTrans = ((Component)this).transform;
		mCollider = ((Component)this).GetComponent<Collider>();
		mButton = ((Component)this).GetComponent<UIButton>();
		mDragScrollView = ((Component)this).GetComponent<UIDragScrollView>();
	}

	private void OnPress(bool isPressed)
	{
		if (isPressed)
		{
			mPressTime = RealTime.time;
		}
	}

	private void OnDragStart()
	{
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		if (!((Behaviour)this).enabled || mTouchID != int.MinValue)
		{
			return;
		}
		if (restriction != 0)
		{
			if (restriction == Restriction.Horizontal)
			{
				Vector2 totalDelta = UICamera.currentTouch.totalDelta;
				if (Mathf.Abs(totalDelta.x) < Mathf.Abs(totalDelta.y))
				{
					return;
				}
			}
			else if (restriction == Restriction.Vertical)
			{
				Vector2 totalDelta2 = UICamera.currentTouch.totalDelta;
				if (Mathf.Abs(totalDelta2.x) > Mathf.Abs(totalDelta2.y))
				{
					return;
				}
			}
			else if (restriction == Restriction.PressAndHold && mPressTime + pressAndHoldDelay > RealTime.time)
			{
				return;
			}
		}
		if (cloneOnDrag)
		{
			GameObject val = NGUITools.AddChild(((Component)((Component)this).transform.parent).gameObject, ((Component)this).gameObject);
			val.transform.localPosition = ((Component)this).transform.localPosition;
			val.transform.localRotation = ((Component)this).transform.localRotation;
			val.transform.localScale = ((Component)this).transform.localScale;
			UIButtonColor component = val.GetComponent<UIButtonColor>();
			if ((Object)(object)component != (Object)null)
			{
				component.defaultColor = ((Component)this).GetComponent<UIButtonColor>().defaultColor;
			}
			UICamera.currentTouch.dragged = val;
			UIDragDropItem component2 = val.GetComponent<UIDragDropItem>();
			component2.Start();
			component2.OnDragDropStart();
		}
		else
		{
			OnDragDropStart();
		}
	}

	private void OnDrag(Vector2 delta)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		if (((Behaviour)this).enabled && mTouchID == UICamera.currentTouchID)
		{
			OnDragDropMove(Vector2.op_Implicit(delta) * mRoot.pixelSizeAdjustment);
		}
	}

	private void OnDragEnd()
	{
		if (((Behaviour)this).enabled && mTouchID == UICamera.currentTouchID)
		{
			OnDragDropRelease(UICamera.hoveredObject);
		}
	}

	protected virtual void OnDragDropStart()
	{
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)mDragScrollView != (Object)null)
		{
			((Behaviour)mDragScrollView).enabled = false;
		}
		if ((Object)(object)mButton != (Object)null)
		{
			mButton.isEnabled = false;
		}
		else if ((Object)(object)mCollider != (Object)null)
		{
			mCollider.enabled = false;
		}
		mTouchID = UICamera.currentTouchID;
		mParent = mTrans.parent;
		mRoot = NGUITools.FindInParents<UIRoot>(mParent);
		mGrid = NGUITools.FindInParents<UIGrid>(mParent);
		mTable = NGUITools.FindInParents<UITable>(mParent);
		if ((Object)(object)UIDragDropRoot.root != (Object)null)
		{
			mTrans.parent = UIDragDropRoot.root;
		}
		Vector3 localPosition = mTrans.localPosition;
		localPosition.z = 0f;
		mTrans.localPosition = localPosition;
		TweenPosition component = ((Component)this).GetComponent<TweenPosition>();
		if ((Object)(object)component != (Object)null)
		{
			((Behaviour)component).enabled = false;
		}
		SpringPosition component2 = ((Component)this).GetComponent<SpringPosition>();
		if ((Object)(object)component2 != (Object)null)
		{
			((Behaviour)component2).enabled = false;
		}
		NGUITools.MarkParentAsChanged(((Component)this).gameObject);
		if ((Object)(object)mTable != (Object)null)
		{
			mTable.repositionNow = true;
		}
		if ((Object)(object)mGrid != (Object)null)
		{
			mGrid.repositionNow = true;
		}
	}

	protected virtual void OnDragDropMove(Vector3 delta)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		Transform obj = mTrans;
		obj.localPosition += delta;
	}

	protected virtual void OnDragDropRelease(GameObject surface)
	{
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		if (!cloneOnDrag)
		{
			mTouchID = int.MinValue;
			if ((Object)(object)mButton != (Object)null)
			{
				mButton.isEnabled = true;
			}
			else if ((Object)(object)mCollider != (Object)null)
			{
				mCollider.enabled = true;
			}
			UIDragDropContainer uIDragDropContainer = (Object.op_Implicit((Object)(object)surface) ? NGUITools.FindInParents<UIDragDropContainer>(surface) : null);
			if ((Object)(object)uIDragDropContainer != (Object)null)
			{
				mTrans.parent = (((Object)(object)uIDragDropContainer.reparentTarget != (Object)null) ? uIDragDropContainer.reparentTarget : ((Component)uIDragDropContainer).transform);
				Vector3 localPosition = mTrans.localPosition;
				localPosition.z = 0f;
				mTrans.localPosition = localPosition;
			}
			else
			{
				mTrans.parent = mParent;
			}
			mParent = mTrans.parent;
			mGrid = NGUITools.FindInParents<UIGrid>(mParent);
			mTable = NGUITools.FindInParents<UITable>(mParent);
			if ((Object)(object)mDragScrollView != (Object)null)
			{
				((Behaviour)mDragScrollView).enabled = true;
			}
			NGUITools.MarkParentAsChanged(((Component)this).gameObject);
			if ((Object)(object)mTable != (Object)null)
			{
				mTable.repositionNow = true;
			}
			if ((Object)(object)mGrid != (Object)null)
			{
				mGrid.repositionNow = true;
			}
		}
		else
		{
			NGUITools.Destroy((Object)(object)((Component)this).gameObject);
		}
	}
}
