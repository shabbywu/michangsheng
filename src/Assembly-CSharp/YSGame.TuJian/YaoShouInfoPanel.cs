using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using WXB;

namespace YSGame.TuJian;

public class YaoShouInfoPanel : InfoPanelBase
{
	private Image _YaoShouImage;

	protected RectTransform _HyContentTransform;

	protected SymbolText _HyText;

	public Color HyTextColor1 = new Color(0.12156863f, 19f / 51f, 28f / 51f);

	public Color HyTextHoverColor1 = new Color(9f / 85f, 16f / 51f, 0.4627451f);

	public Color HyTextColor2 = new Color(0.4627451f, 4f / 15f, 0.02745098f);

	public Color HyTextHoverColor2 = new Color(32f / 85f, 0.21960784f, 0.02745098f);

	private static Dictionary<string, int> LevelDropdownDict = new Dictionary<string, int>
	{
		{ "炼气期", 1 },
		{ "筑基期", 2 },
		{ "金丹期", 3 },
		{ "元婴期", 4 },
		{ "化神期", 5 }
	};

	public void Start()
	{
		Init();
	}

	public override void Update()
	{
		base.Update();
		RefreshSVHeight();
	}

	public override void RefreshDataList()
	{
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		base.RefreshDataList();
		TuJianItemTab.Inst.SetDropdown(3, 0);
		if (TuJianManager.Inst.NeedRefreshDataList)
		{
			if (TuJianItemTab.Inst.PinJieDropdown.value == 0 && TuJianManager.Inst.Searcher.SearchCount == 0)
			{
				TuJianItemTab.Inst.FilterSSV.DataList = TuJianDB.ItemTuJianFilterData[5];
			}
			else
			{
				DataList.Clear();
				foreach (Dictionary<int, string> item in TuJianDB.ItemTuJianFilterData[5])
				{
					int key = item.First().Key;
					string value = item.First().Value;
					bool flag = true;
					if (TuJianItemTab.Inst.PinJieDropdown.value > 0 && LevelDropdownDict[TuJianDB.YaoShouLevelNameData[key]] != TuJianItemTab.Inst.PinJieDropdown.value)
					{
						flag = false;
					}
					if (TuJianManager.Inst.Searcher.SearchCount > 0 && !TuJianManager.Inst.Searcher.IsContansSearch(value) && !TuJianManager.Inst.Searcher.IsContansSearch(TuJianDB.YaoShouDescData[key]))
					{
						flag = false;
					}
					if (flag)
					{
						DataList.Add(new Dictionary<int, string> { { key, value } });
					}
				}
				TuJianItemTab.Inst.FilterSSV.DataList = DataList;
			}
			if (TuJianItemTab.Inst.FilterSSV.DataList.Count == 0)
			{
				((Graphic)_YaoShouImage).color = new Color(0f, 0f, 0f, 0f);
			}
			else
			{
				((Graphic)_YaoShouImage).color = Color.white;
			}
			TuJianManager.Inst.NeedRefreshDataList = false;
		}
		if (isOnHyperlink)
		{
			TuJianItemTab.Inst.FilterSSV.NowSelectID = hylinkArgs[2];
			TuJianItemTab.Inst.FilterSSV.NeedResetToTop = false;
			isOnHyperlink = false;
		}
	}

	public override void RefreshPanelData()
	{
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		base.RefreshPanelData();
		RefreshDataList();
		int nowSelectID = TuJianItemTab.Inst.FilterSSV.NowSelectID;
		if (nowSelectID < 1)
		{
			((Text)_HyText).text = "";
			return;
		}
		TuJianManager.Inst.NowPageHyperlink = $"1_5_{nowSelectID}";
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("#c449491名称：#n" + TuJianDB.YaoShouNameData[nowSelectID] + " <pos v=0.62 t=1>#c449491境界：#n" + TuJianDB.YaoShouLevelNameData[nowSelectID]);
		stringBuilder.Append("\n\n#c449491产出：#n");
		for (int i = 0; i < TuJianDB.YaoShouChanChuData[nowSelectID].Count; i++)
		{
			int num = TuJianDB.YaoShouChanChuData[nowSelectID][i];
			JSONObject jSONObject = num.ItemJson();
			string text = jSONObject["name"].str.ToCN();
			stringBuilder.Append("<hy t=" + text + " l=" + string.Format("1_{0}_{1}", jSONObject["TuJianType"].I, num) + " fc=#" + ColorUtility.ToHtmlStringRGB(HyTextColor1) + " fhc=#" + ColorUtility.ToHtmlStringRGB(HyTextHoverColor1) + " ul=1>");
			if (i != TuJianDB.YaoShouChanChuData[nowSelectID].Count - 1)
			{
				stringBuilder.Append("，");
			}
		}
		stringBuilder.Append("\n\n#c449491栖息：#n");
		string mapID = TuJianDB.YaoShouQiXiMapData[nowSelectID];
		stringBuilder.Append("#c" + ColorUtility.ToHtmlStringRGB(HyTextColor2) + TuJianDB.GetMapNameByID(mapID) + "#n");
		stringBuilder.Append("\n\n#c449491介绍：#n#s24");
		stringBuilder.Append(TuJianDB.YaoShouDescData[nowSelectID] ?? "");
		((Text)_HyText).text = stringBuilder.ToString();
		_YaoShouImage.sprite = TuJianDB.GetYaoShouFace(nowSelectID);
	}

	public void Init()
	{
		_YaoShouImage = ((Component)((Component)this).transform.Find("YaoShouMask/YaoShouImage")).GetComponent<Image>();
		ref RectTransform hyContentTransform = ref _HyContentTransform;
		Transform obj = ((Component)this).transform.Find("HyTextSV/Viewport/Content");
		hyContentTransform = (RectTransform)(object)((obj is RectTransform) ? obj : null);
		_HyText = ((Component)((Component)this).transform.Find("HyTextSV/Viewport/Content/Text")).GetComponent<SymbolText>();
	}

	public void RefreshSVHeight()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)_HyContentTransform != (Object)null && _HyContentTransform.sizeDelta.y != ((Text)_HyText).preferredHeight + 34f)
		{
			_HyContentTransform.sizeDelta = new Vector2(_HyContentTransform.sizeDelta.x, ((Text)_HyText).preferredHeight + 34f);
		}
	}
}
