using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(UIPanel))]
[AddComponentMenu("NGUI/Interaction/Scroll View")]
public class UIScrollView : MonoBehaviour
{
	public enum Movement
	{
		Horizontal,
		Vertical,
		Unrestricted,
		Custom
	}

	public enum DragEffect
	{
		None,
		Momentum,
		MomentumAndSpring
	}

	public enum ShowCondition
	{
		Always,
		OnlyIfNeeded,
		WhenDragging
	}

	public delegate void OnDragNotification();

	public static BetterList<UIScrollView> list = new BetterList<UIScrollView>();

	public Movement movement;

	public DragEffect dragEffect = DragEffect.MomentumAndSpring;

	public bool restrictWithinPanel = true;

	public bool disableDragIfFits;

	public bool smoothDragStart = true;

	public bool iOSDragEmulation = true;

	public float scrollWheelFactor = 0.25f;

	public float momentumAmount = 35f;

	public UIProgressBar horizontalScrollBar;

	public UIProgressBar verticalScrollBar;

	public ShowCondition showScrollBars = ShowCondition.OnlyIfNeeded;

	public Vector2 customMovement = new Vector2(1f, 0f);

	public UIWidget.Pivot contentPivot;

	public OnDragNotification onDragStarted;

	public OnDragNotification onDragFinished;

	public OnDragNotification onMomentumMove;

	public OnDragNotification onStoppedMoving;

	[HideInInspector]
	[SerializeField]
	private Vector3 scale = new Vector3(1f, 0f, 0f);

	[SerializeField]
	[HideInInspector]
	private Vector2 relativePositionOnReset = Vector2.zero;

	protected Transform mTrans;

	protected UIPanel mPanel;

	protected Plane mPlane;

	protected Vector3 mLastPos;

	protected bool mPressed;

	protected Vector3 mMomentum = Vector3.zero;

	protected float mScroll;

	protected Bounds mBounds;

	protected bool mCalculatedBounds;

	protected bool mShouldMove;

	protected bool mIgnoreCallbacks;

	protected int mDragID = -10;

	protected Vector2 mDragStartOffset = Vector2.zero;

	protected bool mDragStarted;

	public UIPanel panel => mPanel;

	public bool isDragging
	{
		get
		{
			if (mPressed)
			{
				return mDragStarted;
			}
			return false;
		}
	}

	public virtual Bounds bounds
	{
		get
		{
			//IL_0033: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			if (!mCalculatedBounds)
			{
				mCalculatedBounds = true;
				mTrans = ((Component)this).transform;
				mBounds = NGUIMath.CalculateRelativeWidgetBounds(mTrans, mTrans);
			}
			return mBounds;
		}
	}

	public bool canMoveHorizontally
	{
		get
		{
			if (movement != 0 && movement != Movement.Unrestricted)
			{
				if (movement == Movement.Custom)
				{
					return customMovement.x != 0f;
				}
				return false;
			}
			return true;
		}
	}

	public bool canMoveVertically
	{
		get
		{
			if (movement != Movement.Vertical && movement != Movement.Unrestricted)
			{
				if (movement == Movement.Custom)
				{
					return customMovement.y != 0f;
				}
				return false;
			}
			return true;
		}
	}

	public virtual bool shouldMoveHorizontally
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0009: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			Bounds val = bounds;
			float num = ((Bounds)(ref val)).size.x;
			if (mPanel.clipping == UIDrawCall.Clipping.SoftClip)
			{
				num += mPanel.clipSoftness.x * 2f;
			}
			return Mathf.RoundToInt(num - mPanel.width) > 0;
		}
	}

	public virtual bool shouldMoveVertically
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0009: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			Bounds val = bounds;
			float num = ((Bounds)(ref val)).size.y;
			if (mPanel.clipping == UIDrawCall.Clipping.SoftClip)
			{
				num += mPanel.clipSoftness.y * 2f;
			}
			return Mathf.RoundToInt(num - mPanel.height) > 0;
		}
	}

	protected virtual bool shouldMove
	{
		get
		{
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Unknown result type (might be due to invalid IL or missing references)
			//IL_0036: Unknown result type (might be due to invalid IL or missing references)
			//IL_0037: Unknown result type (might be due to invalid IL or missing references)
			//IL_0044: Unknown result type (might be due to invalid IL or missing references)
			//IL_0059: Unknown result type (might be due to invalid IL or missing references)
			//IL_0066: Unknown result type (might be due to invalid IL or missing references)
			//IL_0085: Unknown result type (might be due to invalid IL or missing references)
			//IL_008f: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
			//IL_009d: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
			//IL_00df: Unknown result type (might be due to invalid IL or missing references)
			if (!disableDragIfFits)
			{
				return true;
			}
			if ((Object)(object)mPanel == (Object)null)
			{
				mPanel = ((Component)this).GetComponent<UIPanel>();
			}
			Vector4 finalClipRegion = mPanel.finalClipRegion;
			Bounds val = bounds;
			float num = ((finalClipRegion.z == 0f) ? ((float)Screen.width) : (finalClipRegion.z * 0.5f));
			float num2 = ((finalClipRegion.w == 0f) ? ((float)Screen.height) : (finalClipRegion.w * 0.5f));
			if (canMoveHorizontally)
			{
				if (((Bounds)(ref val)).min.x < finalClipRegion.x - num)
				{
					return true;
				}
				if (((Bounds)(ref val)).max.x > finalClipRegion.x + num)
				{
					return true;
				}
			}
			if (canMoveVertically)
			{
				if (((Bounds)(ref val)).min.y < finalClipRegion.y - num2)
				{
					return true;
				}
				if (((Bounds)(ref val)).max.y > finalClipRegion.y + num2)
				{
					return true;
				}
			}
			return false;
		}
	}

	public Vector3 currentMomentum
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return mMomentum;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			mMomentum = value;
			mShouldMove = true;
		}
	}

	private void Awake()
	{
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		mTrans = ((Component)this).transform;
		mPanel = ((Component)this).GetComponent<UIPanel>();
		if (mPanel.clipping == UIDrawCall.Clipping.None)
		{
			mPanel.clipping = UIDrawCall.Clipping.ConstrainButDontClip;
		}
		if (movement != Movement.Custom && ((Vector3)(ref scale)).sqrMagnitude > 0.001f)
		{
			if (scale.x == 1f && scale.y == 0f)
			{
				movement = Movement.Horizontal;
			}
			else if (scale.x == 0f && scale.y == 1f)
			{
				movement = Movement.Vertical;
			}
			else if (scale.x == 1f && scale.y == 1f)
			{
				movement = Movement.Unrestricted;
			}
			else
			{
				movement = Movement.Custom;
				customMovement.x = scale.x;
				customMovement.y = scale.y;
			}
			scale = Vector3.zero;
		}
		if (contentPivot == UIWidget.Pivot.TopLeft && relativePositionOnReset != Vector2.zero)
		{
			contentPivot = NGUIMath.GetPivot(new Vector2(relativePositionOnReset.x, 1f - relativePositionOnReset.y));
			relativePositionOnReset = Vector2.zero;
		}
	}

	private void OnEnable()
	{
		list.Add(this);
	}

	private void OnDisable()
	{
		list.Remove(this);
	}

	protected virtual void Start()
	{
		if (Application.isPlaying)
		{
			if ((Object)(object)horizontalScrollBar != (Object)null)
			{
				EventDelegate.Add(horizontalScrollBar.onChange, OnScrollBar);
				horizontalScrollBar.alpha = ((showScrollBars == ShowCondition.Always || shouldMoveHorizontally) ? 1f : 0f);
			}
			if ((Object)(object)verticalScrollBar != (Object)null)
			{
				EventDelegate.Add(verticalScrollBar.onChange, OnScrollBar);
				verticalScrollBar.alpha = ((showScrollBars == ShowCondition.Always || shouldMoveVertically) ? 1f : 0f);
			}
		}
	}

	public bool RestrictWithinBounds(bool instant)
	{
		return RestrictWithinBounds(instant, horizontal: true, vertical: true);
	}

	public bool RestrictWithinBounds(bool instant, bool horizontal, bool vertical)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		Bounds val = bounds;
		Vector3 val2 = mPanel.CalculateConstrainOffset(Vector2.op_Implicit(((Bounds)(ref val)).min), Vector2.op_Implicit(((Bounds)(ref val)).max));
		if (!horizontal)
		{
			val2.x = 0f;
		}
		if (!vertical)
		{
			val2.y = 0f;
		}
		if (((Vector3)(ref val2)).sqrMagnitude > 0.1f)
		{
			if (!instant && dragEffect == DragEffect.MomentumAndSpring)
			{
				Vector3 val3 = mTrans.localPosition + val2;
				val3.x = Mathf.Round(val3.x);
				val3.y = Mathf.Round(val3.y);
				SpringPanel.Begin(((Component)mPanel).gameObject, val3, 13f).strength = 8f;
			}
			else
			{
				MoveRelative(val2);
				if (Mathf.Abs(val2.x) > 0.01f)
				{
					mMomentum.x = 0f;
				}
				if (Mathf.Abs(val2.y) > 0.01f)
				{
					mMomentum.y = 0f;
				}
				if (Mathf.Abs(val2.z) > 0.01f)
				{
					mMomentum.z = 0f;
				}
				mScroll = 0f;
			}
			return true;
		}
		return false;
	}

	public void DisableSpring()
	{
		SpringPanel component = ((Component)this).GetComponent<SpringPanel>();
		if ((Object)(object)component != (Object)null)
		{
			((Behaviour)component).enabled = false;
		}
	}

	public void UpdateScrollbars()
	{
		UpdateScrollbars(recalculateBounds: true);
	}

	public virtual void UpdateScrollbars(bool recalculateBounds)
	{
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)mPanel == (Object)null)
		{
			return;
		}
		if ((Object)(object)horizontalScrollBar != (Object)null || (Object)(object)verticalScrollBar != (Object)null)
		{
			if (recalculateBounds)
			{
				mCalculatedBounds = false;
				mShouldMove = shouldMove;
			}
			Bounds val = bounds;
			Vector2 val2 = Vector2.op_Implicit(((Bounds)(ref val)).min);
			Vector2 val3 = Vector2.op_Implicit(((Bounds)(ref val)).max);
			if ((Object)(object)horizontalScrollBar != (Object)null && val3.x > val2.x)
			{
				Vector4 finalClipRegion = mPanel.finalClipRegion;
				int num = Mathf.RoundToInt(finalClipRegion.z);
				if (((uint)num & (true ? 1u : 0u)) != 0)
				{
					num--;
				}
				float num2 = (float)num * 0.5f;
				num2 = Mathf.Round(num2);
				if (mPanel.clipping == UIDrawCall.Clipping.SoftClip)
				{
					num2 -= mPanel.clipSoftness.x;
				}
				float contentSize = val3.x - val2.x;
				float viewSize = num2 * 2f;
				float x = val2.x;
				float x2 = val3.x;
				float num3 = finalClipRegion.x - num2;
				float num4 = finalClipRegion.x + num2;
				x = num3 - x;
				x2 -= num4;
				UpdateScrollbars(horizontalScrollBar, x, x2, contentSize, viewSize, inverted: false);
			}
			if ((Object)(object)verticalScrollBar != (Object)null && val3.y > val2.y)
			{
				Vector4 finalClipRegion2 = mPanel.finalClipRegion;
				int num5 = Mathf.RoundToInt(finalClipRegion2.w);
				if (((uint)num5 & (true ? 1u : 0u)) != 0)
				{
					num5--;
				}
				float num6 = (float)num5 * 0.5f;
				num6 = Mathf.Round(num6);
				if (mPanel.clipping == UIDrawCall.Clipping.SoftClip)
				{
					num6 -= mPanel.clipSoftness.y;
				}
				float contentSize2 = val3.y - val2.y;
				float viewSize2 = num6 * 2f;
				float y = val2.y;
				float y2 = val3.y;
				float num7 = finalClipRegion2.y - num6;
				float num8 = finalClipRegion2.y + num6;
				y = num7 - y;
				y2 -= num8;
				UpdateScrollbars(verticalScrollBar, y, y2, contentSize2, viewSize2, inverted: true);
			}
		}
		else if (recalculateBounds)
		{
			mCalculatedBounds = false;
		}
	}

	protected void UpdateScrollbars(UIProgressBar slider, float contentMin, float contentMax, float contentSize, float viewSize, bool inverted)
	{
		if ((Object)(object)slider == (Object)null)
		{
			return;
		}
		mIgnoreCallbacks = true;
		float num;
		if (viewSize < contentSize)
		{
			contentMin = Mathf.Clamp01(contentMin / contentSize);
			contentMax = Mathf.Clamp01(contentMax / contentSize);
			num = contentMin + contentMax;
			slider.value = ((!inverted) ? ((num > 0.001f) ? (contentMin / num) : 1f) : ((num > 0.001f) ? (1f - contentMin / num) : 0f));
		}
		else
		{
			contentMin = Mathf.Clamp01((0f - contentMin) / contentSize);
			contentMax = Mathf.Clamp01((0f - contentMax) / contentSize);
			num = contentMin + contentMax;
			slider.value = ((!inverted) ? ((num > 0.001f) ? (contentMin / num) : 1f) : ((num > 0.001f) ? (1f - contentMin / num) : 0f));
			if (contentSize > 0f)
			{
				contentMin = Mathf.Clamp01(contentMin / contentSize);
				contentMax = Mathf.Clamp01(contentMax / contentSize);
				num = contentMin + contentMax;
			}
		}
		UIScrollBar uIScrollBar = slider as UIScrollBar;
		if ((Object)(object)uIScrollBar != (Object)null)
		{
			uIScrollBar.barSize = 1f - num;
		}
		mIgnoreCallbacks = false;
	}

	public virtual void SetDragAmount(float x, float y, bool updateScrollbars)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)mPanel == (Object)null)
		{
			mPanel = ((Component)this).GetComponent<UIPanel>();
		}
		DisableSpring();
		Bounds val = bounds;
		if (((Bounds)(ref val)).min.x == ((Bounds)(ref val)).max.x || ((Bounds)(ref val)).min.y == ((Bounds)(ref val)).max.y)
		{
			return;
		}
		Vector4 finalClipRegion = mPanel.finalClipRegion;
		float num = finalClipRegion.z * 0.5f;
		float num2 = finalClipRegion.w * 0.5f;
		float num3 = ((Bounds)(ref val)).min.x + num;
		float num4 = ((Bounds)(ref val)).max.x - num;
		float num5 = ((Bounds)(ref val)).min.y + num2;
		float num6 = ((Bounds)(ref val)).max.y - num2;
		if (mPanel.clipping == UIDrawCall.Clipping.SoftClip)
		{
			num3 -= mPanel.clipSoftness.x;
			num4 += mPanel.clipSoftness.x;
			num5 -= mPanel.clipSoftness.y;
			num6 += mPanel.clipSoftness.y;
		}
		float num7 = Mathf.Lerp(num3, num4, x);
		float num8 = Mathf.Lerp(num6, num5, y);
		if (!updateScrollbars)
		{
			Vector3 localPosition = mTrans.localPosition;
			if (canMoveHorizontally)
			{
				localPosition.x += finalClipRegion.x - num7;
			}
			if (canMoveVertically)
			{
				localPosition.y += finalClipRegion.y - num8;
			}
			mTrans.localPosition = localPosition;
		}
		if (canMoveHorizontally)
		{
			finalClipRegion.x = num7;
		}
		if (canMoveVertically)
		{
			finalClipRegion.y = num8;
		}
		Vector4 baseClipRegion = mPanel.baseClipRegion;
		mPanel.clipOffset = new Vector2(finalClipRegion.x - baseClipRegion.x, finalClipRegion.y - baseClipRegion.y);
		if (updateScrollbars)
		{
			UpdateScrollbars(mDragID == -10);
		}
	}

	public void InvalidateBounds()
	{
		mCalculatedBounds = false;
	}

	[ContextMenu("Reset Clipping Position")]
	public void ResetPosition()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		if (NGUITools.GetActive((Behaviour)(object)this))
		{
			mCalculatedBounds = false;
			Vector2 pivotOffset = NGUIMath.GetPivotOffset(contentPivot);
			SetDragAmount(pivotOffset.x, 1f - pivotOffset.y, updateScrollbars: false);
			SetDragAmount(pivotOffset.x, 1f - pivotOffset.y, updateScrollbars: true);
		}
	}

	public void UpdatePosition()
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		if (!mIgnoreCallbacks && ((Object)(object)horizontalScrollBar != (Object)null || (Object)(object)verticalScrollBar != (Object)null))
		{
			mIgnoreCallbacks = true;
			mCalculatedBounds = false;
			Vector2 pivotOffset = NGUIMath.GetPivotOffset(contentPivot);
			float x = (((Object)(object)horizontalScrollBar != (Object)null) ? horizontalScrollBar.value : pivotOffset.x);
			float y = (((Object)(object)verticalScrollBar != (Object)null) ? verticalScrollBar.value : (1f - pivotOffset.y));
			SetDragAmount(x, y, updateScrollbars: false);
			UpdateScrollbars(recalculateBounds: true);
			mIgnoreCallbacks = false;
		}
	}

	public void OnScrollBar()
	{
		if (!mIgnoreCallbacks)
		{
			mIgnoreCallbacks = true;
			float x = (((Object)(object)horizontalScrollBar != (Object)null) ? horizontalScrollBar.value : 0f);
			float y = (((Object)(object)verticalScrollBar != (Object)null) ? verticalScrollBar.value : 0f);
			SetDragAmount(x, y, updateScrollbars: false);
			mIgnoreCallbacks = false;
		}
	}

	public virtual void MoveRelative(Vector3 relative)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		Transform obj = mTrans;
		obj.localPosition += relative;
		Vector2 clipOffset = mPanel.clipOffset;
		clipOffset.x -= relative.x;
		clipOffset.y -= relative.y;
		mPanel.clipOffset = clipOffset;
		UpdateScrollbars(recalculateBounds: false);
	}

	public void MoveAbsolute(Vector3 absolute)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		Vector3 val = mTrans.InverseTransformPoint(absolute);
		Vector3 val2 = mTrans.InverseTransformPoint(Vector3.zero);
		MoveRelative(val - val2);
	}

	public void Press(bool pressed)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		if (smoothDragStart && pressed)
		{
			mDragStarted = false;
			mDragStartOffset = Vector2.zero;
		}
		if (!((Behaviour)this).enabled || !NGUITools.GetActive(((Component)this).gameObject))
		{
			return;
		}
		if (!pressed && mDragID == UICamera.currentTouchID)
		{
			mDragID = -10;
		}
		mCalculatedBounds = false;
		mShouldMove = shouldMove;
		if (!mShouldMove)
		{
			return;
		}
		mPressed = pressed;
		if (pressed)
		{
			mMomentum = Vector3.zero;
			mScroll = 0f;
			DisableSpring();
			mLastPos = UICamera.lastWorldPosition;
			mPlane = new Plane(mTrans.rotation * Vector3.back, mLastPos);
			Vector2 clipOffset = mPanel.clipOffset;
			clipOffset.x = Mathf.Round(clipOffset.x);
			clipOffset.y = Mathf.Round(clipOffset.y);
			mPanel.clipOffset = clipOffset;
			Vector3 localPosition = mTrans.localPosition;
			localPosition.x = Mathf.Round(localPosition.x);
			localPosition.y = Mathf.Round(localPosition.y);
			mTrans.localPosition = localPosition;
		}
		else
		{
			if (restrictWithinPanel && mPanel.clipping != 0)
			{
				RestrictWithinBounds(dragEffect == DragEffect.None, canMoveHorizontally, canMoveVertically);
			}
			if (onDragFinished != null)
			{
				onDragFinished();
			}
			if (!mShouldMove && onStoppedMoving != null)
			{
				onStoppedMoving();
			}
		}
	}

	public void Drag()
	{
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		if (!((Behaviour)this).enabled || !NGUITools.GetActive(((Component)this).gameObject) || !mShouldMove)
		{
			return;
		}
		if (mDragID == -10)
		{
			mDragID = UICamera.currentTouchID;
		}
		UICamera.currentTouch.clickNotification = UICamera.ClickNotification.BasedOnDelta;
		if (smoothDragStart && !mDragStarted)
		{
			mDragStarted = true;
			mDragStartOffset = UICamera.currentTouch.totalDelta;
			if (onDragStarted != null)
			{
				onDragStarted();
			}
		}
		Ray val = (smoothDragStart ? UICamera.currentCamera.ScreenPointToRay(Vector2.op_Implicit(UICamera.currentTouch.pos - mDragStartOffset)) : UICamera.currentCamera.ScreenPointToRay(Vector2.op_Implicit(UICamera.currentTouch.pos)));
		float num = 0f;
		if (!((Plane)(ref mPlane)).Raycast(val, ref num))
		{
			return;
		}
		Vector3 point = ((Ray)(ref val)).GetPoint(num);
		Vector3 val2 = point - mLastPos;
		mLastPos = point;
		if (val2.x != 0f || val2.y != 0f || val2.z != 0f)
		{
			val2 = mTrans.InverseTransformDirection(val2);
			if (movement == Movement.Horizontal)
			{
				val2.y = 0f;
				val2.z = 0f;
			}
			else if (movement == Movement.Vertical)
			{
				val2.x = 0f;
				val2.z = 0f;
			}
			else if (movement == Movement.Unrestricted)
			{
				val2.z = 0f;
			}
			else
			{
				((Vector3)(ref val2)).Scale(Vector2.op_Implicit(customMovement));
			}
			val2 = mTrans.TransformDirection(val2);
		}
		if (dragEffect == DragEffect.None)
		{
			mMomentum = Vector3.zero;
		}
		else
		{
			mMomentum = Vector3.Lerp(mMomentum, mMomentum + val2 * (0.01f * momentumAmount), 0.67f);
		}
		if (!iOSDragEmulation || dragEffect != DragEffect.MomentumAndSpring)
		{
			MoveAbsolute(val2);
		}
		else
		{
			UIPanel uIPanel = mPanel;
			Bounds val3 = bounds;
			Vector2 min = Vector2.op_Implicit(((Bounds)(ref val3)).min);
			val3 = bounds;
			Vector3 val4 = uIPanel.CalculateConstrainOffset(min, Vector2.op_Implicit(((Bounds)(ref val3)).max));
			if (((Vector3)(ref val4)).magnitude > 1f)
			{
				MoveAbsolute(val2 * 0.5f);
				mMomentum *= 0.5f;
			}
			else
			{
				MoveAbsolute(val2);
			}
		}
		if (restrictWithinPanel && mPanel.clipping != 0 && dragEffect != DragEffect.MomentumAndSpring)
		{
			RestrictWithinBounds(instant: true, canMoveHorizontally, canMoveVertically);
		}
	}

	public void Scroll(float delta)
	{
		if (((Behaviour)this).enabled && NGUITools.GetActive(((Component)this).gameObject) && scrollWheelFactor != 0f)
		{
			DisableSpring();
			mShouldMove = shouldMove;
			if (Mathf.Sign(mScroll) != Mathf.Sign(delta))
			{
				mScroll = 0f;
			}
			mScroll += delta * scrollWheelFactor;
		}
	}

	private void LateUpdate()
	{
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		if (!Application.isPlaying)
		{
			return;
		}
		float deltaTime = RealTime.deltaTime;
		if (showScrollBars != 0 && (Object.op_Implicit((Object)(object)verticalScrollBar) || Object.op_Implicit((Object)(object)horizontalScrollBar)))
		{
			bool flag = false;
			bool flag2 = false;
			if (showScrollBars != ShowCondition.WhenDragging || mDragID != -10 || ((Vector3)(ref mMomentum)).magnitude > 0.01f)
			{
				flag = shouldMoveVertically;
				flag2 = shouldMoveHorizontally;
			}
			if (Object.op_Implicit((Object)(object)verticalScrollBar))
			{
				float alpha = verticalScrollBar.alpha;
				alpha += (flag ? (deltaTime * 6f) : ((0f - deltaTime) * 3f));
				alpha = Mathf.Clamp01(alpha);
				if (verticalScrollBar.alpha != alpha)
				{
					verticalScrollBar.alpha = alpha;
				}
			}
			if (Object.op_Implicit((Object)(object)horizontalScrollBar))
			{
				float alpha2 = horizontalScrollBar.alpha;
				alpha2 += (flag2 ? (deltaTime * 6f) : ((0f - deltaTime) * 3f));
				alpha2 = Mathf.Clamp01(alpha2);
				if (horizontalScrollBar.alpha != alpha2)
				{
					horizontalScrollBar.alpha = alpha2;
				}
			}
		}
		if (!mShouldMove)
		{
			return;
		}
		if (!mPressed)
		{
			if (((Vector3)(ref mMomentum)).magnitude > 0.0001f || mScroll != 0f)
			{
				if (movement == Movement.Horizontal)
				{
					mMomentum -= mTrans.TransformDirection(new Vector3(mScroll * 0.05f, 0f, 0f));
				}
				else if (movement == Movement.Vertical)
				{
					mMomentum -= mTrans.TransformDirection(new Vector3(0f, mScroll * 0.05f, 0f));
				}
				else if (movement == Movement.Unrestricted)
				{
					mMomentum -= mTrans.TransformDirection(new Vector3(mScroll * 0.05f, mScroll * 0.05f, 0f));
				}
				else
				{
					mMomentum -= mTrans.TransformDirection(new Vector3(mScroll * customMovement.x * 0.05f, mScroll * customMovement.y * 0.05f, 0f));
				}
				mScroll = NGUIMath.SpringLerp(mScroll, 0f, 20f, deltaTime);
				Vector3 absolute = NGUIMath.SpringDampen(ref mMomentum, 9f, deltaTime);
				MoveAbsolute(absolute);
				if (restrictWithinPanel && mPanel.clipping != 0)
				{
					RestrictWithinBounds(instant: false, canMoveHorizontally, canMoveVertically);
				}
				if (onMomentumMove != null)
				{
					onMomentumMove();
				}
				return;
			}
			mScroll = 0f;
			mMomentum = Vector3.zero;
			SpringPanel component = ((Component)this).GetComponent<SpringPanel>();
			if (!((Object)(object)component != (Object)null) || !((Behaviour)component).enabled)
			{
				mShouldMove = false;
				if (onStoppedMoving != null)
				{
					onStoppedMoving();
				}
			}
		}
		else
		{
			mScroll = 0f;
			NGUIMath.SpringDampen(ref mMomentum, 9f, deltaTime);
		}
	}
}
