using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YSGame;

// Token: 0x020002BA RID: 698
public class FpBtn : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
	// Token: 0x06001892 RID: 6290 RVA: 0x000B06BC File Offset: 0x000AE8BC
	public void SetCanClick(bool flag)
	{
		this.IsCanClick = flag;
		if (this.stopClickSprite != null)
		{
			if (flag)
			{
				this.targetImage.sprite = this.nomalSprite;
				return;
			}
			if (this.stopClickSprite != null)
			{
				this.targetImage.sprite = this.stopClickSprite;
				return;
			}
			this.targetImage.sprite = this.nomalSprite;
		}
	}

	// Token: 0x06001893 RID: 6291 RVA: 0x000B0724 File Offset: 0x000AE924
	public virtual void OnPointerDown(PointerEventData eventData)
	{
		if (this.IsCanClick)
		{
			if (this.mouseDownSprite != null)
			{
				this.targetImage.sprite = this.mouseDownSprite;
			}
			this.mouseDownEvent.Invoke();
		}
	}

	// Token: 0x06001894 RID: 6292 RVA: 0x000B0758 File Offset: 0x000AE958
	public virtual void OnPointerEnter(PointerEventData eventData)
	{
		if (eventData.dragging)
		{
			return;
		}
		if (this.IsCanClick)
		{
			if (this.mouseEnterSprite != null)
			{
				this.targetImage.sprite = this.mouseEnterSprite;
			}
			this.mouseEnterEvent.Invoke();
		}
		this.IsInBtn = true;
	}

	// Token: 0x06001895 RID: 6293 RVA: 0x000B07A7 File Offset: 0x000AE9A7
	public virtual void OnPointerExit(PointerEventData eventData)
	{
		if (this.IsCanClick)
		{
			this.targetImage.sprite = this.nomalSprite;
			this.mouseOutEvent.Invoke();
		}
		this.IsInBtn = false;
	}

	// Token: 0x06001896 RID: 6294 RVA: 0x000B07D4 File Offset: 0x000AE9D4
	public virtual void OnPointerUp(PointerEventData eventData)
	{
		if (eventData.dragging)
		{
			return;
		}
		if (this.IsCanClick)
		{
			if (MusicMag.instance != null)
			{
				if (this.audioClip != null)
				{
					MusicMag.instance.PlayEffectMusic(this.audioClip, 1f);
				}
				else
				{
					MusicMag.instance.PlayEffectMusic(1, 1f);
				}
			}
			if (this.mouseUpSprite != null)
			{
				this.targetImage.sprite = this.mouseUpSprite;
			}
			else
			{
				this.targetImage.sprite = this.nomalSprite;
			}
			if (this.IsInBtn)
			{
				this.mouseUpEvent.Invoke();
				UnityAction<PointerEventData> mouseUp = this.MouseUp;
				if (mouseUp == null)
				{
					return;
				}
				mouseUp.Invoke(eventData);
			}
		}
	}

	// Token: 0x06001897 RID: 6295 RVA: 0x000B0890 File Offset: 0x000AEA90
	public void SetGrey(bool isGrey)
	{
		this.NowIsGrey = isGrey;
		Material material = isGrey ? GreyMatManager.Grey1 : null;
		Image[] componentsInChildren = base.GetComponentsInChildren<Image>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].material = material;
		}
	}

	// Token: 0x0400139A RID: 5018
	public Image targetImage;

	// Token: 0x0400139B RID: 5019
	public Sprite nomalSprite;

	// Token: 0x0400139C RID: 5020
	public Sprite mouseEnterSprite;

	// Token: 0x0400139D RID: 5021
	public Sprite mouseDownSprite;

	// Token: 0x0400139E RID: 5022
	public Sprite mouseUpSprite;

	// Token: 0x0400139F RID: 5023
	public Sprite stopClickSprite;

	// Token: 0x040013A0 RID: 5024
	public AudioClip audioClip;

	// Token: 0x040013A1 RID: 5025
	public AudioClip MouseHoverAudioClip;

	// Token: 0x040013A2 RID: 5026
	public UnityEvent mouseUpEvent;

	// Token: 0x040013A3 RID: 5027
	public UnityEvent mouseDownEvent;

	// Token: 0x040013A4 RID: 5028
	public UnityEvent mouseEnterEvent;

	// Token: 0x040013A5 RID: 5029
	public UnityEvent mouseOutEvent;

	// Token: 0x040013A6 RID: 5030
	public UnityAction<PointerEventData> MouseUp;

	// Token: 0x040013A7 RID: 5031
	public bool IsInBtn;

	// Token: 0x040013A8 RID: 5032
	private bool IsCanClick = true;

	// Token: 0x040013A9 RID: 5033
	[HideInInspector]
	public bool NowIsGrey;
}
