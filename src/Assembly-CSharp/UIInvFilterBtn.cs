using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YSGame;

// Token: 0x02000417 RID: 1047
public class UIInvFilterBtn : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x17000292 RID: 658
	// (get) Token: 0x06001C2C RID: 7212 RVA: 0x000178CF File Offset: 0x00015ACF
	// (set) Token: 0x06001C2D RID: 7213 RVA: 0x000FACBC File Offset: 0x000F8EBC
	public UIInvFilterBtn.BtnState State
	{
		get
		{
			return this.state;
		}
		set
		{
			if (this.state != value)
			{
				this.state = value;
				switch (this.state)
				{
				case UIInvFilterBtn.BtnState.Normal:
					this.BGImage.sprite = this.NormalSprite;
					this.ShowText.color = this.TextNormalColor;
					if (this.SanJiao != null)
					{
						ShortcutExtensions.DOLocalRotate(this.SanJiao.transform, Vector3.zero, 0.3f, 0);
						return;
					}
					break;
				case UIInvFilterBtn.BtnState.Active:
					this.IsSelected = true;
					this.BGImage.sprite = this.ActiveSprite;
					this.ShowText.color = this.TextActiveColor;
					if (this.SanJiao != null)
					{
						ShortcutExtensions.DOLocalRotate(this.SanJiao.transform, new Vector3(0f, 0f, 180f), 0.3f, 0);
						return;
					}
					break;
				case UIInvFilterBtn.BtnState.HoverNoActive:
					this.BGImage.sprite = this.ActiveSprite;
					this.ShowText.color = this.TextActiveColor;
					break;
				default:
					return;
				}
			}
		}
	}

	// Token: 0x06001C2E RID: 7214 RVA: 0x000178D7 File Offset: 0x00015AD7
	public void ClearListener()
	{
		this.IsNew = true;
		if (this.button == null)
		{
			this.button = base.GetComponent<Button>();
		}
		this.button.onClick.RemoveAllListeners();
	}

	// Token: 0x06001C2F RID: 7215 RVA: 0x0001790A File Offset: 0x00015B0A
	public void AddClickEvent(UnityAction call)
	{
		if (this.button == null)
		{
			this.button = base.GetComponent<Button>();
		}
		this.button.onClick.AddListener(call);
	}

	// Token: 0x06001C30 RID: 7216 RVA: 0x000FADCC File Offset: 0x000F8FCC
	private void Awake()
	{
		if (this.IsNew)
		{
			return;
		}
		this.inventory = base.transform.parent.parent.parent.GetComponent<UIInventory>();
		this.button = base.GetComponent<Button>();
		this.button.onClick.AddListener(delegate()
		{
			this.inventory.UITmpValue = this.BtnData;
			this.inventory.OnFilterBtnClick(this);
		});
	}

	// Token: 0x06001C31 RID: 7217 RVA: 0x00017937 File Offset: 0x00015B37
	public void OnPointerEnter(PointerEventData eventData)
	{
		if (this.State == UIInvFilterBtn.BtnState.Normal)
		{
			this.State = UIInvFilterBtn.BtnState.HoverNoActive;
		}
		if (this.BtnData < 0)
		{
			MusicMag.instance.PlayEffectMusic(3, 1f);
		}
	}

	// Token: 0x06001C32 RID: 7218 RVA: 0x00017961 File Offset: 0x00015B61
	public void OnPointerExit(PointerEventData eventData)
	{
		if (this.State == UIInvFilterBtn.BtnState.HoverNoActive)
		{
			this.State = UIInvFilterBtn.BtnState.Normal;
		}
	}

	// Token: 0x0400182A RID: 6186
	private UIInventory inventory;

	// Token: 0x0400182B RID: 6187
	private Button button;

	// Token: 0x0400182C RID: 6188
	public Image BGImage;

	// Token: 0x0400182D RID: 6189
	public Image SanJiao;

	// Token: 0x0400182E RID: 6190
	public Text ShowText;

	// Token: 0x0400182F RID: 6191
	public string DeafaultName;

	// Token: 0x04001830 RID: 6192
	public Color TextNormalColor;

	// Token: 0x04001831 RID: 6193
	public Color TextActiveColor;

	// Token: 0x04001832 RID: 6194
	public Sprite NormalSprite;

	// Token: 0x04001833 RID: 6195
	public Sprite ActiveSprite;

	// Token: 0x04001834 RID: 6196
	public bool IsSelected;

	// Token: 0x04001835 RID: 6197
	private UIInvFilterBtn.BtnState state;

	// Token: 0x04001836 RID: 6198
	public int BtnData;

	// Token: 0x04001837 RID: 6199
	public bool IsNew;

	// Token: 0x02000418 RID: 1048
	public enum BtnState
	{
		// Token: 0x04001839 RID: 6201
		Normal,
		// Token: 0x0400183A RID: 6202
		Active,
		// Token: 0x0400183B RID: 6203
		HoverNoActive
	}
}
