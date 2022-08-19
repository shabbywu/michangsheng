using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200050B RID: 1291
public class TooltipsBackgroundi : MonoBehaviour
{
	// Token: 0x06002984 RID: 10628 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x06002985 RID: 10629 RVA: 0x0013D5F2 File Offset: 0x0013B7F2
	public void openTooltips()
	{
		this.CloseAction = null;
		this.UseAction = null;
		if (this.use != null)
		{
			this.use.gameObject.SetActive(true);
		}
	}

	// Token: 0x06002986 RID: 10630 RVA: 0x0013D621 File Offset: 0x0013B821
	public void closeTooltips()
	{
		if (this.CloseAction != null)
		{
			this.CloseAction.Invoke();
		}
	}

	// Token: 0x06002987 RID: 10631 RVA: 0x0013D636 File Offset: 0x0013B836
	public void UseItem()
	{
		if (this.UseAction != null)
		{
			this.UseAction.Invoke();
		}
		this.closeTooltips();
	}

	// Token: 0x06002988 RID: 10632 RVA: 0x0013D651 File Offset: 0x0013B851
	public void SetBtnText(string text)
	{
		this.use.GetComponentInChildren<UILabel>().text = text;
		this.tooltipItem.SetBtnSprite(text, "");
	}

	// Token: 0x040025E1 RID: 9697
	public UIButton close;

	// Token: 0x040025E2 RID: 9698
	public UIButton use;

	// Token: 0x040025E3 RID: 9699
	public UnityAction CloseAction;

	// Token: 0x040025E4 RID: 9700
	public UnityAction UseAction;

	// Token: 0x040025E5 RID: 9701
	public TooltipItem tooltipItem;
}
