using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000289 RID: 649
public class UIBiGuanPanel : MonoBehaviour, IESCClose
{
	// Token: 0x06001773 RID: 6003 RVA: 0x000A0EC0 File Offset: 0x0009F0C0
	private void Awake()
	{
		UIBiGuanPanel.Inst = this;
	}

	// Token: 0x06001774 RID: 6004 RVA: 0x000A0EC8 File Offset: 0x0009F0C8
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

	// Token: 0x06001775 RID: 6005 RVA: 0x000A0EF9 File Offset: 0x0009F0F9
	public void ShowPanel()
	{
		this.PanelObj.SetActive(true);
		ESCCloseManager.Inst.RegisterClose(this);
	}

	// Token: 0x06001776 RID: 6006 RVA: 0x000A0F14 File Offset: 0x0009F114
	public void HidePanel()
	{
		this.XiuLian.OnPanelHide();
		this.LingWu.OnPanelHide();
		this.TuPo.OnPanelHide();
		this.GanWu.OnPanelHide();
		this.PanelObj.SetActive(false);
		ESCCloseManager.Inst.UnRegisterClose(this);
	}

	// Token: 0x06001777 RID: 6007 RVA: 0x000A0F64 File Offset: 0x0009F164
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

	// Token: 0x06001778 RID: 6008 RVA: 0x000A101F File Offset: 0x0009F21F
	public bool TryEscClose()
	{
		this.HidePanel();
		return true;
	}

	// Token: 0x0400122D RID: 4653
	public static UIBiGuanPanel Inst;

	// Token: 0x0400122E RID: 4654
	public GameObject PanelObj;

	// Token: 0x0400122F RID: 4655
	public UIBiGuanXiuLianPanel XiuLian;

	// Token: 0x04001230 RID: 4656
	public UIBiGuanLingWuPanel LingWu;

	// Token: 0x04001231 RID: 4657
	public UIBiGuanTuPoPanel TuPo;

	// Token: 0x04001232 RID: 4658
	public UIBiGuanGanWuPanel GanWu;

	// Token: 0x04001233 RID: 4659
	public Text KeFangTimeText;

	// Token: 0x04001234 RID: 4660
	[HideInInspector]
	public int BiGuanType = 1;
}
