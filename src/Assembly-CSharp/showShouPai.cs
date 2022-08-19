using System;
using KBEngine;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x0200046F RID: 1135
public class showShouPai : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x06002385 RID: 9093 RVA: 0x000F320C File Offset: 0x000F140C
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

	// Token: 0x06002386 RID: 9094 RVA: 0x000F327D File Offset: 0x000F147D
	public void OnPointerExit(PointerEventData eventData)
	{
		this.tooltip.showTooltip = false;
	}

	// Token: 0x06002387 RID: 9095 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x06002388 RID: 9096 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x04001C79 RID: 7289
	public TooltipScale tooltip;

	// Token: 0x04001C7A RID: 7290
	public UI_Target target;
}
