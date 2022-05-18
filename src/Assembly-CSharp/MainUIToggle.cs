using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000488 RID: 1160
public class MainUIToggle : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
	// Token: 0x1700029C RID: 668
	// (get) Token: 0x06001F00 RID: 7936 RVA: 0x00019B5C File Offset: 0x00017D5C
	// (set) Token: 0x06001F01 RID: 7937 RVA: 0x00019B69 File Offset: 0x00017D69
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

	// Token: 0x06001F02 RID: 7938 RVA: 0x00019B77 File Offset: 0x00017D77
	private void Awake()
	{
		this.toggleGroup.toggleList.Add(this);
	}

	// Token: 0x06001F03 RID: 7939 RVA: 0x0010AA5C File Offset: 0x00108C5C
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

	// Token: 0x06001F04 RID: 7940 RVA: 0x0010AB38 File Offset: 0x00108D38
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

	// Token: 0x06001F05 RID: 7941 RVA: 0x00019B8A File Offset: 0x00017D8A
	public void SetDisable()
	{
		this.targetImage.sprite = this.disableSprite;
		this.isDisable = true;
	}

	// Token: 0x06001F06 RID: 7942 RVA: 0x00019BA4 File Offset: 0x00017DA4
	public void MoNiClick()
	{
		this.OnPointerDown(null);
	}

	// Token: 0x04001A6C RID: 6764
	[SerializeField]
	private Image targetImage;

	// Token: 0x04001A6D RID: 6765
	[SerializeField]
	private Sprite nomalSprite;

	// Token: 0x04001A6E RID: 6766
	[SerializeField]
	private Sprite selectSprite;

	// Token: 0x04001A6F RID: 6767
	[SerializeField]
	private Sprite disableSprite;

	// Token: 0x04001A70 RID: 6768
	[SerializeField]
	private Text text;

	// Token: 0x04001A71 RID: 6769
	[SerializeField]
	private Color nomalColor;

	// Token: 0x04001A72 RID: 6770
	[SerializeField]
	private Color selectColor;

	// Token: 0x04001A73 RID: 6771
	public MainUIToggleGroup toggleGroup;

	// Token: 0x04001A74 RID: 6772
	public int group = 1;

	// Token: 0x04001A75 RID: 6773
	public bool isDisable;

	// Token: 0x04001A76 RID: 6774
	public bool mustSelect = true;

	// Token: 0x04001A77 RID: 6775
	private Color nullColor = new Color(1f, 1f, 1f, 0f);

	// Token: 0x04001A78 RID: 6776
	private Color hasColor = new Color(1f, 1f, 1f, 1f);

	// Token: 0x04001A79 RID: 6777
	public UnityEvent valueChange;

	// Token: 0x04001A7A RID: 6778
	public UnityEvent clickEvent;

	// Token: 0x04001A7B RID: 6779
	public bool isOn;
}
