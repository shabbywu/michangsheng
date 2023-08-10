using UnityEngine;
using UnityEngine.EventSystems;

public class UTooltipTrigger : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	[Multiline]
	public string Tooltip;

	private bool isShow;

	public bool UseCustomWidth;

	public int CustomWidth;

	private void Awake()
	{
		MessageMag.Instance.Register(MessageName.MSG_APP_OnFocusChanged, OnFocusChanged);
	}

	private void OnDestroy()
	{
		MessageMag.Instance.Remove(MessageName.MSG_APP_OnFocusChanged, OnFocusChanged);
	}

	public void OnFocusChanged(MessageData data)
	{
		if (isShow)
		{
			isShow = false;
			UToolTip.Close();
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (UseCustomWidth)
		{
			UToolTip.Show(Tooltip, CustomWidth);
		}
		else
		{
			UToolTip.Show(Tooltip);
		}
		isShow = true;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		isShow = false;
		UToolTip.Close();
	}
}
