using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000054 RID: 84
[AddComponentMenu("NGUI/Interaction/Button")]
public class UIButton : UIButtonColor
{
	// Token: 0x1700007C RID: 124
	// (get) Token: 0x06000485 RID: 1157 RVA: 0x00018FE8 File Offset: 0x000171E8
	// (set) Token: 0x06000486 RID: 1158 RVA: 0x00019030 File Offset: 0x00017230
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

	// Token: 0x1700007D RID: 125
	// (get) Token: 0x06000487 RID: 1159 RVA: 0x00019099 File Offset: 0x00017299
	// (set) Token: 0x06000488 RID: 1160 RVA: 0x000190B0 File Offset: 0x000172B0
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

	// Token: 0x1700007E RID: 126
	// (get) Token: 0x06000489 RID: 1161 RVA: 0x00019120 File Offset: 0x00017320
	// (set) Token: 0x0600048A RID: 1162 RVA: 0x00019138 File Offset: 0x00017338
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

	// Token: 0x0600048B RID: 1163 RVA: 0x0001919C File Offset: 0x0001739C
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

	// Token: 0x0600048C RID: 1164 RVA: 0x00019210 File Offset: 0x00017410
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

	// Token: 0x0600048D RID: 1165 RVA: 0x0001927B File Offset: 0x0001747B
	protected override void OnDragOver()
	{
		if (this.isEnabled && (this.dragHighlight || UICamera.currentTouch.pressed == base.gameObject))
		{
			base.OnDragOver();
		}
	}

	// Token: 0x0600048E RID: 1166 RVA: 0x000192AA File Offset: 0x000174AA
	protected override void OnDragOut()
	{
		if (this.isEnabled && (this.dragHighlight || UICamera.currentTouch.pressed == base.gameObject))
		{
			base.OnDragOut();
		}
	}

	// Token: 0x0600048F RID: 1167 RVA: 0x000192D9 File Offset: 0x000174D9
	protected virtual void OnClick()
	{
		if (UIButton.current == null && this.isEnabled)
		{
			UIButton.current = this;
			EventDelegate.Execute(this.onClick);
			UIButton.current = null;
		}
	}

	// Token: 0x06000490 RID: 1168 RVA: 0x00019308 File Offset: 0x00017508
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

	// Token: 0x06000491 RID: 1169 RVA: 0x000193D0 File Offset: 0x000175D0
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

	// Token: 0x06000492 RID: 1170 RVA: 0x00019428 File Offset: 0x00017628
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

	// Token: 0x040002B6 RID: 694
	public static UIButton current;

	// Token: 0x040002B7 RID: 695
	public bool dragHighlight;

	// Token: 0x040002B8 RID: 696
	public string hoverSprite;

	// Token: 0x040002B9 RID: 697
	public string pressedSprite;

	// Token: 0x040002BA RID: 698
	public string disabledSprite;

	// Token: 0x040002BB RID: 699
	public Sprite hoverSprite2D;

	// Token: 0x040002BC RID: 700
	public Sprite pressedSprite2D;

	// Token: 0x040002BD RID: 701
	public Sprite disabledSprite2D;

	// Token: 0x040002BE RID: 702
	public bool pixelSnap;

	// Token: 0x040002BF RID: 703
	public List<EventDelegate> onClick = new List<EventDelegate>();

	// Token: 0x040002C0 RID: 704
	[NonSerialized]
	private UISprite mSprite;

	// Token: 0x040002C1 RID: 705
	[NonSerialized]
	private UI2DSprite mSprite2D;

	// Token: 0x040002C2 RID: 706
	[NonSerialized]
	private string mNormalSprite;

	// Token: 0x040002C3 RID: 707
	[NonSerialized]
	private Sprite mNormalSprite2D;
}
