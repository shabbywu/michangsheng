using System;
using UnityEngine;

// Token: 0x020000A3 RID: 163
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/NGUI Slider")]
public class UISlider : UIProgressBar
{
	// Token: 0x170000B7 RID: 183
	// (get) Token: 0x06000646 RID: 1606 RVA: 0x000097E6 File Offset: 0x000079E6
	// (set) Token: 0x06000647 RID: 1607 RVA: 0x000097EE File Offset: 0x000079EE
	[Obsolete("Use 'value' instead")]
	public float sliderValue
	{
		get
		{
			return base.value;
		}
		set
		{
			base.value = value;
		}
	}

	// Token: 0x170000B8 RID: 184
	// (get) Token: 0x06000648 RID: 1608 RVA: 0x00009934 File Offset: 0x00007B34
	// (set) Token: 0x06000649 RID: 1609 RVA: 0x000042DD File Offset: 0x000024DD
	[Obsolete("Use 'fillDirection' instead")]
	public bool inverted
	{
		get
		{
			return base.isInverted;
		}
		set
		{
		}
	}

	// Token: 0x0600064A RID: 1610 RVA: 0x00076F98 File Offset: 0x00075198
	protected override void Upgrade()
	{
		if (this.direction != UISlider.Direction.Upgraded)
		{
			this.mValue = this.rawValue;
			if (this.foreground != null)
			{
				this.mFG = this.foreground.GetComponent<UIWidget>();
			}
			if (this.direction == UISlider.Direction.Horizontal)
			{
				this.mFill = (this.mInverted ? UIProgressBar.FillDirection.RightToLeft : UIProgressBar.FillDirection.LeftToRight);
			}
			else
			{
				this.mFill = (this.mInverted ? UIProgressBar.FillDirection.TopToBottom : UIProgressBar.FillDirection.BottomToTop);
			}
			this.direction = UISlider.Direction.Upgraded;
		}
	}

	// Token: 0x0600064B RID: 1611 RVA: 0x00077010 File Offset: 0x00075210
	protected override void OnStart()
	{
		UIEventListener uieventListener = UIEventListener.Get((this.mBG != null && (this.mBG.GetComponent<Collider>() != null || this.mBG.GetComponent<Collider2D>() != null)) ? this.mBG.gameObject : base.gameObject);
		uieventListener.onPress = (UIEventListener.BoolDelegate)Delegate.Combine(uieventListener.onPress, new UIEventListener.BoolDelegate(this.OnPressBackground));
		uieventListener.onDrag = (UIEventListener.VectorDelegate)Delegate.Combine(uieventListener.onDrag, new UIEventListener.VectorDelegate(this.OnDragBackground));
		if (this.thumb != null && (this.thumb.GetComponent<Collider>() != null || this.thumb.GetComponent<Collider2D>() != null) && (this.mFG == null || this.thumb != this.mFG.cachedTransform))
		{
			UIEventListener uieventListener2 = UIEventListener.Get(this.thumb.gameObject);
			uieventListener2.onPress = (UIEventListener.BoolDelegate)Delegate.Combine(uieventListener2.onPress, new UIEventListener.BoolDelegate(this.OnPressForeground));
			uieventListener2.onDrag = (UIEventListener.VectorDelegate)Delegate.Combine(uieventListener2.onDrag, new UIEventListener.VectorDelegate(this.OnDragForeground));
		}
	}

	// Token: 0x0600064C RID: 1612 RVA: 0x0000993C File Offset: 0x00007B3C
	protected void OnPressBackground(GameObject go, bool isPressed)
	{
		if (UICamera.currentScheme == UICamera.ControlScheme.Controller)
		{
			return;
		}
		this.mCam = UICamera.currentCamera;
		base.value = base.ScreenToValue(UICamera.lastTouchPosition);
		if (!isPressed && this.onDragFinished != null)
		{
			this.onDragFinished();
		}
	}

	// Token: 0x0600064D RID: 1613 RVA: 0x00009979 File Offset: 0x00007B79
	protected void OnDragBackground(GameObject go, Vector2 delta)
	{
		if (UICamera.currentScheme == UICamera.ControlScheme.Controller)
		{
			return;
		}
		this.mCam = UICamera.currentCamera;
		base.value = base.ScreenToValue(UICamera.lastTouchPosition);
	}

	// Token: 0x0600064E RID: 1614 RVA: 0x0007715C File Offset: 0x0007535C
	protected void OnPressForeground(GameObject go, bool isPressed)
	{
		if (UICamera.currentScheme == UICamera.ControlScheme.Controller)
		{
			return;
		}
		if (isPressed)
		{
			this.mOffset = ((this.mFG == null) ? 0f : (base.value - base.ScreenToValue(UICamera.lastTouchPosition)));
			return;
		}
		if (this.onDragFinished != null)
		{
			this.onDragFinished();
		}
	}

	// Token: 0x0600064F RID: 1615 RVA: 0x000099A0 File Offset: 0x00007BA0
	protected void OnDragForeground(GameObject go, Vector2 delta)
	{
		if (UICamera.currentScheme == UICamera.ControlScheme.Controller)
		{
			return;
		}
		this.mCam = UICamera.currentCamera;
		base.value = this.mOffset + base.ScreenToValue(UICamera.lastTouchPosition);
	}

	// Token: 0x06000650 RID: 1616 RVA: 0x000771B8 File Offset: 0x000753B8
	protected void OnKey(KeyCode key)
	{
		if (base.enabled)
		{
			float num = ((float)this.numberOfSteps > 1f) ? (1f / (float)(this.numberOfSteps - 1)) : 0.125f;
			if (base.fillDirection == UIProgressBar.FillDirection.LeftToRight || base.fillDirection == UIProgressBar.FillDirection.RightToLeft)
			{
				if (key == 276)
				{
					base.value = this.mValue - num;
					return;
				}
				if (key == 275)
				{
					base.value = this.mValue + num;
					return;
				}
			}
			else
			{
				if (key == 274)
				{
					base.value = this.mValue - num;
					return;
				}
				if (key == 273)
				{
					base.value = this.mValue + num;
				}
			}
		}
	}

	// Token: 0x040004AD RID: 1197
	[HideInInspector]
	[SerializeField]
	private Transform foreground;

	// Token: 0x040004AE RID: 1198
	[HideInInspector]
	[SerializeField]
	private float rawValue = 1f;

	// Token: 0x040004AF RID: 1199
	[HideInInspector]
	[SerializeField]
	private UISlider.Direction direction = UISlider.Direction.Upgraded;

	// Token: 0x040004B0 RID: 1200
	[HideInInspector]
	[SerializeField]
	protected bool mInverted;

	// Token: 0x020000A4 RID: 164
	private enum Direction
	{
		// Token: 0x040004B2 RID: 1202
		Horizontal,
		// Token: 0x040004B3 RID: 1203
		Vertical,
		// Token: 0x040004B4 RID: 1204
		Upgraded
	}
}
