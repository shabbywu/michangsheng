using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
using YSGame;

public class FpBtn : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
	public Image targetImage;

	public Sprite nomalSprite;

	public Sprite mouseEnterSprite;

	public Sprite mouseDownSprite;

	public Sprite mouseUpSprite;

	public Sprite stopClickSprite;

	public AudioClip audioClip;

	public AudioClip MouseHoverAudioClip;

	public UnityEvent mouseUpEvent;

	public UnityEvent mouseDownEvent;

	public UnityEvent mouseEnterEvent;

	public UnityEvent mouseOutEvent;

	public UnityAction<PointerEventData> MouseUp;

	public bool IsInBtn;

	private bool IsCanClick = true;

	[HideInInspector]
	public bool NowIsGrey;

	public void SetCanClick(bool flag)
	{
		IsCanClick = flag;
		if ((Object)(object)stopClickSprite != (Object)null)
		{
			if (flag)
			{
				targetImage.sprite = nomalSprite;
			}
			else if ((Object)(object)stopClickSprite != (Object)null)
			{
				targetImage.sprite = stopClickSprite;
			}
			else
			{
				targetImage.sprite = nomalSprite;
			}
		}
	}

	public virtual void OnPointerDown(PointerEventData eventData)
	{
		if (IsCanClick)
		{
			if ((Object)(object)mouseDownSprite != (Object)null)
			{
				targetImage.sprite = mouseDownSprite;
			}
			mouseDownEvent.Invoke();
		}
	}

	public virtual void OnPointerEnter(PointerEventData eventData)
	{
		if (eventData.dragging)
		{
			return;
		}
		if (IsCanClick)
		{
			if ((Object)(object)mouseEnterSprite != (Object)null)
			{
				targetImage.sprite = mouseEnterSprite;
			}
			mouseEnterEvent.Invoke();
		}
		IsInBtn = true;
	}

	public virtual void OnPointerExit(PointerEventData eventData)
	{
		if (IsCanClick)
		{
			targetImage.sprite = nomalSprite;
			mouseOutEvent.Invoke();
		}
		IsInBtn = false;
	}

	public virtual void OnPointerUp(PointerEventData eventData)
	{
		if (eventData.dragging || !IsCanClick)
		{
			return;
		}
		if ((Object)(object)MusicMag.instance != (Object)null)
		{
			if ((Object)(object)audioClip != (Object)null)
			{
				MusicMag.instance.PlayEffectMusic(audioClip);
			}
			else
			{
				MusicMag.instance.PlayEffectMusic(1);
			}
		}
		if ((Object)(object)mouseUpSprite != (Object)null)
		{
			targetImage.sprite = mouseUpSprite;
		}
		else
		{
			targetImage.sprite = nomalSprite;
		}
		if (IsInBtn)
		{
			mouseUpEvent.Invoke();
			MouseUp?.Invoke(eventData);
		}
	}

	public void SetGrey(bool isGrey)
	{
		NowIsGrey = isGrey;
		Material material = (isGrey ? GreyMatManager.Grey1 : null);
		Image[] componentsInChildren = ((Component)this).GetComponentsInChildren<Image>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			((Graphic)componentsInChildren[i]).material = material;
		}
	}
}
