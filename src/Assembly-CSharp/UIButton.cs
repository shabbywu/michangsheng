using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200006D RID: 109
[AddComponentMenu("NGUI/Interaction/Button")]
public class UIButton : UIButtonColor
{
	// Token: 0x1700008A RID: 138
	// (get) Token: 0x060004D3 RID: 1235 RVA: 0x000701A0 File Offset: 0x0006E3A0
	// (set) Token: 0x060004D4 RID: 1236 RVA: 0x000701E8 File Offset: 0x0006E3E8
	public override bool isEnabled
	{
		get
		{
			if (!base.enabled)
			{
				return false;
			}
			Collider component = base.GetComponent<Collider>();
			if (component && component.enabled)
			{
				return true;
			}
			Collider2D component2 = base.GetComponent<Collider2D>();
			return component2 && component2.enabled;
		}
		set
		{
			if (this.isEnabled != value)
			{
				Collider component = base.GetComponent<Collider>();
				if (component != null)
				{
					component.enabled = value;
					this.SetState(value ? UIButtonColor.State.Normal : UIButtonColor.State.Disabled, false);
					return;
				}
				Collider2D component2 = base.GetComponent<Collider2D>();
				if (component2 != null)
				{
					component2.enabled = value;
					this.SetState(value ? UIButtonColor.State.Normal : UIButtonColor.State.Disabled, false);
					return;
				}
				base.enabled = value;
			}
		}
	}

	// Token: 0x1700008B RID: 139
	// (get) Token: 0x060004D5 RID: 1237 RVA: 0x000083BA File Offset: 0x000065BA
	// (set) Token: 0x060004D6 RID: 1238 RVA: 0x00070254 File Offset: 0x0006E454
	public string normalSprite
	{
		get
		{
			if (!this.mInitDone)
			{
				this.OnInit();
			}
			return this.mNormalSprite;
		}
		set
		{
			if (this.mSprite != null && !string.IsNullOrEmpty(this.mNormalSprite) && this.mNormalSprite == this.mSprite.spriteName)
			{
				this.mNormalSprite = value;
				this.SetSprite(value);
				NGUITools.SetDirty(this.mSprite);
				return;
			}
			this.mNormalSprite = value;
			if (this.mState == UIButtonColor.State.Normal)
			{
				this.SetSprite(value);
			}
		}
	}

	// Token: 0x1700008C RID: 140
	// (get) Token: 0x060004D7 RID: 1239 RVA: 0x000083D0 File Offset: 0x000065D0
	// (set) Token: 0x060004D8 RID: 1240 RVA: 0x000702C4 File Offset: 0x0006E4C4
	public Sprite normalSprite2D
	{
		get
		{
			if (!this.mInitDone)
			{
				this.OnInit();
			}
			return this.mNormalSprite2D;
		}
		set
		{
			if (this.mSprite2D != null && this.mNormalSprite2D == this.mSprite2D.sprite2D)
			{
				this.mNormalSprite2D = value;
				this.SetSprite(value);
				NGUITools.SetDirty(this.mSprite);
				return;
			}
			this.mNormalSprite2D = value;
			if (this.mState == UIButtonColor.State.Normal)
			{
				this.SetSprite(value);
			}
		}
	}

	// Token: 0x060004D9 RID: 1241 RVA: 0x00070328 File Offset: 0x0006E528
	protected override void OnInit()
	{
		base.OnInit();
		this.mSprite = (this.mWidget as UISprite);
		this.mSprite2D = (this.mWidget as UI2DSprite);
		if (this.mSprite != null)
		{
			this.mNormalSprite = this.mSprite.spriteName;
		}
		if (this.mSprite2D != null)
		{
			this.mNormalSprite2D = this.mSprite2D.sprite2D;
		}
	}

	// Token: 0x060004DA RID: 1242 RVA: 0x0007039C File Offset: 0x0006E59C
	protected override void OnEnable()
	{
		if (this.isEnabled)
		{
			if (this.mInitDone)
			{
				if (UICamera.currentScheme == UICamera.ControlScheme.Controller)
				{
					this.OnHover(UICamera.selectedObject == base.gameObject);
					return;
				}
				if (UICamera.currentScheme == UICamera.ControlScheme.Mouse)
				{
					this.OnHover(UICamera.hoveredObject == base.gameObject);
					return;
				}
				this.SetState(UIButtonColor.State.Normal, false);
				return;
			}
		}
		else
		{
			this.SetState(UIButtonColor.State.Disabled, true);
		}
	}

	// Token: 0x060004DB RID: 1243 RVA: 0x000083E6 File Offset: 0x000065E6
	protected override void OnDragOver()
	{
		if (this.isEnabled && (this.dragHighlight || UICamera.currentTouch.pressed == base.gameObject))
		{
			base.OnDragOver();
		}
	}

	// Token: 0x060004DC RID: 1244 RVA: 0x00008415 File Offset: 0x00006615
	protected override void OnDragOut()
	{
		if (this.isEnabled && (this.dragHighlight || UICamera.currentTouch.pressed == base.gameObject))
		{
			base.OnDragOut();
		}
	}

	// Token: 0x060004DD RID: 1245 RVA: 0x00008444 File Offset: 0x00006644
	protected virtual void OnClick()
	{
		if (UIButton.current == null && this.isEnabled)
		{
			UIButton.current = this;
			EventDelegate.Execute(this.onClick);
			UIButton.current = null;
		}
	}

	// Token: 0x060004DE RID: 1246 RVA: 0x00070408 File Offset: 0x0006E608
	public override void SetState(UIButtonColor.State state, bool immediate)
	{
		base.SetState(state, immediate);
		if (!(this.mSprite != null))
		{
			if (this.mSprite2D != null)
			{
				switch (state)
				{
				case UIButtonColor.State.Normal:
					this.SetSprite(this.mNormalSprite2D);
					return;
				case UIButtonColor.State.Hover:
					this.SetSprite(this.hoverSprite2D);
					return;
				case UIButtonColor.State.Pressed:
					this.SetSprite(this.pressedSprite2D);
					return;
				case UIButtonColor.State.Disabled:
					this.SetSprite(this.disabledSprite2D);
					break;
				default:
					return;
				}
			}
			return;
		}
		switch (state)
		{
		case UIButtonColor.State.Normal:
			this.SetSprite(this.mNormalSprite);
			return;
		case UIButtonColor.State.Hover:
			this.SetSprite(this.hoverSprite);
			return;
		case UIButtonColor.State.Pressed:
			this.SetSprite(this.pressedSprite);
			return;
		case UIButtonColor.State.Disabled:
			this.SetSprite(this.disabledSprite);
			return;
		default:
			return;
		}
	}

	// Token: 0x060004DF RID: 1247 RVA: 0x000704D0 File Offset: 0x0006E6D0
	protected void SetSprite(string sp)
	{
		if (this.mSprite != null && !string.IsNullOrEmpty(sp) && this.mSprite.spriteName != sp)
		{
			this.mSprite.spriteName = sp;
			if (this.pixelSnap)
			{
				this.mSprite.MakePixelPerfect();
			}
		}
	}

	// Token: 0x060004E0 RID: 1248 RVA: 0x00070528 File Offset: 0x0006E728
	protected void SetSprite(Sprite sp)
	{
		if (sp != null && this.mSprite2D != null && this.mSprite2D.sprite2D != sp)
		{
			this.mSprite2D.sprite2D = sp;
			if (this.pixelSnap)
			{
				this.mSprite2D.MakePixelPerfect();
			}
		}
	}

	// Token: 0x04000329 RID: 809
	public static UIButton current;

	// Token: 0x0400032A RID: 810
	public bool dragHighlight;

	// Token: 0x0400032B RID: 811
	public string hoverSprite;

	// Token: 0x0400032C RID: 812
	public string pressedSprite;

	// Token: 0x0400032D RID: 813
	public string disabledSprite;

	// Token: 0x0400032E RID: 814
	public Sprite hoverSprite2D;

	// Token: 0x0400032F RID: 815
	public Sprite pressedSprite2D;

	// Token: 0x04000330 RID: 816
	public Sprite disabledSprite2D;

	// Token: 0x04000331 RID: 817
	public bool pixelSnap;

	// Token: 0x04000332 RID: 818
	public List<EventDelegate> onClick = new List<EventDelegate>();

	// Token: 0x04000333 RID: 819
	[NonSerialized]
	private UISprite mSprite;

	// Token: 0x04000334 RID: 820
	[NonSerialized]
	private UI2DSprite mSprite2D;

	// Token: 0x04000335 RID: 821
	[NonSerialized]
	private string mNormalSprite;

	// Token: 0x04000336 RID: 822
	[NonSerialized]
	private Sprite mNormalSprite2D;
}
