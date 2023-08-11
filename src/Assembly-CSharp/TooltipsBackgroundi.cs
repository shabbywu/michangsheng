using UnityEngine;
using UnityEngine.Events;

public class TooltipsBackgroundi : MonoBehaviour
{
	public UIButton close;

	public UIButton use;

	public UnityAction CloseAction;

	public UnityAction UseAction;

	public TooltipItem tooltipItem;

	private void Start()
	{
	}

	public void openTooltips()
	{
		CloseAction = null;
		UseAction = null;
		if ((Object)(object)use != (Object)null)
		{
			((Component)use).gameObject.SetActive(true);
		}
	}

	public void closeTooltips()
	{
		if (CloseAction != null)
		{
			CloseAction.Invoke();
		}
	}

	public void UseItem()
	{
		if (UseAction != null)
		{
			UseAction.Invoke();
		}
		closeTooltips();
	}

	public void SetBtnText(string text)
	{
		((Component)use).GetComponentInChildren<UILabel>().text = text;
		tooltipItem.SetBtnSprite(text);
	}
}
