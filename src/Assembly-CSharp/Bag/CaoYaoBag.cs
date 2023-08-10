using System.Collections.Generic;
using Bag.Filter;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Bag;

public class CaoYaoBag : BaseBag2, IESCClose
{
	private bool _init;

	[SerializeField]
	private GameObject TempWeiZhiFilter;

	[SerializeField]
	private GameObject TempYaoXinFilter;

	[SerializeField]
	private Text CurQuality;

	[SerializeField]
	private GameObject QualityPanel;

	public List<LianDanFilter> FilterList = new List<LianDanFilter>();

	public List<LianDanFilter> WeiZhiFilterList = new List<LianDanFilter>();

	public List<LianDanFilter> YaoXinFilterList = new List<LianDanFilter>();

	public List<FpBtn> QualityFilterList;

	public Dictionary<int, string> AllYaoXinDict = new Dictionary<int, string>();

	public Dictionary<int, string> ZhuYaoDict = new Dictionary<int, string>();

	public Dictionary<int, string> FuYaoDict = new Dictionary<int, string>();

	public Dictionary<int, string> YaoYinDict = new Dictionary<int, string>();

	public Dictionary<int, string> QualityDict = new Dictionary<int, string>
	{
		{ 0, "全部" },
		{ 1, "一品" },
		{ 2, "二品" },
		{ 3, "三品" },
		{ 4, "四品" },
		{ 5, "五品" },
		{ 6, "六品" }
	};

	public int WeiZhi = 1;

	public int YaoXin;

	public LianDanSlot ToSlot;

	public void Open()
	{
		if (!_init)
		{
			Init(0, isPlayer: true);
		}
		ESCCloseManager.Inst.RegisterClose(this);
		((Component)this).gameObject.SetActive(true);
		UpdateItem(flag: true);
	}

	public override void Init(int npcId, bool isPlayer = false)
	{
		_player = Tools.instance.getPlayer();
		NpcId = npcId;
		IsPlayer = isPlayer;
		ZhuYaoDict.Add(0, "全部");
		FuYaoDict.Add(0, "全部");
		YaoYinDict.Add(0, "全部");
		AllYaoXinDict.Add(0, "全部");
		ZhuYaoDict.Add(-1, "未知");
		FuYaoDict.Add(-1, "未知");
		YaoYinDict.Add(-1, "未知");
		AllYaoXinDict.Add(-1, "未知");
		MLoopListView.InitListView(GetCount(MItemTotalCount), base.OnGetItemByIndex);
		foreach (LianDanItemLeiXin data in LianDanItemLeiXin.DataList)
		{
			if (data.desc.Contains("主药"))
			{
				ZhuYaoDict.Add(data.id, data.name);
			}
			else if (data.desc.Contains("辅药"))
			{
				FuYaoDict.Add(data.id, data.name);
			}
			else
			{
				YaoYinDict.Add(data.id, data.name);
			}
			AllYaoXinDict.Add(data.id, data.name);
		}
		CreateTempList();
		CreateQualityFilter();
		CreateWeiZhiFilter();
		WeiZhiFilterList[0].Click();
		ItemType = ItemType.草药;
		_init = true;
	}

	private void CreateWeiZhiFilter()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		WeiZhi = 1;
		Dictionary<int, string> weiZhiData = GetWeiZhiData();
		float x = TempWeiZhiFilter.transform.localPosition.x;
		float num = TempWeiZhiFilter.transform.localPosition.y;
		foreach (int key in weiZhiData.Keys)
		{
			LianDanFilter component = TempWeiZhiFilter.Inst(TempWeiZhiFilter.transform.parent).GetComponent<LianDanFilter>();
			component.Init(key, weiZhiData[key], SelectWeiZhi, x, num);
			WeiZhiFilterList.Add(component);
			num -= 43f;
		}
	}

	private void CreateYaoXinFilter()
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		Tools.ClearObj(TempYaoXinFilter.transform);
		YaoXinFilterList = new List<LianDanFilter>();
		YaoXin = 0;
		Dictionary<int, string> yaoXinData = GetYaoXinData();
		float x = TempYaoXinFilter.transform.localPosition.x;
		float num = TempYaoXinFilter.transform.localPosition.y;
		foreach (int key in yaoXinData.Keys)
		{
			LianDanFilter component = TempYaoXinFilter.Inst(TempYaoXinFilter.transform.parent).GetComponent<LianDanFilter>();
			component.Init(key, yaoXinData[key], SelectYaoXin, x, num);
			YaoXinFilterList.Add(component);
			num -= 51f;
		}
		((Component)TempYaoXinFilter.transform.parent).GetComponent<RectTransform>().sizeDelta = new Vector2(156f, 0f - num);
		YaoXinFilterList[0].Click();
	}

	private void CreateQualityFilter()
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Expected O, but got Unknown
		ItemQuality = ItemQuality.全部;
		foreach (FpBtn btn in QualityFilterList)
		{
			btn.mouseUpEvent.AddListener((UnityAction)delegate
			{
				SelectQuality(int.Parse(((Object)((Component)btn).gameObject).name));
			});
		}
	}

	public void ShowQualityFilter()
	{
		QualityPanel.SetActive(true);
	}

	public void CloseQualityFilter()
	{
		QualityPanel.SetActive(false);
	}

	public void SelectWeiZhi(int value)
	{
		WeiZhi = value;
		foreach (LianDanFilter weiZhiFilter in WeiZhiFilterList)
		{
			if (weiZhiFilter.Value == WeiZhi)
			{
				weiZhiFilter.SetState(selected: true);
			}
			else
			{
				weiZhiFilter.SetState(selected: false);
			}
		}
		CreateYaoXinFilter();
	}

	private void SelectWeiZhi(LianDanFilter filter)
	{
		foreach (LianDanFilter weiZhiFilter in WeiZhiFilterList)
		{
			weiZhiFilter.SetState(selected: false);
		}
		filter.SetState(selected: true);
		WeiZhi = filter.Value;
		CreateYaoXinFilter();
	}

	private void SelectYaoXin(LianDanFilter filter)
	{
		foreach (LianDanFilter yaoXinFilter in YaoXinFilterList)
		{
			yaoXinFilter.SetState(selected: false);
		}
		filter.SetState(selected: true);
		YaoXin = filter.Value;
		UpdateItem(flag: true);
	}

	private void SelectQuality(int value)
	{
		ItemQuality = (ItemQuality)value;
		CurQuality.SetText(ItemQuality.ToString());
		UpdateItem(flag: true);
		CloseQualityFilter();
	}

	private Dictionary<int, string> GetWeiZhiData()
	{
		return new Dictionary<int, string>
		{
			{ 1, "主药" },
			{ 2, "辅药" },
			{ 3, "药引" }
		};
	}

	private Dictionary<int, string> GetYaoXinData()
	{
		if (WeiZhi == 1)
		{
			return ZhuYaoDict;
		}
		if (WeiZhi == 2)
		{
			return FuYaoDict;
		}
		return YaoYinDict;
	}

	protected override bool FiddlerItem(BaseItem baseItem)
	{
		if (ItemQuality != 0 && baseItem.GetImgQuality() != (int)ItemQuality)
		{
			return false;
		}
		if (baseItem.ItemType != ItemType.草药)
		{
			return false;
		}
		CaoYaoItem caoYaoItem = (CaoYaoItem)baseItem;
		if (YaoXin != 0)
		{
			if (WeiZhi == 1 && YaoXin != caoYaoItem.GetZhuYaoId())
			{
				return false;
			}
			if (WeiZhi == 2 && YaoXin != caoYaoItem.GetFuYaoId())
			{
				return false;
			}
			if (WeiZhi == 3 && YaoXin != caoYaoItem.GetYaoYinId())
			{
				return false;
			}
		}
		return true;
	}

	public void Close()
	{
		ESCCloseManager.Inst.UnRegisterClose(this);
		((Component)this).gameObject.SetActive(false);
	}

	public bool TryEscClose()
	{
		Close();
		return true;
	}

	public BaseItem GetTempItemById(int id)
	{
		if (TempBagList.Count < 1)
		{
			Init(0, isPlayer: true);
		}
		foreach (ITEM_INFO tempBag in TempBagList)
		{
			if (tempBag.itemId == id)
			{
				return BaseItem.Create(id, (int)tempBag.itemCount, tempBag.uuid, tempBag.Seid);
			}
		}
		Debug.LogError((object)"错误,背包不存在此物品");
		return null;
	}
}
