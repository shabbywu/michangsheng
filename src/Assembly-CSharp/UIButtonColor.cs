using System;
using UnityEngine;

// Token: 0x0200006F RID: 111
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Button Color")]
public class UIButtonColor : UIWidgetContainer
{
	// Token: 0x1700008D RID: 141
	// (get) Token: 0x060004E4 RID: 1252 RVA: 0x000084B5 File Offset: 0x000066B5
	// (set) Token: 0x060004E5 RID: 1253 RVA: 0x000084BD File Offset: 0x000066BD
	public UIButtonColor.State state
	{
		get
		{
			return this.mState;
		}
		set
		{
			this.SetState(value, false);
		}
	}

	// Token: 0x1700008E RID: 142
	// (get) Token: 0x060004E6 RID: 1254 RVA: 0x000084C7 File Offset: 0x000066C7
	// (set) Token: 0x060004E7 RID: 1255 RVA: 0x00070580 File Offset: 0x0006E780
	public Color defaultColor
	{
		get
		{
			if (!this.mInitDone)
			{
				this.OnInit();
			}
			return this.mDefaultColor;
		}
		set
		{
			if (!this.mInitDone)
			{
				this.OnInit();
			}
			this.mDefaultColor = value;
			UIButtonColor.State state = this.mState;
			this.mState = UIButtonColor.State.Disabled;
			this.SetState(state, false);
		}
	}

	// Token: 0x1700008F RID: 143
	// (get) Token: 0x060004E8 RID: 1256 RVA: 0x000084DD File Offset: 0x000066DD
	// (set) Token: 0x060004E9 RID: 1257 RVA: 0x000084E5 File Offset: 0x000066E5
	public virtual bool isEnabled
	{
		get
		{
			return base.enabled;
		}
		set
		{
			base.enabled = value;
		}
	}

	// Token: 0x060004EA RID: 1258 RVA: 0x000084EE File Offset: 0x000066EE
	public void ResetDefaultColor()
	{
		this.defaultColor = this.mStartingColor;
	}

	// Token: 0x060004EB RID: 1259 RVA: 0x000084FC File Offset: 0x000066FC
	private void Awake()
	{
		if (!this.mInitDone)
		{
			this.OnInit();
		}
	}

	// Token: 0x060004EC RID: 1260 RVA: 0x0000850C File Offset: 0x0000670C
	private void Start()
	{
		if (!this.isEnabled)
		{
			this.SetState(UIButtonColor.State.Disabled, true);
		}
	}

	// Token: 0x060004ED RID: 1261 RVA: 0x000705B8 File Offset: 0x0006E7B8
	protected virtual void OnInit()
	{
		this.mInitDone = true;
		if (this.tweenTarget == null)
		{
			this.tweenTarget = base.gameObject;
		}
		this.mWidget = this.tweenTarget.GetComponent<UIWidget>();
		if (this.mWidget != null)
		{
			this.mDefaultColor = this.mWidget.color;
			this.mStartingColor = this.mDefaultColor;
			return;
		}
		Renderer component = this.tweenTarget.GetComponent<Renderer>();
		if (component != null)
		{
			this.mDefaultColor = (Application.isPlaying ? component.material.color : component.sharedMaterial.color);
			this.mStartingColor = this.mDefaultColor;
			return;
		}
		Light component2 = this.tweenTarget.GetComponent<Light>();
		if (component2 != null)
		{
			this.mDefaultColor = component2.color;
			this.mStartingColor = this.mDefaultColor;
			return;
		}
		this.tweenTarget = null;
		this.mInitDone = false;
	}

	// Token: 0x060004EE RID: 1262 RVA: 0x000706A8 File Offset: 0x0006E8A8
	protected virtual void OnEnable()
	{
		if (this.mInitDone)
		{
			this.OnHover(UICamera.IsHighlighted(base.gameObject));
		}
		if (UICamera.currentTouch != null)
		{
			if (UICamera.currentTouch.pressed == base.gameObject)
			{
				this.OnPress(true);
				return;
			}
			if (UICamera.currentTouch.current == base.gameObject)
			{
				this.OnHover(true);
			}
		}
	}

	// Token: 0x060004EF RID: 1263 RVA: 0x00070714 File Offset: 0x0006E914
	protected virtual void OnDisable()
	{
		if (this.mInitDone && this.tweenTarget != null)
		{
			this.SetState(UIButtonColor.State.Normal, true);
			TweenColor component = this.tweenTarget.GetComponent<TweenColor>();
			if (component != null)
			{
				component.value = this.mDefaultColor;
				component.enabled = false;
			}
		}
	}

	// Token: 0x060004F0 RID: 1264 RVA: 0x0000851E File Offset: 0x0000671E
	protected virtual void OnHover(bool isOver)
	{
		if (this.isEnabled)
		{
			if (!this.mInitDone)
			{
				this.OnInit();
			}
			if (this.tweenTarget != null)
			{
				this.SetState(isOver ? UIButtonColor.State.Hover : UIButtonColor.State.Normal, false);
			}
		}
	}

	// Token: 0x060004F1 RID: 1265 RVA: 0x00070768 File Offset: 0x0006E968
	protected virtual void OnPress(bool isPressed)
	{
		if (this.isEnabled && UICamera.currentTouch != null)
		{
			if (!this.mInitDone)
			{
				this.OnInit();
			}
			if (this.tweenTarget != null)
			{
				if (isPressed)
				{
					this.SetState(UIButtonColor.State.Pressed, false);
					return;
				}
				if (UICamera.currentTouch.current == base.gameObject)
				{
					if (UICamera.currentScheme == UICamera.ControlScheme.Controller)
					{
						this.SetState(UIButtonColor.State.Hover, false);
						return;
					}
					if (UICamera.currentScheme == UICamera.ControlScheme.Mouse && UICamera.hoveredObject == base.gameObject)
					{
						this.SetState(UIButtonColor.State.Hover, false);
						return;
					}
					this.SetState(UIButtonColor.State.Normal, false);
					return;
				}
				else
				{
					this.SetState(UIButtonColor.State.Normal, false);
				}
			}
		}
	}

	// Token: 0x060004F2 RID: 1266 RVA: 0x00008552 File Offset: 0x00006752
	protected virtual void OnDragOver()
	{
		if (this.isEnabled)
		{
			if (!this.mInitDone)
			{
				this.OnInit();
			}
			if (this.tweenTarget != null)
			{
				this.SetState(UIButtonColor.State.Pressed, false);
			}
		}
	}

	// Token: 0x060004F3 RID: 1267 RVA: 0x00008580 File Offset: 0x00006780
	protected virtual void OnDragOut()
	{
		if (this.isEnabled)
		{
			if (!this.mInitDone)
			{
				this.OnInit();
			}
			if (this.tweenTarget != null)
			{
				this.SetState(UIButtonColor.State.Normal, false);
			}
		}
	}

	// Token: 0x060004F4 RID: 1268 RVA: 0x000085AE File Offset: 0x000067AE
	protected virtual void OnSelect(bool isSelected)
	{
		if (this.isEnabled && (!isSelected || UICamera.currentScheme == UICamera.ControlScheme.Controller) && this.tweenTarget != null)
		{
			this.OnHover(isSelected);
		}
	}

	// Token: 0x060004F5 RID: 1269 RVA: 0x000085D8 File Offset: 0x000067D8
	public virtual void SetState(UIButtonColor.State state, bool instant)
	{
		if (!this.mInitDone)
		{
			this.mInitDone = true;
			this.OnInit();
		}
		if (this.mState != state)
		{
			this.mState = state;
			this.UpdateColor(instant);
		}
	}

	// Token: 0x060004F6 RID: 1270 RVA: 0x00070810 File Offset: 0x0006EA10
	public void UpdateColor(bool instant)
	{
		TweenColor tweenColor;
		switch (this.mState)
		{
		case UIButtonColor.State.Hover:
			tweenColor = TweenColor.Begin(this.tweenTarget, this.duration, this.hover);
			break;
		case UIButtonColor.State.Pressed:
			tweenColor = TweenColor.Begin(this.tweenTarget, this.duration, this.pressed);
			break;
		case UIButtonColor.State.Disabled:
			tweenColor = TweenColor.Begin(this.tweenTarget, this.duration, this.disabledColor);
			break;
		default:
			tweenColor = TweenColor.Begin(this.tweenTarget, this.duration, this.mDefaultColor);
			break;
		}
		if (instant && tweenColor != null)
		{
			tweenColor.value = tweenColor.to;
			tweenColor.enabled = false;
		}
	}

	// Token: 0x04000339 RID: 825
	public GameObject tweenTarget;

	// Token: 0x0400033A RID: 826
	public Color hover = new Color(0.88235295f, 0.78431374f, 0.5882353f, 1f);

	// Token: 0x0400033B RID: 827
	public Color pressed = new Color(0.7176471f, 0.6392157f, 0.48235294f, 1f);

	// Token: 0x0400033C RID: 828
	public Color disabledColor = Color.grey;

	// Token: 0x0400033D RID: 829
	public float duration = 0.2f;

	// Token: 0x0400033E RID: 830
	[NonSerialized]
	protected Color mStartingColor;

	// Token: 0x0400033F RID: 831
	[NonSerialized]
	protected Color mDefaultColor;

	// Token: 0x04000340 RID: 832
	[NonSerialized]
	protected bool mInitDone;

	// Token: 0x04000341 RID: 833
	[NonSerialized]
	protected UIWidget mWidget;

	// Token: 0x04000342 RID: 834
	[NonSerialized]
	protected UIButtonColor.State mState;

	// Token: 0x02000070 RID: 112
	public enum State
	{
		// Token: 0x04000344 RID: 836
		Normal,
		// Token: 0x04000345 RID: 837
		Hover,
		// Token: 0x04000346 RID: 838
		Pressed,
		// Token: 0x04000347 RID: 839
		Disabled
	}
}
