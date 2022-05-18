using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200079C RID: 1948
public class TooltipsBackgroundi : MonoBehaviour
{
	// Token: 0x06003189 RID: 12681 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x0600318A RID: 12682 RVA: 0x0002440B File Offset: 0x0002260B
	public void openTooltips()
	{
		this.CloseAction = null;
		this.UseAction = null;
		if (this.use != null)
		{
			this.use.gameObject.SetActive(true);
		}
	}

	// Token: 0x0600318B RID: 12683 RVA: 0x0002443A File Offset: 0x0002263A
	public void closeTooltips()
	{
		if (this.CloseAction != null)
		{
			this.CloseAction.Invoke();
		}
	}

	// Token: 0x0600318C RID: 12684 RVA: 0x0002444F File Offset: 0x0002264F
	public void UseItem()
	{
		if (this.UseAction != null)
		{
			this.UseAction.Invoke();
		}
		this.closeTooltips();
	}

	// Token: 0x0600318D RID: 12685 RVA: 0x0002446A File Offset: 0x0002266A
	public void SetBtnText(string text)
	{
		this.use.GetComponentInChildren<UILabel>().text = text;
		this.tooltipItem.SetBtnSprite(text, "");
	}

	// Token: 0x04002DC9 RID: 11721
	public UIButton close;

	// Token: 0x04002DCA RID: 11722
	public UIButton use;

	// Token: 0x04002DCB RID: 11723
	public UnityAction CloseAction;

	// Token: 0x04002DCC RID: 11724
	public UnityAction UseAction;

	// Token: 0x04002DCD RID: 11725
	public TooltipItem tooltipItem;
}
