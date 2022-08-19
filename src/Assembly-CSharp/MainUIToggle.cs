using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000323 RID: 803
public class MainUIToggle : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
	// Token: 0x17000252 RID: 594
	// (get) Token: 0x06001BC0 RID: 7104 RVA: 0x000C5925 File Offset: 0x000C3B25
	// (set) Token: 0x06001BC1 RID: 7105 RVA: 0x000C5932 File Offset: 0x000C3B32
	public string Text
	{
		get
		{
			return this.text.text;
		}
		set
		{
			this.text.text = value;
		}
	}

	// Token: 0x06001BC2 RID: 7106 RVA: 0x000C5940 File Offset: 0x000C3B40
	private void Awake()
	{
		this.toggleGroup.toggleList.Add(this);
	}

	// Token: 0x06001BC3 RID: 7107 RVA: 0x000C5954 File Offset: 0x000C3B54
	public void OnValueChange()
	{
		if (this.isOn)
		{
			if (this.text != null)
			{
				this.text.color = this.selectColor;
			}
			if (this.targetImage != null)
			{
				this.targetImage.sprite = this.selectSprite;
				this.targetImage.color = this.hasColor;
			}
		}
		else
		{
			if (this.text != null)
			{
				this.text.color = this.nomalColor;
			}
			if (this.targetImage != null)
			{
				if (this.nomalSprite == null)
				{
					this.targetImage.color = this.nullColor;
				}
				else
				{
					this.targetImage.sprite = this.nomalSprite;
				}
			}
		}
		if (this.valueChange != null)
		{
			this.valueChange.Invoke();
		}
	}

	// Token: 0x06001BC4 RID: 7108 RVA: 0x000C5A30 File Offset: 0x000C3C30
	public void OnPointerDown(PointerEventData eventData)
	{
		if (this.isDisable)
		{
			return;
		}
		if (this.mustSelect)
		{
			if (!this.isOn)
			{
				this.isOn = true;
			}
		}
		else
		{
			this.isOn = !this.isOn;
		}
		this.OnValueChange();
		if (this.isOn && this.toggleGroup != null)
		{
			this.toggleGroup.OnChildToggleChange(this);
		}
		if (this.clickEvent != null)
		{
			this.clickEvent.Invoke();
		}
	}

	// Token: 0x06001BC5 RID: 7109 RVA: 0x000C5AA9 File Offset: 0x000C3CA9
	public void SetDisable()
	{
		this.targetImage.sprite = this.disableSprite;
		this.isDisable = true;
	}

	// Token: 0x06001BC6 RID: 7110 RVA: 0x000C5AC3 File Offset: 0x000C3CC3
	public void MoNiClick()
	{
		this.OnPointerDown(null);
	}

	// Token: 0x04001647 RID: 5703
	[SerializeField]
	private Image targetImage;

	// Token: 0x04001648 RID: 5704
	[SerializeField]
	private Sprite nomalSprite;

	// Token: 0x04001649 RID: 5705
	[SerializeField]
	private Sprite selectSprite;

	// Token: 0x0400164A RID: 5706
	[SerializeField]
	private Sprite disableSprite;

	// Token: 0x0400164B RID: 5707
	[SerializeField]
	private Text text;

	// Token: 0x0400164C RID: 5708
	[SerializeField]
	private Color nomalColor;

	// Token: 0x0400164D RID: 5709
	[SerializeField]
	private Color selectColor;

	// Token: 0x0400164E RID: 5710
	public MainUIToggleGroup toggleGroup;

	// Token: 0x0400164F RID: 5711
	public int group = 1;

	// Token: 0x04001650 RID: 5712
	public bool isDisable;

	// Token: 0x04001651 RID: 5713
	public bool mustSelect = true;

	// Token: 0x04001652 RID: 5714
	private Color nullColor = new Color(1f, 1f, 1f, 0f);

	// Token: 0x04001653 RID: 5715
	private Color hasColor = new Color(1f, 1f, 1f, 1f);

	// Token: 0x04001654 RID: 5716
	public UnityEvent valueChange;

	// Token: 0x04001655 RID: 5717
	public UnityEvent clickEvent;

	// Token: 0x04001656 RID: 5718
	public bool isOn;
}
