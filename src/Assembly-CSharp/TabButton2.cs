using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
using YSGame;

public class TabButton2 : TabButton, IPointerEnterHandler, IEventSystemHandler
{
	private Button Button;

	public Sprite NormalSprite;

	public Sprite HighlightSprite;

	public Sprite PressSprite;

	public Sprite SelectedSprite;

	public AudioClip ClickSound;

	public AudioClip MouseHoverSound;

	private SpriteState loseState;

	private SpriteState toggleState;

	private Image Image;

	public UnityEvent OnToggleEvent;

	public UnityEvent OnLoseEvent;

	public override void Awake()
	{
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Expected O, but got Unknown
		((SpriteState)(ref toggleState)).highlightedSprite = SelectedSprite;
		((SpriteState)(ref toggleState)).pressedSprite = SelectedSprite;
		((SpriteState)(ref loseState)).highlightedSprite = HighlightSprite;
		((SpriteState)(ref loseState)).pressedSprite = PressSprite;
		Button = ((Component)this).GetComponent<Button>();
		Image = ((Component)this).GetComponent<Image>();
		((UnityEvent)Button.onClick).AddListener(new UnityAction(OnButtonClick));
		Group.AddTab(this);
	}

	public override void OnToggle()
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		base.OnToggle();
		Image.sprite = SelectedSprite;
		((Selectable)Button).spriteState = toggleState;
		if (OnToggleEvent != null)
		{
			OnToggleEvent.Invoke();
		}
	}

	public override void OnLose()
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		base.OnLose();
		Image.sprite = NormalSprite;
		((Selectable)Button).spriteState = loseState;
		if (OnLoseEvent != null)
		{
			OnLoseEvent.Invoke();
		}
	}

	public override void OnButtonClick()
	{
		base.OnButtonClick();
	}

	public void PlayClickSound()
	{
		if ((Object)(object)ClickSound != (Object)null)
		{
			MusicMag.instance.PlayEffectMusic(ClickSound);
		}
		else
		{
			MusicMag.instance.PlayEffectMusic(1);
		}
	}

	public void PlayHoverSound()
	{
		if ((Object)(object)MouseHoverSound != (Object)null)
		{
			MusicMag.instance.PlayEffectMusic(MouseHoverSound);
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		PlayHoverSound();
	}
}
