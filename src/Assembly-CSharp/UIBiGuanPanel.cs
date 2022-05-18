using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003B4 RID: 948
public class UIBiGuanPanel : MonoBehaviour, IESCClose
{
	// Token: 0x06001A4B RID: 6731 RVA: 0x000166F1 File Offset: 0x000148F1
	private void Awake()
	{
		UIBiGuanPanel.Inst = this;
	}

	// Token: 0x06001A4C RID: 6732 RVA: 0x000166F9 File Offset: 0x000148F9
	public void OpenBiGuan(int type)
	{
		if (Tools.instance.getPlayer().getStaticID() != 0)
		{
			this.BiGuanType = type;
			this.ShowPanel();
			return;
		}
		UIPopTip.Inst.Pop("必须装备主修功法才能修炼！", PopTipIconType.叹号);
	}

	// Token: 0x06001A4D RID: 6733 RVA: 0x0001672A File Offset: 0x0001492A
	public void ShowPanel()
	{
		this.PanelObj.SetActive(true);
		ESCCloseManager.Inst.RegisterClose(this);
	}

	// Token: 0x06001A4E RID: 6734 RVA: 0x000E8444 File Offset: 0x000E6644
	public void HidePanel()
	{
		this.XiuLian.OnPanelHide();
		this.LingWu.OnPanelHide();
		this.TuPo.OnPanelHide();
		this.GanWu.OnPanelHide();
		this.PanelObj.SetActive(false);
		ESCCloseManager.Inst.UnRegisterClose(this);
	}

	// Token: 0x06001A4F RID: 6735 RVA: 0x000E8494 File Offset: 0x000E6694
	public void RefreshKeFangTime()
	{
		string screenName = Tools.getScreenName();
		DateTime residueTime = Tools.instance.getPlayer().zulinContorl.getResidueTime(screenName);
		if (residueTime.Year > 4998)
		{
			this.KeFangTimeText.gameObject.SetActive(false);
			return;
		}
		this.KeFangTimeText.gameObject.SetActive(true);
		this.KeFangTimeText.text = string.Concat(new object[]
		{
			residueTime.Year - 1,
			"年",
			residueTime.Month - 1,
			"月",
			residueTime.Day - 1,
			"日"
		});
	}

	// Token: 0x06001A50 RID: 6736 RVA: 0x00016743 File Offset: 0x00014943
	public bool TryEscClose()
	{
		this.HidePanel();
		return true;
	}

	// Token: 0x040015AA RID: 5546
	public static UIBiGuanPanel Inst;

	// Token: 0x040015AB RID: 5547
	public GameObject PanelObj;

	// Token: 0x040015AC RID: 5548
	public UIBiGuanXiuLianPanel XiuLian;

	// Token: 0x040015AD RID: 5549
	public UIBiGuanLingWuPanel LingWu;

	// Token: 0x040015AE RID: 5550
	public UIBiGuanTuPoPanel TuPo;

	// Token: 0x040015AF RID: 5551
	public UIBiGuanGanWuPanel GanWu;

	// Token: 0x040015B0 RID: 5552
	public Text KeFangTimeText;

	// Token: 0x040015B1 RID: 5553
	[HideInInspector]
	public int BiGuanType = 1;
}
