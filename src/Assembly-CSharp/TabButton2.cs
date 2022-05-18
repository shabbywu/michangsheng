using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YSGame;

// Token: 0x020004D7 RID: 1239
public class TabButton2 : TabButton, IPointerEnterHandler, IEventSystemHandler
{
	// Token: 0x06002060 RID: 8288 RVA: 0x001131C0 File Offset: 0x001113C0
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

	// Token: 0x06002061 RID: 8289 RVA: 0x0001A97A File Offset: 0x00018B7A
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

	// Token: 0x06002062 RID: 8290 RVA: 0x0001A9B7 File Offset: 0x00018BB7
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

	// Token: 0x06002063 RID: 8291 RVA: 0x0001A9F4 File Offset: 0x00018BF4
	public override void OnButtonClick()
	{
		base.OnButtonClick();
	}

	// Token: 0x06002064 RID: 8292 RVA: 0x0001A9FC File Offset: 0x00018BFC
	public void PlayClickSound()
	{
		if (this.ClickSound != null)
		{
			MusicMag.instance.PlayEffectMusic(this.ClickSound, 1f);
			return;
		}
		MusicMag.instance.PlayEffectMusic(1, 1f);
	}

	// Token: 0x06002065 RID: 8293 RVA: 0x0001AA32 File Offset: 0x00018C32
	public void PlayHoverSound()
	{
		if (this.MouseHoverSound != null)
		{
			MusicMag.instance.PlayEffectMusic(this.MouseHoverSound, 1f);
		}
	}

	// Token: 0x06002066 RID: 8294 RVA: 0x0001AA57 File Offset: 0x00018C57
	public void OnPointerEnter(PointerEventData eventData)
	{
		this.PlayHoverSound();
	}

	// Token: 0x04001BD4 RID: 7124
	private Button Button;

	// Token: 0x04001BD5 RID: 7125
	public Sprite NormalSprite;

	// Token: 0x04001BD6 RID: 7126
	public Sprite HighlightSprite;

	// Token: 0x04001BD7 RID: 7127
	public Sprite PressSprite;

	// Token: 0x04001BD8 RID: 7128
	public Sprite SelectedSprite;

	// Token: 0x04001BD9 RID: 7129
	public AudioClip ClickSound;

	// Token: 0x04001BDA RID: 7130
	public AudioClip MouseHoverSound;

	// Token: 0x04001BDB RID: 7131
	private SpriteState loseState;

	// Token: 0x04001BDC RID: 7132
	private SpriteState toggleState;

	// Token: 0x04001BDD RID: 7133
	private Image Image;

	// Token: 0x04001BDE RID: 7134
	public UnityEvent OnToggleEvent;

	// Token: 0x04001BDF RID: 7135
	public UnityEvent OnLoseEvent;
}
