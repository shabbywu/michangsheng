using System;
using UnityEngine;

// Token: 0x02000069 RID: 105
[AddComponentMenu("NGUI/UI/Image Button")]
public class UIImageButton : MonoBehaviour
{
	// Token: 0x17000086 RID: 134
	// (get) Token: 0x0600052F RID: 1327 RVA: 0x0001C610 File Offset: 0x0001A810
	// (set) Token: 0x06000530 RID: 1328 RVA: 0x0001C634 File Offset: 0x0001A834
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

	// Token: 0x06000531 RID: 1329 RVA: 0x0001C667 File Offset: 0x0001A867
	private void OnEnable()
	{
		if (this.target == null)
		{
			this.target = base.GetComponentInChildren<UISprite>();
		}
		this.UpdateImage();
	}

	// Token: 0x06000532 RID: 1330 RVA: 0x0001C68C File Offset: 0x0001A88C
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

	// Token: 0x06000533 RID: 1331 RVA: 0x0001C720 File Offset: 0x0001A920
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

	// Token: 0x06000534 RID: 1332 RVA: 0x0001C771 File Offset: 0x0001A971
	private void OnHover(bool isOver)
	{
		if (this.isEnabled && this.target != null)
		{
			this.SetSprite(isOver ? this.hoverSprite : this.normalSprite);
		}
	}

	// Token: 0x06000535 RID: 1333 RVA: 0x0001C7A0 File Offset: 0x0001A9A0
	private void OnPress(bool pressed)
	{
		if (pressed)
		{
			this.SetSprite(this.pressedSprite);
			return;
		}
		this.UpdateImage();
	}

	// Token: 0x06000536 RID: 1334 RVA: 0x0001C7B8 File Offset: 0x0001A9B8
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

	// Token: 0x04000358 RID: 856
	public UISprite target;

	// Token: 0x04000359 RID: 857
	public string normalSprite;

	// Token: 0x0400035A RID: 858
	public string hoverSprite;

	// Token: 0x0400035B RID: 859
	public string pressedSprite;

	// Token: 0x0400035C RID: 860
	public string disabledSprite;

	// Token: 0x0400035D RID: 861
	public bool pixelSnap = true;
}
