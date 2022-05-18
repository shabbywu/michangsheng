using System;
using UnityEngine;

// Token: 0x0200009E RID: 158
[ExecuteInEditMode]
[RequireComponent(typeof(UIPanel))]
[AddComponentMenu("NGUI/Interaction/Scroll View")]
public class UIScrollView : MonoBehaviour
{
	// Token: 0x170000AE RID: 174
	// (get) Token: 0x06000621 RID: 1569 RVA: 0x00009819 File Offset: 0x00007A19
	public UIPanel panel
	{
		get
		{
			return this.mPanel;
		}
	}

	// Token: 0x170000AF RID: 175
	// (get) Token: 0x06000622 RID: 1570 RVA: 0x00009821 File Offset: 0x00007A21
	public bool isDragging
	{
		get
		{
			return this.mPressed && this.mDragStarted;
		}
	}

	// Token: 0x170000B0 RID: 176
	// (get) Token: 0x06000623 RID: 1571 RVA: 0x00009833 File Offset: 0x00007A33
	public virtual Bounds bounds
	{
		get
		{
			if (!this.mCalculatedBounds)
			{
				this.mCalculatedBounds = true;
				this.mTrans = base.transform;
				this.mBounds = NGUIMath.CalculateRelativeWidgetBounds(this.mTrans, this.mTrans);
			}
			return this.mBounds;
		}
	}

	// Token: 0x170000B1 RID: 177
	// (get) Token: 0x06000624 RID: 1572 RVA: 0x0000986D File Offset: 0x00007A6D
	public bool canMoveHorizontally
	{
		get
		{
			return this.movement == UIScrollView.Movement.Horizontal || this.movement == UIScrollView.Movement.Unrestricted || (this.movement == UIScrollView.Movement.Custom && this.customMovement.x != 0f);
		}
	}

	// Token: 0x170000B2 RID: 178
	// (get) Token: 0x06000625 RID: 1573 RVA: 0x000098A2 File Offset: 0x00007AA2
	public bool canMoveVertically
	{
		get
		{
			return this.movement == UIScrollView.Movement.Vertical || this.movement == UIScrollView.Movement.Unrestricted || (this.movement == UIScrollView.Movement.Custom && this.customMovement.y != 0f);
		}
	}

	// Token: 0x170000B3 RID: 179
	// (get) Token: 0x06000626 RID: 1574 RVA: 0x000759E8 File Offset: 0x00073BE8
	public virtual bool shouldMoveHorizontally
	{
		get
		{
			float num = this.bounds.size.x;
			if (this.mPanel.clipping == UIDrawCall.Clipping.SoftClip)
			{
				num += this.mPanel.clipSoftness.x * 2f;
			}
			return Mathf.RoundToInt(num - this.mPanel.width) > 0;
		}
	}

	// Token: 0x170000B4 RID: 180
	// (get) Token: 0x06000627 RID: 1575 RVA: 0x00075A48 File Offset: 0x00073C48
	public virtual bool shouldMoveVertically
	{
		get
		{
			float num = this.bounds.size.y;
			if (this.mPanel.clipping == UIDrawCall.Clipping.SoftClip)
			{
				num += this.mPanel.clipSoftness.y * 2f;
			}
			return Mathf.RoundToInt(num - this.mPanel.height) > 0;
		}
	}

	// Token: 0x170000B5 RID: 181
	// (get) Token: 0x06000628 RID: 1576 RVA: 0x00075AA8 File Offset: 0x00073CA8
	protected virtual bool shouldMove
	{
		get
		{
			if (!this.disableDragIfFits)
			{
				return true;
			}
			if (this.mPanel == null)
			{
				this.mPanel = base.GetComponent<UIPanel>();
			}
			Vector4 finalClipRegion = this.mPanel.finalClipRegion;
			Bounds bounds = this.bounds;
			float num = (finalClipRegion.z == 0f) ? ((float)Screen.width) : (finalClipRegion.z * 0.5f);
			float num2 = (finalClipRegion.w == 0f) ? ((float)Screen.height) : (finalClipRegion.w * 0.5f);
			if (this.canMoveHorizontally)
			{
				if (bounds.min.x < finalClipRegion.x - num)
				{
					return true;
				}
				if (bounds.max.x > finalClipRegion.x + num)
				{
					return true;
				}
			}
			if (this.canMoveVertically)
			{
				if (bounds.min.y < finalClipRegion.y - num2)
				{
					return true;
				}
				if (bounds.max.y > finalClipRegion.y + num2)
				{
					return true;
				}
			}
			return false;
		}
	}

	// Token: 0x170000B6 RID: 182
	// (get) Token: 0x06000629 RID: 1577 RVA: 0x000098D8 File Offset: 0x00007AD8
	// (set) Token: 0x0600062A RID: 1578 RVA: 0x000098E0 File Offset: 0x00007AE0
	public Vector3 currentMomentum
	{
		get
		{
			return this.mMomentum;
		}
		set
		{
			this.mMomentum = value;
			this.mShouldMove = true;
		}
	}

	// Token: 0x0600062B RID: 1579 RVA: 0x00075BA4 File Offset: 0x00073DA4
	private void Awake()
	{
		this.mTrans = base.transform;
		this.mPanel = base.GetComponent<UIPanel>();
		if (this.mPanel.clipping == UIDrawCall.Clipping.None)
		{
			this.mPanel.clipping = UIDrawCall.Clipping.ConstrainButDontClip;
		}
		if (this.movement != UIScrollView.Movement.Custom && this.scale.sqrMagnitude > 0.001f)
		{
			if (this.scale.x == 1f && this.scale.y == 0f)
			{
				this.movement = UIScrollView.Movement.Horizontal;
			}
			else if (this.scale.x == 0f && this.scale.y == 1f)
			{
				this.movement = UIScrollView.Movement.Vertical;
			}
			else if (this.scale.x == 1f && this.scale.y == 1f)
			{
				this.movement = UIScrollView.Movement.Unrestricted;
			}
			else
			{
				this.movement = UIScrollView.Movement.Custom;
				this.customMovement.x = this.scale.x;
				this.customMovement.y = this.scale.y;
			}
			this.scale = Vector3.zero;
		}
		if (this.contentPivot == UIWidget.Pivot.TopLeft && this.relativePositionOnReset != Vector2.zero)
		{
			this.contentPivot = NGUIMath.GetPivot(new Vector2(this.relativePositionOnReset.x, 1f - this.relativePositionOnReset.y));
			this.relativePositionOnReset = Vector2.zero;
		}
	}

	// Token: 0x0600062C RID: 1580 RVA: 0x000098F0 File Offset: 0x00007AF0
	private void OnEnable()
	{
		UIScrollView.list.Add(this);
	}

	// Token: 0x0600062D RID: 1581 RVA: 0x000098FD File Offset: 0x00007AFD
	private void OnDisable()
	{
		UIScrollView.list.Remove(this);
	}

	// Token: 0x0600062E RID: 1582 RVA: 0x00075D1C File Offset: 0x00073F1C
	protected virtual void Start()
	{
		if (Application.isPlaying)
		{
			if (this.horizontalScrollBar != null)
			{
				EventDelegate.Add(this.horizontalScrollBar.onChange, new EventDelegate.Callback(this.OnScrollBar));
				this.horizontalScrollBar.alpha = ((this.showScrollBars == UIScrollView.ShowCondition.Always || this.shouldMoveHorizontally) ? 1f : 0f);
			}
			if (this.verticalScrollBar != null)
			{
				EventDelegate.Add(this.verticalScrollBar.onChange, new EventDelegate.Callback(this.OnScrollBar));
				this.verticalScrollBar.alpha = ((this.showScrollBars == UIScrollView.ShowCondition.Always || this.shouldMoveVertically) ? 1f : 0f);
			}
		}
	}

	// Token: 0x0600062F RID: 1583 RVA: 0x0000990B File Offset: 0x00007B0B
	public bool RestrictWithinBounds(bool instant)
	{
		return this.RestrictWithinBounds(instant, true, true);
	}

	// Token: 0x06000630 RID: 1584 RVA: 0x00075DD8 File Offset: 0x00073FD8
	public bool RestrictWithinBounds(bool instant, bool horizontal, bool vertical)
	{
		Bounds bounds = this.bounds;
		Vector3 vector = this.mPanel.CalculateConstrainOffset(bounds.min, bounds.max);
		if (!horizontal)
		{
			vector.x = 0f;
		}
		if (!vertical)
		{
			vector.y = 0f;
		}
		if (vector.sqrMagnitude > 0.1f)
		{
			if (!instant && this.dragEffect == UIScrollView.DragEffect.MomentumAndSpring)
			{
				Vector3 vector2 = this.mTrans.localPosition + vector;
				vector2.x = Mathf.Round(vector2.x);
				vector2.y = Mathf.Round(vector2.y);
				SpringPanel.Begin(this.mPanel.gameObject, vector2, 13f).strength = 8f;
			}
			else
			{
				this.MoveRelative(vector);
				if (Mathf.Abs(vector.x) > 0.01f)
				{
					this.mMomentum.x = 0f;
				}
				if (Mathf.Abs(vector.y) > 0.01f)
				{
					this.mMomentum.y = 0f;
				}
				if (Mathf.Abs(vector.z) > 0.01f)
				{
					this.mMomentum.z = 0f;
				}
				this.mScroll = 0f;
			}
			return true;
		}
		return false;
	}

	// Token: 0x06000631 RID: 1585 RVA: 0x00075F20 File Offset: 0x00074120
	public void DisableSpring()
	{
		SpringPanel component = base.GetComponent<SpringPanel>();
		if (component != null)
		{
			component.enabled = false;
		}
	}

	// Token: 0x06000632 RID: 1586 RVA: 0x00009916 File Offset: 0x00007B16
	public void UpdateScrollbars()
	{
		this.UpdateScrollbars(true);
	}

	// Token: 0x06000633 RID: 1587 RVA: 0x00075F44 File Offset: 0x00074144
	public virtual void UpdateScrollbars(bool recalculateBounds)
	{
		if (this.mPanel == null)
		{
			return;
		}
		if (this.horizontalScrollBar != null || this.verticalScrollBar != null)
		{
			if (recalculateBounds)
			{
				this.mCalculatedBounds = false;
				this.mShouldMove = this.shouldMove;
			}
			Bounds bounds = this.bounds;
			Vector2 vector = bounds.min;
			Vector2 vector2 = bounds.max;
			if (this.horizontalScrollBar != null && vector2.x > vector.x)
			{
				Vector4 finalClipRegion = this.mPanel.finalClipRegion;
				int num = Mathf.RoundToInt(finalClipRegion.z);
				if ((num & 1) != 0)
				{
					num--;
				}
				float num2 = (float)num * 0.5f;
				num2 = Mathf.Round(num2);
				if (this.mPanel.clipping == UIDrawCall.Clipping.SoftClip)
				{
					num2 -= this.mPanel.clipSoftness.x;
				}
				float contentSize = vector2.x - vector.x;
				float viewSize = num2 * 2f;
				float num3 = vector.x;
				float num4 = vector2.x;
				float num5 = finalClipRegion.x - num2;
				float num6 = finalClipRegion.x + num2;
				num3 = num5 - num3;
				num4 -= num6;
				this.UpdateScrollbars(this.horizontalScrollBar, num3, num4, contentSize, viewSize, false);
			}
			if (this.verticalScrollBar != null && vector2.y > vector.y)
			{
				Vector4 finalClipRegion2 = this.mPanel.finalClipRegion;
				int num7 = Mathf.RoundToInt(finalClipRegion2.w);
				if ((num7 & 1) != 0)
				{
					num7--;
				}
				float num8 = (float)num7 * 0.5f;
				num8 = Mathf.Round(num8);
				if (this.mPanel.clipping == UIDrawCall.Clipping.SoftClip)
				{
					num8 -= this.mPanel.clipSoftness.y;
				}
				float contentSize2 = vector2.y - vector.y;
				float viewSize2 = num8 * 2f;
				float num9 = vector.y;
				float num10 = vector2.y;
				float num11 = finalClipRegion2.y - num8;
				float num12 = finalClipRegion2.y + num8;
				num9 = num11 - num9;
				num10 -= num12;
				this.UpdateScrollbars(this.verticalScrollBar, num9, num10, contentSize2, viewSize2, true);
				return;
			}
		}
		else if (recalculateBounds)
		{
			this.mCalculatedBounds = false;
		}
	}

	// Token: 0x06000634 RID: 1588 RVA: 0x00076178 File Offset: 0x00074378
	protected void UpdateScrollbars(UIProgressBar slider, float contentMin, float contentMax, float contentSize, float viewSize, bool inverted)
	{
		if (slider == null)
		{
			return;
		}
		this.mIgnoreCallbacks = true;
		float num;
		if (viewSize < contentSize)
		{
			contentMin = Mathf.Clamp01(contentMin / contentSize);
			contentMax = Mathf.Clamp01(contentMax / contentSize);
			num = contentMin + contentMax;
			slider.value = (inverted ? ((num > 0.001f) ? (1f - contentMin / num) : 0f) : ((num > 0.001f) ? (contentMin / num) : 1f));
		}
		else
		{
			contentMin = Mathf.Clamp01(-contentMin / contentSize);
			contentMax = Mathf.Clamp01(-contentMax / contentSize);
			num = contentMin + contentMax;
			slider.value = (inverted ? ((num > 0.001f) ? (1f - contentMin / num) : 0f) : ((num > 0.001f) ? (contentMin / num) : 1f));
			if (contentSize > 0f)
			{
				contentMin = Mathf.Clamp01(contentMin / contentSize);
				contentMax = Mathf.Clamp01(contentMax / contentSize);
				num = contentMin + contentMax;
			}
		}
		UIScrollBar uiscrollBar = slider as UIScrollBar;
		if (uiscrollBar != null)
		{
			uiscrollBar.barSize = 1f - num;
		}
		this.mIgnoreCallbacks = false;
	}

	// Token: 0x06000635 RID: 1589 RVA: 0x00076288 File Offset: 0x00074488
	public virtual void SetDragAmount(float x, float y, bool updateScrollbars)
	{
		if (this.mPanel == null)
		{
			this.mPanel = base.GetComponent<UIPanel>();
		}
		this.DisableSpring();
		Bounds bounds = this.bounds;
		if (bounds.min.x == bounds.max.x || bounds.min.y == bounds.max.y)
		{
			return;
		}
		Vector4 finalClipRegion = this.mPanel.finalClipRegion;
		float num = finalClipRegion.z * 0.5f;
		float num2 = finalClipRegion.w * 0.5f;
		float num3 = bounds.min.x + num;
		float num4 = bounds.max.x - num;
		float num5 = bounds.min.y + num2;
		float num6 = bounds.max.y - num2;
		if (this.mPanel.clipping == UIDrawCall.Clipping.SoftClip)
		{
			num3 -= this.mPanel.clipSoftness.x;
			num4 += this.mPanel.clipSoftness.x;
			num5 -= this.mPanel.clipSoftness.y;
			num6 += this.mPanel.clipSoftness.y;
		}
		float num7 = Mathf.Lerp(num3, num4, x);
		float num8 = Mathf.Lerp(num6, num5, y);
		if (!updateScrollbars)
		{
			Vector3 localPosition = this.mTrans.localPosition;
			if (this.canMoveHorizontally)
			{
				localPosition.x += finalClipRegion.x - num7;
			}
			if (this.canMoveVertically)
			{
				localPosition.y += finalClipRegion.y - num8;
			}
			this.mTrans.localPosition = localPosition;
		}
		if (this.canMoveHorizontally)
		{
			finalClipRegion.x = num7;
		}
		if (this.canMoveVertically)
		{
			finalClipRegion.y = num8;
		}
		Vector4 baseClipRegion = this.mPanel.baseClipRegion;
		this.mPanel.clipOffset = new Vector2(finalClipRegion.x - baseClipRegion.x, finalClipRegion.y - baseClipRegion.y);
		if (updateScrollbars)
		{
			this.UpdateScrollbars(this.mDragID == -10);
		}
	}

	// Token: 0x06000636 RID: 1590 RVA: 0x0000991F File Offset: 0x00007B1F
	public void InvalidateBounds()
	{
		this.mCalculatedBounds = false;
	}

	// Token: 0x06000637 RID: 1591 RVA: 0x00076494 File Offset: 0x00074694
	[ContextMenu("Reset Clipping Position")]
	public void ResetPosition()
	{
		if (NGUITools.GetActive(this))
		{
			this.mCalculatedBounds = false;
			Vector2 pivotOffset = NGUIMath.GetPivotOffset(this.contentPivot);
			this.SetDragAmount(pivotOffset.x, 1f - pivotOffset.y, false);
			this.SetDragAmount(pivotOffset.x, 1f - pivotOffset.y, true);
		}
	}

	// Token: 0x06000638 RID: 1592 RVA: 0x000764F0 File Offset: 0x000746F0
	public void UpdatePosition()
	{
		if (!this.mIgnoreCallbacks && (this.horizontalScrollBar != null || this.verticalScrollBar != null))
		{
			this.mIgnoreCallbacks = true;
			this.mCalculatedBounds = false;
			Vector2 pivotOffset = NGUIMath.GetPivotOffset(this.contentPivot);
			float x = (this.horizontalScrollBar != null) ? this.horizontalScrollBar.value : pivotOffset.x;
			float y = (this.verticalScrollBar != null) ? this.verticalScrollBar.value : (1f - pivotOffset.y);
			this.SetDragAmount(x, y, false);
			this.UpdateScrollbars(true);
			this.mIgnoreCallbacks = false;
		}
	}

	// Token: 0x06000639 RID: 1593 RVA: 0x000765A0 File Offset: 0x000747A0
	public void OnScrollBar()
	{
		if (!this.mIgnoreCallbacks)
		{
			this.mIgnoreCallbacks = true;
			float x = (this.horizontalScrollBar != null) ? this.horizontalScrollBar.value : 0f;
			float y = (this.verticalScrollBar != null) ? this.verticalScrollBar.value : 0f;
			this.SetDragAmount(x, y, false);
			this.mIgnoreCallbacks = false;
		}
	}

	// Token: 0x0600063A RID: 1594 RVA: 0x00076610 File Offset: 0x00074810
	public virtual void MoveRelative(Vector3 relative)
	{
		this.mTrans.localPosition += relative;
		Vector2 clipOffset = this.mPanel.clipOffset;
		clipOffset.x -= relative.x;
		clipOffset.y -= relative.y;
		this.mPanel.clipOffset = clipOffset;
		this.UpdateScrollbars(false);
	}

	// Token: 0x0600063B RID: 1595 RVA: 0x00076678 File Offset: 0x00074878
	public void MoveAbsolute(Vector3 absolute)
	{
		Vector3 vector = this.mTrans.InverseTransformPoint(absolute);
		Vector3 vector2 = this.mTrans.InverseTransformPoint(Vector3.zero);
		this.MoveRelative(vector - vector2);
	}

	// Token: 0x0600063C RID: 1596 RVA: 0x000766B0 File Offset: 0x000748B0
	public void Press(bool pressed)
	{
		if (this.smoothDragStart && pressed)
		{
			this.mDragStarted = false;
			this.mDragStartOffset = Vector2.zero;
		}
		if (base.enabled && NGUITools.GetActive(base.gameObject))
		{
			if (!pressed && this.mDragID == UICamera.currentTouchID)
			{
				this.mDragID = -10;
			}
			this.mCalculatedBounds = false;
			this.mShouldMove = this.shouldMove;
			if (!this.mShouldMove)
			{
				return;
			}
			this.mPressed = pressed;
			if (pressed)
			{
				this.mMomentum = Vector3.zero;
				this.mScroll = 0f;
				this.DisableSpring();
				this.mLastPos = UICamera.lastWorldPosition;
				this.mPlane = new Plane(this.mTrans.rotation * Vector3.back, this.mLastPos);
				Vector2 clipOffset = this.mPanel.clipOffset;
				clipOffset.x = Mathf.Round(clipOffset.x);
				clipOffset.y = Mathf.Round(clipOffset.y);
				this.mPanel.clipOffset = clipOffset;
				Vector3 localPosition = this.mTrans.localPosition;
				localPosition.x = Mathf.Round(localPosition.x);
				localPosition.y = Mathf.Round(localPosition.y);
				this.mTrans.localPosition = localPosition;
				return;
			}
			if (this.restrictWithinPanel && this.mPanel.clipping != UIDrawCall.Clipping.None)
			{
				this.RestrictWithinBounds(this.dragEffect == UIScrollView.DragEffect.None, this.canMoveHorizontally, this.canMoveVertically);
			}
			if (this.onDragFinished != null)
			{
				this.onDragFinished();
			}
			if (!this.mShouldMove && this.onStoppedMoving != null)
			{
				this.onStoppedMoving();
			}
		}
	}

	// Token: 0x0600063D RID: 1597 RVA: 0x0007685C File Offset: 0x00074A5C
	public void Drag()
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject) && this.mShouldMove)
		{
			if (this.mDragID == -10)
			{
				this.mDragID = UICamera.currentTouchID;
			}
			UICamera.currentTouch.clickNotification = UICamera.ClickNotification.BasedOnDelta;
			if (this.smoothDragStart && !this.mDragStarted)
			{
				this.mDragStarted = true;
				this.mDragStartOffset = UICamera.currentTouch.totalDelta;
				if (this.onDragStarted != null)
				{
					this.onDragStarted();
				}
			}
			Ray ray = this.smoothDragStart ? UICamera.currentCamera.ScreenPointToRay(UICamera.currentTouch.pos - this.mDragStartOffset) : UICamera.currentCamera.ScreenPointToRay(UICamera.currentTouch.pos);
			float num = 0f;
			if (this.mPlane.Raycast(ray, ref num))
			{
				Vector3 point = ray.GetPoint(num);
				Vector3 vector = point - this.mLastPos;
				this.mLastPos = point;
				if (vector.x != 0f || vector.y != 0f || vector.z != 0f)
				{
					vector = this.mTrans.InverseTransformDirection(vector);
					if (this.movement == UIScrollView.Movement.Horizontal)
					{
						vector.y = 0f;
						vector.z = 0f;
					}
					else if (this.movement == UIScrollView.Movement.Vertical)
					{
						vector.x = 0f;
						vector.z = 0f;
					}
					else if (this.movement == UIScrollView.Movement.Unrestricted)
					{
						vector.z = 0f;
					}
					else
					{
						vector.Scale(this.customMovement);
					}
					vector = this.mTrans.TransformDirection(vector);
				}
				if (this.dragEffect == UIScrollView.DragEffect.None)
				{
					this.mMomentum = Vector3.zero;
				}
				else
				{
					this.mMomentum = Vector3.Lerp(this.mMomentum, this.mMomentum + vector * (0.01f * this.momentumAmount), 0.67f);
				}
				if (!this.iOSDragEmulation || this.dragEffect != UIScrollView.DragEffect.MomentumAndSpring)
				{
					this.MoveAbsolute(vector);
				}
				else if (this.mPanel.CalculateConstrainOffset(this.bounds.min, this.bounds.max).magnitude > 1f)
				{
					this.MoveAbsolute(vector * 0.5f);
					this.mMomentum *= 0.5f;
				}
				else
				{
					this.MoveAbsolute(vector);
				}
				if (this.restrictWithinPanel && this.mPanel.clipping != UIDrawCall.Clipping.None && this.dragEffect != UIScrollView.DragEffect.MomentumAndSpring)
				{
					this.RestrictWithinBounds(true, this.canMoveHorizontally, this.canMoveVertically);
				}
			}
		}
	}

	// Token: 0x0600063E RID: 1598 RVA: 0x00076B20 File Offset: 0x00074D20
	public void Scroll(float delta)
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject) && this.scrollWheelFactor != 0f)
		{
			this.DisableSpring();
			this.mShouldMove = this.shouldMove;
			if (Mathf.Sign(this.mScroll) != Mathf.Sign(delta))
			{
				this.mScroll = 0f;
			}
			this.mScroll += delta * this.scrollWheelFactor;
		}
	}

	// Token: 0x0600063F RID: 1599 RVA: 0x00076B94 File Offset: 0x00074D94
	private void LateUpdate()
	{
		if (!Application.isPlaying)
		{
			return;
		}
		float deltaTime = RealTime.deltaTime;
		if (this.showScrollBars != UIScrollView.ShowCondition.Always && (this.verticalScrollBar || this.horizontalScrollBar))
		{
			bool flag = false;
			bool flag2 = false;
			if (this.showScrollBars != UIScrollView.ShowCondition.WhenDragging || this.mDragID != -10 || this.mMomentum.magnitude > 0.01f)
			{
				flag = this.shouldMoveVertically;
				flag2 = this.shouldMoveHorizontally;
			}
			if (this.verticalScrollBar)
			{
				float num = this.verticalScrollBar.alpha;
				num += (flag ? (deltaTime * 6f) : (-deltaTime * 3f));
				num = Mathf.Clamp01(num);
				if (this.verticalScrollBar.alpha != num)
				{
					this.verticalScrollBar.alpha = num;
				}
			}
			if (this.horizontalScrollBar)
			{
				float num2 = this.horizontalScrollBar.alpha;
				num2 += (flag2 ? (deltaTime * 6f) : (-deltaTime * 3f));
				num2 = Mathf.Clamp01(num2);
				if (this.horizontalScrollBar.alpha != num2)
				{
					this.horizontalScrollBar.alpha = num2;
				}
			}
		}
		if (!this.mShouldMove)
		{
			return;
		}
		if (!this.mPressed)
		{
			if (this.mMomentum.magnitude > 0.0001f || this.mScroll != 0f)
			{
				if (this.movement == UIScrollView.Movement.Horizontal)
				{
					this.mMomentum -= this.mTrans.TransformDirection(new Vector3(this.mScroll * 0.05f, 0f, 0f));
				}
				else if (this.movement == UIScrollView.Movement.Vertical)
				{
					this.mMomentum -= this.mTrans.TransformDirection(new Vector3(0f, this.mScroll * 0.05f, 0f));
				}
				else if (this.movement == UIScrollView.Movement.Unrestricted)
				{
					this.mMomentum -= this.mTrans.TransformDirection(new Vector3(this.mScroll * 0.05f, this.mScroll * 0.05f, 0f));
				}
				else
				{
					this.mMomentum -= this.mTrans.TransformDirection(new Vector3(this.mScroll * this.customMovement.x * 0.05f, this.mScroll * this.customMovement.y * 0.05f, 0f));
				}
				this.mScroll = NGUIMath.SpringLerp(this.mScroll, 0f, 20f, deltaTime);
				Vector3 absolute = NGUIMath.SpringDampen(ref this.mMomentum, 9f, deltaTime);
				this.MoveAbsolute(absolute);
				if (this.restrictWithinPanel && this.mPanel.clipping != UIDrawCall.Clipping.None)
				{
					this.RestrictWithinBounds(false, this.canMoveHorizontally, this.canMoveVertically);
				}
				if (this.onMomentumMove != null)
				{
					this.onMomentumMove();
					return;
				}
			}
			else
			{
				this.mScroll = 0f;
				this.mMomentum = Vector3.zero;
				SpringPanel component = base.GetComponent<SpringPanel>();
				if (component != null && component.enabled)
				{
					return;
				}
				this.mShouldMove = false;
				if (this.onStoppedMoving != null)
				{
					this.onStoppedMoving();
					return;
				}
			}
		}
		else
		{
			this.mScroll = 0f;
			NGUIMath.SpringDampen(ref this.mMomentum, 9f, deltaTime);
		}
	}

	// Token: 0x0400047E RID: 1150
	public static BetterList<UIScrollView> list = new BetterList<UIScrollView>();

	// Token: 0x0400047F RID: 1151
	public UIScrollView.Movement movement;

	// Token: 0x04000480 RID: 1152
	public UIScrollView.DragEffect dragEffect = UIScrollView.DragEffect.MomentumAndSpring;

	// Token: 0x04000481 RID: 1153
	public bool restrictWithinPanel = true;

	// Token: 0x04000482 RID: 1154
	public bool disableDragIfFits;

	// Token: 0x04000483 RID: 1155
	public bool smoothDragStart = true;

	// Token: 0x04000484 RID: 1156
	public bool iOSDragEmulation = true;

	// Token: 0x04000485 RID: 1157
	public float scrollWheelFactor = 0.25f;

	// Token: 0x04000486 RID: 1158
	public float momentumAmount = 35f;

	// Token: 0x04000487 RID: 1159
	public UIProgressBar horizontalScrollBar;

	// Token: 0x04000488 RID: 1160
	public UIProgressBar verticalScrollBar;

	// Token: 0x04000489 RID: 1161
	public UIScrollView.ShowCondition showScrollBars = UIScrollView.ShowCondition.OnlyIfNeeded;

	// Token: 0x0400048A RID: 1162
	public Vector2 customMovement = new Vector2(1f, 0f);

	// Token: 0x0400048B RID: 1163
	public UIWidget.Pivot contentPivot;

	// Token: 0x0400048C RID: 1164
	public UIScrollView.OnDragNotification onDragStarted;

	// Token: 0x0400048D RID: 1165
	public UIScrollView.OnDragNotification onDragFinished;

	// Token: 0x0400048E RID: 1166
	public UIScrollView.OnDragNotification onMomentumMove;

	// Token: 0x0400048F RID: 1167
	public UIScrollView.OnDragNotification onStoppedMoving;

	// Token: 0x04000490 RID: 1168
	[HideInInspector]
	[SerializeField]
	private Vector3 scale = new Vector3(1f, 0f, 0f);

	// Token: 0x04000491 RID: 1169
	[SerializeField]
	[HideInInspector]
	private Vector2 relativePositionOnReset = Vector2.zero;

	// Token: 0x04000492 RID: 1170
	protected Transform mTrans;

	// Token: 0x04000493 RID: 1171
	protected UIPanel mPanel;

	// Token: 0x04000494 RID: 1172
	protected Plane mPlane;

	// Token: 0x04000495 RID: 1173
	protected Vector3 mLastPos;

	// Token: 0x04000496 RID: 1174
	protected bool mPressed;

	// Token: 0x04000497 RID: 1175
	protected Vector3 mMomentum = Vector3.zero;

	// Token: 0x04000498 RID: 1176
	protected float mScroll;

	// Token: 0x04000499 RID: 1177
	protected Bounds mBounds;

	// Token: 0x0400049A RID: 1178
	protected bool mCalculatedBounds;

	// Token: 0x0400049B RID: 1179
	protected bool mShouldMove;

	// Token: 0x0400049C RID: 1180
	protected bool mIgnoreCallbacks;

	// Token: 0x0400049D RID: 1181
	protected int mDragID = -10;

	// Token: 0x0400049E RID: 1182
	protected Vector2 mDragStartOffset = Vector2.zero;

	// Token: 0x0400049F RID: 1183
	protected bool mDragStarted;

	// Token: 0x0200009F RID: 159
	public enum Movement
	{
		// Token: 0x040004A1 RID: 1185
		Horizontal,
		// Token: 0x040004A2 RID: 1186
		Vertical,
		// Token: 0x040004A3 RID: 1187
		Unrestricted,
		// Token: 0x040004A4 RID: 1188
		Custom
	}

	// Token: 0x020000A0 RID: 160
	public enum DragEffect
	{
		// Token: 0x040004A6 RID: 1190
		None,
		// Token: 0x040004A7 RID: 1191
		Momentum,
		// Token: 0x040004A8 RID: 1192
		MomentumAndSpring
	}

	// Token: 0x020000A1 RID: 161
	public enum ShowCondition
	{
		// Token: 0x040004AA RID: 1194
		Always,
		// Token: 0x040004AB RID: 1195
		OnlyIfNeeded,
		// Token: 0x040004AC RID: 1196
		WhenDragging
	}

	// Token: 0x020000A2 RID: 162
	// (Invoke) Token: 0x06000643 RID: 1603
	public delegate void OnDragNotification();
}
