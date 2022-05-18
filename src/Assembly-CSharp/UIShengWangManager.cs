using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000504 RID: 1284
public class UIShengWangManager : MonoBehaviour
{
	// Token: 0x0600212D RID: 8493 RVA: 0x0001B55E File Offset: 0x0001975E
	private void Awake()
	{
		UIShengWangManager.Inst = this;
	}

	// Token: 0x0600212E RID: 8494 RVA: 0x0001B566 File Offset: 0x00019766
	private void OnEnable()
	{
		this.RefreshUI();
	}

	// Token: 0x0600212F RID: 8495 RVA: 0x001157FC File Offset: 0x001139FC
	public void RefreshUI()
	{
		int ningZhouShengWang = PlayerEx.GetNingZhouShengWang();
		this.NingZhouShengWangtext.text = ningZhouShengWang.ToString();
		int ningZhouShengWangLevel = PlayerEx.GetNingZhouShengWangLevel();
		float ningZhouShengWangProcess = PlayerEx.GetNingZhouShengWangProcess();
		if (ningZhouShengWangLevel >= 4)
		{
			this.NingZhouShengWangList[0].Set(false);
			this.NingZhouShengWangList[1].Set(false);
			this.NingZhouShengWangList[2].Set(false);
			for (int i = 3; i <= 6; i++)
			{
				this.NingZhouShengWangList[i].Set(i <= ningZhouShengWangLevel - 1);
			}
			this.NingZhouSliderLeft.fillAmount = 0f;
			this.NingZhouSliderRight.fillAmount = ningZhouShengWangProcess;
		}
		else
		{
			this.NingZhouShengWangList[3].Set(false);
			this.NingZhouShengWangList[4].Set(false);
			this.NingZhouShengWangList[5].Set(false);
			this.NingZhouShengWangList[6].Set(false);
			for (int j = 0; j <= 2; j++)
			{
				this.NingZhouShengWangList[j].Set(j >= ningZhouShengWangLevel - 1);
			}
			this.NingZhouSliderRight.fillAmount = 0f;
			this.NingZhouSliderLeft.fillAmount = ningZhouShengWangProcess;
		}
		int seaShengWang = PlayerEx.GetSeaShengWang();
		this.SeaShengWangtext.text = seaShengWang.ToString();
		int seaShengWangLevel = PlayerEx.GetSeaShengWangLevel();
		float seaShengWangProcess = PlayerEx.GetSeaShengWangProcess();
		if (seaShengWangLevel >= 4)
		{
			this.SeaShengWangList[0].Set(false);
			this.SeaShengWangList[1].Set(false);
			this.SeaShengWangList[2].Set(false);
			for (int k = 3; k <= 6; k++)
			{
				this.SeaShengWangList[k].Set(k <= seaShengWangLevel - 1);
			}
			this.SeaSliderLeft.fillAmount = 0f;
			this.SeaSliderRight.fillAmount = seaShengWangProcess;
		}
		else
		{
			this.SeaShengWangList[3].Set(false);
			this.SeaShengWangList[4].Set(false);
			this.SeaShengWangList[5].Set(false);
			this.SeaShengWangList[6].Set(false);
			for (int l = 0; l <= 2; l++)
			{
				this.SeaShengWangList[l].Set(l >= seaShengWangLevel - 1);
			}
			this.SeaSliderRight.fillAmount = 0f;
			this.SeaSliderLeft.fillAmount = seaShengWangProcess;
		}
		this.SetNingZhouXuanShang();
		this.SetSeaXuanShang();
		this.SetShiLiInfo(this.NowShowShiLiID, this.NowShowShiLiName);
	}

	// Token: 0x06002130 RID: 8496 RVA: 0x00115AAC File Offset: 0x00113CAC
	public void SetNingZhouXuanShang()
	{
		int ningZhouShengWang = PlayerEx.GetNingZhouShengWang();
		int shangJinPingFen = PlayerEx.GetShangJinPingFen(0);
		int num;
		string text;
		PlayerEx.CalcXuanShang(ningZhouShengWang, shangJinPingFen, out num, out text);
		text = text.Replace("{quyu}", "宁州");
		this.NingZhouShangJinText.text = num.ToString();
		this.NingZhouXuanShangDescText.text = text;
	}

	// Token: 0x06002131 RID: 8497 RVA: 0x00115B00 File Offset: 0x00113D00
	public void SetSeaXuanShang()
	{
		int seaShengWang = PlayerEx.GetSeaShengWang();
		int shangJinPingFen = PlayerEx.GetShangJinPingFen(19);
		int num;
		string text;
		PlayerEx.CalcXuanShang(seaShengWang, shangJinPingFen, out num, out text);
		text = text.Replace("{quyu}", "无尽之海");
		this.SeaShangJinText.text = num.ToString();
		this.SeaXuanShangDescText.text = text;
	}

	// Token: 0x06002132 RID: 8498 RVA: 0x00115B54 File Offset: 0x00113D54
	public void SetShiLiInfo(int id, string ShiLiName)
	{
		int shengwang;
		if (id == 1)
		{
			shengwang = PlayerEx.GetMenPaiShengWang();
		}
		else
		{
			shengwang = PlayerEx.GetShengWang(id);
		}
		int num = PlayerEx.CalcShengWangLevel(shengwang);
		int chenghaoLevel = 0;
		this.PanelShenFenText.text = "无";
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
			this.PanelShenFenText.text = PlayerEx.GetMenPaiChengHao();
		}
		else
		{
			chenghaoLevel = PlayerEx.GetShiLiChengHaoLevel(id);
			JSONObject jsonobject = jsonData.instance.ShiLiShenFenData.list.Find((JSONObject d) => d["ShiLi"].I == id && d["ShenFen"].I == chenghaoLevel);
			if (jsonobject != null)
			{
				this.PanelShenFenText.text = jsonobject["Name"].Str;
			}
		}
		this.PanelShiLiText.text = ShiLiName;
		this.PanelShengWangText.text = shengwang.ToString();
		UITeQuanItem[] componentsInChildren = this.ContentRT.GetComponentsInChildren<UITeQuanItem>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Object.Destroy(componentsInChildren[i].gameObject);
		}
		foreach (JSONObject jsonobject2 in jsonData.instance.DiYuShengWangData.list)
		{
			if (jsonobject2["ShiLi"].I == id)
			{
				UITeQuanItem component = Object.Instantiate<GameObject>(this.TeQuanItemPrefab, this.ContentRT).GetComponent<UITeQuanItem>();
				if (num >= jsonobject2["ShengWangLV"].I && chenghaoLevel >= jsonobject2["ShenFen"].I)
				{
					component.SetText(jsonobject2["TeQuan"].Str);
				}
				else
				{
					component.SetLockText(jsonobject2["TeQuan"].Str);
				}
			}
		}
	}

	// Token: 0x04001C9E RID: 7326
	public static UIShengWangManager Inst;

	// Token: 0x04001C9F RID: 7327
	public Text NingZhouShengWangtext;

	// Token: 0x04001CA0 RID: 7328
	public List<UIShengWangItem> NingZhouShengWangList = new List<UIShengWangItem>();

	// Token: 0x04001CA1 RID: 7329
	public Image NingZhouSliderLeft;

	// Token: 0x04001CA2 RID: 7330
	public Image NingZhouSliderRight;

	// Token: 0x04001CA3 RID: 7331
	public Text SeaShengWangtext;

	// Token: 0x04001CA4 RID: 7332
	public List<UIShengWangItem> SeaShengWangList = new List<UIShengWangItem>();

	// Token: 0x04001CA5 RID: 7333
	public Image SeaSliderLeft;

	// Token: 0x04001CA6 RID: 7334
	public Image SeaSliderRight;

	// Token: 0x04001CA7 RID: 7335
	public Text PanelShiLiText;

	// Token: 0x04001CA8 RID: 7336
	public Text PanelShengWangText;

	// Token: 0x04001CA9 RID: 7337
	public Text PanelShenFenText;

	// Token: 0x04001CAA RID: 7338
	public Text NingZhouShangJinText;

	// Token: 0x04001CAB RID: 7339
	public Text NingZhouXuanShangDescText;

	// Token: 0x04001CAC RID: 7340
	public Text SeaShangJinText;

	// Token: 0x04001CAD RID: 7341
	public Text SeaXuanShangDescText;

	// Token: 0x04001CAE RID: 7342
	public RectTransform ContentRT;

	// Token: 0x04001CAF RID: 7343
	public GameObject TeQuanItemPrefab;

	// Token: 0x04001CB0 RID: 7344
	private static bool firstEnable;

	// Token: 0x04001CB1 RID: 7345
	public int NowShowShiLiID;

	// Token: 0x04001CB2 RID: 7346
	public string NowShowShiLiName;
}
