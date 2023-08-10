using System;
using System.Collections.Generic;
using JSONClass;
using UnityEngine;
using UnityEngine.UI;

namespace Tab;

[Serializable]
public class TabWuDaoPanel : ITabPanelBase
{
	private bool _isInit;

	private List<TabWuDaoToggle> _wudaoSelectList;

	private TabWuDaoToggle _curSelectType;

	public WuDaoTooltip WuDaoTooltip;

	private Dictionary<string, Sprite> _wudaoImgDict;

	private TabWuDaoLevel _wuDaoLevel;

	private Text _curWuDaoDian;

	private Text _curSiXuNum;

	public Dictionary<int, GameObject> WudaoSkillListDict { get; private set; }

	public Dictionary<string, Sprite> WudaoBgImgDict { get; private set; }

	public TabWuDaoPanel(GameObject gameObject)
	{
		_go = gameObject;
		_isInit = false;
	}

	private void Init()
	{
		_wudaoImgDict = ResManager.inst.LoadSpriteAtlas("NewTab/WuDaoType");
		WudaoBgImgDict = ResManager.inst.LoadSpriteAtlas("NewTab/WuDaoBgImg");
		WudaoSkillListDict = new Dictionary<int, GameObject>();
		WuDaoTooltip = new WuDaoTooltip(Get("WuDaoSkillTooltip"));
		_curWuDaoDian = Get<Text>("Clouds/悟道点/wudaodian");
		_curSiXuNum = Get<Text>("Clouds/思绪/sixu");
		for (int i = 1; i <= 4; i++)
		{
			WudaoSkillListDict.Add(i, Get($"WuDaoSkill/{i}"));
		}
		GameObject obj = Get("WuDaoSelect/List/ViewPort/Content/WuDaoToggle");
		Transform transform = Get("WuDaoSelect/List/ViewPort/Content").transform;
		_wudaoSelectList = new List<TabWuDaoToggle>();
		foreach (WuDaoAllTypeJson data in WuDaoAllTypeJson.DataList)
		{
			_wudaoSelectList.Add(new TabWuDaoToggle(obj.Inst(transform), data.id, _wudaoImgDict[data.id.ToString()]));
		}
		SelectTypeCallBack(_wudaoSelectList[0]);
	}

	public void SelectTypeCallBack(TabWuDaoToggle toggle)
	{
		if (_curSelectType != null)
		{
			_curSelectType.SetIsActive(flag: false);
		}
		_curSelectType = toggle;
		_curSelectType.SetIsActive(flag: true);
		foreach (TabWuDaoToggle wudaoSelect in _wudaoSelectList)
		{
			wudaoSelect.UpdateUI();
		}
		if (_wuDaoLevel == null)
		{
			_wuDaoLevel = new TabWuDaoLevel(Get("WuDaoLevel"), _curSelectType.Id);
		}
		else
		{
			_wuDaoLevel.UpdateUI(_curSelectType.Id);
		}
	}

	public void UpdateWuDaoDian()
	{
		_curWuDaoDian.text = Tools.instance.getPlayer().wuDaoMag.GetNowWuDaoDian().ToString();
	}

	public void UpdateUI()
	{
		UpdateWuDaoDian();
		_curSiXuNum.text = Tools.instance.getPlayer().LingGuang.Count.ToString();
		if (_curSelectType != null)
		{
			_wuDaoLevel.UpdateUI(_curSelectType.Id);
		}
	}

	public override void Show()
	{
		if (!_isInit)
		{
			Init();
			_isInit = true;
		}
		UpdateUI();
		_go.SetActive(true);
	}
}
