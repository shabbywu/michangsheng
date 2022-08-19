using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000382 RID: 898
public class UIShengWangManager : MonoBehaviour
{
	// Token: 0x06001DB6 RID: 7606 RVA: 0x000D18ED File Offset: 0x000CFAED
	private void Awake()
	{
		UIShengWangManager.Inst = this;
	}

	// Token: 0x06001DB7 RID: 7607 RVA: 0x000D18F5 File Offset: 0x000CFAF5
	private void OnEnable()
	{
		this.RefreshUI();
	}

	// Token: 0x06001DB8 RID: 7608 RVA: 0x000D1900 File Offset: 0x000CFB00
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

	// Token: 0x06001DB9 RID: 7609 RVA: 0x000D1BB0 File Offset: 0x000CFDB0
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

	// Token: 0x06001DBA RID: 7610 RVA: 0x000D1C04 File Offset: 0x000CFE04
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

	// Token: 0x06001DBB RID: 7611 RVA: 0x000D1C58 File Offset: 0x000CFE58
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

	// Token: 0x04001845 RID: 6213
	public static UIShengWangManager Inst;

	// Token: 0x04001846 RID: 6214
	public Text NingZhouShengWangtext;

	// Token: 0x04001847 RID: 6215
	public List<UIShengWangItem> NingZhouShengWangList = new List<UIShengWangItem>();

	// Token: 0x04001848 RID: 6216
	public Image NingZhouSliderLeft;

	// Token: 0x04001849 RID: 6217
	public Image NingZhouSliderRight;

	// Token: 0x0400184A RID: 6218
	public Text SeaShengWangtext;

	// Token: 0x0400184B RID: 6219
	public List<UIShengWangItem> SeaShengWangList = new List<UIShengWangItem>();

	// Token: 0x0400184C RID: 6220
	public Image SeaSliderLeft;

	// Token: 0x0400184D RID: 6221
	public Image SeaSliderRight;

	// Token: 0x0400184E RID: 6222
	public Text PanelShiLiText;

	// Token: 0x0400184F RID: 6223
	public Text PanelShengWangText;

	// Token: 0x04001850 RID: 6224
	public Text PanelShenFenText;

	// Token: 0x04001851 RID: 6225
	public Text NingZhouShangJinText;

	// Token: 0x04001852 RID: 6226
	public Text NingZhouXuanShangDescText;

	// Token: 0x04001853 RID: 6227
	public Text SeaShangJinText;

	// Token: 0x04001854 RID: 6228
	public Text SeaXuanShangDescText;

	// Token: 0x04001855 RID: 6229
	public RectTransform ContentRT;

	// Token: 0x04001856 RID: 6230
	public GameObject TeQuanItemPrefab;

	// Token: 0x04001857 RID: 6231
	private static bool firstEnable;

	// Token: 0x04001858 RID: 6232
	public int NowShowShiLiID;

	// Token: 0x04001859 RID: 6233
	public string NowShowShiLiName;
}
