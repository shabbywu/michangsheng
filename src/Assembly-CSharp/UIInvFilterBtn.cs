using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YSGame;

// Token: 0x020002CC RID: 716
public class UIInvFilterBtn : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x17000248 RID: 584
	// (get) Token: 0x06001924 RID: 6436 RVA: 0x000B4C0D File Offset: 0x000B2E0D
	// (set) Token: 0x06001925 RID: 6437 RVA: 0x000B4C18 File Offset: 0x000B2E18
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

	// Token: 0x06001926 RID: 6438 RVA: 0x000B4D27 File Offset: 0x000B2F27
	public void ClearListener()
	{
		this.IsNew = true;
		if (this.button == null)
		{
			this.button = base.GetComponent<Button>();
		}
		this.button.onClick.RemoveAllListeners();
	}

	// Token: 0x06001927 RID: 6439 RVA: 0x000B4D5A File Offset: 0x000B2F5A
	public void AddClickEvent(UnityAction call)
	{
		if (this.button == null)
		{
			this.button = base.GetComponent<Button>();
		}
		this.button.onClick.AddListener(call);
	}

	// Token: 0x06001928 RID: 6440 RVA: 0x000B4D88 File Offset: 0x000B2F88
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

	// Token: 0x06001929 RID: 6441 RVA: 0x000B4DE6 File Offset: 0x000B2FE6
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

	// Token: 0x0600192A RID: 6442 RVA: 0x000B4E10 File Offset: 0x000B3010
	public void OnPointerExit(PointerEventData eventData)
	{
		if (this.State == UIInvFilterBtn.BtnState.HoverNoActive)
		{
			this.State = UIInvFilterBtn.BtnState.Normal;
		}
	}

	// Token: 0x04001460 RID: 5216
	private UIInventory inventory;

	// Token: 0x04001461 RID: 5217
	private Button button;

	// Token: 0x04001462 RID: 5218
	public Image BGImage;

	// Token: 0x04001463 RID: 5219
	public Image SanJiao;

	// Token: 0x04001464 RID: 5220
	public Text ShowText;

	// Token: 0x04001465 RID: 5221
	public string DeafaultName;

	// Token: 0x04001466 RID: 5222
	public Color TextNormalColor;

	// Token: 0x04001467 RID: 5223
	public Color TextActiveColor;

	// Token: 0x04001468 RID: 5224
	public Sprite NormalSprite;

	// Token: 0x04001469 RID: 5225
	public Sprite ActiveSprite;

	// Token: 0x0400146A RID: 5226
	public bool IsSelected;

	// Token: 0x0400146B RID: 5227
	private UIInvFilterBtn.BtnState state;

	// Token: 0x0400146C RID: 5228
	public int BtnData;

	// Token: 0x0400146D RID: 5229
	public bool IsNew;

	// Token: 0x02001321 RID: 4897
	public enum BtnState
	{
		// Token: 0x0400678C RID: 26508
		Normal,
		// Token: 0x0400678D RID: 26509
		Active,
		// Token: 0x0400678E RID: 26510
		HoverNoActive
	}
}
