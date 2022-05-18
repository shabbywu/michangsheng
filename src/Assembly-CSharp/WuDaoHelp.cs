using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x0200079E RID: 1950
public class WuDaoHelp : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x06003195 RID: 12693 RVA: 0x000244E5 File Offset: 0x000226E5
	private void Start()
	{
		if (this.HelpToolTips == null)
		{
			this.HelpToolTips = GameObject.Find("TooltipTianFu").GetComponent<TooltipScale>();
		}
	}

	// Token: 0x06003196 RID: 12694 RVA: 0x0018AD14 File Offset: 0x00188F14
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

	// Token: 0x06003197 RID: 12695 RVA: 0x0002450A File Offset: 0x0002270A
	protected virtual void OnMouseExit()
	{
		this.HelpToolTips.showTooltip = false;
	}

	// Token: 0x06003198 RID: 12696 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x06003199 RID: 12697 RVA: 0x00024518 File Offset: 0x00022718
	private void OnHover(bool isOver)
	{
		if (isOver)
		{
			this.OnMouseEnter();
			return;
		}
		this.OnMouseExit();
	}

	// Token: 0x0600319A RID: 12698 RVA: 0x0002452A File Offset: 0x0002272A
	public void OnPointerEnter(PointerEventData eventData)
	{
		this.OnMouseEnter();
	}

	// Token: 0x0600319B RID: 12699 RVA: 0x00024532 File Offset: 0x00022732
	public void OnPointerExit(PointerEventData eventData)
	{
		this.OnMouseExit();
	}

	// Token: 0x04002DD8 RID: 11736
	public TooltipScale HelpToolTips;

	// Token: 0x04002DD9 RID: 11737
	public string text = "";

	// Token: 0x04002DDA RID: 11738
	public int type;
}
