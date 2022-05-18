using System;
using UnityEngine;

// Token: 0x0200008A RID: 138
[AddComponentMenu("NGUI/UI/Image Button")]
public class UIImageButton : MonoBehaviour
{
	// Token: 0x17000094 RID: 148
	// (get) Token: 0x06000585 RID: 1413 RVA: 0x00072B90 File Offset: 0x00070D90
	// (set) Token: 0x06000586 RID: 1414 RVA: 0x00072BB4 File Offset: 0x00070DB4
	public bool isEnabled
	{
		get
		{
			Collider component = base.GetComponent<Collider>();
			return component && component.enabled;
		}
		set
		{
			Collider component = base.GetComponent<Collider>();
			if (!component)
			{
				return;
			}
			if (component.enabled != value)
			{
				component.enabled = value;
				this.UpdateImage();
			}
		}
	}

	// Token: 0x06000587 RID: 1415 RVA: 0x00008FF4 File Offset: 0x000071F4
	private void OnEnable()
	{
		if (this.target == null)
		{
			this.target = base.GetComponentInChildren<UISprite>();
		}
		this.UpdateImage();
	}

	// Token: 0x06000588 RID: 1416 RVA: 0x00072BE8 File Offset: 0x00070DE8
	private void OnValidate()
	{
		if (this.target != null)
		{
			if (string.IsNullOrEmpty(this.normalSprite))
			{
				this.normalSprite = this.target.spriteName;
			}
			if (string.IsNullOrEmpty(this.hoverSprite))
			{
				this.hoverSprite = this.target.spriteName;
			}
			if (string.IsNullOrEmpty(this.pressedSprite))
			{
				this.pressedSprite = this.target.spriteName;
			}
			if (string.IsNullOrEmpty(this.disabledSprite))
			{
				this.disabledSprite = this.target.spriteName;
			}
		}
	}

	// Token: 0x06000589 RID: 1417 RVA: 0x00072C7C File Offset: 0x00070E7C
	private void UpdateImage()
	{
		if (this.target != null)
		{
			if (this.isEnabled)
			{
				this.SetSprite(UICamera.IsHighlighted(base.gameObject) ? this.hoverSprite : this.normalSprite);
				return;
			}
			this.SetSprite(this.disabledSprite);
		}
	}

	// Token: 0x0600058A RID: 1418 RVA: 0x00009016 File Offset: 0x00007216
	private void OnHover(bool isOver)
	{
		if (this.isEnabled && this.target != null)
		{
			this.SetSprite(isOver ? this.hoverSprite : this.normalSprite);
		}
	}

	// Token: 0x0600058B RID: 1419 RVA: 0x00009045 File Offset: 0x00007245
	private void OnPress(bool pressed)
	{
		if (pressed)
		{
			this.SetSprite(this.pressedSprite);
			return;
		}
		this.UpdateImage();
	}

	// Token: 0x0600058C RID: 1420 RVA: 0x00072CD0 File Offset: 0x00070ED0
	private void SetSprite(string sprite)
	{
		if (this.target.atlas == null || this.target.atlas.GetSprite(sprite) == null)
		{
			return;
		}
		this.target.spriteName = sprite;
		if (this.pixelSnap)
		{
			this.target.MakePixelPerfect();
		}
	}

	// Token: 0x040003E9 RID: 1001
	public UISprite target;

	// Token: 0x040003EA RID: 1002
	public string normalSprite;

	// Token: 0x040003EB RID: 1003
	public string hoverSprite;

	// Token: 0x040003EC RID: 1004
	public string pressedSprite;

	// Token: 0x040003ED RID: 1005
	public string disabledSprite;

	// Token: 0x040003EE RID: 1006
	public bool pixelSnap = true;
}
