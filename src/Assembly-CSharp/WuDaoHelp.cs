using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x0200050D RID: 1293
public class WuDaoHelp : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x06002990 RID: 10640 RVA: 0x0013DAD0 File Offset: 0x0013BCD0
	private void Start()
	{
		if (this.HelpToolTips == null)
		{
			this.HelpToolTips = GameObject.Find("TooltipTianFu").GetComponent<TooltipScale>();
		}
	}

	// Token: 0x06002991 RID: 10641 RVA: 0x0013DAF8 File Offset: 0x0013BCF8
	protected virtual void OnMouseEnter()
	{
		if (this.type == 1)
		{
			this.text = this.text.Replace("感兴趣的物品：", "[d3b068]感兴趣的物品[-]");
			this.text = this.text.Replace("[007b06]", "[d3b068]");
		}
		this.HelpToolTips.uILabel.text = this.text;
		this.HelpToolTips.showTooltip = true;
	}

	// Token: 0x06002992 RID: 10642 RVA: 0x0013DB66 File Offset: 0x0013BD66
	protected virtual void OnMouseExit()
	{
		this.HelpToolTips.showTooltip = false;
	}

	// Token: 0x06002993 RID: 10643 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x06002994 RID: 10644 RVA: 0x0013DB74 File Offset: 0x0013BD74
	private void OnHover(bool isOver)
	{
		if (isOver)
		{
			this.OnMouseEnter();
			return;
		}
		this.OnMouseExit();
	}

	// Token: 0x06002995 RID: 10645 RVA: 0x0013DB86 File Offset: 0x0013BD86
	public void OnPointerEnter(PointerEventData eventData)
	{
		this.OnMouseEnter();
	}

	// Token: 0x06002996 RID: 10646 RVA: 0x0013DB8E File Offset: 0x0013BD8E
	public void OnPointerExit(PointerEventData eventData)
	{
		this.OnMouseExit();
	}

	// Token: 0x040025F0 RID: 9712
	public TooltipScale HelpToolTips;

	// Token: 0x040025F1 RID: 9713
	public string text = "";

	// Token: 0x040025F2 RID: 9714
	public int type;
}
