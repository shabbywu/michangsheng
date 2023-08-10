using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainUILinGenCell : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
	[SerializeField]
	private Image targetImage;

	[SerializeField]
	private Sprite nomalSprite;

	[SerializeField]
	private Sprite enterSprite;

	[SerializeField]
	private Sprite clickSprite;

	[SerializeField]
	private Sprite isOn_nomalSprite;

	[SerializeField]
	private Sprite isOn_enterSprite;

	[SerializeField]
	private Sprite isOn_clickSprite;

	public bool isOn;

	public Text precent;

	public int index;

	public UnityAction<int> clickEvent;

	public void OnPointerDown(PointerEventData eventData)
	{
		if (isOn)
		{
			targetImage.sprite = isOn_clickSprite;
		}
		else
		{
			targetImage.sprite = clickSprite;
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (isOn)
		{
			targetImage.sprite = isOn_enterSprite;
		}
		else
		{
			targetImage.sprite = enterSprite;
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (isOn)
		{
			targetImage.sprite = isOn_nomalSprite;
		}
		else
		{
			targetImage.sprite = nomalSprite;
		}
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		isOn = !isOn;
		OnvalueChange();
	}

	public void OnvalueChange()
	{
		if (clickEvent != null)
		{
			clickEvent.Invoke(index);
		}
		if (isOn)
		{
			targetImage.sprite = isOn_nomalSprite;
		}
		else
		{
			targetImage.sprite = nomalSprite;
		}
	}
}
