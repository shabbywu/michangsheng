using System;
using UnityEngine;

// Token: 0x02000074 RID: 116
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/NGUI Slider")]
public class UISlider : UIProgressBar
{
	// Token: 0x170000A7 RID: 167
	// (get) Token: 0x060005DE RID: 1502 RVA: 0x0001F779 File Offset: 0x0001D979
	// (set) Token: 0x060005DF RID: 1503 RVA: 0x0001F781 File Offset: 0x0001D981
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

	// Token: 0x170000A8 RID: 168
	// (get) Token: 0x060005E0 RID: 1504 RVA: 0x00021294 File Offset: 0x0001F494
	// (set) Token: 0x060005E1 RID: 1505 RVA: 0x00004095 File Offset: 0x00002295
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

	// Token: 0x060005E2 RID: 1506 RVA: 0x0002129C File Offset: 0x0001F49C
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

	// Token: 0x060005E3 RID: 1507 RVA: 0x00021314 File Offset: 0x0001F514
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

	// Token: 0x060005E4 RID: 1508 RVA: 0x00021460 File Offset: 0x0001F660
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

	// Token: 0x060005E5 RID: 1509 RVA: 0x0002149D File Offset: 0x0001F69D
	protected void OnDragBackground(GameObject go, Vector2 delta)
	{
		if (UICamera.currentScheme == UICamera.ControlScheme.Controller)
		{
			return;
		}
		this.mCam = UICamera.currentCamera;
		base.value = base.ScreenToValue(UICamera.lastTouchPosition);
	}

	// Token: 0x060005E6 RID: 1510 RVA: 0x000214C4 File Offset: 0x0001F6C4
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

	// Token: 0x060005E7 RID: 1511 RVA: 0x0002151E File Offset: 0x0001F71E
	protected void OnDragForeground(GameObject go, Vector2 delta)
	{
		if (UICamera.currentScheme == UICamera.ControlScheme.Controller)
		{
			return;
		}
		this.mCam = UICamera.currentCamera;
		base.value = this.mOffset + base.ScreenToValue(UICamera.lastTouchPosition);
	}

	// Token: 0x060005E8 RID: 1512 RVA: 0x0002154C File Offset: 0x0001F74C
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

	// Token: 0x040003EA RID: 1002
	[HideInInspector]
	[SerializeField]
	private Transform foreground;

	// Token: 0x040003EB RID: 1003
	[HideInInspector]
	[SerializeField]
	private float rawValue = 1f;

	// Token: 0x040003EC RID: 1004
	[HideInInspector]
	[SerializeField]
	private UISlider.Direction direction = UISlider.Direction.Upgraded;

	// Token: 0x040003ED RID: 1005
	[HideInInspector]
	[SerializeField]
	protected bool mInverted;

	// Token: 0x020011F3 RID: 4595
	private enum Direction
	{
		// Token: 0x04006413 RID: 25619
		Horizontal,
		// Token: 0x04006414 RID: 25620
		Vertical,
		// Token: 0x04006415 RID: 25621
		Upgraded
	}
}
