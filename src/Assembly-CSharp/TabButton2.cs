using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YSGame;

// Token: 0x0200035A RID: 858
public class TabButton2 : TabButton, IPointerEnterHandler, IEventSystemHandler
{
	// Token: 0x06001CF6 RID: 7414 RVA: 0x000CE46C File Offset: 0x000CC66C
	public override void Awake()
	{
		this.toggleState.highlightedSprite = this.SelectedSprite;
		this.toggleState.pressedSprite = this.SelectedSprite;
		this.loseState.highlightedSprite = this.HighlightSprite;
		this.loseState.pressedSprite = this.PressSprite;
		this.Button = base.GetComponent<Button>();
		this.Image = base.GetComponent<Image>();
		this.Button.onClick.AddListener(new UnityAction(this.OnButtonClick));
		this.Group.AddTab(this);
	}

	// Token: 0x06001CF7 RID: 7415 RVA: 0x000CE4FE File Offset: 0x000CC6FE
	public override void OnToggle()
	{
		base.OnToggle();
		this.Image.sprite = this.SelectedSprite;
		this.Button.spriteState = this.toggleState;
		if (this.OnToggleEvent != null)
		{
			this.OnToggleEvent.Invoke();
		}
	}

	// Token: 0x06001CF8 RID: 7416 RVA: 0x000CE53B File Offset: 0x000CC73B
	public override void OnLose()
	{
		base.OnLose();
		this.Image.sprite = this.NormalSprite;
		this.Button.spriteState = this.loseState;
		if (this.OnLoseEvent != null)
		{
			this.OnLoseEvent.Invoke();
		}
	}

	// Token: 0x06001CF9 RID: 7417 RVA: 0x000CE578 File Offset: 0x000CC778
	public override void OnButtonClick()
	{
		base.OnButtonClick();
	}

	// Token: 0x06001CFA RID: 7418 RVA: 0x000CE580 File Offset: 0x000CC780
	public void PlayClickSound()
	{
		if (this.ClickSound != null)
		{
			MusicMag.instance.PlayEffectMusic(this.ClickSound, 1f);
			return;
		}
		MusicMag.instance.PlayEffectMusic(1, 1f);
	}

	// Token: 0x06001CFB RID: 7419 RVA: 0x000CE5B6 File Offset: 0x000CC7B6
	public void PlayHoverSound()
	{
		if (this.MouseHoverSound != null)
		{
			MusicMag.instance.PlayEffectMusic(this.MouseHoverSound, 1f);
		}
	}

	// Token: 0x06001CFC RID: 7420 RVA: 0x000CE5DB File Offset: 0x000CC7DB
	public void OnPointerEnter(PointerEventData eventData)
	{
		this.PlayHoverSound();
	}

	// Token: 0x0400177C RID: 6012
	private Button Button;

	// Token: 0x0400177D RID: 6013
	public Sprite NormalSprite;

	// Token: 0x0400177E RID: 6014
	public Sprite HighlightSprite;

	// Token: 0x0400177F RID: 6015
	public Sprite PressSprite;

	// Token: 0x04001780 RID: 6016
	public Sprite SelectedSprite;

	// Token: 0x04001781 RID: 6017
	public AudioClip ClickSound;

	// Token: 0x04001782 RID: 6018
	public AudioClip MouseHoverSound;

	// Token: 0x04001783 RID: 6019
	private SpriteState loseState;

	// Token: 0x04001784 RID: 6020
	private SpriteState toggleState;

	// Token: 0x04001785 RID: 6021
	private Image Image;

	// Token: 0x04001786 RID: 6022
	public UnityEvent OnToggleEvent;

	// Token: 0x04001787 RID: 6023
	public UnityEvent OnLoseEvent;
}
