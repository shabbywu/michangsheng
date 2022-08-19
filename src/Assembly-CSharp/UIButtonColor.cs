using System;
using UnityEngine;

// Token: 0x02000056 RID: 86
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Button Color")]
public class UIButtonColor : UIWidgetContainer
{
	// Token: 0x1700007F RID: 127
	// (get) Token: 0x06000496 RID: 1174 RVA: 0x000194C1 File Offset: 0x000176C1
	// (set) Token: 0x06000497 RID: 1175 RVA: 0x000194C9 File Offset: 0x000176C9
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

	// Token: 0x17000080 RID: 128
	// (get) Token: 0x06000498 RID: 1176 RVA: 0x000194D3 File Offset: 0x000176D3
	// (set) Token: 0x06000499 RID: 1177 RVA: 0x000194EC File Offset: 0x000176EC
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

	// Token: 0x17000081 RID: 129
	// (get) Token: 0x0600049A RID: 1178 RVA: 0x00019524 File Offset: 0x00017724
	// (set) Token: 0x0600049B RID: 1179 RVA: 0x0001952C File Offset: 0x0001772C
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

	// Token: 0x0600049C RID: 1180 RVA: 0x00019535 File Offset: 0x00017735
	public void ResetDefaultColor()
	{
		this.defaultColor = this.mStartingColor;
	}

	// Token: 0x0600049D RID: 1181 RVA: 0x00019543 File Offset: 0x00017743
	private void Awake()
	{
		if (!this.mInitDone)
		{
			this.OnInit();
		}
	}

	// Token: 0x0600049E RID: 1182 RVA: 0x00019553 File Offset: 0x00017753
	private void Start()
	{
		if (!this.isEnabled)
		{
			this.SetState(UIButtonColor.State.Disabled, true);
		}
	}

	// Token: 0x0600049F RID: 1183 RVA: 0x00019568 File Offset: 0x00017768
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

	// Token: 0x060004A0 RID: 1184 RVA: 0x00019658 File Offset: 0x00017858
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

	// Token: 0x060004A1 RID: 1185 RVA: 0x000196C4 File Offset: 0x000178C4
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

	// Token: 0x060004A2 RID: 1186 RVA: 0x00019717 File Offset: 0x00017917
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

	// Token: 0x060004A3 RID: 1187 RVA: 0x0001974C File Offset: 0x0001794C
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

	// Token: 0x060004A4 RID: 1188 RVA: 0x000197F1 File Offset: 0x000179F1
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

	// Token: 0x060004A5 RID: 1189 RVA: 0x0001981F File Offset: 0x00017A1F
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

	// Token: 0x060004A6 RID: 1190 RVA: 0x0001984D File Offset: 0x00017A4D
	protected virtual void OnSelect(bool isSelected)
	{
		if (this.isEnabled && (!isSelected || UICamera.currentScheme == UICamera.ControlScheme.Controller) && this.tweenTarget != null)
		{
			this.OnHover(isSelected);
		}
	}

	// Token: 0x060004A7 RID: 1191 RVA: 0x00019877 File Offset: 0x00017A77
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

	// Token: 0x060004A8 RID: 1192 RVA: 0x000198A8 File Offset: 0x00017AA8
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

	// Token: 0x040002C6 RID: 710
	public GameObject tweenTarget;

	// Token: 0x040002C7 RID: 711
	public Color hover = new Color(0.88235295f, 0.78431374f, 0.5882353f, 1f);

	// Token: 0x040002C8 RID: 712
	public Color pressed = new Color(0.7176471f, 0.6392157f, 0.48235294f, 1f);

	// Token: 0x040002C9 RID: 713
	public Color disabledColor = Color.grey;

	// Token: 0x040002CA RID: 714
	public float duration = 0.2f;

	// Token: 0x040002CB RID: 715
	[NonSerialized]
	protected Color mStartingColor;

	// Token: 0x040002CC RID: 716
	[NonSerialized]
	protected Color mDefaultColor;

	// Token: 0x040002CD RID: 717
	[NonSerialized]
	protected bool mInitDone;

	// Token: 0x040002CE RID: 718
	[NonSerialized]
	protected UIWidget mWidget;

	// Token: 0x040002CF RID: 719
	[NonSerialized]
	protected UIButtonColor.State mState;

	// Token: 0x020011DD RID: 4573
	public enum State
	{
		// Token: 0x040063C3 RID: 25539
		Normal,
		// Token: 0x040063C4 RID: 25540
		Hover,
		// Token: 0x040063C5 RID: 25541
		Pressed,
		// Token: 0x040063C6 RID: 25542
		Disabled
	}
}
