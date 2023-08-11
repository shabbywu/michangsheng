using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Tab;

public class TabFangAnPanel : UIBase
{
	public Text Name;

	private GameObject XuanZePanel;

	private GameObject CurFangAnPanel;

	private List<TabFangAn> _fangAns = new List<TabFangAn>();

	public TabFangAnPanel(GameObject gameObject)
	{
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Expected O, but got Unknown
		_go = gameObject;
		XuanZePanel = Get("选择方案");
		CurFangAnPanel = Get("当前方案");
		Name = Get<Text>("当前方案/Text");
		Get<FpBtn>("当前方案").mouseUpEvent.AddListener(new UnityAction(ClickEvent));
		Init();
	}

	private void Init()
	{
		for (int i = 0; i < XuanZePanel.transform.childCount; i++)
		{
			_fangAns.Add(new TabFangAn(((Component)XuanZePanel.transform.GetChild(i)).gameObject, i + 1));
		}
	}

	public void Show()
	{
		_go.SetActive(true);
		CurFangAnPanel.SetActive(true);
		UpdateCurFanAn();
	}

	public void UpdateCurFanAn()
	{
		switch (SingletonMono<TabUIMag>.Instance.TabBag.GetCurBagType())
		{
		case BagType.背包:
			Name.SetText("方案" + Tools.instance.getPlayer().StreamData.FangAnData.CurEquipIndex.ToCNNumber());
			break;
		case BagType.技能:
			Name.SetText("方案" + (Tools.instance.getPlayer().nowConfigEquipSkill + 1).ToCNNumber());
			break;
		case BagType.功法:
			Name.SetText("方案" + (Tools.instance.getPlayer().nowConfigEquipStaticSkill + 1).ToCNNumber());
			break;
		}
		XuanZePanel.SetActive(false);
		CurFangAnPanel.SetActive(true);
	}

	private void ClickEvent()
	{
		XuanZePanel.SetActive(true);
	}

	public void Close()
	{
		_go.SetActive(false);
		XuanZePanel.SetActive(false);
		CurFangAnPanel.SetActive(false);
	}
}
