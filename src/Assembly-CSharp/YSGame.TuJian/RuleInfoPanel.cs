using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WXB;

namespace YSGame.TuJian;

public class RuleInfoPanel : InfoPanelBase
{
	public GameObject DoubleSVItem;

	private GameObject _NormalSV;

	private GameObject _DoubleSV;

	private RectTransform _HyContentTransform;

	private RectTransform _DoubleContentTransform;

	private Scrollbar _HyScrollbar;

	private Scrollbar _DoubleScrollbar;

	private SymbolText _HyText;

	public Color HyTextColor = new Color(0.003921569f, 0.4745098f, 37f / 85f);

	public Color HyTextHoverColor = new Color(0.015686275f, 33f / 85f, 0.35686275f);

	private List<CiZhuiSVItem> DoubleSVItemList = new List<CiZhuiSVItem>();

	private List<CiZhuiSVItem> HideDoubleSVItemList = new List<CiZhuiSVItem>();

	private bool needSetPos;

	private int setPosCount;

	private int posID;

	public void Start()
	{
		Init();
	}

	public override void Update()
	{
		base.Update();
		RefreshSVHeight();
	}

	public void Init()
	{
		_NormalSV = ((Component)((Component)this).transform.Find("HyTextSV")).gameObject;
		_DoubleSV = ((Component)((Component)this).transform.Find("DoubleHyTextSV")).gameObject;
		ref RectTransform hyContentTransform = ref _HyContentTransform;
		Transform obj = ((Component)this).transform.Find("HyTextSV/Viewport/Content");
		hyContentTransform = (RectTransform)(object)((obj is RectTransform) ? obj : null);
		ref RectTransform doubleContentTransform = ref _DoubleContentTransform;
		Transform obj2 = ((Component)this).transform.Find("DoubleHyTextSV/Viewport/Content");
		doubleContentTransform = (RectTransform)(object)((obj2 is RectTransform) ? obj2 : null);
		_HyText = ((Component)((Component)this).transform.Find("HyTextSV/Viewport/Content/Text")).GetComponent<SymbolText>();
		_HyScrollbar = ((Component)((Component)this).transform.Find("HyTextSV/Scrollbar Vertical")).GetComponent<Scrollbar>();
		_DoubleScrollbar = ((Component)((Component)this).transform.Find("DoubleHyTextSV/Scrollbar Vertical")).GetComponent<Scrollbar>();
	}

	public void RefreshSVHeight()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)_HyContentTransform != (Object)null && _HyContentTransform.sizeDelta.y != ((Text)_HyText).preferredHeight + 34f)
		{
			_HyContentTransform.sizeDelta = new Vector2(_HyContentTransform.sizeDelta.x, ((Text)_HyText).preferredHeight + 34f);
		}
		if (!((Object)(object)_DoubleContentTransform != (Object)null))
		{
			return;
		}
		float num = 34f;
		foreach (CiZhuiSVItem doubleSVItem in DoubleSVItemList)
		{
			num += doubleSVItem.Height;
		}
		if (_DoubleContentTransform.sizeDelta.y == num)
		{
			return;
		}
		_DoubleContentTransform.sizeDelta = new Vector2(_DoubleContentTransform.sizeDelta.x, num);
		if (!needSetPos)
		{
			return;
		}
		if (setPosCount > 2)
		{
			needSetPos = false;
			setPosCount = 0;
			return;
		}
		float num2 = 0f;
		int num3 = DoubleSVItemList.Count - 1;
		while (num3 >= 0 && DoubleSVItemList[num3].ID != posID)
		{
			num2 += DoubleSVItemList[num3].Height;
			num3--;
		}
		_DoubleContentTransform.anchoredPosition = new Vector2(_DoubleContentTransform.anchoredPosition.x, num2 - 34f);
		setPosCount++;
	}

	public void EnableNormalPanel()
	{
		_DoubleSV.SetActive(false);
		_NormalSV.SetActive(true);
	}

	public void EnableDoublePanel()
	{
		_NormalSV.SetActive(false);
		_DoubleSV.SetActive(true);
	}

	public override void OnHyperlink(int[] args)
	{
		base.OnHyperlink(args);
		if (args[1] / 100 == 1)
		{
			needSetPos = true;
			if (args[1] == 101)
			{
				posID = args[3];
			}
			else
			{
				posID = args[2];
			}
		}
	}

	public string FindSearch()
	{
		string result = "";
		foreach (KeyValuePair<int, List<int>> ruleCiZhuiIndexDatum in TuJianDB.RuleCiZhuiIndexData)
		{
			int key = ruleCiZhuiIndexDatum.Key;
			foreach (int item in ruleCiZhuiIndexDatum.Value)
			{
				DoubleItem doubleItem = TuJianDB.RuleDoubleData[item];
				if (TuJianManager.Inst.Searcher.IsContansSearch(doubleItem.Name))
				{
					result = $"2_101_{key}_{item}";
					return result;
				}
			}
		}
		foreach (KeyValuePair<int, List<int>> ruleDoubleIndexDatum in TuJianDB.RuleDoubleIndexData)
		{
			int key2 = ruleDoubleIndexDatum.Key;
			foreach (int item2 in ruleDoubleIndexDatum.Value)
			{
				DoubleItem doubleItem2 = TuJianDB.RuleDoubleData[item2];
				if (TuJianManager.Inst.Searcher.IsContansSearch(doubleItem2.Name))
				{
					result = $"2_{key2}_{item2}";
					return result;
				}
			}
		}
		return result;
	}

	public override void RefreshPanelData()
	{
		base.RefreshPanelData();
		int nowSelectID = TuJianRuleTab.Inst.TypeSSV.NowSelectID;
		if (TuJianDB.RuleTuJianTypeDoubleSVData[nowSelectID])
		{
			EnableDoublePanel();
			List<int> list;
			if (nowSelectID == 101)
			{
				int nowSelectID2 = TuJianRuleTab.Inst.FilterSSV.NowSelectID;
				list = TuJianDB.RuleCiZhuiIndexData[nowSelectID2];
				if (needSetPos)
				{
					TuJianManager.Inst.NowPageHyperlink = $"2_{nowSelectID}_{nowSelectID2}_{posID}";
				}
				else
				{
					TuJianManager.Inst.NowPageHyperlink = $"2_{nowSelectID}_{nowSelectID2}_{list[0]}";
				}
			}
			else
			{
				list = TuJianDB.RuleDoubleIndexData[nowSelectID];
				if (needSetPos)
				{
					TuJianManager.Inst.NowPageHyperlink = $"2_{nowSelectID}_{posID}";
				}
				else
				{
					TuJianManager.Inst.NowPageHyperlink = $"2_{nowSelectID}_{list[0]}";
				}
			}
			HideAllDoubleItem();
			for (int num = list.Count - 1; num >= 0; num--)
			{
				CiZhuiSVItem doubleItem = GetDoubleItem();
				DoubleItem doubleItem2 = TuJianDB.RuleDoubleData[list[num]];
				doubleItem.SetCiZhui(list[num], doubleItem2.Name, doubleItem2.Desc);
				((Component)doubleItem).transform.SetSiblingIndex(0);
			}
			_DoubleScrollbar.value = 1f;
			return;
		}
		EnableNormalPanel();
		string text;
		if (TuJianDB.RuleTuJianTypeHasChildData[nowSelectID])
		{
			int num2 = TuJianRuleTab.Inst.FilterSSV.NowSelectID;
			if (num2 == -1)
			{
				num2 = 1;
			}
			TuJianManager.Inst.NowPageHyperlink = $"2_{nowSelectID}_{num2}";
			text = TuJianDB.RuleTuJianTypeChildDescData[nowSelectID][num2];
		}
		else
		{
			TuJianManager.Inst.NowPageHyperlink = $"2_{nowSelectID}_0";
			text = TuJianDB.RuleTuJianTypeDescData[nowSelectID];
		}
		text = text.Replace("<Title>", "\n\n#w2#s40");
		text = text.Replace("</Title>", "#n#s28#w1\n");
		text = text.Replace("<Image>", "#W<sprite n=");
		text = text.Replace("</Image>", ">#n");
		((Text)_HyText).text = text;
		_HyScrollbar.value = 1f;
	}

	public void HideAllDoubleItem()
	{
		foreach (CiZhuiSVItem doubleSVItem in DoubleSVItemList)
		{
			((Component)doubleSVItem).gameObject.SetActive(false);
			HideDoubleSVItemList.Add(doubleSVItem);
		}
		DoubleSVItemList.Clear();
	}

	public CiZhuiSVItem GetDoubleItem()
	{
		CiZhuiSVItem ciZhuiSVItem;
		if (HideDoubleSVItemList.Count > 0)
		{
			ciZhuiSVItem = HideDoubleSVItemList[0];
			HideDoubleSVItemList.RemoveAt(0);
			((Component)ciZhuiSVItem).gameObject.SetActive(true);
		}
		else
		{
			ciZhuiSVItem = Object.Instantiate<GameObject>(DoubleSVItem, (Transform)(object)_DoubleContentTransform).GetComponent<CiZhuiSVItem>();
		}
		DoubleSVItemList.Add(ciZhuiSVItem);
		return ciZhuiSVItem;
	}
}
