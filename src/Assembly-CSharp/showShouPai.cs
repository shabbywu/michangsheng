using System;
using KBEngine;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x0200062C RID: 1580
public class showShouPai : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x0600273E RID: 10046 RVA: 0x00133300 File Offset: 0x00131500
	public void OnPointerEnter(PointerEventData eventData)
	{
		Avatar avatar = this.target.avatar;
		this.tooltip.uILabel.text = string.Concat(new object[]
		{
			"[FF8300]手牌上限[-]：",
			avatar.NowCard,
			"\u3000\u3000\u3000[FF8300]当前手牌[-]：",
			avatar.crystal.getCardNum()
		});
		this.tooltip.showTooltip = true;
	}

	// Token: 0x0600273F RID: 10047 RVA: 0x0001F264 File Offset: 0x0001D464
	public void OnPointerExit(PointerEventData eventData)
	{
		this.tooltip.showTooltip = false;
	}

	// Token: 0x06002740 RID: 10048 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x06002741 RID: 10049 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x04002151 RID: 8529
	public TooltipScale tooltip;

	// Token: 0x04002152 RID: 8530
	public UI_Target target;
}
