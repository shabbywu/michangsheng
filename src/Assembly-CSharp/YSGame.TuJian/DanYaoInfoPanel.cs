using System.Collections.Generic;
using System.Linq;
using System.Text;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using WXB;

namespace YSGame.TuJian;

public class DanYaoInfoPanel : InfoPanelBase
{
	private Image _QualityImage;

	private Image _QualityUpImage;

	private Image _ItemIconImage;

	private SymbolText _HyText1;

	private SymbolText _HyText2;

	private SymbolText _HyText3;

	private Text _NumberText;

	private Button _LeftButton;

	private Button _RightButton;

	public Color HyTextColor = new Color(11f / 51f, 49f / 85f, 0.4117647f);

	public Color HyTextHoverColor = new Color(10f / 51f, 26f / 51f, 0.36862746f);

	private static readonly string[] typeStr = new string[3] { "药引", "主药", "辅药" };

	private List<DanFangData> playerDanFangList = new List<DanFangData>();

	private int danFangNumber;

	private void Start()
	{
		Init();
		SetDanFangNumber(1);
	}

	public void Init()
	{
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Expected O, but got Unknown
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Expected O, but got Unknown
		_QualityImage = ((Component)((Component)this).transform.Find("ItemBG/QualityBg")).GetComponent<Image>();
		_ItemIconImage = ((Component)((Component)this).transform.Find("ItemBG/QualityBg/ItemIcon")).GetComponent<Image>();
		_QualityUpImage = ((Component)((Component)this).transform.Find("ItemBG/QualityBg/ItemIcon/QualityUp")).GetComponent<Image>();
		_HyText1 = ((Component)((Component)this).transform.Find("InfoText1")).GetComponent<SymbolText>();
		_HyText2 = ((Component)((Component)this).transform.Find("InfoText2")).GetComponent<SymbolText>();
		_HyText3 = ((Component)((Component)this).transform.Find("DanFangInfoBG/InfoText")).GetComponent<SymbolText>();
		_NumberText = ((Component)((Component)this).transform.Find("DanFangInfoBG/NumberText")).GetComponent<Text>();
		_LeftButton = ((Component)((Component)this).transform.Find("DanFangInfoBG/LeftBtn")).GetComponent<Button>();
		_RightButton = ((Component)((Component)this).transform.Find("DanFangInfoBG/RightBtn")).GetComponent<Button>();
		((UnityEvent)_LeftButton.onClick).AddListener(new UnityAction(LastDanFang));
		((UnityEvent)_RightButton.onClick).AddListener(new UnityAction(NextDanFang));
	}

	public override void RefreshDataList()
	{
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		base.RefreshDataList();
		TuJianItemTab.Inst.SetDropdown(1, 0);
		if (TuJianManager.Inst.NeedRefreshDataList)
		{
			if (TuJianItemTab.Inst.PinJieDropdown.value == 0 && TuJianManager.Inst.Searcher.SearchCount == 0)
			{
				TuJianItemTab.Inst.FilterSSV.DataList = TuJianDB.ItemTuJianFilterData[4];
			}
			else
			{
				DataList.Clear();
				foreach (Dictionary<int, string> item in TuJianDB.ItemTuJianFilterData[4])
				{
					int key = item.First().Key;
					string value = item.First().Value;
					JSONObject jSONObject = key.ItemJson();
					bool flag = true;
					if (TuJianItemTab.Inst.PinJieDropdown.value > 0 && jSONObject["quality"].I != TuJianItemTab.Inst.PinJieDropdown.value)
					{
						flag = false;
					}
					if (TuJianManager.Inst.Searcher.SearchCount > 0 && !TuJianManager.Inst.Searcher.IsContansSearch(value) && !TuJianManager.Inst.Searcher.IsContansSearch(jSONObject["desc2"].Str))
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
				((Graphic)_ItemIconImage).color = new Color(0f, 0f, 0f, 0f);
				((Graphic)_QualityImage).color = new Color(0f, 0f, 0f, 0f);
				((Graphic)_QualityUpImage).color = new Color(0f, 0f, 0f, 0f);
				((Component)_NumberText).gameObject.SetActive(false);
				((Component)_LeftButton).gameObject.SetActive(false);
				((Component)_RightButton).gameObject.SetActive(false);
			}
			else
			{
				((Graphic)_ItemIconImage).color = Color.white;
				((Graphic)_QualityImage).color = Color.white;
				((Graphic)_QualityUpImage).color = Color.white;
				((Component)_NumberText).gameObject.SetActive(true);
				((Component)_LeftButton).gameObject.SetActive(true);
				((Component)_RightButton).gameObject.SetActive(true);
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
		base.RefreshPanelData();
		RefreshDataList();
		int nowSelectID = TuJianItemTab.Inst.FilterSSV.NowSelectID;
		if (nowSelectID < 1)
		{
			((Text)_HyText1).text = "";
			((Text)_HyText2).text = "";
			((Text)_HyText3).text = "";
			return;
		}
		TuJianManager.Inst.NowPageHyperlink = $"1_4_{nowSelectID}";
		JSONObject jSONObject = nowSelectID.ItemJson();
		bool flag = TuJianManager.Inst.IsUnlockedItem(nowSelectID) || TuJianManager.IsDebugMode;
		Avatar player = Tools.instance.getPlayer();
		bool flag2 = player != null;
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("#c449491名称：#n" + jSONObject["name"].Str + " <pos v=0.68 t=1>#c449491品级：#n" + jSONObject["quality"].I.ToCNNumber() + "品");
		stringBuilder.Append("\n\n#c449491类型：#n丹药");
		int i = jSONObject["CanUse"].I;
		int i2 = jSONObject["DanDu"].I;
		if (flag)
		{
			if (flag2)
			{
				stringBuilder.Append($"\n\n#c449491耐药：#n{$"{Tools.getJsonobject(player.NaiYaoXin, nowSelectID.ToString())}/{i}"}<pos v=0.68 t=1>#c449491丹毒：#n{i2}");
			}
			else
			{
				stringBuilder.Append($"\n\n#c449491耐药：#n{i}<pos v=0.68 t=1>#c449491丹毒：#n{i2}");
			}
		}
		else
		{
			stringBuilder.Append("\n\n#c449491耐药：#n未知<pos v=0.68 t=1>#c449491丹毒：#n未知");
		}
		stringBuilder.Append("\n\n#c449491药效：#n");
		if (flag)
		{
			stringBuilder.Append(jSONObject["desc"].Str ?? "");
		}
		else
		{
			stringBuilder.Append("未知");
		}
		((Text)_HyText1).text = stringBuilder.ToString();
		stringBuilder.Clear();
		if (flag2)
		{
			RefreshDanFangList(nowSelectID);
			if (playerDanFangList.Count > 0)
			{
				if (danFangNumber > playerDanFangList.Count)
				{
					danFangNumber = 1;
				}
				DanFangData danFangData = playerDanFangList[danFangNumber - 1];
				string value = "";
				if (danFangData.YaoCaiTypeCount == 4)
				{
					value = "#s13 \n";
				}
				else if (danFangData.YaoCaiTypeCount == 3)
				{
					value = "#s30 \n";
				}
				else if (danFangData.YaoCaiTypeCount == 2)
				{
					value = "\n#s13 \n";
				}
				stringBuilder.Append(value);
				stringBuilder.Append(YaoCaoStr(0, danFangData.YaoYinID, danFangData.YaoYinCount));
				stringBuilder.Append(YaoCaoStr(1, danFangData.ZhuYao1ID, danFangData.ZhuYao1Count));
				stringBuilder.Append(YaoCaoStr(1, danFangData.ZhuYao2ID, danFangData.ZhuYao2Count));
				stringBuilder.Append(YaoCaoStr(2, danFangData.FuYao1ID, danFangData.FuYao1Count));
				stringBuilder.Append(YaoCaoStr(2, danFangData.FuYao2ID, danFangData.FuYao2Count));
			}
			else
			{
				SetDanFangNumber(1);
				stringBuilder.Append("#s27<pos v=0.28 t=1>" + typeStr[0] + "：未知#n");
				stringBuilder.Append("\n#s27<pos v=0.28 t=1>" + typeStr[1] + "：未知#n");
				stringBuilder.Append("\n#s27<pos v=0.28 t=1>" + typeStr[1] + "：未知#n");
				stringBuilder.Append("\n#s27<pos v=0.28 t=1>" + typeStr[2] + "：未知#n");
				stringBuilder.Append("\n#s27<pos v=0.28 t=1>" + typeStr[2] + "：未知#n");
			}
		}
		else
		{
			DanFangData danFangData2 = TuJianDB.DanFangDataDict[nowSelectID];
			if (flag)
			{
				string value2 = "";
				if (danFangData2.YaoCaiTypeCount == 4)
				{
					value2 = "#s13 \n";
				}
				else if (danFangData2.YaoCaiTypeCount == 3)
				{
					value2 = "#s30 \n";
				}
				else if (danFangData2.YaoCaiTypeCount == 2)
				{
					value2 = "\n#s13 \n";
				}
				stringBuilder.Append(value2);
				stringBuilder.Append(YaoCaoStr(0, danFangData2.YaoYinID, danFangData2.YaoYinCount));
				stringBuilder.Append(YaoCaoStr(1, danFangData2.ZhuYao1ID, danFangData2.ZhuYao1Count));
				stringBuilder.Append(YaoCaoStr(1, danFangData2.ZhuYao2ID, danFangData2.ZhuYao2Count));
				stringBuilder.Append(YaoCaoStr(2, danFangData2.FuYao1ID, danFangData2.FuYao1Count));
				stringBuilder.Append(YaoCaoStr(2, danFangData2.FuYao2ID, danFangData2.FuYao2Count));
			}
			else
			{
				SetDanFangNumber(1);
				stringBuilder.Append("#s27<pos v=0.28 t=1>" + typeStr[0] + "：未知#n");
				stringBuilder.Append("\n#s27<pos v=0.28 t=1>" + typeStr[1] + "：未知#n");
				stringBuilder.Append("\n#s27<pos v=0.28 t=1>" + typeStr[1] + "：未知#n");
				stringBuilder.Append("\n#s27<pos v=0.28 t=1>" + typeStr[2] + "：未知#n");
				stringBuilder.Append("\n#s27<pos v=0.28 t=1>" + typeStr[2] + "：未知#n");
			}
		}
		((Text)_HyText3).text = stringBuilder.ToString();
		stringBuilder.Clear();
		stringBuilder.Append("#c449491介绍：#n#s24");
		if (flag)
		{
			stringBuilder.Append(jSONObject["desc2"].Str ?? "");
		}
		else
		{
			stringBuilder.Append("未知");
		}
		((Text)_HyText2).text = stringBuilder.ToString();
		SetItemIcon(nowSelectID);
	}

	private string YaoCaoStr(int strIndex, int id, int count)
	{
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		if (count > 0)
		{
			JSONObject jSONObject = id.ItemJson();
			string text = TuJianDB.YaoCaoTypeData[jSONObject[$"yaoZhi{strIndex + 1}"].I];
			return "#s27<pos v=0.28 t=1>" + typeStr[strIndex] + "(" + text + ")：<hy t=" + string.Format("{0}x{1}", jSONObject["name"].Str, count) + " l=" + $"1_1_{id}" + " fc=#" + ColorUtility.ToHtmlStringRGB(HyTextColor) + " fhc=#" + ColorUtility.ToHtmlStringRGB(HyTextHoverColor) + " ul=1>#n\n";
		}
		return "";
	}

	private void RefreshDanFangList(int id)
	{
		List<JSONObject> list = Tools.instance.getPlayer().DanFang.list;
		playerDanFangList.Clear();
		foreach (JSONObject item in list)
		{
			if (item["ID"].I == id)
			{
				DanFangData danFangData = new DanFangData();
				danFangData.ItemID = item["ID"].I;
				danFangData.YaoYinID = item["Type"][0].I;
				danFangData.ZhuYao1ID = item["Type"][1].I;
				danFangData.ZhuYao2ID = item["Type"][2].I;
				danFangData.FuYao1ID = item["Type"][3].I;
				danFangData.FuYao2ID = item["Type"][4].I;
				danFangData.YaoYinCount = item["Num"][0].I;
				danFangData.ZhuYao1Count = item["Num"][1].I;
				danFangData.ZhuYao2Count = item["Num"][2].I;
				danFangData.FuYao1Count = item["Num"][3].I;
				danFangData.FuYao2Count = item["Num"][4].I;
				danFangData.CalcYaoCaiTypeCount();
				playerDanFangList.Add(danFangData);
			}
		}
	}

	public void SetDanFangNumber(int number)
	{
		_NumberText.text = $"丹方{number}：";
		danFangNumber = number;
	}

	public void NextDanFang()
	{
		if (Tools.instance.getPlayer() != null && playerDanFangList.Count > 0)
		{
			danFangNumber++;
			if (danFangNumber > playerDanFangList.Count)
			{
				danFangNumber = 1;
			}
			SetDanFangNumber(danFangNumber);
			RefreshPanelData();
		}
	}

	public void LastDanFang()
	{
		if (Tools.instance.getPlayer() != null && playerDanFangList.Count > 0)
		{
			danFangNumber--;
			if (danFangNumber < 1)
			{
				danFangNumber = playerDanFangList.Count;
			}
			SetDanFangNumber(danFangNumber);
			RefreshPanelData();
		}
	}

	public void SetItemIcon(int id)
	{
		_ItemIconImage.sprite = TuJianDB.GetItemIconSprite(id);
		_QualityImage.sprite = TuJianDB.GetItemQualitySprite(id);
		_QualityUpImage.sprite = TuJianDB.GetItemQualityUpSprite(id);
	}
}
