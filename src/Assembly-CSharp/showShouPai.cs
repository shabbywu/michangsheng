using KBEngine;
using UnityEngine;
using UnityEngine.EventSystems;

public class showShouPai : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	public TooltipScale tooltip;

	public UI_Target target;

	public void OnPointerEnter(PointerEventData eventData)
	{
		Avatar avatar = target.avatar;
		tooltip.uILabel.text = "[FF8300]手牌上限[-]：" + avatar.NowCard + "\u3000\u3000\u3000[FF8300]当前手牌[-]：" + avatar.crystal.getCardNum();
		tooltip.showTooltip = true;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		tooltip.showTooltip = false;
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
