using System;
using UnityEngine;
using UnityEngine.UI;

public class UIBiGuanPanel : MonoBehaviour, IESCClose
{
	public static UIBiGuanPanel Inst;

	public GameObject PanelObj;

	public UIBiGuanXiuLianPanel XiuLian;

	public UIBiGuanLingWuPanel LingWu;

	public UIBiGuanTuPoPanel TuPo;

	public UIBiGuanGanWuPanel GanWu;

	public Text KeFangTimeText;

	[HideInInspector]
	public int BiGuanType = 1;

	private void Awake()
	{
		Inst = this;
	}

	public void OpenBiGuan(int type)
	{
		if (Tools.instance.getPlayer().getStaticID() != 0)
		{
			BiGuanType = type;
			ShowPanel();
		}
		else
		{
			UIPopTip.Inst.Pop("必须装备主修功法才能修炼！");
		}
	}

	public void ShowPanel()
	{
		PanelObj.SetActive(true);
		ESCCloseManager.Inst.RegisterClose(this);
	}

	public void HidePanel()
	{
		XiuLian.OnPanelHide();
		LingWu.OnPanelHide();
		TuPo.OnPanelHide();
		GanWu.OnPanelHide();
		PanelObj.SetActive(false);
		ESCCloseManager.Inst.UnRegisterClose(this);
	}

	public void RefreshKeFangTime()
	{
		string screenName = Tools.getScreenName();
		DateTime residueTime = Tools.instance.getPlayer().zulinContorl.getResidueTime(screenName);
		if (residueTime.Year > 4998)
		{
			((Component)KeFangTimeText).gameObject.SetActive(false);
			return;
		}
		((Component)KeFangTimeText).gameObject.SetActive(true);
		KeFangTimeText.text = residueTime.Year - 1 + "年" + (residueTime.Month - 1) + "月" + (residueTime.Day - 1) + "日";
	}

	public bool TryEscClose()
	{
		HidePanel();
		return true;
	}
}
