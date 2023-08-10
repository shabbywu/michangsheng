using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShengWangManager : MonoBehaviour
{
	public static UIShengWangManager Inst;

	public Text NingZhouShengWangtext;

	public List<UIShengWangItem> NingZhouShengWangList = new List<UIShengWangItem>();

	public Image NingZhouSliderLeft;

	public Image NingZhouSliderRight;

	public Text SeaShengWangtext;

	public List<UIShengWangItem> SeaShengWangList = new List<UIShengWangItem>();

	public Image SeaSliderLeft;

	public Image SeaSliderRight;

	public Text PanelShiLiText;

	public Text PanelShengWangText;

	public Text PanelShenFenText;

	public Text NingZhouShangJinText;

	public Text NingZhouXuanShangDescText;

	public Text SeaShangJinText;

	public Text SeaXuanShangDescText;

	public RectTransform ContentRT;

	public GameObject TeQuanItemPrefab;

	private static bool firstEnable;

	public int NowShowShiLiID;

	public string NowShowShiLiName;

	private void Awake()
	{
		Inst = this;
	}

	private void OnEnable()
	{
		RefreshUI();
	}

	public void RefreshUI()
	{
		int ningZhouShengWang = PlayerEx.GetNingZhouShengWang();
		NingZhouShengWangtext.text = ningZhouShengWang.ToString();
		int ningZhouShengWangLevel = PlayerEx.GetNingZhouShengWangLevel();
		float ningZhouShengWangProcess = PlayerEx.GetNingZhouShengWangProcess();
		if (ningZhouShengWangLevel >= 4)
		{
			NingZhouShengWangList[0].Set(active: false);
			NingZhouShengWangList[1].Set(active: false);
			NingZhouShengWangList[2].Set(active: false);
			for (int i = 3; i <= 6; i++)
			{
				NingZhouShengWangList[i].Set(i <= ningZhouShengWangLevel - 1);
			}
			NingZhouSliderLeft.fillAmount = 0f;
			NingZhouSliderRight.fillAmount = ningZhouShengWangProcess;
		}
		else
		{
			NingZhouShengWangList[3].Set(active: false);
			NingZhouShengWangList[4].Set(active: false);
			NingZhouShengWangList[5].Set(active: false);
			NingZhouShengWangList[6].Set(active: false);
			for (int j = 0; j <= 2; j++)
			{
				NingZhouShengWangList[j].Set(j >= ningZhouShengWangLevel - 1);
			}
			NingZhouSliderRight.fillAmount = 0f;
			NingZhouSliderLeft.fillAmount = ningZhouShengWangProcess;
		}
		int seaShengWang = PlayerEx.GetSeaShengWang();
		SeaShengWangtext.text = seaShengWang.ToString();
		int seaShengWangLevel = PlayerEx.GetSeaShengWangLevel();
		float seaShengWangProcess = PlayerEx.GetSeaShengWangProcess();
		if (seaShengWangLevel >= 4)
		{
			SeaShengWangList[0].Set(active: false);
			SeaShengWangList[1].Set(active: false);
			SeaShengWangList[2].Set(active: false);
			for (int k = 3; k <= 6; k++)
			{
				SeaShengWangList[k].Set(k <= seaShengWangLevel - 1);
			}
			SeaSliderLeft.fillAmount = 0f;
			SeaSliderRight.fillAmount = seaShengWangProcess;
		}
		else
		{
			SeaShengWangList[3].Set(active: false);
			SeaShengWangList[4].Set(active: false);
			SeaShengWangList[5].Set(active: false);
			SeaShengWangList[6].Set(active: false);
			for (int l = 0; l <= 2; l++)
			{
				SeaShengWangList[l].Set(l >= seaShengWangLevel - 1);
			}
			SeaSliderRight.fillAmount = 0f;
			SeaSliderLeft.fillAmount = seaShengWangProcess;
		}
		SetNingZhouXuanShang();
		SetSeaXuanShang();
		SetShiLiInfo(NowShowShiLiID, NowShowShiLiName);
	}

	public void SetNingZhouXuanShang()
	{
		int ningZhouShengWang = PlayerEx.GetNingZhouShengWang();
		int shangJinPingFen = PlayerEx.GetShangJinPingFen(0);
		PlayerEx.CalcXuanShang(ningZhouShengWang, shangJinPingFen, out var shangjin, out var desc);
		desc = desc.Replace("{quyu}", "宁州");
		NingZhouShangJinText.text = shangjin.ToString();
		NingZhouXuanShangDescText.text = desc;
	}

	public void SetSeaXuanShang()
	{
		int seaShengWang = PlayerEx.GetSeaShengWang();
		int shangJinPingFen = PlayerEx.GetShangJinPingFen(19);
		PlayerEx.CalcXuanShang(seaShengWang, shangJinPingFen, out var shangjin, out var desc);
		desc = desc.Replace("{quyu}", "无尽之海");
		SeaShangJinText.text = shangjin.ToString();
		SeaXuanShangDescText.text = desc;
	}

	public void SetShiLiInfo(int id, string ShiLiName)
	{
		int shengwang = ((id != 1) ? PlayerEx.GetShengWang(id) : PlayerEx.GetMenPaiShengWang());
		int num = PlayerEx.CalcShengWangLevel(shengwang);
		int chenghaoLevel = 0;
		PanelShenFenText.text = "无";
		if (id == 1)
		{
			if (PlayerEx.Player.menPai == 0)
			{
				chenghaoLevel = 0;
			}
			else
			{
				chenghaoLevel = PlayerEx.GetShiLiChengHaoLevel(1);
			}
			PanelShenFenText.text = PlayerEx.GetMenPaiChengHao();
		}
		else
		{
			chenghaoLevel = PlayerEx.GetShiLiChengHaoLevel(id);
			JSONObject jSONObject = jsonData.instance.ShiLiShenFenData.list.Find((JSONObject d) => d["ShiLi"].I == id && d["ShenFen"].I == chenghaoLevel);
			if (jSONObject != null)
			{
				PanelShenFenText.text = jSONObject["Name"].Str;
			}
		}
		PanelShiLiText.text = ShiLiName;
		PanelShengWangText.text = shengwang.ToString();
		UITeQuanItem[] componentsInChildren = ((Component)ContentRT).GetComponentsInChildren<UITeQuanItem>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Object.Destroy((Object)(object)((Component)componentsInChildren[i]).gameObject);
		}
		foreach (JSONObject item in jsonData.instance.DiYuShengWangData.list)
		{
			if (item["ShiLi"].I == id)
			{
				UITeQuanItem component = Object.Instantiate<GameObject>(TeQuanItemPrefab, (Transform)(object)ContentRT).GetComponent<UITeQuanItem>();
				if (num >= item["ShengWangLV"].I && chenghaoLevel >= item["ShenFen"].I)
				{
					component.SetText(item["TeQuan"].Str);
				}
				else
				{
					component.SetLockText(item["TeQuan"].Str);
				}
			}
		}
	}
}
