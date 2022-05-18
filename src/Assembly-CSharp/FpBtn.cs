using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YSGame;

// Token: 0x020003F6 RID: 1014
public class FpBtn : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
	// Token: 0x06001B85 RID: 7045 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x06001B86 RID: 7046 RVA: 0x000F6CFC File Offset: 0x000F4EFC
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

	// Token: 0x06001B87 RID: 7047 RVA: 0x00017203 File Offset: 0x00015403
	public void OnPointerDown(PointerEventData eventData)
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

	// Token: 0x06001B88 RID: 7048 RVA: 0x000F6D64 File Offset: 0x000F4F64
	public void OnPointerEnter(PointerEventData eventData)
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

	// Token: 0x06001B89 RID: 7049 RVA: 0x00017237 File Offset: 0x00015437
	public void OnPointerExit(PointerEventData eventData)
	{
		if (this.IsCanClick)
		{
			this.targetImage.sprite = this.nomalSprite;
			this.mouseOutEvent.Invoke();
		}
		this.IsInBtn = false;
	}

	// Token: 0x06001B8A RID: 7050 RVA: 0x000F6DB4 File Offset: 0x000F4FB4
	public void OnPointerUp(PointerEventData eventData)
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

	// Token: 0x06001B8B RID: 7051 RVA: 0x000F6E70 File Offset: 0x000F5070
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

	// Token: 0x0400173D RID: 5949
	public Image targetImage;

	// Token: 0x0400173E RID: 5950
	public Sprite nomalSprite;

	// Token: 0x0400173F RID: 5951
	public Sprite mouseEnterSprite;

	// Token: 0x04001740 RID: 5952
	public Sprite mouseDownSprite;

	// Token: 0x04001741 RID: 5953
	public Sprite mouseUpSprite;

	// Token: 0x04001742 RID: 5954
	public Sprite stopClickSprite;

	// Token: 0x04001743 RID: 5955
	public AudioClip audioClip;

	// Token: 0x04001744 RID: 5956
	public AudioClip MouseHoverAudioClip;

	// Token: 0x04001745 RID: 5957
	public UnityEvent mouseUpEvent;

	// Token: 0x04001746 RID: 5958
	public UnityEvent mouseDownEvent;

	// Token: 0x04001747 RID: 5959
	public UnityEvent mouseEnterEvent;

	// Token: 0x04001748 RID: 5960
	public UnityEvent mouseOutEvent;

	// Token: 0x04001749 RID: 5961
	public UnityAction<PointerEventData> MouseUp;

	// Token: 0x0400174A RID: 5962
	public bool IsInBtn;

	// Token: 0x0400174B RID: 5963
	private bool IsCanClick = true;

	// Token: 0x0400174C RID: 5964
	[HideInInspector]
	public bool NowIsGrey;
}
