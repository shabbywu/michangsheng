using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainUIToggle : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
	[SerializeField]
	private Image targetImage;

	[SerializeField]
	private Sprite nomalSprite;

	[SerializeField]
	private Sprite selectSprite;

	[SerializeField]
	private Sprite disableSprite;

	[SerializeField]
	private Text text;

	[SerializeField]
	private Color nomalColor;

	[SerializeField]
	private Color selectColor;

	public MainUIToggleGroup toggleGroup;

	public int group = 1;

	public bool isDisable;

	public bool mustSelect = true;

	private Color nullColor = new Color(1f, 1f, 1f, 0f);

	private Color hasColor = new Color(1f, 1f, 1f, 1f);

	public UnityEvent valueChange;

	public UnityEvent clickEvent;

	public bool isOn;

	public string Text
	{
		get
		{
			return text.text;
		}
		set
		{
			text.text = value;
		}
	}

	private void Awake()
	{
		toggleGroup.toggleList.Add(this);
	}

	public void OnValueChange()
	{
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		if (isOn)
		{
			if ((Object)(object)text != (Object)null)
			{
				((Graphic)text).color = selectColor;
			}
			if ((Object)(object)targetImage != (Object)null)
			{
				targetImage.sprite = selectSprite;
				((Graphic)targetImage).color = hasColor;
			}
		}
		else
		{
			if ((Object)(object)text != (Object)null)
			{
				((Graphic)text).color = nomalColor;
			}
			if ((Object)(object)targetImage != (Object)null)
			{
				if ((Object)(object)nomalSprite == (Object)null)
				{
					((Graphic)targetImage).color = nullColor;
				}
				else
				{
					targetImage.sprite = nomalSprite;
				}
			}
		}
		if (valueChange != null)
		{
			valueChange.Invoke();
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (isDisable)
		{
			return;
		}
		if (mustSelect)
		{
			if (!isOn)
			{
				isOn = true;
			}
		}
		else
		{
			isOn = !isOn;
		}
		OnValueChange();
		if (isOn && (Object)(object)toggleGroup != (Object)null)
		{
			toggleGroup.OnChildToggleChange(this);
		}
		if (clickEvent != null)
		{
			clickEvent.Invoke();
		}
	}

	public void SetDisable()
	{
		targetImage.sprite = disableSprite;
		isDisable = true;
	}

	public void MoNiClick()
	{
		OnPointerDown(null);
	}
}
