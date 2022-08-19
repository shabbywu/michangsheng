using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000322 RID: 802
public class MainUILinGenCell : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
	// Token: 0x06001BBA RID: 7098 RVA: 0x000C5836 File Offset: 0x000C3A36
	public void OnPointerDown(PointerEventData eventData)
	{
		if (this.isOn)
		{
			this.targetImage.sprite = this.isOn_clickSprite;
			return;
		}
		this.targetImage.sprite = this.clickSprite;
	}

	// Token: 0x06001BBB RID: 7099 RVA: 0x000C5863 File Offset: 0x000C3A63
	public void OnPointerEnter(PointerEventData eventData)
	{
		if (this.isOn)
		{
			this.targetImage.sprite = this.isOn_enterSprite;
			return;
		}
		this.targetImage.sprite = this.enterSprite;
	}

	// Token: 0x06001BBC RID: 7100 RVA: 0x000C5890 File Offset: 0x000C3A90
	public void OnPointerExit(PointerEventData eventData)
	{
		if (this.isOn)
		{
			this.targetImage.sprite = this.isOn_nomalSprite;
			return;
		}
		this.targetImage.sprite = this.nomalSprite;
	}

	// Token: 0x06001BBD RID: 7101 RVA: 0x000C58BD File Offset: 0x000C3ABD
	public void OnPointerUp(PointerEventData eventData)
	{
		this.isOn = !this.isOn;
		this.OnvalueChange();
	}

	// Token: 0x06001BBE RID: 7102 RVA: 0x000C58D4 File Offset: 0x000C3AD4
	public void OnvalueChange()
	{
		if (this.clickEvent != null)
		{
			this.clickEvent.Invoke(this.index);
		}
		if (this.isOn)
		{
			this.targetImage.sprite = this.isOn_nomalSprite;
			return;
		}
		this.targetImage.sprite = this.nomalSprite;
	}

	// Token: 0x0400163C RID: 5692
	[SerializeField]
	private Image targetImage;

	// Token: 0x0400163D RID: 5693
	[SerializeField]
	private Sprite nomalSprite;

	// Token: 0x0400163E RID: 5694
	[SerializeField]
	private Sprite enterSprite;

	// Token: 0x0400163F RID: 5695
	[SerializeField]
	private Sprite clickSprite;

	// Token: 0x04001640 RID: 5696
	[SerializeField]
	private Sprite isOn_nomalSprite;

	// Token: 0x04001641 RID: 5697
	[SerializeField]
	private Sprite isOn_enterSprite;

	// Token: 0x04001642 RID: 5698
	[SerializeField]
	private Sprite isOn_clickSprite;

	// Token: 0x04001643 RID: 5699
	public bool isOn;

	// Token: 0x04001644 RID: 5700
	public Text precent;

	// Token: 0x04001645 RID: 5701
	public int index;

	// Token: 0x04001646 RID: 5702
	public UnityAction<int> clickEvent;
}
