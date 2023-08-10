using UnityEngine;
using UnityEngine.EventSystems;

public class WuDaoHelp : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	public TooltipScale HelpToolTips;

	public string text = "";

	public int type;

	private void Start()
	{
		if ((Object)(object)HelpToolTips == (Object)null)
		{
			HelpToolTips = GameObject.Find("TooltipTianFu").GetComponent<TooltipScale>();
		}
	}

	protected virtual void OnMouseEnter()
	{
		if (type == 1)
		{
			text = text.Replace("感兴趣的物品：", "[d3b068]感兴趣的物品[-]");
			text = text.Replace("[007b06]", "[d3b068]");
		}
		HelpToolTips.uILabel.text = text;
		HelpToolTips.showTooltip = true;
	}

	protected virtual void OnMouseExit()
	{
		HelpToolTips.showTooltip = false;
	}

	private void Update()
	{
	}

	private void OnHover(bool isOver)
	{
		if (isOver)
		{
			OnMouseEnter();
		}
		else
		{
			OnMouseExit();
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		OnMouseEnter();
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		OnMouseExit();
	}
}
