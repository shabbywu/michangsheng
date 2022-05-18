using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000487 RID: 1159
public class MainUILinGenCell : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
	// Token: 0x06001EFA RID: 7930 RVA: 0x00019ABE File Offset: 0x00017CBE
	public void OnPointerDown(PointerEventData eventData)
	{
		if (this.isOn)
		{
			this.targetImage.sprite = this.isOn_clickSprite;
			return;
		}
		this.targetImage.sprite = this.clickSprite;
	}

	// Token: 0x06001EFB RID: 7931 RVA: 0x00019AEB File Offset: 0x00017CEB
	public void OnPointerEnter(PointerEventData eventData)
	{
		if (this.isOn)
		{
			this.targetImage.sprite = this.isOn_enterSprite;
			return;
		}
		this.targetImage.sprite = this.enterSprite;
	}

	// Token: 0x06001EFC RID: 7932 RVA: 0x00019B18 File Offset: 0x00017D18
	public void OnPointerExit(PointerEventData eventData)
	{
		if (this.isOn)
		{
			this.targetImage.sprite = this.isOn_nomalSprite;
			return;
		}
		this.targetImage.sprite = this.nomalSprite;
	}

	// Token: 0x06001EFD RID: 7933 RVA: 0x00019B45 File Offset: 0x00017D45
	public void OnPointerUp(PointerEventData eventData)
	{
		this.isOn = !this.isOn;
		this.OnvalueChange();
	}

	// Token: 0x06001EFE RID: 7934 RVA: 0x0010AA08 File Offset: 0x00108C08
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

	// Token: 0x04001A61 RID: 6753
	[SerializeField]
	private Image targetImage;

	// Token: 0x04001A62 RID: 6754
	[SerializeField]
	private Sprite nomalSprite;

	// Token: 0x04001A63 RID: 6755
	[SerializeField]
	private Sprite enterSprite;

	// Token: 0x04001A64 RID: 6756
	[SerializeField]
	private Sprite clickSprite;

	// Token: 0x04001A65 RID: 6757
	[SerializeField]
	private Sprite isOn_nomalSprite;

	// Token: 0x04001A66 RID: 6758
	[SerializeField]
	private Sprite isOn_enterSprite;

	// Token: 0x04001A67 RID: 6759
	[SerializeField]
	private Sprite isOn_clickSprite;

	// Token: 0x04001A68 RID: 6760
	public bool isOn;

	// Token: 0x04001A69 RID: 6761
	public Text precent;

	// Token: 0x04001A6A RID: 6762
	public int index;

	// Token: 0x04001A6B RID: 6763
	public UnityAction<int> clickEvent;
}
